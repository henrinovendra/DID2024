using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DID.Entities
{
    public class PenggunaAplikasi
    {
        public int UserId { get; set; } = 0;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string company { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public List<string> Modules { get; set; } = new List<string>();

        public void Zero()
        {
            UserId = 0;
            UserName = "-";
            Password = "-";
            company = "-";
            Role = "-";
            Modules = new List<string>();
        }

        public PenggunaAplikasi ShallowCopy()
        {
            return (PenggunaAplikasi)this.MemberwiseClone();
        }
        public void FromJSONString (string iJsonString)
        {
          
            if (string.IsNullOrWhiteSpace(iJsonString)) return;
            if (iJsonString.Substring(0, 1) == "[") iJsonString.Substring(1, iJsonString.Length - 1);
            if (iJsonString.Substring(iJsonString.Length - 1, 1) == "]") iJsonString = iJsonString.Substring(0, iJsonString.Length - 1);
            PenggunaAplikasi d = (PenggunaAplikasi)JsonSerializer.Deserialize<PenggunaAplikasi>(iJsonString);
            this.UserId = d.UserId;
            this.UserName = d.UserName;
            this.Password = d.Password;
            this.company = d.company;
            this.Role = d.Role;
            d = null;
        }
        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}
