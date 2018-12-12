using HogwartsRunningClub.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HogwartsRunningClub.Models
{
    public class House
    {

        [Key]
        public int HouseId { get; set; }

        [Required]
        [Display(Name = "House")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "House Description")]
        public string Description { get; set; }

        [Display(Name = "House Miles")]
        public double TotalMiles { get; set; }

        public virtual ICollection<ApplicationUser> HouseUsers { get; set; }


    }
}