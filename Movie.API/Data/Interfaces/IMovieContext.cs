using Movie.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.API.Data.Interfaces
{
    public interface IMovieContext
    {
        IEnumerable<MovieModel> Movies { get; }
      //  IEnumerable<Franchise> Franchises { get;  }
    }
}
