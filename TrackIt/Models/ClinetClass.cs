using System.ComponentModel.DataAnnotations;

namespace TrackIt.Models
{
    public class ClinetClass
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
