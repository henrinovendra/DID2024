using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DID.Entities
{
    public class TransaksiPembelianInternal
    {
        public string transaksiId { get; set; } = string.Empty;
        public int nomorSO { get; set; } = 0;
        public int nomorPR { get; set; } = 0;
        public int nomorPO { get; set; } = 0;
        public int nomorCOA { get; set; } = 0;
        public string namaBarang { get; set; } = string.Empty;
        public string satuan { get; set; } = string.Empty;
        public string company { get; set; } = string.Empty;
        public int quantity { get; set; } = 0;
        public Decimal hargaSatuan { get; set; } = 0;
        public Decimal nominalMaterial { get; set; } = 0;
        public Decimal nominalService { get; set; } = 0;
        public Decimal biayaLain { get; set; } = 0;
        public Decimal discount { get; set; } = 0;
        public Decimal ppn { get; set; } = 0;
        public Decimal totalHarga { get; set; } = 0;
        public string keterangan { get; set; } = string.Empty;

        public string namaXBarang { get; set; } = string.Empty;
        public int nomorXPO { get; set; } = 0;
        public int idVendor { get; set; } = 0;
        public string nomorSJ { get; set; } = string.Empty;
        public string nomorReference { get; set; } = string.Empty;
        public string nomorInvoice { get; set; } = string.Empty;
        public string fakturPajak { get; set; } = string.Empty;


        public DateTime? tanggalInvoice { get; set; } = new System.DateTime(2000, 1, 1, 0, 0, 0);

        public DateTime? tanggalJatuhTempo { get; set; } = new System.DateTime(2000, 1, 1, 0, 0, 0);

        public DateTime? tanggalPO { get; set; } = new System.DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime? tanggalDelivery { get; set; } = new System.DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime? tanggalPurchase { get; set; } = new System.DateTime(2000, 1, 1, 0, 0, 0);


        public void Zero()
        {
            transaksiId = "-";
            nomorSO = 0;
            nomorPR = 0;
            nomorPO = 0;
            nomorCOA = 0;

            namaBarang = "-";
            satuan = "-";
            company = "-";

            quantity = 0;
            hargaSatuan = 0;
            nominalMaterial = 0;
            nominalService = 0;
            biayaLain = 0;
            discount = 0;
            ppn = 0;
            totalHarga = 0;
            keterangan = "-";
            tanggalPO = new System.DateTime(2000, 1, 1, 0, 0, 0);
            tanggalDelivery = new System.DateTime(2000, 1, 1, 0, 0, 0);
            tanggalPurchase = new System.DateTime(2000, 1, 1, 0, 0, 0);

            //exx
            idVendor = 0;
            namaXBarang = "-";
            nomorXPO = 0;
            nomorSJ = "-";
            nomorInvoice = "-";
            fakturPajak = "-";
            nomorReference = "-";
            tanggalInvoice = new System.DateTime(2000, 1, 1, 0, 0, 0);
            tanggalJatuhTempo = new System.DateTime(2000, 1, 1, 0, 0, 0);



           
        }
       
        public TransaksiPembelianInternal ShallowCopy()
        {
            return (TransaksiPembelianInternal)this.MemberwiseClone();
        }
        public void FromJSONString(string iJsonString)
        {
            if (string.IsNullOrWhiteSpace(iJsonString)) return;
            if (iJsonString.Substring(0, 1) == "[") iJsonString.Substring(1, iJsonString.Length - 1);
            if (iJsonString.Substring(iJsonString.Length - 1, 1) == "]") iJsonString = iJsonString.Substring(0, iJsonString.Length - 1);
            TransaksiPembelianInternal d = (TransaksiPembelianInternal)JsonSerializer.Deserialize<TransaksiPembelianInternal>(iJsonString);
            this.transaksiId = d.transaksiId;
            this.nomorSO = d.nomorSO;
            this.nomorPR = d.nomorPR;
            this.nomorPO = d.nomorPO;
            this.nomorCOA = d.nomorCOA;


            this.namaBarang = d.namaBarang;
            this.company = d.company;
            this.satuan = d.satuan;

            this.quantity = d.quantity;
            this.hargaSatuan = d.hargaSatuan;
            this.nominalMaterial = d.nominalMaterial;
            this.nominalService = d.nominalService;
            this.biayaLain = d.biayaLain;
            this.discount = d.discount;
            this.ppn = d.ppn;
            this.totalHarga = d.totalHarga;

            this.keterangan = d.keterangan;
            this.tanggalDelivery = d.tanggalDelivery;
            this.tanggalPO = d.tanggalPO;
            this.tanggalPurchase = d.tanggalPurchase;

            this.idVendor = d.idVendor;
            this.namaXBarang = d.namaXBarang;
            this.nomorXPO = d.nomorXPO;
            this.nomorSJ = d.nomorSJ;
            this.nomorInvoice = d.nomorInvoice;
            this.nomorReference = d.nomorReference;
            this.fakturPajak = d.fakturPajak;
            this.tanggalInvoice = d.tanggalInvoice;
            this.tanggalJatuhTempo = d.tanggalJatuhTempo;
            d = null;
        }
        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this);
        }


    }
}
