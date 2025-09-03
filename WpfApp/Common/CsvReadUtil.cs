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

        public IEnumerable<IDictionary<string, object>> CsvRead()
        {
            string csvPath = Path.Combine(AppContext.BaseDirectory, "data", fileName);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };
            var result = new List<IDictionary<string, object>>();

            using (var reader = new StreamReader(csvPath))
            using (var csv = new CsvReader(reader, config))
            {
                foreach (var record in csv.GetRecords<dynamic>())
                {
                    // dynamic は ExpandoObject として返される
                    var dict = (IDictionary<string, object>)record;
                    result.Add(dict);
                }
            }

            return result;
        }
    }
}
