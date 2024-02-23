using System.ComponentModel;
using Xunit;
using Moq;
using P5_Express_Voitures_Identity.Models.Service;
using Microsoft.EntityFrameworkCore;
using P5_Express_Voitures_Identity.Data;
using P5_Express_Voitures_Identity.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using P5_Express_Voitures_Identity.Models;


namespace P5_Express_Voitures_Identity.Tests
{
    
    public class ReparationVMTests
    {
        [Fact]
        [Description("si la somme des réparations est de 1000€ il doit retourner 1000€")]
        public void CalculTotalReparationTest()
        {
            //Arrange
            var dbContextOptions2 = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb2")
                .Options;
            var dbContext = new ApplicationDbContext(dbContextOptions2);

            dbContext.Reparations.AddRange(
                new Reparation() { IdVoiture = 1, TypeIntervention = null, PrixIntervention = 500 },
                new Reparation() { IdVoiture = 1, TypeIntervention = null, PrixIntervention = 500 }
            );
            dbContext.SaveChanges();
            var reparationVM = new ReparationsVM(1, null, dbContext);

            //Act
            float TotalReparation = reparationVM.CalculTotalReparation();

            //Assert
            Assert.Equal(1000, TotalReparation);

            var reparations = dbContext.Reparations.Where(r => r.IdVoiture == 1).ToList();
            dbContext.Reparations.RemoveRange(reparations);
            dbContext.SaveChanges();
        }
    }
}