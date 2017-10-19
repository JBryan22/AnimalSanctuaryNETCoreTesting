using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalSanctuary.Models.Repositories
{
    public class EFAnimalRepository : IAnimalRepository
    {
        AnimalSanctuaryContext db = new AnimalSanctuaryContext();

        public EFAnimalRepository(AnimalSanctuaryContext connection = null)
        {
            if (connection == null)
            {
                this.db = new AnimalSanctuaryContext();
            }
            else
            {
                this.db = connection;
            }
        }

        public IQueryable<Animal> Animals
        { get { return db.Animals; } }

        public IQueryable<Veterinarian> Veterinarians
        { get { return db.Veterinarians; } }

        public Animal Save(Animal animal)
        {
            db.Animals.Add(animal);
            db.SaveChanges();
            return animal;
        }

        public Animal Edit(Animal animal)
        {
            db.Entry(animal).State = EntityState.Modified;
            db.SaveChanges();
            return animal;
        }

        public void Remove(Animal animal)
        {
            db.Animals.Remove(animal);
            db.SaveChanges();
        }

        public void RemoveAll()
        {
            db.Animals.RemoveRange(db.Animals.ToList());
            db.SaveChanges();
        }
    }
}
