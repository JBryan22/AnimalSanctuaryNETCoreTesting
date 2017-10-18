using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnimalSanctuary.Models;
using AnimalSanctuary.Models.Repositories;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnimalSanctuary.Controllers
{
    public class AnimalsController : Controller
    {
        private IAnimalRepository animalRepo;

        public AnimalsController(IAnimalRepository thisRepo = null)
        {
            if (thisRepo == null)
            {
                this.animalRepo = new EFAnimalRepository();
            }
            else
            {
                this.animalRepo = thisRepo;
            }
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
           

            return View(animalRepo.Animals.Include(animals => animals.Veterinarian).ToList());
        }

        public IActionResult Details(int id)
        {
            var thisAnimal = animalRepo.Animals.FirstOrDefault(Animals => Animals.AnimalId == id);
            return View(thisAnimal);
        }

        public IActionResult Create()
        {
            ViewBag.VeterinarianId = new SelectList(animalRepo.Veterinarians, "VeterinarianId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Animal animal)
        {
            animalRepo.Save(animal);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisAnimal = animalRepo.Animals.FirstOrDefault(animals => animals.AnimalId == id);
            ViewBag.VeterinarianId = new SelectList(animalRepo.Veterinarians, "VeterinarianId", "Name");
            return View(thisAnimal);
        }

        [HttpPost]
        public IActionResult Edit(Animal animal)
        {
            animalRepo.Edit(animal);
            return RedirectToAction("Index");
        }

		public IActionResult Delete(int id)
		{
			var thisAnimal = animalRepo.Animals.FirstOrDefault(animals => animals.AnimalId == id);
			return View(thisAnimal);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
			var thisAnimal = animalRepo.Animals.FirstOrDefault(animals => animals.AnimalId == id);
			animalRepo.Remove(thisAnimal);
			return RedirectToAction("Index");
		}

		public IActionResult Done(int id)
		{
			var thisAnimal = animalRepo.Animals.FirstOrDefault(animals => animals.AnimalId == id);
			return View(thisAnimal);
		}

		[HttpPost]
		public IActionResult Done(Animal animal)
		{
			animalRepo.Edit(animal);
			return RedirectToAction("Index");
		}



    }
}
