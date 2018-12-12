using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HogwartsRunningClub.Models
{
    public class TopicCategory
    {
        
        [Key]
        public int TopicCategoryId { get; set; }

        [Required]
        [Display(Name = "Topic Category")]
        public string Label { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }

    }
}