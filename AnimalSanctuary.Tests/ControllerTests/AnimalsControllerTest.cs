using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AnimalSanctuary.Models;
using AnimalSanctuary.Models.Repositories;
using AnimalSanctuary.Controllers;
using Moq;
using System.Linq;
using System;

//new Veterinarian {VeterinarianId = 1, Name="Jesse", Specialty="African" },
//new Veterinarian {VeterinarianId = 2, Name="Charlie", Specialty="Alligators" }

namespace AnimalSanctuary.Tests.ControllerTests 
{
    [TestClass]
    public class AnimalsControllerTest : IDisposable
	{
        Mock<IAnimalRepository> mock = new Mock<IAnimalRepository>();
		EFAnimalRepository db = new EFAnimalRepository(new TestDbContext());
		EFVeterinarianRepository db2 = new EFVeterinarianRepository(new TestDbContext());

		public void Dispose()
		{
			db.RemoveAll();
			db2.RemoveAll();
		}

        private void DbSetup()
        {
            mock.Setup(mock => mock.Animals).Returns(new Animal[]
            {
                new Animal {AnimalId = 1, Name = "Ellie", Species = "Elephant", Sex = "Female", HabitatType = "Savanna", MedicalEmergency = false, VeterinarianId = 1},
                new Animal {AnimalId = 2, Name = "Allie", Species = "Alligator", Sex = "Female", HabitatType = "Lagoon", MedicalEmergency = false, VeterinarianId = 2},
                new Animal {AnimalId = 3, Name = "George", Species = "Chimpanzee", Sex = "Male", HabitatType = "Jungle", MedicalEmergency = false, VeterinarianId = 1},
            }.AsQueryable());
        }

        [TestMethod]
        public void Mock_GetViewResultIndex_Test() //confirms route returns view
        {
            DbSetup();
            AnimalsController controller = new AnimalsController(mock.Object);

            var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void Mock_IndexListOfAnimals_Test()
        {
            DbSetup();
            ViewResult indexView = new AnimalsController(mock.Object).Index() as ViewResult;

            var result = indexView.ViewData.Model;

            Assert.IsInstanceOfType(result, typeof(List<Animal>));
        }

        [TestMethod]
        public void Mock_ConfirmEntry_Test()
        {
            DbSetup();
            AnimalsController controller = new AnimalsController(mock.Object);
            Animal testAnimal = new Animal();
            testAnimal.AnimalId = 1;
            testAnimal.Name = "Ellie";
            testAnimal.Species = "Elephant";
            testAnimal.Sex = "Female";
            testAnimal.HabitatType = "Savanna";
            testAnimal.MedicalEmergency = false;
            testAnimal.VeterinarianId = 1;

            ViewResult indexView = controller.Index() as ViewResult;
            var collection = indexView.ViewData.Model as List<Animal>;

            CollectionAssert.Contains(collection, testAnimal);
        }
        [TestMethod]
        public void DB_CreateNewEntry_test()
        {
			AnimalsController controller = new AnimalsController(db);
			VeterinariansController controller1 = new VeterinariansController(db2);
            Veterinarian testVet = new Veterinarian("Jesse", "Elephants");
            testVet.VeterinarianId = 1;
            Animal testAnimal = new Animal();
			testAnimal.AnimalId = 1;
			testAnimal.Name = "Ellie";
			testAnimal.Species = "Elephant";
			testAnimal.Sex = "Female";
			testAnimal.HabitatType = "Savanna";
			testAnimal.MedicalEmergency = false;
			testAnimal.VeterinarianId = 1;

            controller1.Create(testVet);

            controller.Create(testAnimal);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Animal>;



            CollectionAssert.Contains(collection, testAnimal);
        }


    }
}
