using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommandDLL
{
    public class AllowancePDFProtocolBuilder : DocumentBuilderPDF, IAllowanceProtocolBuilder
    {
        public void SetCoefK(float k, float error)
        {
            base.TryReplaceSring("_k_estimated_", k.ToString("0.00"));
            base.TryReplaceSring("_kerror_", error.ToString("0.00"));
        }

        public void SetDetectorData(long[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                var replaceRaw = $"_det{i + 1}_";
                base.TryReplaceSring(replaceRaw, data[i].ToString());
            }
        }

        public void SetEnergy(float energy)
        {
            base.TryReplaceSring("_energy_", energy.ToString("0.00"));
        }

        public void SetHeterogeneity(float value)
        {
            
        }

        public void SetHumidity(float humidity)
        {
            base.TryReplaceSring("_humidity_", humidity.ToString("0.0"));
        }

        public void SetPressure(int pressure)
        {
            base.TryReplaceSring("_pressure_", pressure.ToString());
        }

        public void SetProtocol(string protocol)
        {
            base.TryReplaceSring("_protocol_n_", protocol);
        }

        public void SetProtocolDate(DataType dataType)
        {
            base.TryReplaceSring("_protocol_date_", dataType.ToString());
        }

        public void SetSesionBegin(DateTime begin)
        {
            base.TryReplaceSring("_from_", begin.ToString());
        }

        public void SetSesionEnd(DateTime end)
        {
            base.TryReplaceSring("_to_", end.ToString());
        }

        public void SetTemperature(float temperature)
        {
            base.TryReplaceSring("_ambient_temperature_", temperature.ToString());
        }

        public void SetUsedIon(string ionName, int isotop)
        {
            base.TryReplaceSring("_ion_type_", ionName);
            base.TryReplaceSring("_ion_", isotop.ToString());
        }
    }
}
