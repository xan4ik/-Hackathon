namespace CommandDLL
{
    public class NonStandartPDFProtocolBuilder : MonitorPDFProtocolBuilder, INotStandartMonitoringProtocolBuilder 
    {
        public void SetRightHeterogeneity(float value)
        {
            base.TryReplaceSring("_right_", value.ToString("0.00"));
        }

        public void SetLeftHeterogeneity(float value)
        {
            base.TryReplaceSring("_left_", value.ToString("0.00"));
        }
    }

}

