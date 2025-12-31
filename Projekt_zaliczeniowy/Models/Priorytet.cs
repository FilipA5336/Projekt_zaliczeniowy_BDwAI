using System.ComponentModel.DataAnnotations;

namespace Projekt_zaliczeniowy.Models
{
    public class Priorytet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nazwa { get; set; }

        // Relacja 1:N
        public virtual ICollection<Zgloszenie>? Zgloszenia { get; set; }
    }
}
