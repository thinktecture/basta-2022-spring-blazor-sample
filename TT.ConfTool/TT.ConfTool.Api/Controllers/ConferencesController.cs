using AutoMapper;
using ConfTool.Server.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TT.ConfTool.Api.Models;
using TT.ConfTool.Shared.DTO;

namespace TT.ConfTool.Api.Controllers
{
    [Authorize("api")]
    [ApiController]
    [Route("api/[controller]")]
    public class ConferencesController : ControllerBase
    {
        private readonly ConferencesDbContext _conferencesDbContext;
        private readonly IMapper _mapper;
        private readonly IHubContext<ConferencesHub> _hubContext;
        public ConferencesController(ConferencesDbContext conferencesDbContext
            , IMapper mapper
            , IHubContext<ConferencesHub> hubContext)
        {
            _conferencesDbContext = conferencesDbContext ?? throw new ArgumentNullException(nameof(conferencesDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        [HttpGet]
        public async Task<IEnumerable<ConferenceOverview>> GetAsync()
        {
            var conferences = await _conferencesDbContext.Conferences.OrderByDescending(c => c.DateCreated).ToListAsync();

            return _mapper.Map<IEnumerable<ConferenceOverview>>(conferences);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ConferenceDetails>> GetAsync(string id)
        {
            var conferenceDetails = await _conferencesDbContext.Conferences.FindAsync(Guid.Parse(id));

            if (conferenceDetails == null)
            {
                return NotFound();
            }

            return _mapper.Map<ConferenceDetails>(conferenceDetails);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ConferenceDetails>> PutConferenceAsync(ConferenceDetails conference)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var conf = _mapper.Map<Models.Conference>(conference);
            conf.DateCreated = DateTime.UtcNow;

            var currentConf = _conferencesDbContext.Conferences.FirstOrDefaultAsync(conf => conf.ID == conference.ID);
            if (currentConf == null)
            {
                return NotFound();
            }
            _conferencesDbContext.Conferences.Update(conf);
            await _conferencesDbContext.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("NewConferenceAdded");

            return CreatedAtAction("Get", new { id = conference.ID }, _mapper.Map<ConferenceDetails>(conf));
        }

        [HttpPost]
        public async Task<ActionResult<ConferenceDetails>> PostConferenceAsync(ConferenceDetails conference)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var conf = _mapper.Map<Models.Conference>(conference);
            conf.DateCreated = DateTime.UtcNow;

            _conferencesDbContext.Conferences.Add(conf);
            await _conferencesDbContext.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("NewConferenceAdded");

            return CreatedAtAction("Get", new { id = conference.ID }, _mapper.Map<ConferenceDetails>(conf));
        }
    }
}
