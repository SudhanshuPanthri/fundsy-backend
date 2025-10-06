using Fundsy_backend.Data;
using Fundsy_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Fundsy_backend.Services
{
    public class LookupService : ILookupService
    {
        private readonly AppDBContext _context;
        public LookupService(AppDBContext context)
        {
            _context = context;
        }
        public async Task<List<Role>> GetRolesAsync()
        {
            var data = await _context.Roles.ToListAsync();
            if (data == null)
            {
                return new List<Role>();
            }
            return data;
        }

        public async Task<List<Status>> GetStatusesAsync()
        {
            var data = await _context.Statuses.ToListAsync();
            if (data == null)
            {
                return new List<Status>();
            }
            return data;
        }
    }
}
