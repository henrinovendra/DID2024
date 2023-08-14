using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DID.Entities
{
    public class KodeAplikasi
    {
        public string headCode { get; set; } = string.Empty;
        public string childCode { get; set; } = string.Empty;
        public string keterangan { get; set; } = string.Empty;

        public string KeyID { get { this.SetKeyID(); return this.mLnKeyID; } set { this.mLnKeyID = value; this.mBoKeyID = false; } }

        public void Zero()
        {
            headCode = "-";
            childCode = "-";
            keterangan = "-";
            this.mLnKeyID = "0";
            this.mBoKeyID = false;
        }
        private string mLnKeyID = "0";
        private bool mBoKeyID = false;
        public void SetKeyID()
        {
            if (!this.mBoKeyID)
            {
                this.mLnKeyID = Convert.ToString(string.Format("{0:d2}{1:d4}", headCode, childCode));
                this.mBoKeyID = true;
            }
        }

        public KodeAplikasi ShallowCopy()
        {
            return (KodeAplikasi)this.MemberwiseClone();
        }
        public void FromJSONString(string iJSONString)
        {
            if (string.IsNullOrWhiteSpace(iJSONString)) return;
            if (iJSONString.Substring(0, 1) == "[") iJSONString = iJSONString.Substring(1, iJSONString.Length - 1);
            if (iJSONString.Substring(iJSONString.Length - 1, 1) == "]") iJSONString = iJSONString.Substring(0, iJSONString.Length - 1);
            KodeAplikasi d = (KodeAplikasi)JsonSerializer.Deserialize<KodeAplikasi>(iJSONString);
            this.headCode = d.headCode;
            this.childCode = d.childCode;
            this.keterangan = d.keterangan;
            this.SetKeyID();
            d = null;
        }
        public string ToJSONString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
