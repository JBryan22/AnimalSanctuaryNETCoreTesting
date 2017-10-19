using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AnimalSanctuary.Models;
using AnimalSanctuary.Models.Repositories;
using AnimalSanctuary.Controllers;
using Moq;
using System.Linq;
using System;



namespace AnimalSanctuary.Tests.ControllerTests
{
    [TestClass]
    public class VeterinariansControllerTest : IDisposable
    {
        Mock<IVeterinarianRepository> mock = new Mock<IVeterinarianRepository>();
        EFVeterinarianRepository db = new EFVeterinarianRepository(new TestDbContext());

        private void DbSetup()
        {
            mock.Setup(mock => mock.Veterinarians).Returns(new Veterinarian[]
            {
                new Veterinarian {VeterinarianId = 1, Name="Jesse", Specialty="African" },
                new Veterinarian {VeterinarianId = 2, Name="Charlie", Specialty="Alligators" }
            }.AsQueryable());
        }

        [TestMethod]
        public void DB_CreateNewEntry_test()
        {
            VeterinariansController controller = new VeterinariansController(db);
            Veterinarian testVeterinarian = new Veterinarian();
            testVeterinarian.Name = "Jesse";
            testVeterinarian.Specialty = "African";

            controller.Create(testVeterinarian);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Veterinarian>;

            CollectionAssert.Contains(collection, testVeterinarian);
        }

        public void Dispose()
        {
            db.RemoveAll();
        }

    }
}