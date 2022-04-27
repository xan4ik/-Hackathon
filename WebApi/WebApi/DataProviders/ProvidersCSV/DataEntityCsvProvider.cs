using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Tools;

namespace WebAPI.DataProviders.ProvidersCSV
{
    public class DataEntityCsvProvider : CsvProvider<DataEntity>
    {
        public DataEntityCsvProvider() : base(@"wwwroot\data.csv")
        { }

        protected override DataEntity ParseLine(string line)
        {
            var items = line.Split(';');
            return new DataEntity()
            {
                Id = items[0].GetIntOrDefault(),
                Organization = items[1],
                TestObjects = new string[] { items[2], items[3], items[4], items[5] },
                DetectorAVG = items[7].GetFloatOrDefault(),
                TD = new int[] {
                     (int)items[8].GetFloatOrDefault(),
                     (int)items[9].GetFloatOrDefault(),
                     (int)items[10].GetFloatOrDefault(),
                     (int)items[11].GetFloatOrDefault(),
                     (int)items[12].GetFloatOrDefault(),
                     (int)items[13].GetFloatOrDefault(),
                     (int)items[14].GetFloatOrDefault(),
                     (int)items[15].GetFloatOrDefault(),
                     (int)items[16].GetFloatOrDefault(),
                },
                OnlineDetectorAVG = items[17].GetFloatOrDefault(),
                OD = new int[] {
                    (int)items[18].GetFloatOrDefault(),
                    (int)items[19].GetFloatOrDefault(),
                    (int)items[20].GetFloatOrDefault(),
                    (int)items[21].GetFloatOrDefault(),
                },
                FlowIntensity = items[22].GetFloatOrDefault(),
                AccessProtocolNumber = items[23],
                IrradiationAngle = items[24].GetIntOrDefault(),
                FacilityPressure = items[25].GetIntOrDefault(),
                FacilityPumidity = items[26].GetIntOrDefault(),
                FacilityTemperature = items[27].GetIntOrDefault(),
                SessionTemperature = items[28].GetIntOrDefault(),
                HeterogeneityPercent = items[29].GetFloatOrDefault(),
                K = items[30].GetIntOrDefault(),
                MinusPlus = items[31].GetFloatOrDefault(),
                LeftHeterogeneity = items[32].GetFloatOrDefault(),
                RightHeterogeneity = items[33].GetFloatOrDefault(),
            };
        }
    }
}
