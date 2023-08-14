using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DID.Controllers
{
    [Route("/api/core/cvend")]
    public class VendorController : Controller
    {
        const string VALIDATION_ERROR = "The request failed due to a validation error";

		[HttpGet("maxkode")]
		public async Task<DID.Entities.Vendor> GetMaxKode()
		{
			return await DID.DataLayers.Vendor.GetMaxKode();
		}
		[HttpGet]
        public async Task<List<DID.Entities.Vendor>> GetListToEdit()
        {
            return await DID.DataLayers.Vendor.GetDbList();
        }
		[HttpPost]
		public async Task<IActionResult> Post(string values)
		{
			Console.WriteLine(values);
			try
			{
				var vData = new DID.Entities.Vendor();
				PopulateModel(vData, JsonSerializer.Deserialize<System.Collections.IDictionary>(values));
				if (!TryValidateModel(vData)) return BadRequest(VALIDATION_ERROR);
				var vRetu = await DID.DataLayers.Vendor.InsertObj(vData);
				if (vRetu) return Ok();
				else return StatusCode(500, "Data gagal ditambahkan.");
			}
			catch
			{
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put(int key, string values)
		{

			Console.WriteLine(values);
			try
			{
				DID.Entities.Vendor vData = await DID.DataLayers.Vendor.GetMemoryItem((short)key);
				Console.WriteLine(vData);
				PopulateModel(vData, JsonSerializer.Deserialize<System.Collections.IDictionary>(values));
				if (!TryValidateModel(vData)) return BadRequest(VALIDATION_ERROR);
				var vRetu = await DID.DataLayers.Vendor.UpdateObject(vData);
				if (vRetu) return Ok();
				else return StatusCode(500, "Data gagal diupdate.");
			}
			catch
			{
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int key)
		{
			try
			{
				DID.Entities.Vendor vData = await DID.DataLayers.Vendor.GetMemoryItem((short)key);
				if (!TryValidateModel(vData)) return BadRequest(VALIDATION_ERROR);
				var vRetu = await DID.DataLayers.Vendor.DeleteObject(vData);
				if (vRetu) return Ok();
				else return BadRequest();
			}
			catch
			{
				return StatusCode(500, "Internal server error");
			}
		}
		private void PopulateModel(DID.Entities.Vendor iData, System.Collections.IDictionary dObject)
		{
			if (dObject.Contains("kodeVendor")) { iData.kodeVendor = DID.Convertion.ToInteger(System.Convert.ToString(dObject["kodeVendor"])); }
			if (dObject.Contains("namaVendor")) { iData.namaVendor = System.Convert.ToString(dObject["namaVendor"]); }
			if (dObject.Contains("alamatVendor")) { iData.alamatVendor = System.Convert.ToString(dObject["alamatVendor"]); }

			if (dObject.Contains("nomorTelepon")) { iData.nomorTelepon= DID.Convertion.ToLong(System.Convert.ToString(dObject["nomorTelepon"])); }
            if (dObject.Contains("namaPIC")) { iData.namaPIC = System.Convert.ToString(dObject["namaPIC"]); }
            if (dObject.Contains("nomorHP")) { iData.nomorHP = DID.Convertion.ToLong(System.Convert.ToString(dObject["nomorHP"])); }
            if (dObject.Contains("email")) { iData.email = System.Convert.ToString(dObject["email"]); }
            if (dObject.Contains("namaBank")) { iData.namaBank = System.Convert.ToString(dObject["namaBank"]); }
			if (dObject.Contains("alamatBank")) { iData.alamatBank = System.Convert.ToString(dObject["alamatBank"]); }
			if (dObject.Contains("nomorRekening")) { iData.nomorRekening = DID.Convertion.ToLong(System.Convert.ToString(dObject["nomorRekening"])); }
            if (dObject.Contains("namaRekening")) { iData.namaRekening = System.Convert.ToString(dObject["namaRekening"]); }
			if (dObject.Contains("npwp")) { iData.npwp = DID.Convertion.ToLong(System.Convert.ToString(dObject["npwp"])); }

		}
	}

}
