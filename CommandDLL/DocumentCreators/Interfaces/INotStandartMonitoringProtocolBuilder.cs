namespace CommandDLL
{
    public interface INotStandartMonitoringProtocolBuilder : IMonitoringPrtocolBuilder 
    {
        void SetLeftHeterogeneity(float value);
        void SetRightHeterogeneity(float value);
    }

}

