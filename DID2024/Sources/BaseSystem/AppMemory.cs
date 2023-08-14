using System;
using System.Collections.Generic;
namespace DID
{
    public class AppMemory
    {

        //MEMORY KODE APLIKASI 
        public static bool DaftarKodeAplikasiLoaded = false;
        public static List<DID.Entities.KodeAplikasi> DaftarKodeAplikasi = new List<Entities.KodeAplikasi>();
        public static List<DID.Entities.KodeAplikasi> DaftarKodeAplikasiType = new List<Entities.KodeAplikasi>();
        public static List<DID.Entities.KodeAplikasi> DaftarCompany = new List<Entities.KodeAplikasi>();
        public static List<DID.Entities.KodeAplikasi> DaftarSatuan = new List<Entities.KodeAplikasi>();


        public static bool DaftarBarangLoaded = false;
        public static List<DID.Entities.Barang> DaftarBarang = new List<Entities.Barang>();
        public static List<DID.Entities.Barang> DaftarJenisBarang = new List<Entities.Barang>();

        public static bool DaftarBarangLoadedByCompany = false;
        public static List<DID.Entities.Barang> DaftarBarangByCompany = new List<Entities.Barang>();
        public static List<DID.Entities.Barang> DaftarJenisBarangByCompany = new List<Entities.Barang>();



        public static bool DaftarVendorIsLoaded = false;
        public static List<DID.Entities.Vendor> DaftarVendor = new List<Entities.Vendor>();

        public static bool DaftarTransaksiPembelianIsLoaded = false;
        public static List<DID.Entities.TransaksiPembelianInternal>  DaftarListTransaksi= new List<Entities.TransaksiPembelianInternal>();

    }
}