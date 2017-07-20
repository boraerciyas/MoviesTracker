using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesTracker.Models
{
    public class Ogrenci
    {
        public int ID { get; set; }
        public int OgrenciNo { get; set; }
        public string Ad { get; set; }
        public string SoyAd { get; set; }
        public string Sube { get; set; }
        public int Sinif { get; set; }
    }
}