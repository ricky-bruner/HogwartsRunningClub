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
        public double Distance { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public ApplicationUser User { get; set; }
    }
}