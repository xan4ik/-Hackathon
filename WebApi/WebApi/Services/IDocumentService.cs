using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.DocumentDTOs;

namespace WebAPI.Services
{
    public class IDocumentService 
    {
        private ITimeEntityService _timeService;
        private IIonInfoService _ionService;
        private IDataEntityService _dataService;

        public IDocumentService(ITimeEntityService timeService, IIonInfoService ionService, IDataEntityService dataService)
        {
            _timeService = timeService;
            _ionService = ionService;
            _dataService = dataService;
        }


        public NonStandartDocumentDTO GetDoumentData(int entityID) 
        {
            if (_timeService.HasNotSession(entityID)) 
            {
                throw new Exception("No session with id " + entityID);
            }
        }
    }
}
