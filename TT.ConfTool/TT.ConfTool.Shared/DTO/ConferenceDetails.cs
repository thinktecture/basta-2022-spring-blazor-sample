using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TT.ConfTool.Shared.DTO
{
    [DataContract]
    public class ConferenceDetails
    {
        [DataMember(Order = 1)]
        public Guid ID { get; set; }

        [DataMember(Order = 2)]
        [Required]
        public string Title { get; set; }

        [DataMember(Order = 3)]
        [Required]
        public DateTime? DateFrom { get; set; }

        [DataMember(Order = 4)]
        [Required]
        public DateTime? DateTo { get; set; }

        [DataMember(Order = 5)]
        public string Country { get; set; }

        [DataMember(Order = 6)]
        public string City { get; set; }

        [DataMember(Order = 7)]
        public string Url { get; set; } = String.Empty;
    }
}
