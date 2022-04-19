namespace Domain
{
    public class DataEntity
    {
        public int Id { get; set; } // номер строки в документе
        public string Organization { get; set; }
        public string[] TestObjects { get; set; }
        public double DetectorAVG { get; set; }
        public int[] TD { get; set; }
        public double[] OnlineDetectorAVG { get; set; }
        public int[] OD { get; set; }
        public double FlowIntensity { get; set; }
        public string AccessProtocolNumber { get; set; }
        public int IrradiationAngle { get; set; }
        public int FacilityPressure { get; set; }
        public int FacilityPumidity { get; set; }
        public int FacilityTemperature { get; set; }
        public int SessionTemperature { get; set; }
        public float HeterogeneityPercent {get;set;}
        public double MinusPlus { get; set; }
        public double LeftHeterogeneity { get; set; }
        public double RightHeterogeneity { get; set; }
        public double K { get; set; }
    }
}
