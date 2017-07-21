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
                case DatabaseColumn.Title:
                    order = DatabaseColumn.Title;
                    break;
                case DatabaseColumn.Rate:
                    order = DatabaseColumn.Rate;
                    break;
                case DatabaseColumn.ReleaseTime:
                    order = DatabaseColumn.ReleaseTime;
                    break;
                case DatabaseColumn.Genre:
                    order = DatabaseColumn.Genre;
                    break;
                case DatabaseColumn.Status:
                    order = DatabaseColumn.Status;
                    break;
                default:
                    order = DatabaseColumn.Title;
                    break;
            }
            switch (orderDirection)
            {
                case OrderDirection.Ascending:
                    orderDirection = OrderDirection.Ascending;
                    break;
                case OrderDirection.Descending:
                    orderDirection = OrderDirection.Descending;
                    break;
                default:
                    orderDirection = OrderDirection.Ascending;
                    break;
            }

            ViewBag.Order = order;
            ViewBag.OrderDirection = orderDirection;

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
         
        [HttpPost] 
        public ActionResult DeleteFilm(int movieId)
        { 
            bool deleteResult = Service.Database.DeleteFilm(movieId);

            return Json(new
            {
                Result = deleteResult   //    if (data.Result) {   data.Result => burdaki Result demek 
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult StatusUpdate(int movieId, bool statusChecked)
        {
            int checkeda;
            if(statusChecked) {checkeda = 1; }
            else checkeda= 0;
            bool updateStatus = Service.Database.UpdateStatus(movieId, checkeda);
            return Json(new {
                Result = updateStatus
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RateUpdate(int movieId, decimal newRate)
        {
            bool updateRate = Service.Database.UpdateRate(movieId, newRate);
            return Json(new { Result = updateRate }, JsonRequestBehavior.AllowGet);
        }
    }
}
