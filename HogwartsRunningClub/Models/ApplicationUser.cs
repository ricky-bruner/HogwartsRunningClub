using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HogwartsRunningClub.Models
{
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser() { }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Wizarding World Name")]
        public string PotterName { get; set; }

        public int HouseId { get; set; }

        public string ImagePath { get; set; }

        [Display(Name = "City")]
        public string Location { get; set; }

        [Display(Name = "Total Miles Run")]
        public double MilesRun { get; set; }

        public House House { get; set; }

        public virtual ICollection<Topic> UserTopics { get; set; }

        public virtual ICollection<Comment> UserComments { get; set; }

        public virtual ICollection<Workout> UserWorkouts { get; set; }

        public virtual ICollection<UserRace> UserRaces { get; set; }

    }
}
