
using Newtonsoft.Json;

namespace Main.Model
{
    public class LoginModel
    {
        [JsonProperty("username")]
        public required string Username { get; set; }
        [JsonProperty("password")]
        public required string Password { get; set; }
    }
}
