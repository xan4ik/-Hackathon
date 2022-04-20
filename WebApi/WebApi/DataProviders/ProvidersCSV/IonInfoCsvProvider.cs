using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DataProviders.ProvidersCSV
{
    public class IonInfoCsvProvider : CsvProvider<IonInfo>
    {
        private int id = 0;

        public IonInfoCsvProvider() : base(@"wwwroot\ion.csv")
        { }

        protected override IonInfo ParseLine(string line)
        {
            var items = line.Split(';');
            var item =  new IonInfo();

            item.Id = id++;
            item.IonName = items[0];
            item.Isotope = int.Parse(items[1]);
            item.OutputNumberInSession = int.Parse(items[2]);
            item.Enviroment = items[5];
            item.SurfaceEnergyOfTestObject = float.Parse(items[6]);
            item.EnergyErrorOfTestObjecе = float.Parse(items[7]);
            item.DistanceSI = float.Parse(items[8]);
            item.DistanceErrorSI = float.Parse(items[9]);
            item.LPE = float.Parse(items[10]);
            item.ErrorLPE = float.Parse(items[11]);
            item.IonConductorEnergy = float.Parse(items[12]);
            item.SessionNumberInYear = int.Parse(items[13]);
            item.EnviromentCode = items[14];


            return item;
        }
    }
}
