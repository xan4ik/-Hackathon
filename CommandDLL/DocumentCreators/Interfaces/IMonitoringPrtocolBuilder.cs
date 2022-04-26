using System;

namespace CommandDLL
{
    public interface IMonitoringPrtocolBuilder : IDocumentBuilder
    {
        void SetSeansNumber(uint number);
        void SetOrganizationName(string name);
        void SetWorkName(string name);
        void SetIrradiatedItem(long number);
        void SetIrradiationBegin(DateTime time);
        void SetDuration(TimeSpan span);

        void SetAngle(float angle);
        void SetTemperature(float temperature);
        void SetDegreydorMaterial(string material);
        void SetSickness(string sickness);

        void SetIonType(string ion);
        void SetEnergy(float energy, float error);
        void SetMillage(float distance, float error);
        void SetLinearLoss(float loss, float error);

        void SetCounterData(long[] data);
        void SetCoefK(float k, float error);
        void SetAllowenceProtocolName(string name);

        void SetActualK(float k);

        void SetDetectorData(long[] data);
    }

}

