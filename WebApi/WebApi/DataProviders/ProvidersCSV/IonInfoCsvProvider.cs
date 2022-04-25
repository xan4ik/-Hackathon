using Domain;
using WebAPI.Tools;

namespace WebAPI.DataProviders.ProvidersCSV
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
            item.Isotope = items[1].GetIntOrDefault();
            item.OutputNumberInSession = items[2].GetIntOrDefault();
            item.Enviroment = items[5];
            item.SurfaceEnergyOfTestObject = items[6].GetFloatOrDefault();
            item.EnergyErrorOfTestObjecе = items[7].GetFloatOrDefault();;
            item.DistanceSI = items[8].GetFloatOrDefault();
            item.DistanceErrorSI = items[9].GetFloatOrDefault();
            item.LPE = items[10].GetFloatOrDefault();
            item.ErrorLPE = items[11].GetFloatOrDefault();
            item.IonConductorEnergy = items[12].GetFloatOrDefault();
            item.SessionNumberInYear = items[13].GetIntOrDefault();
            item.EnviromentCode = items[14];


            return item;
        }
    }
}
