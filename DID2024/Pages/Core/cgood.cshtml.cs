using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DIDProject2023.Pages.Core
{
    public class cgoodModel : PageModel
    {

        public async Task<IActionResult> OnGet()
        {
            // Periksa apakah pengguna sudah terotentikasi
            if (!User.Identity.IsAuthenticated)
            {
                // Pengguna sudah terotentikasi, alihkan ke halaman yang sesuai
                return RedirectToPage("/auth/login"); // Ganti dengan halaman yang sesuai
            }

            // Jika pengguna belum terotentikasi, tampilkan halaman login
            return Page();
        }
    }
}
