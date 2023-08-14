using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace DID.Controllers
{
    [Route("/api/transaksi/pembelian")]
    public class TransaksiPembelianController: Controller
    {
        const string VALIDATION_ERROR = "The request failed due to a validation error";

        [HttpGet("maxkode")]
        public async Task<DID.Entities.TransaksiPembelianInternal> GetMaxKode(string iCompany)
        {
            Console.WriteLine(iCompany);
            return await DID.DataLayers.TransaksiPembelianInternal.GetMaxKode(iCompany);
        }

       
        [HttpGet]
        public async Task<List<DID.Entities.TransaksiPembelianInternal>> GetListToEdit()
        {
            var username = User.Identity.Name;
            var company = User.FindFirstValue(CustomClaim.CustomClaimTypes.company);
            return await DID.DataLayers.TransaksiPembelianInternal.GetDbListByCompany(company);
        }
        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            try
            {
                var vData = new DID.Entities.TransaksiPembelianInternal();
                PopulateModel(vData, JsonSerializer.Deserialize<System.Collections.IDictionary>(values));
                if (!TryValidateModel(vData)) return BadRequest(VALIDATION_ERROR);
                var vRetur = await DID.DataLayers.TransaksiPembelianInternal.InsertObject(vData);
                if (vRetur) return Ok();
                else return StatusCode(500, "Data gagal ditambahkan.");
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put(string key, string values)
        {
            try
            {
                DID.Entities.TransaksiPembelianInternal vData = await DID.DataLayers.TransaksiPembelianInternal.GetMemoryItem(key);
                PopulateModel(vData, JsonSerializer.Deserialize<System.Collections.IDictionary>(values));
                if (!TryValidateModel(vData)) return BadRequest(VALIDATION_ERROR);
                var vRetu = await DID.DataLayers.TransaksiPembelianInternal.UpdatetObject(vData);
                if (vRetu) return Ok();
                else return StatusCode(500, "Data gagal diupdate.");
            }
            catch
            {

                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string key)
        {
            try
            {
                DID.Entities.TransaksiPembelianInternal vData = await DID.DataLayers.TransaksiPembelianInternal.GetMemoryItem(key);
                if (!TryValidateModel(vData)) return BadRequest(VALIDATION_ERROR);
                var vRetu = await DID.DataLayers.TransaksiPembelianInternal.DeleteObject(vData);
                if (vRetu) return Ok();
                else return BadRequest();
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
        private void PopulateModel(DID.Entities.TransaksiPembelianInternal iData, System.Collections.IDictionary dObject)
        {
            if (dObject.Contains("transaksiId")) { iData.transaksiId = System.Convert.ToString(dObject["transaksiId"]); }
            if (dObject.Contains("nomorSO")) { iData.nomorSO = DID.Convertion.ToInteger(System.Convert.ToString( dObject["nomorSO"])); }
            if (dObject.Contains("nomorPO")) { iData.nomorPO = DID.Convertion.ToInteger(System.Convert.ToString( dObject["nomorPO"])); }
            if (dObject.Contains("nomorPR")) { iData.nomorPR = DID.Convertion.ToInteger(System.Convert.ToString( dObject["nomorPR"])); }
            if (dObject.Contains("nomorCOA")) { iData.nomorCOA = DID.Convertion.ToInteger(System.Convert.ToString(dObject["nomorCOA"])); }
            if (dObject.Contains("namaBarang")) { iData.namaBarang = System.Convert.ToString(dObject["namaBarang"]); }
            if (dObject.Contains("satuan")) { iData.satuan = System.Convert.ToString(dObject["satuan"]); }
            if (dObject.Contains("company")) { iData.company = System.Convert.ToString(dObject["company"]); }
            if (dObject.Contains("quantity")) { iData.quantity = DID.Convertion.ToInteger(System.Convert.ToString( dObject["quantity"])); }
            if (dObject.Contains("hargaSatuan")) { iData.hargaSatuan = DID.Convertion.ToDecimal(System.Convert.ToString(dObject["hargaSatuan"])); }
            if (dObject.Contains("nominalMaterial")) { iData.nominalMaterial = DID.Convertion.ToDecimal(System.Convert.ToString(dObject["nominalMaterial"])); }
            if (dObject.Contains("nominalService")) { iData.nominalService = DID.Convertion.ToDecimal(System.Convert.ToString(dObject["nominalService"])); }
            if (dObject.Contains("biayaLain")) { iData.biayaLain = DID.Convertion.ToDecimal(System.Convert.ToString(dObject["biayaLain"])); }
            if (dObject.Contains("discount")) { iData.discount = DID.Convertion.ToDecimal(System.Convert.ToString(dObject["discount"])); }
            if (dObject.Contains("ppn")) { iData.ppn = DID.Convertion.ToDecimal(System.Convert.ToString(dObject["ppn"])); }
            if (dObject.Contains("totalHarga")) { iData.totalHarga = DID.Convertion.ToDecimal(System.Convert.ToString(dObject["totalHarga"])); }
            if (dObject.Contains("keterangan")) { iData.keterangan = System.Convert.ToString(dObject["keterangan"]); }
            if (dObject.Contains("namaXBarang")) { iData.namaXBarang = System.Convert.ToString(dObject["namaXBarang"]); }
            if(dObject.Contains("nomorXPO")) { iData.nomorXPO = DID.Convertion.ToInteger(System.Convert.ToString(dObject["nomorXPO"])); }
            if(dObject.Contains("idVendor")) { iData.idVendor = DID.Convertion.ToInteger(System.Convert.ToString(dObject["idVendor"])); }
            if (dObject.Contains("nomorSJ")) { iData.nomorSJ = System.Convert.ToString(dObject["nomorSJ"]); }
            if (dObject.Contains("nomorReference")) { iData.nomorReference = System.Convert.ToString(dObject["nomorReference"]); }
            if (dObject.Contains("nomorInvoice")) { iData.nomorInvoice = System.Convert.ToString(dObject["nomorInvoice"]); }
            if (dObject.Contains("fakturPajak")) { iData.fakturPajak = System.Convert.ToString(dObject["fakturPajak"]); }
            if (dObject.Contains("tanggalInvoice")) { iData.tanggalInvoice = DID.Convertion.yyyy_MM_dd_HH_mm_ss2Date(System.Convert.ToString(dObject["tanggalInvoice"])); }
            if (dObject.Contains("tanggalJatuhTempo")) { iData.tanggalJatuhTempo = DID.Convertion.yyyy_MM_dd_HH_mm_ss2Date(System.Convert.ToString(dObject["tanggalJatuhTempo"])); }

            if (dObject.Contains("tanggalPO")) { iData.tanggalPO = DID.Convertion.yyyy_MM_dd_HH_mm_ss2Date(System.Convert.ToString(dObject["tanggalPO"])); }
            if (dObject.Contains("tanggalPurchase")) { iData.tanggalPurchase = DID.Convertion.yyyy_MM_dd_HH_mm_ss2Date(System.Convert.ToString(dObject["tanggalPurchase"])); }
            if (dObject.Contains("tanggalDelivery")) { iData.tanggalDelivery = DID.Convertion.yyyy_MM_dd_HH_mm_ss2Date(System.Convert.ToString(dObject["tanggalDelivery"])); }
        }

    }
}
