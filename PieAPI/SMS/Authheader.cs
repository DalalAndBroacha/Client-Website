using System.Runtime.Serialization;

namespace PieAPI.SMS
{
    public class Authheader
    {
        public string app_code { get; set; }
        public string user { get; set; }

        public string password { get; set; }
    }
}
