using System.ComponentModel.DataAnnotations;

namespace capstone.web.api.DTOs
{
    public class NotesDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
