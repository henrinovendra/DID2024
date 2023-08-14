using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace DID.Controllers
{
    [Route("/api/core/cgood")]
    [Authorize]
    public class BarangController : Controller
    {
        const string VALIDATION_ERROR = "The request failed due to a validation error";
        
        [HttpGet("barangbycompany")]
        public async Task<List<DID.Entities.Barang>> GetDbBarangByCompany(string iCompany)
        {
            return await DID.DataLayers.Barang.GetDbListByCompany(iCompany);
        }
        [HttpGet("maxkode")]
        public async Task<DID.Entities.Barang> GetMaxKode(string iCompany)
        {
            return await DID.DataLayers.Barang.GetMaxKode(iCompany);
        }
        [HttpGet("headcode")]
        public async Task<List<DID.Entities.Barang>> GetListHeadCode()
        {
            return await DID.DataLayers.Barang.GetJenisBarang();
        }
        [HttpPost("headcode")]
        public async Task<IActionResult> PostHeadCode (string values)
        {
            try
            {
                var vData = new DID.Entities.Barang();
                PopulateModal(vData, JsonSerializer.Deserialize<System.Collections.IDictionary>(values));
                if (!TryValidateModel(vData)) return BadRequest(VALIDATION_ERROR);
                var vRetu = await DID.DataLayers.Barang.InsertHeadObject(vData);
                if (vRetu) return Ok();
                else return StatusCode(500, "Data gagal ditambahkan.");
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet]
        public async Task<List<DID.Entities.Barang>> GetListToEdit()
        {
            var company = @User.FindFirstValue(CustomClaim.CustomClaimTypes.company);
            return await DID.DataLayers.Barang.GetDbListByCompany(company);
        }
        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            try
            {
                var vData = new DID.Entities.Barang();
                PopulateModel(vData, JsonSerializer.Deserialize<System.Collections.IDictionary>(values));
                if (!TryValidateModel(vData)) return BadRequest(VALIDATION_ERROR);
                var vRetu = await DID.DataLayers.Barang.InsertObject(vData);
                if (vRetu) return Ok();
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
                DID.Entities.Barang vData = await DID.DataLayers.Barang.GetMemoryItemByKey(key);
                PopulateModel(vData, JsonSerializer.Deserialize<System.Collections.IDictionary>(values));
                if (!TryValidateModel(vData)) return BadRequest(VALIDATION_ERROR);
                var vRetu = await DID.DataLayers.Barang.UpdateObject(vData);
                Console.WriteLine(vData);
                if (vRetu) return Ok();
                else return StatusCode(500, "Data gagal diupdate.");
            }
            catch
            {
                Console.WriteLine("aff");

                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string key)
        {
            try
            {
                DID.Entities.Barang vData = await DID.DataLayers.Barang.GetMemoryItemByKey(key);
                if (!TryValidateModel(vData)) return BadRequest(VALIDATION_ERROR);
                var vRetu = await DID.DataLayers.Barang.DeleteObject(vData);
                if (vRetu) return Ok();
                else return BadRequest();
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        private void PopulateModal(DID.Entities.Barang iData, System.Collections.IDictionary dObject)
        {
            if (dObject.Contains("jenisBarang")) { iData.jenisBarang = System.Convert.ToString(dObject["jenisBarang"]); iData.SetKeyID(); ; }
            if (dObject.Contains("keterangan")) { iData.keterangan = System.Convert.ToString(dObject["keterangan"]); }

        }
        private void PopulateModel(DID.Entities.Barang iData, System.Collections.IDictionary dObject)
        {
            if (dObject.Contains("jenisBarang")) { iData.jenisBarang = System.Convert.ToString(dObject["jenisBarang"]); iData.SetKeyID(); ; }
            if (dObject.Contains("typeBarang")) { iData.typeBarang = System.Convert.ToString(dObject["typeBarang"]); iData.SetKeyID(); ; }
            if (dObject.Contains("keterangan")) { iData.keterangan = System.Convert.ToString(dObject["keterangan"]); }
            if (dObject.Contains("xnamaBarang")) { iData.xnamaBarang = System.Convert.ToString(dObject["xnamaBarang"]); }
            if (dObject.Contains("satuan")) { iData.satuan = System.Convert.ToString(dObject["satuan"]); }
            if (dObject.Contains("company")) { iData.company = System.Convert.ToString(dObject["company"]); }
            if (dObject.Contains("quantity")) { iData.quantity = DID.Convertion.ToInteger(dObject["quantity"]); }

        }
    }
}
