
using Newtonsoft.Json;

namespace Main.Model
{
    public class RegisterModel
    {
        [JsonProperty("name")]
        public required string Name { get; set; }
        [JsonProperty("phone")]
        public required string Phone { get; set; }
        [JsonProperty("email")]
        public required string Email { get; set; }
        [JsonProperty("password")]
        public required string Password { get; set; }
    }
}
