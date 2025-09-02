using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;

namespace WpfApp.Common
{
    public class CsvReadUtil
    {
        private string fileName;

        public CsvReadUtil(string fileName)
        {
            this.fileName = fileName;
        }
        public List<dynamic> CsvRead()
        {
            string csvPath = Path.Combine(AppContext.BaseDirectory, "data", fileName);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };

            using (var reader = new StreamReader(csvPath))
            using (var csv = new CsvReader(reader, config))
            {
                return csv.GetRecords<dynamic>().ToList();
            }
        }
    }
}
