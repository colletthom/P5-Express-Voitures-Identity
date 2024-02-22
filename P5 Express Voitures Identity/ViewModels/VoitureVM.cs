using P5_Express_Voitures_Identity.Data;
using P5_Express_Voitures_Identity.Models;
using P5_Express_Voitures_Identity.Models.Service;
using static P5_Express_Voitures_Identity.Models.Voiture;

namespace P5_Express_Voitures_Identity.ViewModels
{
    public class VoitureVM
    {
        public ApplicationDbContext _context;

        public Voiture Voiture { get; set; }

        public float TotalReparation;

        public string StatutVoiture;

        public VoitureVM(Voiture Voiture, ApplicationDbContext context)
        {
            this.Voiture = Voiture;
            this._context = context;
        }

        public float CalculTotalReparation()
        {
            ReparationService reparationService = new ReparationService(_context);
            return TotalReparation = reparationService.SommeReparations(this.Voiture.Id);
        }

        public float CalculPrixVente()
        {
            return this.Voiture.PrixAchat + CalculTotalReparation() + this.MargeParVoiture();
        }

        public int MargeParVoiture()
        {
            return _context.Marges.OrderByDescending(m => m.Id).FirstOrDefault().Value;
        }

        public string CalculStatutVoiture()
        {
            if (Voiture.DateDisponibiliteALaVente != null)
            {
                if (Voiture.DateVente == null)
                {
                    return StatutVoiture = "Disponible";
                }
                else
                {
                    return StatutVoiture = "Vendue";
                }
            }
            return StatutVoiture = "en préparation, bientôt à la vente";
        }
    }
}
