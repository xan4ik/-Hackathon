namespace WebApi.DTO
{
    public struct IonShortInfo 
    {
        public string IonName { get; set; }
        public int Isotope { get; set; }
        public float DistanceSI { get; set; }
        public float DestanceErrorSI { get; set; }
    }
}
