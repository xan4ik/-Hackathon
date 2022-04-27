using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DocumentDTOs
{
    public class AllowanceDocumnetDTO
    {
        public string Protocol { get; set; }
        public DateTime ProtocolDate { get; set; }
        public string IonName { get; set; }
        public int Isotop { get; set; }
        public float Energy { get; set; }
        public DateTime SesionBegin { get; set; }
        public DateTime SesionEnd { get; set; }
        public float Temperature { get; set; }
        public int Pressure { get; set; }
        public float Humidity { get; set; }
        public int[] DetectorData { get; set; }
        public float K { get; set; }
        public float ErrorK { get; set; }
        public float Heterogeneity { get; set; }
    }
}
