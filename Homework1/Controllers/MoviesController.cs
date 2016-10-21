using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Homework1.Models;
using System.IO;
using System.Web;

namespace Homework1.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Movies
        [HttpGet]
        public ActionResult Index(string type, string searchString)
        {
          
            var TypeLst = new List<string>();
            var GenerQry = from d in db.Movies
                           orderby d.mGenre
                           select d.mGenre;
            TypeLst.AddRange(GenerQry.Distinct());
            ViewBag.type = new SelectList(TypeLst);
            var movies = from m  in db.Movies 
                         select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.mTitle.Contains(searchString)
                    );
            }
            if (!String.IsNullOrEmpty(type))
            {
                //movies = movies.Where(x => x.mGenre == type);
                movies = movies.OrderBy(x => x.id);
            }
            return View(movies);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }


       


    }
}
