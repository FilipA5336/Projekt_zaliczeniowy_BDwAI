using System.ComponentModel.DataAnnotations;

namespace Projekt_zaliczeniowy.Models
{
    public class Kategoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa kategorii jest wymagana")]
        [StringLength(50)]
        public string Nazwa { get; set; }

        // Relacja 1:N
        public virtual ICollection<Zgloszenie>? Zgloszenia { get; set; }
    }
}
