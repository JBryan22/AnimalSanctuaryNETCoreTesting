using System;
namespace AnimalSanctuary.Models
{
    public class Animal
    {
        public int AnimalId
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Species
        {
            get;
            set;
        }
      public string Sex
        {
            get;
            set;
        } 
        public string HabitatType
        {
            get;
            set;
        }
        public bool MedicalEmergency
        {
            get;
            set;
        }
        public int VeterinarianId
        {
            get;
            set;
        }
        public virtual Veterinarian Veterinarian
        {
            get;
            set;
        }

		public override bool Equals(System.Object otherAnimal)
		{
			if (!(otherAnimal is Animal))
			{
				return false;
			}
			else
			{
				Animal newItem = (Animal)otherAnimal;
				return this.AnimalId.Equals(newItem.AnimalId);
			}
		}

		public override int GetHashCode()
		{
			return this.AnimalId.GetHashCode();
		}

        public Animal() {}

        public Animal(string name, string species, string sex, string habitat, bool medical, int vetId)
        {
            this.Name = name;
            this.Species = species;
            this.Sex = sex;
            this.HabitatType = habitat;
            this.MedicalEmergency = medical;
            this.VeterinarianId = vetId;
        }
    }
}
