using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnimalSanctuary.Models;
using AnimalSanctuary.Models.Repositories;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnimalSanctuary.Controllers
{
    public class VeterinariansController : Controller
    {
        private AnimalSanctuaryContext db = new AnimalSanctuaryContext();
        private IVeterinarianRepository veterinarianRepo;

        public VeterinariansController(IVeterinarianRepository thisRepo = null)
        {
            if (thisRepo == null)
            {
                this.veterinarianRepo = new EFVeterinarianRepository();
            }
            else
            {
                this.veterinarianRepo = thisRepo;
            }
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(veterinarianRepo.Veterinarians.ToList());
        }
        public IActionResult Details(int id)
        {
            var thisVeterinarian = veterinarianRepo.Veterinarians.FirstOrDefault(veterinarians => veterinarians.VeterinarianId == id);
            return View(thisVeterinarian);
        }
        public IActionResult Create()
        {
			return View();
		}

		[HttpPost]
		public IActionResult Create(Veterinarian veterinarian)
		{   
			veterinarianRepo.Save(veterinarian);
			return RedirectToAction("Index");
		}

		public IActionResult Edit(int id)
		{
			var thisVeterinarian = veterinarianRepo.Veterinarians.FirstOrDefault(veterinarians => veterinarians.VeterinarianId == id);
			return View(thisVeterinarian);
		}
		[HttpPost]
		public IActionResult Edit(Veterinarian veterinarian)
		{
            veterinarianRepo.Edit(veterinarian);
			return RedirectToAction("Index");
		}
		public IActionResult Delete(int id)
		{
			var thisVeterinarian = veterinarianRepo.Veterinarians.FirstOrDefault(veterinarians => veterinarians.VeterinarianId == id);
			return View(thisVeterinarian);
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
			var thisVeterinarian = veterinarianRepo.Veterinarians.First(veterinarians => veterinarians.VeterinarianId == id);
			veterinarianRepo.Remove(thisVeterinarian);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
    }
}
