using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DataProviders.ProvidersCSV
{
    public abstract class CsvProvider<T> : IDataProvider<T>
    {
        private LinkedList<T> _entities;
        private string _pathToCSV;

        public CsvProvider(string pathToCSV)
        {
            _pathToCSV = pathToCSV;
        }

        public IEnumerable<T> GetData()
        {
            if (_entities != null)
            {
                return _entities;
            }

            CreateEntities();
            return _entities;
        }

        private void CreateEntities()
        {
            _entities = new LinkedList<T>();
            using (var reader = new StreamReader(_pathToCSV, System.Text.Encoding.Default))
            {
                reader.ReadLine(); // skip first line

                var line = reader.ReadLine();
                while (line != null)
                {
                    var entity = ParseLine(line);
                    _entities.AddLast(entity);

                    line = reader.ReadLine();
                }
            }
        }
        protected abstract T ParseLine(string line);

    }
}
