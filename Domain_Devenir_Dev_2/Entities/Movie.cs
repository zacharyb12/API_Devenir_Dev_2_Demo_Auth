using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Devenir_Dev_2.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string Genre { get; set; }

        public int ReleaseDate{ get; set; }

        public string Director { get; set; }

        public double AverageRating { get; set; }

        public string ImageUrl { get; set; }
    }
}
