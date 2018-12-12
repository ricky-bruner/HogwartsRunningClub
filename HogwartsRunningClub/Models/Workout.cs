using System;
using System.ComponentModel.DataAnnotations;

namespace HogwartsRunningClub.Models
{
    public class Workout
    {

        [Key]
        public int WorkoutId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Distance Traveled")]
        public double Distance { get; set; }

        [Required]
        [Display(Name = "When")]
        public DateTime Date { get; set; }

        [Required]
        public ApplicationUser User { get; set; }
    }
}