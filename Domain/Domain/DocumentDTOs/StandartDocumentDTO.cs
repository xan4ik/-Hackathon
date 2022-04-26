using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DocumentDTOs
{
    public class StandartDocumentDTO
    {
        public int SessionNumber { get; set; }
        public string OrganizationName { get; set; }
        public string WorkName { get; set; }
        public string[] IrradiatedItems { get; set; }
        public DateTime IrradiatedBegin { get; set; }
        public long IrradiationTime { get; set; }
        public float Angle { get; set; }
        public float Temperature { get; set; }
        public float Energy { get; set; }
        public float EnergyError { get; set; }
        public float DistanceSI { get; set; }
        public float DistanceError { get; set; }
        public float LinearLoss { get; set; }
        public float LinearLossError { get; set; }
        public int[] TD {get; set;}
        public int[] OD { get; set; }
        public float K { get; set; }
        public float ErrorK { get; set; }
        public float Heterogeneity { get; set; }
    }
}
