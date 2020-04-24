using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace qnomyex1.Data
{
    public class qnomyex1Context : DbContext
    {
        public qnomyex1Context (DbContextOptions<qnomyex1Context> options)
            : base(options)
        {
        }

        public DbSet<qnomyex1.Data.Appointment> Appointment { get; set; }
    }
}
