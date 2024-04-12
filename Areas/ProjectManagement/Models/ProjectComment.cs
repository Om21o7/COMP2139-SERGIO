using System.ComponentModel.DataAnnotations;

namespace COMP2139_Labs.Areas.ProjectManagement.Models
{
    public class ProjectComment
    {
        [Key]
        public int ProjectCommentId { get; set; }

        [Required]
        [Display(Name = "Comment")]
        [StringLength(500, ErrorMessage = "Comment length cannot exceed 500 characters!")]
        public string Content { get; set; }

        [Display(Name = "Date Posted")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DatePosted { get; set; }

        public int ProjectId { get; set; }
        public Project? Project { get; set; }


    }
}
