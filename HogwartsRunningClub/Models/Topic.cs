using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HogwartsRunningClub.Models
{
    public class Topic
    {
        
        [Key]
        public int TopicId { get; set; }

        [Required]
        [StringLength(65, ErrorMessage = "Please shorten the title to 65 characters")]
        [Display(Name = "Topic Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(1500)]
        [Display(Name = "Discussion")]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Required]
        [Display(Name = "Exclusive to House?")]
        public bool HouseExclusive { get; set; }

        [Required]
        public int TopicCategoryId { get; set; }

        public TopicCategory TopicCategory { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

    }
}