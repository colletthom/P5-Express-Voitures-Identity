using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace P5_Express_Voitures_Identity.Models
{
    public class Reparation
    {
        public int Id { get; set; }

        public int IdVoiture { get; set; }

        [Display(Name = "Type Intervention")]
        public string? TypeIntervention { get; set; }

        [Display(Name = "Prix Intervention")]
        [RegularExpression(@"^[0-9]+(\,[0-9]{1,2})?$", ErrorMessage = "Le prix doit être un nombre")]
        public float PrixIntervention { get; set; }

        [NotMapped]
        public Voiture? Voiture { get; set; }
    }
}
