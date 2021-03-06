﻿using System;
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
    public class AdminSongsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminSongs
        [HttpGet]
        public ActionResult Index(string type, string searchString)
        {
            var TypeLst = new List<string>();
            var GenerQry = from d in db.Songs
                           orderby d.sGenre
                           select d.sGenre;
            TypeLst.AddRange(GenerQry.Distinct());
            ViewBag.type = new SelectList(TypeLst);
            var songs = from m in db.Songs
                        select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                songs = songs.Where(s => s.sTitle.Contains(searchString)
                    );
            }
            if (!String.IsNullOrEmpty(type))
            {
                songs = songs.Where(x => x.sGenre == type);
            }
            return View(songs);
        }

        // GET: AdminSongs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // GET: AdminSongs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminSongs/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,sTitle,sGenre,sReleaseDate,sCountry,sphoto,sContent,sLink")] Song song, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                db.Songs.Add(song);
                db.SaveChanges();
                if (file != null && file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/uploads"),
                        song.id.ToString() + ".jpg");
                    file.SaveAs(path);
                }
                return RedirectToAction("Index");
            }

            return View(song);
        }

        // GET: AdminSongs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: AdminSongs/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,sTitle,sGenre,sReleaseDate,sCountry,sphoto,sContent,sLink")] Song song)
        {
            if (ModelState.IsValid)
            {
                db.Entry(song).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(song);
        }

        // GET: AdminSongs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: AdminSongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Song song = db.Songs.Find(id);
            db.Songs.Remove(song);
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
