using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Homework1.Models
{
    public class Song
    {
        public int id { get; set; }
        [Display(Name = "歌名")]
        public string sTitle { get; set; }
        [Display(Name = "类型")]
        public string sGenre { get; set; }
        [Display(Name = "日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime sReleaseDate { get; set; }
        [Display(Name = "地区")]
        public string sCountry { get; set; }
        [Display(Name = "图片")]
        public string sphoto { get; set; }
        [Display(Name = "简介")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string sContent { get; set; }
        [Display(Name = "下载地址")]
        public string sLink { get; set; }
    }
}