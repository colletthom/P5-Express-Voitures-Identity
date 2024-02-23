using Microsoft.EntityFrameworkCore;
using P5_Express_Voitures_Identity.Data;
using P5_Express_Voitures_Identity.Models;
using P5_Express_Voitures_Identity.Models.Service;
using P5_Express_Voitures_Identity.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5_Express_Voitures_Identity_Test
{
    public class ReparationServiceTests
    {
        private readonly IReparationService _reparationService;

        public ReparationServiceTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            var dbContext = new ApplicationDbContext(dbContextOptions);

            this._reparationService = new ReparationService(dbContext);
        }

        [Fact]
        [Description("on créé deux réparations de 500€ et on vérifie qu'il calcule 1000€ de réparations sur la voiture ayant l'ID 1")]
        public void SommeReparationTest()
        {
            //Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            var dbContext = new ApplicationDbContext(dbContextOptions);

            var reparations = dbContext.Reparations.Where(r => r.IdVoiture == 1).ToList();
            dbContext.Reparations.RemoveRange(reparations);
            dbContext.SaveChanges();

            var reparation1 = new Reparation();
            reparation1.IdVoiture = 1;
            reparation1.PrixIntervention = 500;

            var reparation2 = new Reparation();
            reparation2.IdVoiture = 1;
            reparation2.PrixIntervention = 500;

            dbContext.Reparations.AddRange(reparation1,reparation2);
            dbContext.SaveChanges();


            //Act
            float Somme = _reparationService.SommeReparations(1);

            //Assert
            Assert.Equal(1000, Somme);

            reparations = dbContext.Reparations.Where(r => r.IdVoiture == 1).ToList();
            dbContext.Reparations.RemoveRange(reparations);
            dbContext.SaveChanges();
        }
    }
}
