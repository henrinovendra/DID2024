using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace newdid.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {


            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            HttpContext.Response.Cookies.Delete("AspNetCore" + CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect atau tampilkan halaman setelah logout
            return RedirectToPage("/Auth/Login");
        }
    }
}
