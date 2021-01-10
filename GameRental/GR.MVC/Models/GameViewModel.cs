using GR.Domains.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GR.MVC.Models
{
    public class GameViewModel
    {
        public Guid Id { get; set; }
        public Uri ImgLink { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [StringLength(30)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Select at least 1 Platform")]
        public List<Platform> Platforms { get; set; }
        [Required(ErrorMessage = "Select at least 1 Genre")]
        public List<Genre> Genres { get; set; }
    }
}
