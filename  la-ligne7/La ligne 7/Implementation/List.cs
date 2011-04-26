#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace ligne7
{
    class MyList<T>
    {
        public class Element
        {
            public T Value { get; set; }
            public Element Suivant { get; set; }
        }

        private Element list;
        public int Longueur { get; set; }

        public MyList()
        {
            list = null;
            Longueur = 0;
        }

        public void Add(T value)
        {
            Element nouveau = new Element();
            nouveau.Value = value;
            nouveau.Suivant = list;
            list = nouveau;
            Longueur++;
        }

        public void Delete(Element value)
        {
            Element parcours = list, prec = list;

            while (parcours != null && parcours != value)
            {
                prec = parcours;
                parcours = parcours.Suivant;
            }

            if (parcours != null && parcours == value)
            {
                if (parcours == list)
                {
                    list = parcours.Suivant;
                }
                else
                {
                    prec.Suivant = parcours.Suivant;
                }

                Longueur--;
            }
        }

        public Element First()
        {
            return list;
        }

        public IEnumerable<Element> Enum()
        {
            Element parcours = list;

            while (parcours != null)
            {
                yield return parcours;
                parcours = parcours.Suivant;
            }
        }

        public IEnumerable<T> EnumValue()
        {
            foreach (Element element in Enum())
            {
                yield return element.Value;
            }
        }
    }
}
