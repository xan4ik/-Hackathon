using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Domain.DocumentDTOs;

namespace CommandDLL
{
    public class AllowancePDFProtocolBuilder : DocumentBuilderPDF, IDocumentBuilder<AllowanceDocumnetDTO>
    {
        public void SetContent(AllowanceDocumnetDTO content)
        {
            base.TryReplaceSring("_k_estimated_", content.K.ToString("0.00"));
            base.TryReplaceSring("_kerror_", content.ErrorK.ToString("0.00"));
            base.TryReplaceSring("_energy_", content.Energy.ToString("0.00"));
            base.TryReplaceSring("_humidity_", content.Humidity.ToString("0.0"));
            base.TryReplaceSring("_pressure_", content.Pressure.ToString());
            base.TryReplaceSring("_protocol_n_", content.Protocol);
            base.TryReplaceSring("_protocol_date_", content.ProtocolDate.ToString());
            base.TryReplaceSring("_from_", content.SesionBegin.ToString());
            base.TryReplaceSring("_to_", content.SesionEnd.ToString());
            base.TryReplaceSring("_ambient_temperature_", content.Temperature.ToString());
            base.TryReplaceSring("_ion_", content.IonName);
            base.TryReplaceSring("_ion_type_", content.Isotop.ToString());

            for (int i = 0; i < content.DetectorData.Length; i++)
            {
                var replaceRaw = $"_det{i + 1}_";
                base.TryReplaceSring(replaceRaw, content.DetectorData[i].ToString());
            }
        }
    }
}
