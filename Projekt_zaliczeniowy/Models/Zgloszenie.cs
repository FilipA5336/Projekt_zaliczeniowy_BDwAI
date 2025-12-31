using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_zaliczeniowy.Models
{
    public class Zgloszenie
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Tytuł musi mieć od 5 do 100 znaków")]
        public string Tytul { get; set; }

        [Required(ErrorMessage = "Opis błędu jest wymagany")]
        public string Opis { get; set; }

        public DateTime DataUtworzenia { get; set; } = DateTime.Now;

        // Relacja do Kategorii (Klucz obcy)
        [Display(Name = "Kategoria")]
        public int KategoriaId { get; set; }
        [ForeignKey("KategoriaId")]
        public virtual Kategoria? Kategoria { get; set; }

        // Relacja do Priorytetu (Klucz obcy)
        [Display(Name = "Priorytet")]
        public int PriorytetId { get; set; }
        [ForeignKey("PriorytetId")]
        public virtual Priorytet? Priorytet { get; set; }

        // Autor zgłoszenia (powiązanie z Identity)
        public string? UzytkownikId { get; set; }

        // Relacja 1:N z komentarzami
        public virtual ICollection<Komentarz>? Komentarze { get; set; }
    }
}
