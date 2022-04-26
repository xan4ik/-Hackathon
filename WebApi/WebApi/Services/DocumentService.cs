using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.DocumentDTOs;
using WebAPI.DataProviders;

namespace WebAPI.Services
{
    public interface IDocumentService 
    {
        NonStandartDocumentDTO GetNonStandartDoumentData(int entityID);
        StandartDocumentDTO GetStandartDoumentData(int entityID);
        AllowanceDocumnetDTO GetAllowanceDoumentData(int entityID);
        bool ContainsInfo(int entityID);

    }

    public class DocumentService :IDocumentService
    {
        private IDataProvider<TimeEntity> _timeProvider;
        private IDataProvider<IonInfo> _ionProvider;
        private IDataProvider<DataEntity> _dataProvider;

        public DocumentService(IDataProvider<TimeEntity> timeService, IDataProvider<IonInfo> ionService, IDataProvider<DataEntity> dataService)
        {
            _timeProvider = timeService;
            _ionProvider = ionService;
            _dataProvider = dataService;
        }

       
        public NonStandartDocumentDTO GetNonStandartDoumentData(int entityID)
        {
            if (ContainsInfo(entityID))
            {
                return CreateNonStandartDocumentDTO(entityID);
            }

            throw new Exception("No data found" + entityID);
        }

        public StandartDocumentDTO GetStandartDoumentData(int entityID)
        {
            if (ContainsInfo(entityID))
            {
                return CreateStandartDocumentDTO(entityID);
            }

            throw new Exception("No data found" + entityID);
        }

        public AllowanceDocumnetDTO GetAllowanceDoumentData(int entityID)
        {
            if (ContainsInfo(entityID))
            {
                return CreateAllowanceDocumentDTO(entityID);
            }

            throw new Exception("No data found" + entityID);
        }

        public bool ContainsInfo(int entityID)
        {
            return _timeProvider.GetData().Any(x => x.Id == entityID) &&
                   _dataProvider.GetData().Any(x => x.Id == entityID);
        }

        private AllowanceDocumnetDTO CreateAllowanceDocumentDTO(int entityID)
        {
            var timeEntity = _timeProvider.GetData().First(x => x.Id == entityID);
            var dataEntity = _dataProvider.GetData().First(x => x.Id == entityID);
            var ionInfo = _ionProvider.GetData().First(x => x.IonName == timeEntity.IonName);


            return new AllowanceDocumnetDTO()
            {
                Protocol = "I don't know",
                ProtocolDate = DateTime.Now,
                IonName = ionInfo.IonName,
                Isotop = ionInfo.Isotope,
                Energy = ionInfo.SurfaceEnergyOfTestObject,
                SesionBegin = timeEntity.SessionBegin,
                SesionEnd = timeEntity.SessionEnd,
                Temperature = dataEntity.FacilityTemperature,
                Pressure = dataEntity.FacilityPressure, 
                Humidity = dataEntity.FacilityPumidity,
                DetectorData = dataEntity.TD,
                K = (float)dataEntity.K,
                ErrorK = (float)dataEntity.MinusPlus,
                Heterogeneity = dataEntity.HeterogeneityPercent
            };
        }

        private StandartDocumentDTO CreateStandartDocumentDTO(int entityID)
        {
            var timeEntity = _timeProvider.GetData().First(x => x.Id == entityID);
            var dataEntity = _dataProvider.GetData().First(x => x.Id == entityID);
            var ionInfo = _ionProvider.GetData().First(x => x.IonName == timeEntity.IonName);


            return new StandartDocumentDTO()
            {
                SessionNumber = entityID,
                OrganizationName = dataEntity.Organization,
                WorkName = string.Concat(entityID.ToString(), "-", timeEntity.SessionBegin.Year.ToString()),
                IrradiationTime = timeEntity.ExposureSpan.Ticks,
                IrradiatedBegin = timeEntity.SessionBegin,
                IrradiatedItems = dataEntity.TestObjects,
                Angle = dataEntity.IrradiationAngle,
                Temperature = dataEntity.FacilityTemperature,
                Energy = ionInfo.SurfaceEnergyOfTestObject,
                EnergyError = ionInfo.EnergyErrorOfTestObjecе,
                DistanceSI = ionInfo.DistanceSI,
                DistanceError = ionInfo.DistanceErrorSI,
                LinearLoss = ionInfo.LPE,
                LinearLossError = ionInfo.ErrorLPE,
                TD = dataEntity.TD,
                OD = dataEntity.OD,
                K = (float)dataEntity.K,
                ErrorK = (float)dataEntity.MinusPlus,
                Heterogeneity = dataEntity.HeterogeneityPercent
            };
        }


        private NonStandartDocumentDTO CreateNonStandartDocumentDTO(int entityID)
        {
            var timeEntity = _timeProvider.GetData().First(x => x.Id == entityID);
            var dataEntity = _dataProvider.GetData().First(x => x.Id == entityID);
            var ionInfo = _ionProvider.GetData().First(x => x.IonName == timeEntity.IonName);


            return new NonStandartDocumentDTO()
            {
                SessionNumber = entityID,
                OrganizationName = dataEntity.Organization,
                WorkName = string.Concat(entityID.ToString(), "-", timeEntity.SessionBegin.Year.ToString()),
                IrradiationTime = timeEntity.ExposureSpan.Ticks,
                IrradiatedBegin = timeEntity.SessionBegin,
                IrradiatedItems = dataEntity.TestObjects,
                Angle = dataEntity.IrradiationAngle,
                Temperature = dataEntity.FacilityTemperature,
                Energy = ionInfo.SurfaceEnergyOfTestObject,
                EnergyError = ionInfo.EnergyErrorOfTestObjecе,
                DistanceSI = ionInfo.DistanceSI,
                DistanceError = ionInfo.DistanceErrorSI,
                LinearLoss = ionInfo.LPE,
                LinearLossError = ionInfo.ErrorLPE,
                TD =  dataEntity.TD,
                OD = dataEntity.OD,
                K = (float)dataEntity.K,
                ErrorK = (float)dataEntity.MinusPlus,
                LeftHeterogeneity = dataEntity.LeftHeterogeneity,
                RightHeterogeneity = dataEntity.RightHeterogeneity
            };

        }
    }
}
