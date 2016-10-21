using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Homework1.Models;
using System.IO;

namespace Homework1.Controllers
{
    [Authorize]
    public class AdminMoviesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminMovies
        [HttpGet]
        public ActionResult Index(string type, string searchString)
        {
            var TypeLst = new List<string>();
            var GenerQry = from d in db.Movies
                           orderby d.mGenre
                           select d.mGenre;
            TypeLst.AddRange(GenerQry.Distinct());
            ViewBag.type = new SelectList(TypeLst);
            var movies = from m in db.Movies
                         select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.mTitle.Contains(searchString)
                    );
            }
            if (!String.IsNullOrEmpty(type))
            {
                movies = movies.Where(x => x.mGenre == type);
            }
            return View(movies);
        }

        // GET: AdminMovies/Details/5
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

        // GET: AdminMovies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminMovies/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,mTitle,mGenre,mReleaseDate,mCountry,photo,mContent,mLink")] Movie movie, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                if (file != null && file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/uploads"),
                        movie.id.ToString() + ".jpg");
                    file.SaveAs(path);
                }
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: AdminMovies/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: AdminMovies/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,mTitle,mGenre,mReleaseDate,mCountry,photo,mContent,mLink")] Movie movie, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                if (file != null && file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/uploads"),
                        movie.id.ToString() + ".jpg");
                    file.SaveAs(path);
                }
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: AdminMovies/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: AdminMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
