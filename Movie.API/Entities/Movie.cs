using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.API.Entities
{

    public class MovieModel
    {

        public int ID { get; set; }

        public string Title { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public double Rating { get; set; }

        public string Franchise { get; set; }
    }
}
