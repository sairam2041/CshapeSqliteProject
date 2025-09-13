namespace WpfApp.Dto
{
    public class SqlInfoDto
    {
        public string? Query {  get; init; }
        public IEnumerable<IDictionary<string, object>>? ValueSet {  get; init; }
    }
}
