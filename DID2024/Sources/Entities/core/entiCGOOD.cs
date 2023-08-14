using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DID.Entities
{ 
    public class Barang
    {
        public string jenisBarang { get; set; } = string.Empty;
        public string typeBarang { get; set; } = string.Empty;
        public string keterangan { get; set; } = string.Empty;
        public string xnamaBarang { get; set; } = string.Empty;
        public string satuan { get; set; } = string.Empty;
        public string company { get; set; } = string.Empty;
        public int quantity { get; set; } = 0;
        public DateTime lastRevisi { get; set; } = new System.DateTime(2000, 1, 1, 0, 0, 0);
        

       
        public string KeyID { get { this.SetKeyID(); return this.mLnKeyID; } set { this.mLnKeyID = value; this.mBoKeyID = false; }}
        private string mLnKeyID = "0";
        private bool mBoKeyID = false;

        public void SetKeyID()
        {
            if (!this.mBoKeyID)
            {
                this.mLnKeyID = Convert.ToString(string.Format("{0:d2}{1:d4}", jenisBarang, typeBarang));
                this.mBoKeyID = true;
            }
        }


        public void Zero()
        {
            jenisBarang = "-";
            typeBarang = "-";
            keterangan = "-";
            xnamaBarang = "-";
            company = "-";
            satuan = "-";
            quantity = 0;
      
            lastRevisi = new System.DateTime(2000, 1, 1, 0, 0, 0);
        }

        public Barang ShallowCopy()
        {
            return (Barang)this.MemberwiseClone();
        }
        public void FromJSONString(string iJsonString)
        {
            if (string.IsNullOrWhiteSpace(iJsonString)) return;
            if (iJsonString.Substring(0, 1) == "[") iJsonString.Substring(1, iJsonString.Length - 1);
            if (iJsonString.Substring(iJsonString.Length - 1, 1) == "]") iJsonString = iJsonString.Substring(0, iJsonString.Length - 1);
            Barang d = (Barang)JsonSerializer.Deserialize<Barang>(iJsonString);
            this.jenisBarang = d.jenisBarang;
            this.typeBarang = d.typeBarang;
            this.keterangan = d.keterangan;
            this.xnamaBarang = d.xnamaBarang;
            this.company = d.company;
            this.satuan = d.satuan;
            this.quantity = d.quantity;
            this.lastRevisi = d.lastRevisi;
            d = null;
        }
        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
