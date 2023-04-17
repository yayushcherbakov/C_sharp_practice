using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse2.Entities
{
    public class Classifier
    {
        public string Name = "Default";
        public List<Classifier> InnerClassifiers;
        public List<Product> Products;
        public int SortCode;
        public Classifier()
        {
            InnerClassifiers = new List<Classifier>();
            Products = new List<Product>();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
