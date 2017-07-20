using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MoviesTracker.Models
{
    public class Film
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 2)]
        public string Title
        {
            get; set;
        }


        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseTime
        {
            get; set;
        }

        //[RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [Required]
        [StringLength(40)]
        public string Genre
        {
            get; set;
        }
        [DisplayFormat(DataFormatString ="{0:0.0}", ApplyFormatInEditMode =true)]
        [Range(0.0, 9.9, ErrorMessage = "Rate values should be 0 to 10.")]
        public decimal Rate { get; set; }
        public Boolean Status{ get; set; }
    }

    //public class MoviesDBContext : DbContext
    //{
    //    public DbSet<Film> Films { get; set; }
    //}
}