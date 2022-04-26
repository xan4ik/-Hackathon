namespace CommandDLL
{
    public class StandartPDFMonitorBuilder : MonitorPDFProtocolBuilder, IStandartMonitoringProtocolBuilder
    {
        public void SetHeterogeneity(float value)
        {
            base.TryReplaceSring("_heterogeneity_", value.ToString("0.00"));
        }
    }

}

