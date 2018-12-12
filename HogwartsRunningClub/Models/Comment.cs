using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HogwartsRunningClub.Models
{
    public class Comment
    {

        [Key]
        public int CommentId { get; set; }

        [Required]
        [Display(Name = "Comment")]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int TopicId { get; set; }

        public Topic Topic { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

    }
}