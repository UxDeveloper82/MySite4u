using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySite4u.Models
{
    public class Portfolio
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

        [Display(Name = "Please enter the Websitelink")]
        public string Link { get; set; }

        [Display(Name = "Please enter Category")]
        public string Category { get; set; }
        [Display(Name = "Please choose an image")]
        public string PortfolioPhoto { get; set; }
    }
}
