using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;

namespace Calculators.Fertilizer
{
    public static class dcFertilzerExtension
    {
        public static Molecule GetMolecule(this dcFertilizer dc, string moleculeSymbol)
        {
            IEnumerable<Molecule> c = dc.Molecules.Where(co => co.Symbol == moleculeSymbol);

            Molecule mol;

            if (c.Count() < 1)
            {
                //insert
                List<ElementMolecule> elements = dc.GetElementMoleculeBySymbol(moleculeSymbol);
                mol = new Molecule
                {
                    Symbol = moleculeSymbol
                };
                mol.ElementMolecules.AddRange(elements);

                //FigureAtomicWeight(mol);
                mol.AtomicMass = 0;
                foreach (ElementMolecule ec in mol.ElementMolecules)
                {
                    mol.AtomicMass += ec.MolecularMass;
                }

                dc.Molecules.InsertOnSubmit(mol);
                dc.SubmitChanges();
            }
            else
            {
                mol = c.FirstOrDefault();
            }

            return mol;
        }
        public static List<ElementMolecule> GetElementMoleculeBySymbol(this dcFertilizer dc, string moleculeSymbol)
        {
            List<ElementMolecule> retVal = new List<ElementMolecule>();
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("[A-Z]");
            System.Text.RegularExpressions.MatchCollection mc = r.Matches(moleculeSymbol);
            for (int i = 0; i < mc.Count; i++)
            {
                System.Text.RegularExpressions.Match cElement = mc[i];
                int startIndex = cElement.Index;
                int stopIndex;
                if (mc.Count > i + 1)
                    stopIndex = mc[i + 1].Index;
                else
                    stopIndex = moleculeSymbol.Length;

                System.Text.RegularExpressions.Regex q = new System.Text.RegularExpressions.Regex("[0-9]");

                string elem = moleculeSymbol.Substring(startIndex, stopIndex - startIndex);
                System.Text.RegularExpressions.MatchCollection mcQ = q.Matches(elem);
                ElementMolecule ec = new ElementMolecule();
                string symbol;
                if (mcQ.Count > 0)
                {
                    ec.Quantity = int.Parse(elem.Substring(mcQ[0].Index));
                    symbol = elem.Substring(0, mcQ[0].Index);
                }
                else
                {
                    symbol = elem;
                    ec.Quantity = 1;
                }

                IEnumerable<Element> dbElem = dc.Elements.Where(e => e.Symbol == symbol);
                if (dbElem.Count() > 0)
                    ec.Element = dbElem.First();

                ec.FigureMolecularMass();

                retVal.Add(ec);
            }
            return retVal;
        }
        public static void FigureMolecularMass(this ElementMolecule ec)
        {
            ec.MolecularMass = ec.Element.AtomicMass * ec.Quantity;
        }
        public static void FigureAtomicWeight(this  Molecule mol)
        {
            mol.AtomicMass = 0;
            foreach (ElementMolecule ec in mol.ElementMolecules)
            {
                mol.AtomicMass += ec.MolecularMass;
            }
        }
        public static Molecule InsertMolecule(this dcFertilizer dc, string moleculeSymbol, IEnumerable<Molecule> c)
        {
            Molecule mol = new Molecule
            {
                Symbol = moleculeSymbol
            };
            mol.ElementMolecules.AddRange(dc.GetElementMoleculeBySymbol(moleculeSymbol));
            mol.FigureAtomicWeight();
            
            //TODO:Verify that the Molecule is valid(this takes a bit of chemistry to do.
            dc.Molecules.InsertOnSubmit(mol);

            dc.SubmitChanges();
            return mol;
        }
        public static double MolecularPercentage(this Molecule m, string elementSymbol)
        {
            IEnumerable<ElementMolecule> ec = m.ElementMolecules.Where(e => e.Element.Symbol == elementSymbol);
            if(ec.Count() > 0)
            {
                return (ec.First().MolecularMass.Value / m.AtomicMass.Value) * 100;
            }
            else
            {
                return 0;
            }
        }

        public static void InstallDataBase(this dcFertilizer dc)
        {
            if (!dc.DatabaseExists())
                dc.CreateDatabase();

            if (dc.Elements.FirstOrDefault() == null)
                ImportData(dc);
        }
        private static void ImportData(dcFertilizer dc)
        {
            LINQtoCSV.CsvContext con = new LINQtoCSV.CsvContext();
            List<csvElemet> csvElems = con.Read<csvElemet>(@"C:\LittleApps\Fertilizer\Fertilizer\Data\Elements.csv").ToList();
            List<Element> elements = csvElems.Select(e => new Element
            {
                AtomicMass = e.Atomic_Weight,
                AtomicNumber = e.AtomicNumber,
                Symbol = e.Symbol
            }).ToList();
            dc.Elements.InsertAllOnSubmit(elements);
            dc.SubmitChanges();
        }
        public class csvElemet
        {
            [LINQtoCSV.CsvColumn(CanBeNull = true, Name = "No.", OutputFormat = "#")]
            public int AtomicNumber { get; set; }
            [LINQtoCSV.CsvColumn(CanBeNull = true, Name = "Atomic Weight")]
            public double Atomic_Weight { get; set; }
            [LINQtoCSV.CsvColumn(CanBeNull = true, Name = "Name")]
            public string Name { get; set; }
            [LINQtoCSV.CsvColumn(CanBeNull = true, Name = "Symbol")]
            public string Symbol { get; set; }
            [LINQtoCSV.CsvColumn(CanBeNull = true, Name = "Melting Point C")]
            public double Melting_Point_C { get; set; }
            [LINQtoCSV.CsvColumn(CanBeNull = true, Name = "Boiling Point C")]
            public double Boiling_Point_C { get; set; }
            [LINQtoCSV.CsvColumn(CanBeNull = true, Name = "Density g/cm3")]
            public double Density_gPercm3 { get; set; }
            [LINQtoCSV.CsvColumn(CanBeNull = true, Name = "Percentage Earth Crust")]
            public double Percentage_Earth_Crust { get; set; }
            [LINQtoCSV.CsvColumn(CanBeNull = true, Name = "Year discovered")]
            public string Year_discovered { get; set; }
            [LINQtoCSV.CsvColumn(CanBeNull = true, Name = "Group*", OutputFormat = "#")]
            public int Group { get; set; }
            [LINQtoCSV.CsvColumn(CanBeNull = true, Name = "Electron Configuration")]
            public string Electron_Configuration { get; set; }
            [LINQtoCSV.CsvColumn(CanBeNull = true, Name = "Ionization Energy eV")]
            public double Ionization_Energy_eV { get; set; }
        }
    }
}
