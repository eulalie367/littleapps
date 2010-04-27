using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculators.Fertilizer
{
    class Program
    {
        static void Main(string[] args)
        {
            viewMolecularMass.mmElement mm = new viewMolecularMass("H2O").Elements.Where(e => e.Symbol == "H").FirstOrDefault();
            string a = "";
        }
    }
    public class viewMolecularMass
    {
        string connectionString = @"Integrated Security=true;Initial Catalog=Fertilizer;Server=DELLLAPTOP";
        public viewMolecularMass()
        {
            this.Symbol = "";
            this.Elements = new List<mmElement>();
        }
        public viewMolecularMass(string symbol) : this()
        {
            this.Symbol = symbol;
            using (dcFertilizer dc = new dcFertilizer(connectionString))
            {
                //dc.InstallDataBase();

                Molecule mol = dc.GetMolecule(this.Symbol);
                foreach (ElementMolecule e in mol.ElementMolecules)
                {
                    this.Elements.Add(new mmElement 
                    {
                        Quantity = e.Quantity ?? 0,
                        MolecularMass = e.MolecularMass ?? 0,
                        Symbol = e.Element.Symbol ?? "",
                        PercentWeight = mol.MolecularPercentage(e.Element.Symbol)
                    });
                }
            }
        }
        public string Symbol {get;set;}
        public List<mmElement> Elements { get; set; }
        public class mmElement
        {
            public string Symbol{get;set;}
            public int Quantity{get;set;}
            public double PercentWeight{get;set;}
            public double MolecularMass{get;set;}
        }
    }
}
