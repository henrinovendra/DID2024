using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DID.DataLayers
{
    public class KodeAplikasi
    {
        #region GetAllMasterCode
        public static async Task<List<DID.Entities.KodeAplikasi>> GetheadCodeList()
        {
			var vList = new List<DID.Entities.KodeAplikasi>();
			try
            {
				await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
				await vConn.OpenAsync();
				await using (var vComd = new Npgsql.NpgsqlCommand("SELECT hcode, ccode, namas FROM mastercode WHERE ccode='*' ORDER BY hcode;", vConn))
                {
					await vComd.PrepareAsync();
					await using (var vReader = await vComd.ExecuteReaderAsync())
						while (await vReader.ReadAsync())
							vList.Add(ReadKodeAplikasi(vReader));
                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine(" .. . . Err 1 GetDbListheadCode KodeAplikasi"); }
			return vList;
        }
		public static async Task<List<DID.Entities.KodeAplikasi>> GetDbList()
        {
			var vList = new List<DID.Entities.KodeAplikasi>();
            try
            {
				await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
				await vConn.OpenAsync();
				using (Npgsql.NpgsqlCommand vComd = new Npgsql.NpgsqlCommand("SELECT hcode, ccode, namas FROM mastercode WHERE ccode!='*' ORDER BY hcode, ccode ", vConn))
                {
					await vComd.PrepareAsync();
					await using (var vReader = await vComd.ExecuteReaderAsync())
						while (await vReader.ReadAsync())
							vList.Add(ReadKodeAplikasi(vReader));
                }
            }
			catch(Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine(" . . . . Err 1 GetDbList KodeAPlikasi"); }
			return vList;
        }
		public static async Task<DID.Entities.KodeAplikasi> GetDbItem(string iheadCode, string ichildCode)
        {
			var vItem = new DID.Entities.KodeAplikasi();
            try
            {
				await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
				await vConn.OpenAsync();
				await using (var vComd = new Npgsql.NpgsqlCommand("SELECT hcode, ccode, namas FROM mastercode WHERE hcode = @vhcode AND ccode = @vccode;", vConn))
				{
					vComd.Parameters.AddWithValue("@vhcode", iheadCode);
					vComd.Parameters.AddWithValue("@vccode", ichildCode);
					await vComd.PrepareAsync();
					await using (var vReader = await vComd.ExecuteReaderAsync())
						while(await vReader.ReadAsync())
							vItem = ReadKodeAplikasi(vReader);
				}
			}
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine(". . . .Err GetDbItem KodeAplikasi "); }
			return vItem;
        }
		#endregion
		#region Memory
		public static async Task ListIntoMemory()
		{
			if (!DID.AppMemory.DaftarKodeAplikasiLoaded)
			{
				DID.AppMemory.DaftarKodeAplikasiType = await GetheadCodeList();
				DID.AppMemory.DaftarSatuan = await GetSatuanList();
				DID.AppMemory.DaftarKodeAplikasi = await GetDbList();
				DID.AppMemory.DaftarCompany = await GetCompanyList();
				DID.AppMemory.DaftarKodeAplikasiLoaded = true;


			}

		}
		public static async Task<List<DID.Entities.KodeAplikasi>> GetMemoryList()
		{
			await ListIntoMemory();
			return DID.AppMemory.DaftarKodeAplikasi;
		}
		public static async Task<DID.Entities.KodeAplikasi> GetMemoryItem(string iheadCode, string ichildCode)
		{
			var vItem = new DID.Entities.KodeAplikasi();
			await ListIntoMemory();
			foreach (DID.Entities.KodeAplikasi vIt in DID.AppMemory.DaftarKodeAplikasi)
			{
				if (vIt.headCode == iheadCode && vIt.childCode == ichildCode)
				{
					vItem = vIt.ShallowCopy();
					break;
				}
			}
			return vItem;
		}
		public static async Task<DID.Entities.KodeAplikasi> GetMemoryItemByKey(string iKeyID)
		{
			var vItem = new DID.Entities.KodeAplikasi();
			await ListIntoMemory();
			foreach (DID.Entities.KodeAplikasi vIt in DID.AppMemory.DaftarKodeAplikasi)
			{
				if (vIt.KeyID == iKeyID)
				{
					vItem = vIt.ShallowCopy();
					break;
				}
			}
			return vItem;
		}
        #endregion
        #region GETcompany 
		public static async Task<List<DID.Entities.KodeAplikasi>> GetCompanyList()
        {
			var vList = new List<DID.Entities.KodeAplikasi>();
            try
            {
				await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
				await vConn.OpenAsync();
				await using (Npgsql.NpgsqlCommand vComd = new Npgsql.NpgsqlCommand("SELECT hcode, ccode, namas FROM mastercode WHERE hcode = 'cmp' and ccode !='*'; ", vConn))
                {
					await vComd.PrepareAsync();
					await using (var vReader = await vComd.ExecuteReaderAsync())
						while (await vReader.ReadAsync())
							vList.Add(ReadKodeAplikasi(vReader));
				}
            }
			catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine(". . . Err Getcompany List 1"); }
			return vList;
        }
		public static async Task<List<DID.Entities.KodeAplikasi>> GetSatuanList()
		{
			var vList = new List<DID.Entities.KodeAplikasi>();
			try
			{
				await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
				await vConn.OpenAsync();
				await using (Npgsql.NpgsqlCommand vComd = new Npgsql.NpgsqlCommand("SELECT hcode, ccode, namas FROM mastercode WHERE hcode = 'stn' and ccode !='*'; ", vConn))
				{
					await vComd.PrepareAsync();
					await using (var vReader = await vComd.ExecuteReaderAsync())
						while (await vReader.ReadAsync())
							vList.Add(ReadKodeAplikasi(vReader));
				}
			}
			catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine(". . . Err Getcompany List 1"); }
			return vList;
		}
		public static async Task<DID.Entities.KodeAplikasi> GetcompanyItem(string ichildCode)
		{
			var vItem = new DID.Entities.KodeAplikasi();
			try
			{
				await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
				await vConn.OpenAsync();
				await using (Npgsql.NpgsqlCommand vComd = new Npgsql.NpgsqlCommand("SELECT hcode, ccode, namas FROM mastercode WHERE hcode = 'cmp' and ccode = @vccode; ", vConn))
				{
					vComd.Parameters.AddWithValue("@vccode", ichildCode);
					await vComd.PrepareAsync();
					await using (var vReader = await vComd.ExecuteReaderAsync())
						while (await vReader.ReadAsync())
							vItem = ReadKodeAplikasi(vReader);
				}
			}
			catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine(". . . Err Getcompany List 1"); }
			return vItem;
		}

        #endregion
        #region Insert/Delete/Update

        public static async Task<bool> InsertDb(string ihcode, string iccode, string inamas)
		{
			bool vBoRetur = false;
			try
			{
				await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
				await vConn.OpenAsync();
			    await using (Npgsql.NpgsqlCommand vComd = new Npgsql.NpgsqlCommand("SELECT public.mastercode_insert ( @vhcode, @vccode, @vnamas );", vConn))
				{
					vComd.Parameters.AddWithValue("@vhcode", ihcode.Trim());
					vComd.Parameters.AddWithValue("@vccode", iccode.Trim());
					vComd.Parameters.AddWithValue("@vnamas", inamas.Trim());
					await vComd.PrepareAsync();
					await vComd.ExecuteNonQueryAsync();
					DID.AppMemory.DaftarKodeAplikasiLoaded = false;
					vBoRetur = true;
				}
			}
			catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine(". . .ERR 1 -- InsertDb KodeAplikasi"); }
			catch (System.InvalidOperationException) { System.Diagnostics.Debug.WriteLine(". . .ERR 2 -- InsertDb KodeAplikasi"); }
			return vBoRetur;
		}
		public static async Task<bool> InsertObject(DID.Entities.KodeAplikasi iKodeAplikasiObj)
		{
			return await InsertDb(iKodeAplikasiObj.headCode, iKodeAplikasiObj.childCode, iKodeAplikasiObj.keterangan);
		}
		public async static Task<bool> UpdateDb(string iheadCode, string ichildCode, string iketerangan)
		{
			bool vBoRetur = false;
			try
			{
				await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
				await vConn.OpenAsync();
				await using (Npgsql.NpgsqlCommand vComd = new Npgsql.NpgsqlCommand("SELECT mastercode_update(@vhcode,@vccode,@vnamas);", vConn))
				{
					vComd.Parameters.AddWithValue("@vhcode", iheadCode.Trim());
					vComd.Parameters.AddWithValue("@vccode", ichildCode.Trim());
					vComd.Parameters.AddWithValue("@vnamas", iketerangan.Trim());
					await vComd.PrepareAsync();
					await vComd.ExecuteNonQueryAsync();
					DID.AppMemory.DaftarKodeAplikasiLoaded = false;

					vBoRetur = true;

				}
			}
			catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine(". . . Err 1 Insert KodeAplikasi "); }
			return vBoRetur;
		}
		public async static Task<bool> UpdateObject(DID.Entities.KodeAplikasi iKodeAplikasi)
		{
			return await UpdateDb(iKodeAplikasi.headCode, iKodeAplikasi.childCode, iKodeAplikasi.keterangan);
		}
		public static async Task<bool> DeleteDb(string iheadCode, string ichildCode)
        {
			var vBoRetur = false;
            try
            {

				await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
				await vConn.OpenAsync();
				await using(Npgsql.NpgsqlCommand vComd= new Npgsql.NpgsqlCommand("SELECT mastercode_delete(@vhcode,@vccode);", vConn))
				{
					vComd.Parameters.AddWithValue("@vhcode", iheadCode);
					vComd.Parameters.AddWithValue("@vccode", ichildCode);
					await vComd.PrepareAsync();
					await vComd.ExecuteNonQueryAsync();
					DID.AppMemory.DaftarKodeAplikasiLoaded = false;
					vBoRetur = true;
				}
            }
			catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine(". . .ERR 1 -- DeleteDb KodeAplikasi"); }
			return vBoRetur;
		}
		public async static Task<bool> DeleteObject(DID.Entities.KodeAplikasi iKodeAplikasi)
		{
			return await DeleteDb(iKodeAplikasi.headCode, iKodeAplikasi.childCode);
		}
        #endregion
        #region Read Data
        private static DID.Entities.KodeAplikasi ReadKodeAplikasi(System.Data.IDataRecord iReader)
		{
			var vItem = new DID.Entities.KodeAplikasi
			{
				headCode = System.Convert.ToString(iReader["hcode"]),
				childCode = System.Convert.ToString(iReader["ccode"]),
				keterangan = System.Convert.ToString(iReader["namas"])
			};
			vItem.SetKeyID();
			return vItem;
		}
		#endregion
		
	}

}
