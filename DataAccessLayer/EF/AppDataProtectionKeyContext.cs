using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EF
{
    public class AppDataProtectionKeyContext : DbContext, IDataProtectionKeyContext
    {

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public AppDataProtectionKeyContext(DbContextOptions<AppDataProtectionKeyContext> options)
            : base(options) {
               
        }
    }
}
