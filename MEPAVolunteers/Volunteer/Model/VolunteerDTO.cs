using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Utility;

namespace Volunteer.Model
{
    public class VolunteerDTO
    {
        [JsonProperty("volunteerid")]
        public required string VolunteerID { get; set; }
        [JsonProperty("name")]
        public required string Name { get; set; }
        [JsonProperty("phone")]
        public required string Phone { get; set; }
        [JsonProperty("address")]
        public required string Address { get; set; }
        [JsonProperty("email")]
        public required string Email { get; set; }
        [JsonProperty("gender")]
        public required string Gender { get; set; }
        [JsonProperty("personID")]
        public required string PersonID { get; set; }
        [JsonProperty("status")]
        public required string Status { get; set; }
         
        [JsonProperty("birthdate")]
        public required DateTime BirthDate { get; set; }

        [JsonProperty("regency")]
        public required string Regency { get; set; }
        [JsonProperty("district")]
        public required string District { get; set; }
        [JsonProperty("ward")]
        public required string Ward { get; set; }
        [JsonProperty("village")]
        public required string Village { get; set; }
        [JsonProperty("rt")]
        public required string RT { get; set; }
        [JsonProperty("rw")]
        public required string RW { get; set; }

        [JsonProperty("volunteername")]
        public string VolunteerName { get; set; } = string.Empty;
        [JsonProperty("volunteerregency")]
        public string VolunteerRegency { get; set; } = string.Empty;
        [JsonProperty("volunteerdistrict")]
        public string VolunteerDistrict { get; set; } = string.Empty;

        [JsonProperty("role")]
        public Role Role { get; set; }

    }
}
