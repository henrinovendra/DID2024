using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DID2024.Sources.Controllers.Core
{
    [Route("/api/core/cuser")]

    public class PenggunaAplikasiController : Controller
    {
        public async Task<DID.Entities.PenggunaAplikasi> GetPengguna(string iUserName)
        {
            return await DID.DataLayers.PenggunaAplikasi.GetDbItem(iUserName);
        }
    }
}
