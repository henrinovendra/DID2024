using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DID.DataLayers
{
    public class TransaksiPembelianInternal
    {

        #region Memory
        public static async Task ListIntoMemory()
        {
            if (!DID.AppMemory.DaftarTransaksiPembelianIsLoaded)
            {
                DID.AppMemory.DaftarListTransaksi = await GetDbList();
                DID.AppMemory.DaftarTransaksiPembelianIsLoaded = true;
            }
        }
        public static async Task<List<DID.Entities.TransaksiPembelianInternal>> GetMemoryItem()
        {
            await ListIntoMemory();
            return DID.AppMemory.DaftarListTransaksi;
        }
        public static async Task<DID.Entities.TransaksiPembelianInternal> GetMemoryItem(string itransaksiId)
        {
            var vItem = new DID.Entities.TransaksiPembelianInternal();
            await ListIntoMemory();
            foreach (DID.Entities.TransaksiPembelianInternal vIt in DID.AppMemory.DaftarListTransaksi)
            {
                if (vIt.transaksiId == itransaksiId)
                {
                    vItem = vIt.ShallowCopy();
                    break;
                }
            }
            return vItem;
        }
        #endregion 
        #region GetListData
        public static async Task<List<DID.Entities.TransaksiPembelianInternal>> GetDbList()
        {
            var vList = new List<Entities.TransaksiPembelianInternal>();
            
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using(var vComd = new Npgsql.NpgsqlCommand(
                    "SELECT t1.trsid, t1.nomso, t1.nompr, t1.nompo, t1.nocoa, t1.brgnm, t1.stnbr, t1.cmpnm, t1.qtybr, t1.prprc, t1.nombr, t1.nosrv, t1.cstll, t1.dscnt, t1.ppncs, t1.ttlhg, t1.pcktr, t1.podte, t1.dedte, t1.pcdte, t2.nomsj, t2.noref, t2.noinv, t2.nomfp, t2.indte, t2.jtdte, t2.idven, t2.noepo, t2.brenm FROM trpblin t1 JOIN trpblex t2 ON t1.trsid = t2.trsid;", 
                    vConn))
                        {
                            await vComd.PrepareAsync();
                            await using (var vReader = await vComd.ExecuteReaderAsync())
                                while (await vReader.ReadAsync())
                                    vList.Add(ReadTransaksi(vReader));
                        }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine("Err GetDBlist TransaksiPembelian "); }
            return vList;
        }
        public static async Task<List<DID.Entities.TransaksiPembelianInternal>> GetDbListByCompany(string iCompany)
        {
            var vList = new List<Entities.TransaksiPembelianInternal>();

            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (var vComd = new Npgsql.NpgsqlCommand(
                    "SELECT t1.trsid, t1.nomso, t1.nompr, t1.nompo, t1.nocoa, t1.brgnm, t1.stnbr, t1.cmpnm, t1.qtybr, t1.prprc, t1.nombr, t1.nosrv, t1.cstll, t1.dscnt, t1.ppncs, t1.ttlhg, t1.pcktr, t1.podte, t1.dedte, t1.pcdte, t2.nomsj, t2.noref, t2.noinv, t2.nomfp, t2.indte, t2.jtdte, t2.idven, t2.noepo, t2.brenm FROM trpblin t1 JOIN trpblex t2 ON t1.trsid = t2.trsid WHERE t1.cmpnm = @vcmpnm;",
                    vConn))
                {
                    vComd.Parameters.AddWithValue("@vcmpnm", iCompany);
                    await vComd.PrepareAsync();
                    await using (var vReader = await vComd.ExecuteReaderAsync())
                        while (await vReader.ReadAsync())
                            vList.Add(ReadTransaksi(vReader));
                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine("Err GetDBlist TransaksiPembelian "); }
            return vList;
        }
        public static async Task<DID.Entities.TransaksiPembelianInternal> GetDbItem(string iTransaksiId)
        {
            var vItem = new DID.Entities.TransaksiPembelianInternal();
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (var vComd = new Npgsql.NpgsqlCommand("SELECT trsid, nomso, nompr, nompo, nocoa, brgnm, stnbr, cmpnm, qtybr, prprc, nombr, nosrv, cstll, dscnt, ppncs, ttlhg, pcktr, podte, dedte, pcdte FROM trpblin WHERE trsid=@vtrsid;",vConn))
                {
                    vComd.Parameters.AddWithValue("@vtrsid", iTransaksiId);
                    await vComd.PrepareAsync();
                    await using (var vReader = await vComd.ExecuteReaderAsync())
                        while (await vReader.ReadAsync())
                            vItem = ReadTransaksi(vReader);
                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine("Err GetDBlist TransaksiPembelian "); }
            return vItem;
        }
        public static async Task<DID.Entities.TransaksiPembelianInternal> GetMaxKode(string iCompany)
        {
            var vItem = new DID.Entities.TransaksiPembelianInternal();
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (var vComd = new Npgsql.NpgsqlCommand("SELECT MAX(trsid) as trsid FROM trpblin  WHERE trsid ~ '^\\d{6}[A-Z]{3}\\d{3}$' AND  cmpnm = @vcmpnm;", vConn))
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
        #region  Insert/update/delete DBPembelian
        public static async Task<bool> InsertDB
            (string iTransaksiId, int inomorSO, int inomorPO, int inomorPR, int inomorCOA, string inamaBarang, string iSatuan, string iCompany, int iQuantity, decimal iHargaSatuan, 
            decimal iNominalMaterial,decimal iNominalService, decimal iBiayaLain, decimal iDiscount, decimal iPPN, decimal itotalHarga, string iKeterangan, string inamaXBarang, int inomorXPO, int iidVendor,
            string inomorSJ,string inomorReference,string inomorInvoice,  string ifaktuPajak,DateTime? itanggalInvoice, DateTime? itanggalJatuhTempo, DateTime? iTanggalPo, DateTime? iTanggalDelivery, DateTime? iTanggalPurchase)
        {
            var vBoRetur = false;
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (var vComd = new Npgsql.NpgsqlCommand("SELECT trpbl_insert(@vtrsid,@vnomso,@vnompr,@vnompo,@vnocoa,@vbrgnm,@vstnbr,@vcmpnm,@vqtybr,@vprprc,@vnombr,@vnosrv,@vcstll,@vdscnt,@vppncs,@vttlhg,@vpcktr,@vbrenm, @vnoepo, @vidven,@vnomsj, @vnoref, @vnoinv,@vnomfp, @vindte, @vjtdte, @vpodte, @vdedte, @vpcdte)", vConn))
                {
                    vComd.Parameters.AddWithValue("@vtrsid", iTransaksiId);
                    vComd.Parameters.AddWithValue("@vnomso", inomorSO);
                    vComd.Parameters.AddWithValue("@vnompr", inomorPR);
                    vComd.Parameters.AddWithValue("@vnocoa", inomorCOA);
                    vComd.Parameters.AddWithValue("@vnompo", inomorPO);
                    vComd.Parameters.AddWithValue("@vbrgnm", inamaBarang);
                    vComd.Parameters.AddWithValue("@vstnbr", iSatuan);
                    vComd.Parameters.AddWithValue("@vcmpnm", iCompany);
                    vComd.Parameters.AddWithValue("@vqtybr", iQuantity);
                    vComd.Parameters.AddWithValue("@vprprc", iHargaSatuan);
                    vComd.Parameters.AddWithValue("@vnombr", iNominalMaterial);
                    vComd.Parameters.AddWithValue("@vnosrv", iNominalService);
                    vComd.Parameters.AddWithValue("@vcstll", iBiayaLain);
                    vComd.Parameters.AddWithValue("@vdscnt", iDiscount);
                    vComd.Parameters.AddWithValue("@vppncs", iPPN);
                    vComd.Parameters.AddWithValue("@vttlhg", itotalHarga);
                    vComd.Parameters.AddWithValue("@vpcktr", iKeterangan);
                    vComd.Parameters.AddWithValue("@vbrenm", inamaXBarang);
                    vComd.Parameters.AddWithValue("@vnoepo", inomorXPO);
                    vComd.Parameters.AddWithValue("@vidven", iidVendor);
                    vComd.Parameters.AddWithValue("@vnomsj", inomorSJ);

                    vComd.Parameters.AddWithValue("@vnoref", inomorReference);
                    vComd.Parameters.AddWithValue("@vnoinv", inomorInvoice);
                    vComd.Parameters.AddWithValue("@vnomfp", ifaktuPajak);
                    vComd.Parameters.AddWithValue("@vindte", itanggalInvoice);
                    vComd.Parameters.AddWithValue("@vjtdte", itanggalJatuhTempo);


                    vComd.Parameters.AddWithValue("@vpodte", iTanggalPo);
                    vComd.Parameters.AddWithValue("@vdedte", iTanggalDelivery);
                    vComd.Parameters.AddWithValue("@vpcdte", iTanggalPurchase);
                    await vComd.PrepareAsync();
                    await vComd.ExecuteNonQueryAsync();
                    DID.AppMemory.DaftarBarangLoadedByCompany = false;
                    DID.AppMemory.DaftarTransaksiPembelianIsLoaded = false;
                    vBoRetur = true;
                }

            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine("Err InsertDb Transaksi Pembelian"); }
            return vBoRetur;
        }

        public static async Task<bool> InsertObject(DID.Entities.TransaksiPembelianInternal iTransaksi)
        {
            return await InsertDB
                (iTransaksi.transaksiId, iTransaksi.nomorSO, iTransaksi.nomorPO, iTransaksi.nomorPR, iTransaksi.nomorCOA, iTransaksi.namaBarang, iTransaksi.satuan, iTransaksi.company,iTransaksi.quantity, iTransaksi.hargaSatuan,
                iTransaksi.nominalMaterial, iTransaksi.nominalService, iTransaksi.biayaLain, iTransaksi.discount, iTransaksi.ppn, iTransaksi.totalHarga,iTransaksi.keterangan,iTransaksi.namaXBarang, iTransaksi.nomorXPO, iTransaksi.idVendor,
                iTransaksi.nomorSJ, iTransaksi.nomorReference, iTransaksi.nomorInvoice,iTransaksi.fakturPajak, iTransaksi.tanggalInvoice, iTransaksi.tanggalJatuhTempo, iTransaksi.tanggalPO, iTransaksi.tanggalDelivery, iTransaksi.tanggalPurchase);
        }
        public static async Task<bool> UpdateDb(string iTransaksiId, int inomorSO, int inomorPO, int inomorPR, int inomorCOA, string inamaBarang, string iSatuan, string iCompany, int iQuantity, decimal iHargaSatuan,
            decimal iNominalMaterial, decimal iNominalService, decimal iBiayaLain, decimal iDiscount, decimal iPPN, decimal itotalHarga, string iKeterangan, string inamaXBarang, int inomorXPO, int iidVendor,
            string inomorSJ, string inomorReference, string inomorInvoice, string ifaktuPajak, DateTime? itanggalInvoice, DateTime? itanggalJatuhTempo, DateTime? iTanggalPo, DateTime? iTanggalDelivery, DateTime? iTanggalPurchase)
        {
            var vBoRetur = false;
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (var vComd = new Npgsql.NpgsqlCommand("SELECT trpbl_update(@vtrsid,@vnomso,@vnompr,@vnompo,@vnocoa,@vbrgnm,@vstnbr,@vcmpnm,@vqtybr,@vprprc,@vnombr,@vnosrv,@vcstll,@vdscnt,@vppncs,@vttlhg,@vpcktr,@vbrenm, @vnoepo, @vidven,@vnomsj, @vnoref, @vnoinv,@vnomfp, @vindte, @vjtdte, @vpodte, @vdedte, @vpcdte)", vConn))
                {
                    vComd.Parameters.AddWithValue("@vtrsid", iTransaksiId);
                    vComd.Parameters.AddWithValue("@vnomso", inomorSO);
                    vComd.Parameters.AddWithValue("@vnompr", inomorPR);
                    vComd.Parameters.AddWithValue("@vnocoa", inomorCOA);
                    vComd.Parameters.AddWithValue("@vnompo", inomorPO);
                    vComd.Parameters.AddWithValue("@vbrgnm", inamaBarang);
                    vComd.Parameters.AddWithValue("@vstnbr", iSatuan);
                    vComd.Parameters.AddWithValue("@vcmpnm", iCompany);
                    vComd.Parameters.AddWithValue("@vqtybr", iQuantity);
                    vComd.Parameters.AddWithValue("@vprprc", iHargaSatuan);
                    vComd.Parameters.AddWithValue("@vnombr", iNominalMaterial);
                    vComd.Parameters.AddWithValue("@vnosrv", iNominalService);
                    vComd.Parameters.AddWithValue("@vcstll", iBiayaLain);
                    vComd.Parameters.AddWithValue("@vdscnt", iDiscount);
                    vComd.Parameters.AddWithValue("@vppncs", iPPN);
                    vComd.Parameters.AddWithValue("@vttlhg", itotalHarga);
                    vComd.Parameters.AddWithValue("@vpcktr", iKeterangan);
                    vComd.Parameters.AddWithValue("@vbrenm", inamaXBarang);
                    vComd.Parameters.AddWithValue("@vnoepo", inomorXPO);
                    vComd.Parameters.AddWithValue("@vidven", iidVendor);
                    vComd.Parameters.AddWithValue("@vnomsj", inomorSJ);

                    vComd.Parameters.AddWithValue("@vnoref", inomorReference);
                    vComd.Parameters.AddWithValue("@vnoinv", inomorInvoice);
                    vComd.Parameters.AddWithValue("@vnomfp", ifaktuPajak);
                    vComd.Parameters.AddWithValue("@vindte", itanggalInvoice);
                    vComd.Parameters.AddWithValue("@vjtdte", itanggalJatuhTempo);


                    vComd.Parameters.AddWithValue("@vpodte", iTanggalPo);
                    vComd.Parameters.AddWithValue("@vdedte", iTanggalDelivery);
                    vComd.Parameters.AddWithValue("@vpcdte", iTanggalPurchase);
                    await vComd.PrepareAsync();
                    await vComd.ExecuteNonQueryAsync();
                    DID.AppMemory.DaftarBarangLoadedByCompany = false;
                    DID.AppMemory.DaftarTransaksiPembelianIsLoaded = false;
                    vBoRetur = true;
                }

            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine("Err update Transaksi Pembelian"); }
            return vBoRetur;
        }

        public static async Task<bool> UpdatetObject(DID.Entities.TransaksiPembelianInternal iTransaksi)
        {
            return await UpdateDb
                 (iTransaksi.transaksiId, iTransaksi.nomorSO, iTransaksi.nomorPO, iTransaksi.nomorPR, iTransaksi.nomorCOA, iTransaksi.namaBarang, iTransaksi.satuan, iTransaksi.company, iTransaksi.quantity, iTransaksi.hargaSatuan,
                iTransaksi.nominalMaterial, iTransaksi.nominalService, iTransaksi.biayaLain, iTransaksi.discount, iTransaksi.ppn, iTransaksi.totalHarga, iTransaksi.keterangan, iTransaksi.namaXBarang, iTransaksi.nomorXPO, iTransaksi.idVendor,
                iTransaksi.nomorSJ, iTransaksi.nomorReference, iTransaksi.nomorInvoice, iTransaksi.fakturPajak, iTransaksi.tanggalInvoice, iTransaksi.tanggalJatuhTempo, iTransaksi.tanggalPO, iTransaksi.tanggalDelivery, iTransaksi.tanggalPurchase);
        }
        public static async Task<bool> DeleteDb(string iTransaksiId)
        {
            var vRetur = false;
            try
            {
                await using var vConn = new Npgsql.NpgsqlConnection(DID.Configuration.ConfigDbConnection);
                await vConn.OpenAsync();
                await using (var vComd = new Npgsql.NpgsqlCommand("SELECT trpbl_delete(@vtrsid)", vConn))
                {
                    vComd.Parameters.AddWithValue("@vtrsid", iTransaksiId);
                    await vComd.PrepareAsync();
                    await vComd.ExecuteNonQueryAsync();
                    DID.AppMemory.DaftarBarangLoadedByCompany = false;
                    DID.AppMemory.DaftarTransaksiPembelianIsLoaded = false;
                    vRetur = true;
                }
            }
            catch (Npgsql.NpgsqlException) { System.Diagnostics.Debug.WriteLine("Err Delete Transaksi Pembelian"); }
            return vRetur;
        }
        public static async Task<bool> DeleteObject(DID.Entities.TransaksiPembelianInternal iTransaksi)
        {
            return await DeleteDb(iTransaksi.transaksiId);
        }
        #endregion
        #region ReadData
        private static DID.Entities.TransaksiPembelianInternal ReadMaxKode (System.Data.IDataRecord iReader)
        {
            var vItem = new DID.Entities.TransaksiPembelianInternal
            {
                transaksiId = System.Convert.ToString(iReader["trsid"])

            };
            return vItem;
        }
        private static DID.Entities.TransaksiPembelianInternal ReadTransaksi(System.Data.IDataRecord iReader)
        {
            var vItem = new DID.Entities.TransaksiPembelianInternal
            {
                transaksiId = System.Convert.ToString(iReader["trsid"]),
                nomorSO = System.Convert.ToInt32(iReader["nomso"]),
                nomorPO = System.Convert.ToInt32(iReader["nompo"]),
                nomorPR = System.Convert.ToInt32(iReader["nompr"]),
                nomorCOA = System.Convert.ToInt32(iReader["nocoa"]),
                namaBarang = System.Convert.ToString(iReader["brgnm"]),
                satuan = System.Convert.ToString(iReader["stnbr"]),
                company = System.Convert.ToString(iReader["cmpnm"]),
                quantity = System.Convert.ToInt32(iReader["qtybr"]),
                hargaSatuan = System.Convert.ToDecimal(iReader["prprc"]),
                nominalMaterial = System.Convert.ToDecimal(iReader["nombr"]),
                nominalService = System.Convert.ToDecimal(iReader["nosrv"]),
                biayaLain = System.Convert.ToDecimal(iReader["cstll"]),
                discount = System.Convert.ToDecimal(iReader["dscnt"]),
                ppn = System.Convert.ToDecimal(iReader["ppncs"]),
                totalHarga = System.Convert.ToDecimal(iReader["ttlhg"]),
                tanggalPO = System.Convert.ToDateTime(iReader["podte"]),
                tanggalDelivery = System.Convert.ToDateTime(iReader["dedte"]),
                tanggalPurchase = System.Convert.ToDateTime(iReader["pcdte"]),
                keterangan = System.Convert.ToString(iReader["pcktr"]),

                nomorInvoice = System.Convert.ToString(iReader["noinv"]),
                idVendor = System.Convert.ToInt32(iReader["idven"]),
                nomorXPO = System.Convert.ToInt32(iReader["noepo"]),
                namaXBarang = System.Convert.ToString(iReader["brenm"]),
                nomorReference = System.Convert.ToString(iReader["noref"]),
                nomorSJ = System.Convert.ToString(iReader["nomsj"]),
                fakturPajak = System.Convert.ToString(iReader["nomfp"]),
                tanggalInvoice = System.Convert.ToDateTime(iReader["indte"]),
                tanggalJatuhTempo = System.Convert.ToDateTime(iReader["jtdte"])
            };

            return vItem;
        }

        #endregion

    }
}
