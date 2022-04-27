using System;
using System.Linq;
using Domain.DocumentDTOs;

namespace CommandDLL
{
    public abstract class StandartPDFMonitorBuilder : DocumentBuilderPDF, IDocumentBuilder<StandartDocumentDTO>
    {

        public void SetContent(StandartDocumentDTO content)
        {
            base.TryReplaceSring("_k_actual_", content.K.ToString("0.00"));
            base.TryReplaceSring("_protocol_n_", "default");
            base.TryReplaceSring("_angle_", content.Angle.ToString("0.00"));
            base.TryReplaceSring("_k_estimated_", content.K.ToString("0.00"));
            base.TryReplaceSring("_kerror_", content.ErrorK.ToString("0.00"));
            base.TryReplaceSring("_degreydor_material_", "-");
            base.TryReplaceSring("_duration_", new TimeSpan(content.IrradiationTime).ToString());
            base.TryReplaceSring("_irradiation_start_time_", content.IrradiatedBegin.ToString());
            base.TryReplaceSring("_org_name_", content.OrganizationName);
            base.TryReplaceSring("_seans_number_", content.SessionNumber.ToString());
            base.TryReplaceSring("_sickness_", "-");
            base.TryReplaceSring("_temperature_", content.Temperature.ToString("0.00"));
            base.TryReplaceSring("_work_cipher_", content.WorkName);
            base.TryReplaceSring("_linear_loss_", $"{content.LinearLoss.ToString("0.00")}+-{content.LinearLoss.ToString("0.00")}");
            base.TryReplaceSring("_mileage_", $"{content.DistanceSI.ToString("0.00")}+-{content.DistanceSI.ToString("0.00")}");
            base.TryReplaceSring("_avg_", content.OD.Average().ToString());
            base.TryReplaceSring("_energy_", $"{content.Energy.ToString("0.00")}+-{content.EnergyError.ToString("0.00")}");
            base.TryReplaceSring("_ion_type_", content.IonName);
            base.TryReplaceSring("_ion_", content.Isotop.ToString());
            base.TryReplaceSring("_heterogeneity_", content.Heterogeneity.ToString("0.00"));
            //item? 

            for (int i = 0; i < content.OD.Length; i++)
            {
                var replaceString = $"_{i+1}_";
                base.TryReplaceSring(replaceString, content.OD[i].ToString());
            }

            for (int i = 0; i < content.TD.Length; i++)
            {
                var replaceString = $"_det{i+1}_";
                base.TryReplaceSring(replaceString, content.TD[i].ToString());
            }
        }        
    }

}

