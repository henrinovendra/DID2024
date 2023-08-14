using System;
using System.Threading.Tasks;
using System.Text.Json;

namespace DID.Entities
{
    public class Vendor
    {
        public int kodeVendor { get; set; } = 0;
        public string namaVendor { get; set; } = string.Empty;
        public string alamatVendor { get; set; } = string.Empty;
        public long nomorTelepon { get; set; } = 0;
        public string namaPIC { get; set; } = string.Empty;
        public long nomorHP { get; set; } = 0;
        public string email { get; set; } = string.Empty;
        public string namaBank { get; set; } = string.Empty;
        public string alamatBank { get; set; } = string.Empty;
        public long nomorRekening { get; set; } = 0;
        public string namaRekening { get; set; } = string.Empty;
        public long npwp { get; set; } = 0;
        public void Zero()
        {
            kodeVendor = 0;
            namaVendor = "-";
            alamatVendor = "-";
            nomorTelepon = 0;
            namaPIC = "-";
            nomorHP = 0;
            email = "-";
            namaBank = "-";
            alamatBank = "-";
            nomorRekening = 0;
            namaRekening = "-";
            npwp = 0;
        }
        
        public Vendor SallowCopy()
        {
            return (Vendor)this.MemberwiseClone();

        }
        public void FromJsonString(string iJsonString)
        {
            if (string.IsNullOrWhiteSpace(iJsonString)) return;
            if (iJsonString.Substring(0, 1) == "[") iJsonString.Substring(1, iJsonString.Length - 1);
            if (iJsonString.Substring(iJsonString.Length - 1, 1) == "]") iJsonString = iJsonString.Substring(0, iJsonString.Length - 1);
            Vendor d = (Vendor)JsonSerializer.Deserialize<Vendor>(iJsonString);
            this.kodeVendor = d.kodeVendor;
            this.namaVendor = d.namaVendor;
            this.alamatVendor = d.alamatVendor;
            this.nomorTelepon = d.nomorTelepon;
            this.namaPIC = d.namaPIC;
            this.nomorHP = d.nomorHP;
            this.email = d.email;
            this.namaBank = d.namaBank;
            this.alamatBank = d.alamatBank;
            this.namaRekening = d.namaRekening;
            this.nomorRekening = d.nomorRekening;
            this.npwp = d.npwp;
            d = null;
        }
        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}