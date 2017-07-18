using System.Linq;
using System.Net;
using System.Web.Mvc;
using MoviesTracker.Models;
using System.Collections.Generic;
using System;

namespace MoviesTracker.Controllers
{
    public class FilmsController : Controller
    {
        // GET: Films
        public ActionResult Index(string searchString, string genre, string order, string orderDirection)
        {
            switch (order)
            {
                case "Title":
                    order = "Title";
                    break;
                case "Rate":
                    order = "Rate";
                    break;
                case "ReleaseTime":
                    order = "ReleaseTime";
                    break;
                case "Genre":
                    order = "Genre";
                    break;
                default:
                    order = "Title";
                    break;
            }

            var movies = Service.Database.GetFilms(order, orderDirection);
            
            var genreListUnsplitted = new List<string>();

            var genreQry = from n in movies
                           orderby n.Genre
                           select n.Genre;

            var GenreList = new List<string>();

            genreListUnsplitted.AddRange(genreQry.Distinct());


            for (int i = 0; i < genreListUnsplitted.Count; i++)
            {
                foreach(string key in genreListUnsplitted[i].Split(','))
                { 
                        GenreList.Add(key.Trim());
                } 
            } 

            ViewBag.genre = new SelectList(GenreList.Distinct().OrderBy(x => x)); 

            if (!string.IsNullOrEmpty(genre))
            {
                movies = movies.Where(x => x.Genre.Contains(genre));
            }
            
            if(!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            return View(movies);
        }

        // GET: Films/Details/5
        public ActionResult Details(int id)
        {
            Film film = Service.Database.GetFilms(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // GET: Films/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Film film)
        {
            Service.Database.SetFilms(film);
            if (ModelState.IsValid)
            {
                
                return RedirectToAction("Index");
            }

            return View(film);
        }

        // GET: Films/Edit/5
        public ActionResult Edit(int id)
        {
            Film film = Service.Database.GetFilms(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: Films/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Film film)
        {
            if (ModelState.IsValid) Service.Database.UpdateFilm(film);
            
            return View(film);
        }

        // GET: Films/Delete/5
        public ActionResult Delete(int id)
        {
            Film film = Service.Database.GetFilms(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Film film = Service.Database.GetFilms(id);
            Service.Database.DeleteFilm(id);
            return RedirectToAction("Index");
        }
    }
}
