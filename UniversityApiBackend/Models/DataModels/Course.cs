using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public enum Level
    {
        Basic,
        Medium,
        Advanced,
        Expert
    }

    public class Course : BaseEntity 
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
        public Level Level { get; set; } = Level.Basic;
    }
}
