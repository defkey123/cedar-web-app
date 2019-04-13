using System;
using Microsoft.EntityFrameworkCore;

namespace CedarWebApp.Models
{
    public class CedarContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public CedarContext(DbContextOptions<CedarContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        // public DbSet<Hobby> Hobbies { get; set; }
        // public DbSet<HobbyJoined> HobbiesJoined { get; set; }
    }
}