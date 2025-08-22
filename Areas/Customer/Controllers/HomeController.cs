using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCeima.DataAccess;
using MyCeima.Utility;
using MyCeima.ViewModel;

namespace MyCeima.Areas.Customer.Controllers
{
    [Area(SD.CustomerArea)]
    public class HomeController : Controller
    {
        ApplicaitonDBcontext _context = new();

       
        public IActionResult Index(Special? input , [FromQuery]int page = 1)
        {
          var movies = _context.Movies.Include(e => e.Cinema).Include(e => e.Category).AsQueryable();

            if (input.NameMV is not null)
            {
                movies = movies.Where(e => e.Name.Contains(input.NameMV));
                ViewBag.Name = input.NameMV;
            }
              
            if (input.CategoryId is not null)

                movies = movies.Where(e => e.CategoryId == input.CategoryId);

            if (input.CinemaId is not null)
                movies = movies.Where(e => e.CinemaId == input.CinemaId);

            if (input.MovieStatus is not null)
                movies = movies.Where(e => e.MovieStatus == input.MovieStatus);


            var mymoveis = Math.Ceiling(movies.Count() / 4.5);
           
            movies = movies.Skip((page - 1) * 4).Take(4);


            ViewBag.Move = mymoveis;
            ViewBag.Current = page;

            return View(movies.ToList());
        }



        public IActionResult Category()
        {
            var Category = _context.Categories;

            return View(Category.ToList());
        }


        public IActionResult Ceima()
        {
            var cima = _context.Cinemas;

            return View(cima.ToList());
        }

        public IActionResult Details([FromRoute] int id)
        {
            var mov = _context.Movies.Include(e => e.Category).Include(e => e.Cinema).Include(e => e.ActorMovie).FirstOrDefault(e => e.Id == id);

            var product = _context.ActorMovies.Include(e => e.Actor).Where(e => e.MoviesId == id).ToList();

            var justActor = new Actor();

            NewClass Indded = new()
            {
                myMov = mov,
                Act = product,
                actor = justActor

            };

            return View(Indded);
        }


        public IActionResult ActorDetails([FromRoute] int id)
        {
            var actor = _context.Actors.FirstOrDefault(e=>e.Id == id);

            var product = _context.ActorMovies.Include(e => e.Movie).Where(e => e.ActorId == id).ToList();

            var mov = new Movies();

            SecondClass Second = new() 
            {
                Actor = actor,
                Movies = product,
                movie = mov
            };

            return View(Second);
        }

        public IActionResult CategoryCollection([FromRoute] int id)
        {
            var movies = _context.Movies.Include(e => e.Cinema).Include(e => e.Category).AsQueryable();
            movies = movies.Where(e => e.CategoryId == id);

            var onemovie = _context.Categories.FirstOrDefault(e => e.Id == id);
            ViewBag.CatID = onemovie;

          


            return View(movies.ToList());
        }

        public IActionResult CenimaCollection([FromRoute] int id)
        {
            var movies = _context.Movies.Include(e => e.Cinema).Include(e => e.Category).AsQueryable();
            movies = movies.Where(e => e.CinemaId == id);

            var onemovie = _context.Cinemas.FirstOrDefault(e => e.Id == id);
            ViewBag.CatID = onemovie;


            return View(movies.ToList());
        }




    }


}
