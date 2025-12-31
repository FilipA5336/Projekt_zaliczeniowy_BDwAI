using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_zaliczeniowy.Models
{
    public class Komentarz
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Tresc { get; set; }
        public DateTime DataDodania { get; set; } = DateTime.Now;

        // Relacja do Zgłoszenia
        public int ZgloszenieId { get; set; }
        [ForeignKey("ZgloszenieId")]
        public virtual Zgloszenie? Zgloszenie { get; set; }

        public string? Autor { get; set; }
    }
}
