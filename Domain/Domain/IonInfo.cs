namespace Domain
{
    public class IonInfo 
    {
        public int Id { get; set; } // номер строки в документе
        public string IonName { get; set; }
        public int Isotope { get; set; }
        public int OutputNumberInNumber { get; set; }
        public string Enviroment { get; set; }
        public string EnviromentCode { get; set; }
        public int SessionNumberInYear { get; set; }
        public float SurfaceEnergyOfTestObject { get; set; }
        public float EnergyErrorOfTestObjecе { get; set; }
        public float DistanceSI { get; set; }
        public float DestanceErrorSI { get; set; }
        public float LPE { get; set; }
        public float ErrorLPE { get; set; }
        public float IonConductorEnergy { get; set; }
    }
}
