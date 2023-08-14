using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DID.Page.Core
{
    [Authorize]
    public class codeModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
