namespace WpfApp.Models
{
    public class SqlInfoDto
    {
        public required string sqlQuery { get; set; }
        public IEnumerable<SqlParameterSet>? ParameterSets { get; set; }
    }

    public class SqlParameterSet
    {
        public List<KeyValuePair<string, object>> Parameters { get; set; } = new();
    }
}
