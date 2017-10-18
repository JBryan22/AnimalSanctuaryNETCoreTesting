using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimalSanctuary.Models;

namespace AnimalSanctuary.Tests
{
    [TestClass]
    public class AnimalTest
    {
        [TestMethod]
        public void GetPropertiesTest()
        {
            var animal = new Animal("Ellie", "Elephant", "Female", "Savanna", false, 1);

			var nameResult = animal.Name;
			var speciesResult = animal.Species;
			var sexResult = animal.Sex;
			var habitatResult = animal.HabitatType;
			var medicalResult = animal.MedicalEmergency;

			Assert.AreEqual("Ellie", nameResult);
			Assert.AreEqual("Elephant", speciesResult);
			Assert.AreEqual("Female", sexResult);
			Assert.AreEqual("Savanna", habitatResult);
			Assert.AreEqual(false, medicalResult);
        }

        [TestMethod]
        public void GetVetPropsTest()
        {
            var veterinarian = new Veterinarian("Jesse", "Elephants");

            var nameResult = veterinarian.Name;
            var specialityResult = veterinarian.Specialty;

            Assert.AreEqual("Jesse", nameResult);
            Assert.AreEqual("Elephants", specialityResult);
        }
    }
}
