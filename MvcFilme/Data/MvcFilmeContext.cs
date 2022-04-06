using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcFilme.Models;

namespace MvcFilme.Data
{
    public class MvcFilmeContext : DbContext
    {
        public MvcFilmeContext (DbContextOptions<MvcFilmeContext> options)
            : base(options)
        {
        }

        public DbSet<MvcFilme.Models.Filme> Filme { get; set; }
    }
}
