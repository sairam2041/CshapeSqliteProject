namespace WpfApp.Models
{
    public class SqlInfoDto
    {
        public required string SqlQuery { get; set; }

        public IEnumerable<SqlParameterSet>? WhereParameterSets { get; set; }

        public IEnumerable<SqlParameterSet>? ValueParameterSets { get; set; }
    }

    public class SqlParameterSet
    {
        public List<Dictionary<string, object>> Parameters { get; set; } = new();
    }
}
