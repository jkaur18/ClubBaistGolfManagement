using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClubBaistGolfManagement.Data;
using ClubBaistGolfManagement.Domain;

namespace ClubBaistGolfManagement.Areas.Identity.Pages.Admin.Roles
{
    public class IndexModel : PageModel
    {
        private readonly ClubBaistGolfManagement.Data.ClubBaistManagementContext _context;

        public IndexModel(ClubBaistGolfManagement.Data.ClubBaistManagementContext context)
        {
            _context = context;
        }

        public IList<AspNetRoles> AspNetRoles { get;set; }

        public async Task OnGetAsync()
        {
            AspNetRoles = await _context.AspNetRoles.ToListAsync();
        }
    }
}
