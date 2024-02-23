using System.Collections.Generic;
using System.Threading.Tasks;
using P5_Express_Voitures_Identity.Models;
using P5_Express_Voitures_Identity.ViewModels;
using P5_Express_Voitures_Identity.Data;

namespace P5_Express_Voitures_Identity.Models.Service
{
    public interface IReparationService
    {
        float SommeReparations(int idVoiture);
    }
}
