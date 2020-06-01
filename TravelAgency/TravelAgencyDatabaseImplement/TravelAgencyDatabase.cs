using Microsoft.EntityFrameworkCore;
using TravelAgencyDatabaseImplement.Models;

namespace TravelAgencyDatabaseImplement
{
    public class TravelAgencyDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-2RG8HAM\SQLEXPRESS;Initial Catalog=TravelAgencyDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Tour> Tours { set; get; }
        public virtual DbSet<Travel> Travels { set; get; }
        public virtual DbSet<TravelTour> TravelTours { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
        public virtual DbSet<Payment> Payments { set; get; }
        public object Customers { get; internal set; }
    }
}
