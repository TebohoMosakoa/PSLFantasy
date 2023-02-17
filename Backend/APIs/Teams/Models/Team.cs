using System.ComponentModel.DataAnnotations;

namespace Teams.Models
{
    public class Team
    {
        [Key]
        [Required]
        public Guid TeamId { get; set; }
        [Required(ErrorMessage = "Team name is required")]
        [StringLength(60, ErrorMessage = "Team name can't be longer than 60 characters")]
        public string TeamName { get; set; }
        [Required]
        public string TeamLogo { get; set; }
    }
}
