using MoviesTracker.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MoviesTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult OgrenciyiGetir(int id)
        {
            List<Ogrenci> Ogrenciler = new List<Ogrenci>
            {
                new Ogrenci { ID = 1, Ad = "Gençay", SoyAd = "Yıldız", OgrenciNo = 1234, Sinif = 5, Sube = "A" },
                new Ogrenci { ID = 2, Ad = "Mustafa", SoyAd = "Candan", OgrenciNo = 2342, Sinif = 6, Sube = "B" },
                new Ogrenci { ID = 3, Ad = "Necati", SoyAd = "Şaşmas", OgrenciNo = 5345, Sinif = 7, Sube = "C" },
                new Ogrenci { ID = 4, Ad = "Ayşe", SoyAd = "Kündür", OgrenciNo = 5675, Sinif = 8, Sube = "D" },
                new Ogrenci { ID = 5, Ad = "Furkan", SoyAd = "Somun", OgrenciNo = 8974, Sinif = 9, Sube = "E" }
            };

            Ogrenci ogrenci = Ogrenciler.FirstOrDefault(o => o.ID == id);
            return Json(ogrenci, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult TumOgrencileriGetir()
        {
            List<Ogrenci> Ogrenciler = new List<Ogrenci>
            {
                new Ogrenci { ID = 1, Ad = "Gençay", SoyAd = "Yıldız", OgrenciNo = 1234, Sinif = 5, Sube = "A" },
                new Ogrenci { ID = 2, Ad = "Mustafa", SoyAd = "Candan", OgrenciNo = 2342, Sinif = 6, Sube = "B" },
                new Ogrenci { ID = 3, Ad = "Necati", SoyAd = "Şaşmas", OgrenciNo = 5345, Sinif = 7, Sube = "C" },
                new Ogrenci { ID = 4, Ad = "Ayşe", SoyAd = "Kündür", OgrenciNo = 5675, Sinif = 8, Sube = "D" },
                new Ogrenci { ID = 5, Ad = "Furkan", SoyAd = "Somun", OgrenciNo = 8974, Sinif = 9, Sube = "E" }
            };
            return Json(Ogrenciler, JsonRequestBehavior.AllowGet);
        }
    }
}