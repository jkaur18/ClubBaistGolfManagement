using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClubBaistGolfManagement.Data;
using ClubBaistGolfManagement.Domain;

namespace ClubBaistGolfManagement.Areas.Identity.Pages.Admin.UserRoles
{
    public class IndexModel : PageModel
    {
        private readonly ClubBaistGolfManagement.Data.ClubBaistManagementContext _context;

        public IndexModel(ClubBaistGolfManagement.Data.ClubBaistManagementContext context)
        {
            _context = context;
        }

        public IList<AspNetUserRoles> AspNetUserRoles { get;set; }

        public async Task OnGetAsync()
        {
            AspNetUserRoles = await _context.AspNetUserRoles
                .Include(a => a.Role)
                .Include(a => a.User).ToListAsync();
        }
    }
}
