using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DID.DataLayers
{
    public class PenggunaAplikasi
    {
        #region GetToDatabase User
        

        public static async Task<List<DID.Entities.PenggunaAplikasi>> GetDbList()
        {
            var vList = new List<DID.Entities.PenggunaAplikasi>();
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (var vComd = new Npgsql.NpgsqlCommand("SELECT idusr, usrnm,pswdt,usrrl, cmpnm FROM cuser ORDER BY idusr;", vConn))
                {
                    await vComd.PrepareAsync();
                    await using (var vReader = await vComd.ExecuteReaderAsync())
                        while (await vReader.ReadAsync())
                            vList.Add(ReadPenggunaAplikasi(vReader));
                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine("...Err 1 GetDbList PenggunaAplikasi"); }
            return vList;
        }
        public static async Task<DID.Entities.PenggunaAplikasi> GetDbItem(string iUserName)
        {
            var vItem = new DID.Entities.PenggunaAplikasi();
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (var vComd = new Npgsql.NpgsqlCommand("SELECT idusr, usrnm,pswdt,usrrl, cmpnm FROM cuser WHERE usrnm = @vusrnm ", vConn))
                {
                    vComd.Parameters.AddWithValue("@vusrnm", iUserName);
                    await vComd.PrepareAsync();
                    await using (var vReader = await vComd.ExecuteReaderAsync())
                        while (await vReader.ReadAsync())
                            vItem = ReadPenggunaAplikasi(vReader);
                }
                await using (var vComd = new Npgsql.NpgsqlCommand("SELECT mdusr FROM usmdl WHERE idusr = @vidusr", vConn))
                {
                    vComd.Parameters.AddWithValue("@vidusr", vItem.UserId);
                    await vComd.PrepareAsync();
                    await using var vReader = await vComd.ExecuteReaderAsync();
                    while (await vReader.ReadAsync())
                        vItem.Modules.Add(vReader.GetString(0)); // Assuming 'mdusr' is of type character varying(10)

                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine("...Err 1 GetDbList PenggunaAplikasi"); }
            return vItem;
        }
        #endregion
        #region Insert/Update/Delete to Database User

        public static async Task<bool> InsertDb (int iUserId, string iUserName, string icompany, string iPassword)
        {
            bool vBoRetur = false;
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (var vComd = new Npgsql.NpgsqlCommand("SELECT cuser_insert (@vusrid, @vusrnm, @vcmpnm, @vpswdt);", vConn))
                {
                    vComd.Parameters.AddWithValue("@vusrid", iUserId);
                    vComd.Parameters.AddWithValue("@vusrnm", iUserName.Trim());
                    vComd.Parameters.AddWithValue("@vcmpnm", icompany.Trim());
                    vComd.Parameters.AddWithValue("@vpswdt", iPassword.Trim());
                    await vComd.PrepareAsync();
                    await vComd.ExecuteNonQueryAsync();
                    vBoRetur = true;
                    
                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine("... Err1 InsertDb PenggunaAplikasi"); }
            return vBoRetur;
        }

        public static async Task<bool> InsertObject(DID.Entities.PenggunaAplikasi iPenggunaAplikasi)
        {
            return await InsertDb(iPenggunaAplikasi.UserId, iPenggunaAplikasi.UserName, iPenggunaAplikasi.company, iPenggunaAplikasi.Password);
        }
        public static async Task<bool> UpdateDb(int iUserId, string iUserName, string icompany, string iPassword)
        {
            bool vBoRetur = false;
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (var vComd = new Npgsql.NpgsqlCommand("SELECT cuser_update (@vusrid, @vusrnm, @vcmpnm, @vpswdt);", vConn))
                {
                    vComd.Parameters.AddWithValue("@vusrid", iUserId);
                    vComd.Parameters.AddWithValue("@vusrnm", iUserName.Trim());
                    vComd.Parameters.AddWithValue("@vcmpnm", icompany.Trim());
                    vComd.Parameters.AddWithValue("@vpswdt", iPassword.Trim());
                    await vComd.PrepareAsync();
                    await vComd.ExecuteNonQueryAsync();
                    vBoRetur = true;

                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine("... Err1 InsertDb PenggunaAplikasi"); }
            return vBoRetur;
        }

        public static async Task<bool> UpdateObject(DID.Entities.PenggunaAplikasi iPenggunaAplikasi)
        {
            return await UpdateDb(iPenggunaAplikasi.UserId, iPenggunaAplikasi.UserName, iPenggunaAplikasi.company, iPenggunaAplikasi.Password);
        }


        #endregion
        #region Read Data
        private static DID.Entities.PenggunaAplikasi ReadPenggunaAplikasi(System.Data.IDataRecord iReader)
        {
            var vItem = new DID.Entities.PenggunaAplikasi
            {
                UserId = System.Convert.ToInt32(iReader["idusr"]),
                UserName = System.Convert.ToString(iReader["usrnm"]),
                Password = System.Convert.ToString(iReader["pswdt"]),
                company = System.Convert.ToString(iReader["cmpnm"]),
                Role = System.Convert.ToString(iReader["usrrl"]),
                Modules = new List<string>()
            };
            return vItem;
        }

        


        #endregion

    }
}
