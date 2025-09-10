namespace WpfApp.Repositories.Base
{
    public abstract class BaseSql
    {
        public string TableName {  get; init; }
        public string Schema {  get; init; }

        public BaseSql(string table, string schema)
        {
            TableName = table;
            Schema = schema;
        }
    }
}
