using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework1.Models
{
    public class MediaViewModels
    {
        public List<Movie> MovieModel { get; set; }
        public List<Song>SongModel { get; set; }
         public MediaViewModels( List<Models.Movie> mList,List<Models.Song> sList)
        {
            this.MovieModel = mList;
            this.SongModel = sList;
        }
    }
}