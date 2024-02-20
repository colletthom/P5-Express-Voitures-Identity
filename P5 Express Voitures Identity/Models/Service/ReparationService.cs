using P5_Express_Voitures_Identity.Models;
using P5_Express_Voitures_Identity.ViewModels;
using P5_Express_Voitures_Identity.Data;

namespace P5_Express_Voitures_Identity.Models.Service
{
    public class ReparationService
    {
        private readonly ApplicationDbContext _context;

        public ReparationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public float SommeReparations(int idVoiture)
        {
            float Somme = 0;

            List<ReparationavecVoiture> ListeReparationsVehicule = _context.Reparations
                .Where(r => r.IdVoiture == idVoiture)
                .Select(r => new ReparationavecVoiture { Reparation = r, Voiture = r.Voiture })
                .ToList();


            if (ListeReparationsVehicule != null)
            {
                foreach (var r in ListeReparationsVehicule)
                {
                    Somme += r.Reparation.PrixIntervention;
                }
            }
            return Somme;
        }
    }
}
