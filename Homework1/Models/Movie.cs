using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Homework1.Models
{
    public class Movie
    {
        public int id { get; set; }
        [Display(Name = "电影名")]
        public string mTitle { get; set; }
        [Display(Name = "类型")]
        public string mGenre { get; set; }
        [Display(Name = "上映日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime mReleaseDate { get; set; }
        [Display(Name = "地区")]
        public string mCountry { get; set; }
        [Display(Name = "海报")]
        public string photo { get; set; }
        [Display(Name = "简介")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string mContent { get; set; }
        [Display(Name = "下载地址")]
        public string mLink { get; set; }
    }
}