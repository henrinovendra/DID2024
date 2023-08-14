using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DID.Controllers
{

	[Route("/api/core/ccode")]
	public class KodeAplikasiController : Controller
	{
		const string VALIDATION_ERROR = "The request failed due to a validation error";

		[HttpGet("company")]
		public async Task<DID.Entities.KodeAplikasi> GetCompany(string iCompany)
		{
			return await DID.DataLayers.KodeAplikasi.GetcompanyItem(iCompany);
		}

		[HttpGet]
		public async Task<List<DID.Entities.KodeAplikasi>> GetListToEdit()
		{
			return await DID.DataLayers.KodeAplikasi.GetDbList();
		}
		[HttpPost]
		public async Task<IActionResult> Post(string values)
		{
			try
			{
				var vData = new DID.Entities.KodeAplikasi();
				PopulateModel(vData, JsonSerializer.Deserialize<System.Collections.IDictionary>(values));
				if (!TryValidateModel(vData)) return BadRequest(VALIDATION_ERROR);
				var vRetu = await DID.DataLayers.KodeAplikasi.InsertObject(vData);
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
				DID.Entities.KodeAplikasi vData = await DID.DataLayers.KodeAplikasi.GetMemoryItemByKey(key);
				PopulateModel(vData, JsonSerializer.Deserialize<System.Collections.IDictionary>(values));
				if (!TryValidateModel(vData)) return BadRequest(VALIDATION_ERROR);
				var vRetu = await DID.DataLayers.KodeAplikasi.UpdateObject(vData);
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
				DID.Entities.KodeAplikasi vData = await DID.DataLayers.KodeAplikasi.GetMemoryItemByKey(key);
				if (!TryValidateModel(vData)) return BadRequest(VALIDATION_ERROR);
                var vRetu = await DID.DataLayers.KodeAplikasi.DeleteObject(vData);
                if (vRetu) return Ok();
                else return BadRequest();
			}
			catch
			{
				return StatusCode(500, "Internal server error");
			}
		}

		private void PopulateModel(DID.Entities.KodeAplikasi iData, System.Collections.IDictionary dObject)
		{
			if (dObject.Contains("headCode")) { iData.headCode = System.Convert.ToString(dObject["headCode"]); iData.SetKeyID(); }
			if (dObject.Contains("childCode")) { iData.childCode = System.Convert.ToString(dObject["childCode"]); iData.SetKeyID(); }
			if (dObject.Contains("keterangan")) { iData.keterangan = System.Convert.ToString(dObject["keterangan"]); }
		}

	}
}
