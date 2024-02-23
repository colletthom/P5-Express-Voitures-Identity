using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.Remoting.Contexts;
using P5_Express_Voitures_Identity.Models.Service;

namespace P5_Express_Voitures_Identity.Tests
{
    [TestClass]
    public class ReparationVMTests
    {
        private readonly IReparationService _reparationService;

        [TestMethod]
        [Description("si la somme des réparations est de 1000€ il doit retourner 1000e")]
        public void CalculTotalReparation()
        {
            //Arrange
            float SommeReparation = 1000;
            };

        //Act
        float TotalReparation = _reparationService.SommeReparation;
        }

        //Assert
        if (TotalReparation != 1000)
            Assert.Fail("Echec du test");
    }
}
