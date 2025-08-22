using Microsoft.EntityFrameworkCore;
using MyCeima.ViewModel;

namespace MyCeima.DataAccess
{
    public class ApplicaitonDBcontext : DbContext
    {

        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorMovie> ActorMovies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movies> Movies { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog = MyCeima ;Integrated Security=True; Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;");
        }





    }
}
