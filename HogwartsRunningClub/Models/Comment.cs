using System.ComponentModel.DataAnnotations;

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
        public string UserId { get; set; }

        [Required]
        public int TopicId { get; set; }

        public Topic Topic { get; set; }

        public ApplicationUser User { get; set; }

    }
}