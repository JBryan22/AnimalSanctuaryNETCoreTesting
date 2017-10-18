using System;
namespace AnimalSanctuary.Models
{
    public class Veterinarian
    {

        public int VeterinarianId
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Specialty
        {
            get;
            set;
        }

		public override bool Equals(System.Object otherVeterinarian)
		{
			if (!(otherVeterinarian is Veterinarian))
			{
				return false;
			}
			else
			{
				Veterinarian newItem = (Veterinarian)otherVeterinarian;
				return this.VeterinarianId.Equals(newItem.VeterinarianId);
			}
		}

		public override int GetHashCode()
		{
			return this.VeterinarianId.GetHashCode();
		}

        public Veterinarian() {}

        public Veterinarian(string name, string specialty)
        {
            this.Name = name;
            this.Specialty = specialty;
        }
    }
}
