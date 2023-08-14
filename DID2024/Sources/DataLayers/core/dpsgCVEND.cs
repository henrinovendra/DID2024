using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DID.DataLayers
{
    public class Vendor
    {

        #region Memory
        public static async Task ListIntoMemory()
        {
            if (!DID.AppMemory.DaftarVendorIsLoaded)
            {
                DID.AppMemory.DaftarVendor = await GetDbList();
                DID.AppMemory.DaftarVendorIsLoaded = true;
            }
        }
        public static async Task<List<DID.Entities.Vendor>> GetMemoryList()
        {
            await ListIntoMemory();
            return DID.AppMemory.DaftarVendor;
        }
        public static async Task<DID.Entities.Vendor> GetMemoryItem(int ikodeVendor)
        {
            var vItem = new DID.Entities.Vendor();
            await ListIntoMemory();
            foreach (DID.Entities.Vendor vIt in DID.AppMemory.DaftarVendor)
            {
                if (vIt.kodeVendor == ikodeVendor)
                {
                    vItem = vIt.SallowCopy();
                    break;
                }
            }
            return vItem;
        }
        #endregion
        #region GETMAXKODE
        public  static async Task<DID.Entities.Vendor> GetMaxKode()
        {
            var vItem = new DID.Entities.Vendor();
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (Npgsql.NpgsqlCommand vComd = new Npgsql.NpgsqlCommand("SELECT MAX(idven) as idven FROM cvend", vConn))
                {
                    await vComd.PrepareAsync();
                    await using(var vReader = await vComd.ExecuteReaderAsync())
                        while (await vReader.ReadAsync())
                            vItem = ReadMaxKode(vReader);
                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine(". . . ERR 1 -- GetDbItem Vendor"); }
            catch (System.InvalidOperationException) { System.Diagnostics.Debug.WriteLine(". . . ERR 2 -- GetDbItem Vendor"); }
            return vItem;
        }
        #endregion
        #region List

        public static async Task<List<DID.Entities.Vendor>> GetDbList()
        {
            var vList = new List<DID.Entities.Vendor>();
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (Npgsql.NpgsqlCommand vComd = new Npgsql.NpgsqlCommand("SELECT idven, vennm,addvn,ntelp, picnm, noehp, email,  bnknm,adbnk, norek, nmrek,nnpwp, lsdte FROM cvend ORDER BY idven;", vConn))
                {
                    await vComd.PrepareAsync();
                    await using (var vReader = await vComd.ExecuteReaderAsync())
                        while (await vReader.ReadAsync())
                            vList.Add(ReadVendor(vReader));
                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine(". . .ERR 1 -- Default Vendor"); }
            catch (System.InvalidOperationException) { System.Diagnostics.Debug.WriteLine(". . .ERR 2 -- Default Vendor"); }
            return vList;
        }
        public static async Task<DID.Entities.Vendor> GetDbItem(int ikodeVendor)
        {
            var vItem = new DID.Entities.Vendor();
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (Npgsql.NpgsqlCommand vComd = new Npgsql.NpgsqlCommand("SELECT idven, vennm,addvn, email, picnm, noehp, bnknm, norek, nmrek, lsdte FROM cvend WHERE idven = @vidven;", vConn))
                {
                    vComd.Parameters.AddWithValue("@vidven", ikodeVendor);
                    await vComd.PrepareAsync();
                    await using (var vReader = await vComd.ExecuteReaderAsync())
                        while (await vReader.ReadAsync())
                            vItem = ReadVendor(vReader);
                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine(". . . ERR 1 -- GetDbItem Vendor"); }
            catch (System.InvalidOperationException) { System.Diagnostics.Debug.WriteLine(". . . ERR 2 -- GetDbItem Vendor"); }
            return vItem;
        }
        
        #endregion
        #region insert/update/delete
        public static async Task<bool> DeleteDb(int ikodeVendor)
        {
            bool vRetur = false;
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (Npgsql.NpgsqlCommand vComd = new Npgsql.NpgsqlCommand("SELECT cvend_delete(@vidven);",vConn))
                {
                    vComd.Parameters.AddWithValue("@vidven", ikodeVendor);
                    await vComd.PrepareAsync();
                    await vComd.ExecuteNonQueryAsync();
                    DID.AppMemory.DaftarVendorIsLoaded = false;
                    vRetur = true;
                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine("... Err 1 -- DeleteDb"); }
            catch (System.InvalidOperationException) { System.Diagnostics.Debug.WriteLine("... Err 2 -- DeleteDb"); }
            return vRetur;

        }
        public static async Task<bool> DeleteObject(DID.Entities.Vendor iVendor)
        {
            return await DeleteDb(iVendor.kodeVendor);
        }
        public static async Task<bool> InsertDb(int ikodeVendor, string inamaVendor, string ialamatVendor, long inomorTelepon, string inamaPIC, long inomorHP, string iemail, string inamaBank, string ialamatBank, long inomorRekening, string inamaRekening, long inpwp)
        {
            bool vRetur = false;
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (Npgsql.NpgsqlCommand vComd = new Npgsql.NpgsqlCommand("SELECT cvend_save(@vidven,@vvennm,@vaddvn,@vntelp,@vpicnm, @vnoehp,@vemail,@vbnknm,@vadbnk, @vnorek, @vnmrek,@vnnpwp);", vConn))
                {
                    vComd.Parameters.AddWithValue("@vidven", ikodeVendor);
                    vComd.Parameters.AddWithValue("@vvennm", inamaVendor.Trim());
                    vComd.Parameters.AddWithValue("@vaddvn", ialamatVendor.Trim());
                    vComd.Parameters.AddWithValue("@vntelp", inomorTelepon);
                    vComd.Parameters.AddWithValue("@vpicnm", inamaPIC.Trim());
                    vComd.Parameters.AddWithValue("@vnoehp", inomorHP);
                    vComd.Parameters.AddWithValue("@vemail", iemail.Trim());
                    vComd.Parameters.AddWithValue("@vbnknm", inamaBank.Trim());
                    vComd.Parameters.AddWithValue("@vadbnk", ialamatBank.Trim());
                    vComd.Parameters.AddWithValue("@vnorek", inomorRekening);
                    vComd.Parameters.AddWithValue("@vnmrek", inamaRekening.Trim());
                    vComd.Parameters.AddWithValue("@vnnpwp", inpwp);
                    await vComd.PrepareAsync();
                    await vComd.ExecuteNonQueryAsync();
                    DID.AppMemory.DaftarVendorIsLoaded = false;

                    vRetur = true;

                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine(". . .ERR 1 -- InsertDb Vendor"); }
            catch (System.InvalidOperationException) { System.Diagnostics.Debug.WriteLine(". . .ERR 2 -- InsertDb Vendor"); }
            return vRetur;
        }
        public static async Task<bool> InsertObj(DID.Entities.Vendor iVendor)
        {
            return await InsertDb(iVendor.kodeVendor, iVendor.namaVendor, iVendor.alamatVendor, iVendor.nomorTelepon, iVendor.namaPIC, iVendor.nomorHP, iVendor.email, iVendor.namaBank, iVendor.alamatBank, iVendor.nomorRekening, iVendor.namaRekening, iVendor.npwp);
        }
        public static async Task<bool> UpdateDb(int ikodeVendor, string inamaVendor,string ialamatVendor, long inomorTelepon, string inamaPIC, long inomorHP, string iemail, string inamaBank, string ialamatBank, long inomorRekening, string inamaRekening, long inpwp)

        {
            bool vRetur = false;
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (Npgsql.NpgsqlCommand vComd = new Npgsql.NpgsqlCommand("SELECT cvend_update(@vidven,@vvennm,@vaddvn,@vntelp,@vpicnm, @vnoehp,@vemail,@vbnknm,@vadbnk, @vnorek, @vnmrek,@vnnpwp);", vConn))
                {

                    vComd.Parameters.AddWithValue("@vidven", ikodeVendor);
                    vComd.Parameters.AddWithValue("@vvennm", inamaVendor.Trim());
                    vComd.Parameters.AddWithValue("@vaddvn", ialamatVendor.Trim());
                    vComd.Parameters.AddWithValue("@vntelp", inomorTelepon);
                    vComd.Parameters.AddWithValue("@vpicnm", inamaPIC.Trim());
                    vComd.Parameters.AddWithValue("@vnoehp", inomorHP);
                    vComd.Parameters.AddWithValue("@vemail", iemail.Trim());
                    vComd.Parameters.AddWithValue("@vbnknm", inamaBank.Trim());
                    vComd.Parameters.AddWithValue("@vadbnk", ialamatBank.Trim());
                    vComd.Parameters.AddWithValue("@vnorek", inomorRekening);
                    vComd.Parameters.AddWithValue("@vnmrek", inamaRekening.Trim());
                    vComd.Parameters.AddWithValue("@vnnpwp", inpwp);
                    await vComd.PrepareAsync();
                    await vComd.ExecuteNonQueryAsync();
                    DID.AppMemory.DaftarVendorIsLoaded = false;

                    vRetur = true;

                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine(". . .ERR 1 -- UpdateDB Vendor"); }
            catch (System.InvalidOperationException) { System.Diagnostics.Debug.WriteLine(". . .ERR 2 -- UpdateDb Vendor"); }
            return vRetur;
        }
        public static async Task<bool> UpdateObject(DID.Entities.Vendor iVendor)
        {
            return await UpdateDb(iVendor.kodeVendor, iVendor.namaVendor,iVendor.alamatVendor, iVendor.nomorTelepon, iVendor.namaPIC, iVendor.nomorHP, iVendor.email, iVendor.namaBank, iVendor.alamatBank, iVendor.nomorRekening, iVendor.namaRekening, iVendor.npwp);

        }
        #endregion
        #region Reading
        private static DID.Entities.Vendor ReadMaxKode(System.Data.IDataRecord iReader)
        {
            var vItem = new DID.Entities.Vendor();
            if (!iReader.IsDBNull(iReader.GetOrdinal("idven")))
            {
                vItem.kodeVendor = System.Convert.ToInt32(iReader["idven"]);
            }
            else
            {
                // Atur nilai default jika idven adalah DBNull
                // Misalnya, Anda ingin mengatur nilainya menjadi 0
                vItem.kodeVendor = 0;
            }
            return vItem;
        }
        private static DID.Entities.Vendor ReadVendor(System.Data.IDataRecord iReader)
        {
            var vItem = new DID.Entities.Vendor
            {
                kodeVendor = System.Convert.ToInt32(iReader["idven"]),
                namaVendor = System.Convert.ToString(iReader["vennm"]),
                alamatVendor = System.Convert.ToString(iReader["addvn"]),
                nomorTelepon = System.Convert.ToInt64(iReader["ntelp"]),
                namaPIC = System.Convert.ToString(iReader["picnm"]),
                nomorHP = System.Convert.ToInt64(iReader["noehp"]),
                email = System.Convert.ToString(iReader["email"]),
                namaBank = System.Convert.ToString(iReader["bnknm"]),
                alamatBank = System.Convert.ToString(iReader["adbnk"]),
                namaRekening = System.Convert.ToString(iReader["nmrek"]),
                nomorRekening = System.Convert.ToInt64(iReader["norek"]),
                npwp = System.Convert.ToInt64(iReader["nnpwp"])

            };
            return vItem;
        }
        #endregion
    }
}
