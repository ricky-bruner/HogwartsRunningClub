using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HogwartsRunningClub.Models
{
    public class Race
    {
        
        [Key]
        public int RaceId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Charity")]
        public string CharityName { get; set; }

        [Required]
        [Display(Name = "Charity Information")]
        public string CharityDescription { get; set; }

        [Required]
        [Display(Name = "Distance")]
        public double Distance { get; set; }

        [Required]
        public string MedalImage { get; set; }

        public virtual ICollection<UserRace> RaceParticipants { get; set; }

    }
}