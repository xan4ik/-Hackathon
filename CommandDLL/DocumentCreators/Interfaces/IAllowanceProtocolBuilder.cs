using System;
using System.ComponentModel.DataAnnotations;

namespace CommandDLL
{
    public interface IAllowanceProtocolBuilder : IDocumentBuilder
    {
        void SetProtocol(string protocol);
        void SetProtocolDate(DataType dataType);
        void SetUsedIon(string ionName, int isotop);
        void SetEnergy(float energy);
        void SetSesionBegin(DateTime begin);
        void SetSesionEnd(DateTime end);
        void SetTemperature(float temperature);
        void SetPressure(float pressure);
        void SetHumidity(float humidity);
        void SetDetectorData(long[] data);
        void SetCoefK(float k, float error);
        void SetHeterogeneity(float value);
    }

}

