using System;
using System.Linq;

namespace CommandDLL
{
    public abstract class MonitorPDFProtocolBuilder : DocumentBuilderPDF, IMonitoringPrtocolBuilder
    {
        private const string TemplatePath = "html/non_standard.html";
       
        public void SetActualK(float k)
        {
            base.TryReplaceSring("_k_actual_", k.ToString("0.00"));
        }

        public void SetAllowenceProtocolName(string name)
        {
            base.TryReplaceSring("_protocol_n_", name);
        }

        public void SetAngle(float angle)
        {
            base.TryReplaceSring("_angle_", angle.ToString("0.00"));
        }

        public void SetCoefK(float k, float error)
        {
            base.TryReplaceSring("_k_estimated_", k.ToString("0.00"));
            base.TryReplaceSring("_kerror_", k.ToString("0.00"));
        }

        public void SetCounterData(long[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                var replaceString = $"_{i+1}_";
                base.TryReplaceSring(replaceString, data[i].ToString());
            }

            base.TryReplaceSring("_avg_", data.Average().ToString());
        }

        public void SetDegreydorMaterial(string material)
        {
            base.TryReplaceSring("_degreydor_material_", material);
        }

        public void SetDetectorData(long[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                var replaceString = $"_det{i+1}_";
                base.TryReplaceSring(replaceString, data[i].ToString());
            }
        }

        public void SetDuration(TimeSpan span)
        {
            base.TryReplaceSring("_duration_", span.ToString());
        }

        public void SetEnergy(float energy, float error)
        {
            base.TryReplaceSring("_energy_", $"{energy.ToString("0.00")} +- { error.ToString("0.00")}");
        }

        public void SetIonType(string ion)
        {
            base.TryReplaceSring("_ion_type_", ion);
        }

        public void SetIrradiatedItem(long number)
        {
            base.TryReplaceSring("_irradiated_item_", number.ToString());
        }

        public void SetIrradiationBegin(DateTime time)
        {
            base.TryReplaceSring("_irradiation_start_time_", time.ToString());
        }

        public void SetLinearLoss(float loss, float error)
        {
            base.TryReplaceSring("_linear_loss_", $"{loss.ToString("0.00")} +- { error.ToString("0.00")}");
        }

        public void SetMillage(float distance, float error)
        {
            base.TryReplaceSring("_mileage_", $"{distance.ToString("0.00")} +- { error.ToString("0.00")}");
        }

        public void SetOrganizationName(string name)
        {
            base.TryReplaceSring("_org_name_", name);
        }

        public void SetSeansNumber(uint number)
        {
            base.TryReplaceSring("_seans_number_", number);
        }

        public void SetSickness(string sickness)
        {
            base.TryReplaceSring("_sickness_", sickness);
        }

        public void SetTemperature(float temperature)
        {
            base.TryReplaceSring("_temperature_", temperature.ToString("0.00"));
        }

        public void SetWorkName(string name)
        {
            base.TryReplaceSring("_work_cipher_", name);
        }
    }

}

