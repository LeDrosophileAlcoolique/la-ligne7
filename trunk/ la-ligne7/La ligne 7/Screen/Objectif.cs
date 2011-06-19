using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ligne7
{
    class Objectif
    {
        public int NbrZombieTue { get; set; }
        public int TimeNiveau { get; set; }

        protected string objectif;

        public Objectif(string objectif)
        {
            NbrZombieTue = 0;
            TimeNiveau = 0;
            this.objectif = objectif;
        }

        public bool IsVictoire()
        {
            bool result = false;

            switch (objectif)
            { 
                case "Zombie":
                    result = NbrZombieTue <= 0;
                    break;
                case "Time":
                    result = TimeNiveau <= 0;
                    break;
            }

            return result;
        }

        public void Decrement(int value)
        {
            switch (objectif)
            {
                case "Zombie":
                    NbrZombieTue -= value;
                    break;
                case "Time":
                    TimeNiveau -= value;
                    break;
            }
        }
    }
}
