namespace Domain
{
    public class DataEntity
    {
        public int Id { get; set; } // номер строки в документе
        public string Organization { get; set; }
        public string[] TestObjects { get; set; }
        public float DetectorAVG { get; set; }
        public int[] TD { get; set; }
        public float OnlineDetectorAVG { get; set; }
        public int[] OD { get; set; }
        public float FlowIntensity { get; set; }
        public string AccessProtocolNumber { get; set; }
        public int IrradiationAngle { get; set; }
        public int FacilityPressure { get; set; }
        public int FacilityPumidity { get; set; }
        public int FacilityTemperature { get; set; }
        public int SessionTemperature { get; set; }
        public float HeterogeneityPercent {get;set;}
        public float MinusPlus { get; set; }
        public float LeftHeterogeneity { get; set; }
        public float RightHeterogeneity { get; set; }
        public float K { get; set; }
    }
}
