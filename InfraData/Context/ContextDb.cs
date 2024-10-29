using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace InfraData.Context
{
    public class ContextDb : DbContext
    {
        public ContextDb() { }
        public ContextDb(DbContextOptions<ContextDb> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Equipment> Equipments { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.ApplyConfigurationsFromAssembly(typeof(ContextDb).Assembly);
        }
    }
}
