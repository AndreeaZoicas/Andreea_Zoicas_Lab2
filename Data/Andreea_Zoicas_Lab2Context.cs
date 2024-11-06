using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Andreea_Zoicas_Lab2.Models;

namespace Andreea_Zoicas_Lab2.Data
{
    public class Andreea_Zoicas_Lab2Context : DbContext
    {
        public Andreea_Zoicas_Lab2Context (DbContextOptions<Andreea_Zoicas_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Andreea_Zoicas_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Andreea_Zoicas_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Andreea_Zoicas_Lab2.Models.Author> Author { get; set; } = default!;

    }
}
