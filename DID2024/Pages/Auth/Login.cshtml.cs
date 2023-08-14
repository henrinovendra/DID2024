using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace DID.Page
{
    public class LoginModel : PageModel
    {

        public async Task<IActionResult> OnGet()
        {
            // Periksa apakah pengguna sudah terotentikasi
            if (User.Identity.IsAuthenticated)
            {
                // Pengguna sudah terotentikasi, alihkan ke halaman yang sesuai
                return RedirectToPage("/core/cgood"); // Ganti dengan halaman yang sesuai
            }

            // Jika pengguna belum terotentikasi, tampilkan halaman login
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var userName = Request.Form["Username"];
            var password = Request.Form["Password"];
            var company = Request.Form["company"];
            if(string.IsNullOrEmpty(userName))
            {
                return Page();
            }
            else
            {
                
                var user = await DID.DataLayers.PenggunaAplikasi.GetDbItem(userName);
                if (user != null && user.Password == password && user.company == company)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.Role),

                        new Claim(CustomClaim.CustomClaimTypes.company, user.company)

                    };

                    Console.WriteLine(user.Modules);
                    var claimCompany = User.FindFirstValue(CustomClaim.CustomClaimTypes.company);

                    var claimsIdentity = new ClaimsIdentity(claims, "AuthSession");
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                    };
                    Console.WriteLine(user.company);
                    await HttpContext.SignInAsync(
                        "Cookies",
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties
                    );

                    DID.AppMemory.DaftarBarangLoadedByCompany = false;
                    await DID.DataLayers.Barang.ListIntoMemoryByCompany(user.company);


                    return RedirectToPage("/core/cgood"); // Redirect ke halaman setelah login berhasil
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                    return Page();
                }
            }
        }

    }
}

