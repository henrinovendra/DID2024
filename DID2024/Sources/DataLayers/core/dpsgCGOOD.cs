using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace DID.DataLayers
{
    public class Barang
    {
        #region Memory
        public static async Task ListIntoMemoryByCompany(string iCompany)
        {
            if (!DID.AppMemory.DaftarBarangLoadedByCompany)
            {

                DID.AppMemory.DaftarBarangByCompany = await GetDbListByCompany(iCompany);
                DID.AppMemory.DaftarBarangLoadedByCompany = true;

            }
        }
        public static async Task ListIntoMemory()
        {
            if (!DID.AppMemory.DaftarBarangLoaded)
            {
                DID.AppMemory.DaftarBarang = await GetDbList();
                DID.AppMemory.DaftarJenisBarang = await GetJenisBarang();
                DID.AppMemory.DaftarBarangLoaded = true;
            }
        }
        public static async Task<List<DID.Entities.Barang>> GetMemoryList()
        {
            await ListIntoMemory();
            return DID.AppMemory.DaftarBarang;
        }
        public static async Task<DID.Entities.Barang> GetMemoryItem(string iJenisBarang, string iTypeBarang)
        {
            var vItem = new DID.Entities.Barang();
            await ListIntoMemory();
            foreach (DID.Entities.Barang vIt in DID.AppMemory.DaftarBarang)
            {
                if (vIt.jenisBarang == iJenisBarang && vIt.typeBarang == iTypeBarang)
                {
                    vItem = vIt.ShallowCopy();
                    break;
                }
            }
            return vItem;
        }
        public static async Task<DID.Entities.Barang> GetMemoryItemByKey(string iKeyID)
        {
            var vItem = new DID.Entities.Barang();
            await ListIntoMemory();
            foreach (DID.Entities.Barang vIt in DID.AppMemory.DaftarBarang)
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
        #region GetMaxKode
        public static async Task<DID.Entities.Barang> GetMaxKode(string iCompany)
        {
            var vItem = new DID.Entities.Barang();
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (var vComd = new Npgsql.NpgsqlCommand("SELECT MAX(nomid) as nomid FROM cgood WHERE nomid ~ '^[A-Z]{3}\\d{3}$' AND cmpnm = @vcmpnm;", vConn))
                {
                    vComd.Parameters.AddWithValue("@vcmpnm", iCompany);
                    await vComd.PrepareAsync();
                    await using (var vReader = await vComd.ExecuteReaderAsync())
                        while (await vReader.ReadAsync())
                            vItem = ReadMaxKode(vReader);
                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine("Err 1 Get Max Kode Barang"); }
            return vItem;
        }
        #endregion
        #region Get By Company
        public static async Task<List<DID.Entities.Barang>> GetDbListByCompany(string iCompany)
        {
            var vList = new List<DID.Entities.Barang>();
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (var vComd = new Npgsql.NpgsqlCommand("SELECT typid, nomid, namas,xnama ,cmpnm, stnbr, qtybr,lsdte  FROM cgood WHERE nomid !='*' AND cmpnm =  @vcmpnm ORDER BY namas;", vConn))
                {
                    vComd.Parameters.AddWithValue("@vcmpnm", iCompany);
                    await vComd.PrepareAsync();
                    await using (var vReader = await vComd.ExecuteReaderAsync())
                        while (await vReader.ReadAsync())
                            vList.Add(ReadBarang(vReader));
                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine("Err GetDBlistByCompany Barang"); }
            return vList;
        }

        #endregion
        #region GET DATA
        public static async Task<List<DID.Entities.Barang>> GetJenisBarang()
        {
            var vList = new List<DID.Entities.Barang>();
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (var vComd = new Npgsql.NpgsqlCommand("SELECT typid, nomid, namas,xnama,cmpnm, stnbr, qtybr,lsdte  FROM cgood WHERE nomid='*'  ORDER BY namas;", vConn))
                {
                    await vComd.PrepareAsync();
                    await using (var vReader = await vComd.ExecuteReaderAsync())
                        while (await vReader.ReadAsync())
                            vList.Add(ReadBarang(vReader));
                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine("Err GetDBlist Barang"); }
            return vList;
        }
        public static async Task<List<DID.Entities.Barang>> GetDbList()
        {
            var vList = new List<DID.Entities.Barang>();
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (var vComd = new Npgsql.NpgsqlCommand("SELECT typid, nomid, namas,xnama,cmpnm, stnbr, qtybr,lsdte  FROM cgood WHERE nomid!='*' ORDER BY namas;", vConn))
                {
                    await vComd.PrepareAsync();
                    await using (var vReader = await vComd.ExecuteReaderAsync())
                        while (await vReader.ReadAsync())
                            vList.Add(ReadBarang(vReader));
                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine("Err GetDBlist Barang"); }
            return vList;
        }
        public static async Task<DID.Entities.Barang> GetDbItem(string ijenisBarang, string itypeBarang)
        {
            var vItem = new DID.Entities.Barang();
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (var vComd = new Npgsql.NpgsqlCommand("SELECT typid, nomid, namas,xnama,cmpnm, stnbr, qtybr,lsdte FROM cgood WHERE typid = @vtypid AND nomid=@vnomid;", vConn))
                {
                    vComd.Parameters.AddWithValue("@vtypid", ijenisBarang);
                    vComd.Parameters.AddWithValue("@vnomid", itypeBarang);
                    await vComd.PrepareAsync();
                    await using (var vReader = await vComd.ExecuteReaderAsync())
                        while (await vReader.ReadAsync())
                            vItem = ReadBarang(vReader);
                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine("Err GetDbItem Barang"); }
            return vItem;
        }
        
        #endregion
        #region Insert/Update/Delete
        public static async Task<bool> InsertDb(string ijenisBarang, string itypeBarang, string iketerangan, string ixNamaBarang, string icompany, string isatuan, int iquantity)
        {
            var vReBool = false;
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (var vComd = new Npgsql.NpgsqlCommand("SELECT cgood_insert(@vtypid, @vnomid, @vnamas,@vxnama, @vcmpnm, @vstnbr, @vqtybr);", vConn))
                {
                    vComd.Parameters.AddWithValue("@vtypid", ijenisBarang.Trim());
                    vComd.Parameters.AddWithValue("@vnomid", itypeBarang.Trim());
                    vComd.Parameters.AddWithValue("@vnamas", iketerangan.Trim());
                    vComd.Parameters.AddWithValue("@vxnama", ixNamaBarang.Trim());
                    vComd.Parameters.AddWithValue("@vcmpnm", icompany.Trim());
                    vComd.Parameters.AddWithValue("@vstnbr", isatuan);
                    vComd.Parameters.AddWithValue("@vqtybr", iquantity);
                    await vComd.PrepareAsync();
                    await vComd.ExecuteNonQueryAsync();

                    vReBool = true;
                    DID.AppMemory.DaftarBarangLoaded = false;
                    DID.AppMemory.DaftarBarangLoadedByCompany = false;
                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine("... ERR 1 InsertDb Barang "); }
            return vReBool;
        }
        public static async Task<bool> InsertObject(DID.Entities.Barang iBarang)
        {
            return await InsertDb(iBarang.jenisBarang, iBarang.typeBarang, iBarang.keterangan,iBarang.xnamaBarang, iBarang.company, iBarang.satuan, iBarang.quantity);
        }
        public static async Task<bool> insertDBHead(string ijenisBarang, string iketerangan)
        {
            var vReBool = false;
            try 
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (var vComd = new Npgsql.NpgsqlCommand("SELECT public.cgoodhead_insert(@vtypid, @vnamas)", vConn))
                {
                    vComd.Parameters.AddWithValue("@vtypid", ijenisBarang.Trim());
                    vComd.Parameters.AddWithValue("@vnamas", iketerangan.Trim());
                    await vComd.PrepareAsync();
                    await vComd.ExecuteNonQueryAsync();
                    vReBool = true;
                     DID.AppMemory.DaftarBarangLoaded = false;
                    DID.AppMemory.DaftarBarangLoadedByCompany = false;

                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine("... ERR 1 InsertDbHead Barang "); }
            return vReBool;

        }
        public static async Task<bool> InsertHeadObject (DID.Entities.Barang iBarang)
        {
            return await insertDBHead(iBarang.jenisBarang, iBarang.keterangan);
        }
        public static async Task<bool> UpdateDb(string ijenisBarang, string itypeBarang, string iketerangan,string ixnamaBarang, string icompany, string isatuan, int iquantity)
        {
            var vReBool = false;
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (var vComd = new Npgsql.NpgsqlCommand("SELECT cgood_update(@vtypid, @vnomid, @vnamas,@vxnama, @vcmpnm, @vstnbr, @vqtybr);", vConn))
                {
                    vComd.Parameters.AddWithValue("@vtypid", ijenisBarang.Trim());
                    vComd.Parameters.AddWithValue("@vnomid", itypeBarang.Trim());
                    vComd.Parameters.AddWithValue("@vnamas", iketerangan.Trim());
                    vComd.Parameters.AddWithValue("@vxnama", ixnamaBarang.Trim());
                    vComd.Parameters.AddWithValue("@vcmpnm", icompany.Trim());
                    vComd.Parameters.AddWithValue("@vstnbr", isatuan);
                    vComd.Parameters.AddWithValue("@vqtybr", iquantity);
                    await vComd.PrepareAsync();
                    await vComd.ExecuteNonQueryAsync();

                    vReBool = true;
                    DID.AppMemory.DaftarBarangLoaded = false;
                    DID.AppMemory.DaftarBarangLoadedByCompany = false;
                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine("... ERR 1 updateDB Barang "); }
            return vReBool;
        }
        public static async Task<bool> UpdateObject(DID.Entities.Barang iBarang)
        {
            return await UpdateDb(iBarang.jenisBarang, iBarang.typeBarang, iBarang.keterangan,iBarang.xnamaBarang, iBarang.company, iBarang.satuan, iBarang.quantity);
        }
        public static async Task<bool> DeleteDb(string ijenisBarang, string itypeBarang)
        {
            var vReBool = false;
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (var vComd = new Npgsql.NpgsqlCommand("SELECT cgood_delete(@vtypid, @vnomid);", vConn))
                {
                    vComd.Parameters.AddWithValue("@vtypid", ijenisBarang.Trim());
                    vComd.Parameters.AddWithValue("@vnomid", itypeBarang.Trim());
                    await vComd.PrepareAsync();
                    await vComd.ExecuteNonQueryAsync();

                    vReBool = true;
                    DID.AppMemory.DaftarBarangLoaded = false;
                    DID.AppMemory.DaftarBarangLoadedByCompany = false;
                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine("... ERR 1 DeleteDb Barang "); }
            return vReBool;
        }
        public static async Task<bool> DeleteObject(DID.Entities.Barang iBarang)
        {
            return await DeleteDb(iBarang.jenisBarang, iBarang.typeBarang);
        }

        #endregion
        #region ReadData
        private static DID.Entities.Barang ReadMaxKode(System.Data.IDataRecord iReader)
        {
            var vItem = new DID.Entities.Barang
            {
                typeBarang = System.Convert.ToString(iReader["nomid"])
            };
            return vItem;
        }
        private static DID.Entities.Barang ReadBarang(System.Data.IDataRecord iReader)
        {
            var vItem = new DID.Entities.Barang
            {
                jenisBarang = System.Convert.ToString(iReader["typid"]),
                typeBarang = System.Convert.ToString(iReader["nomid"]),
                keterangan = System.Convert.ToString(iReader["namas"]),
                xnamaBarang = System.Convert.ToString(iReader["xnama"]),

                company = System.Convert.ToString(iReader["cmpnm"]),
                satuan = System.Convert.ToString(iReader["stnbr"]),
                quantity = System.Convert.ToInt32(iReader["qtybr"]),
                lastRevisi = System.Convert.ToDateTime(iReader["lsdte"])
            };


            return vItem;
        }
        #endregion
    }
}
