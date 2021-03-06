using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySite4u.ViewModels
{
    public class PortfolioViewModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Project Name")]
        public string Name { get; set; }
        [Display(Name = "Please enter type")]
        public string Type { get; set; }
        [Display(Name = "Please enter Details")]
        public string Details { get; set; }
        [Display(Name = "Please enter language")]
        public string Language { get; set; }
        [Display(Name = "Please enter the Code link")]
        public string CodeLink { get; set; }
        [Display(Name = "Please enter the link")]
        public string Link { get; set; }
        public string Category { get; set; } = "";
        public string CurrentImage { get; set; }
        [Display(Name = "Please choose an image")]
        public IFormFile PortfolioPhoto { get; set; }
    }
}
