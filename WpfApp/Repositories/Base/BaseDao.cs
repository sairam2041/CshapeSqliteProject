namespace WpfApp.Repositories.Base
{
    public abstract class BaseDao
    {
        protected string _tableName;
        protected IEnumerable<KeyValuePair<string, object>>? _insertData;

        public BaseDao(string tableName, IEnumerable<KeyValuePair<string, object>>? insertData)
        {
            _tableName = tableName;
            _insertData = insertData;
        }

        public IEnumerable<KeyValuePair<string, object>>? createPlaceholderValue()
        {
            return _insertData?.Select(kv => new KeyValuePair<string, object>($"${kv.Key}", kv.Value));
        }

        public string createDeleteSqlQuery()
        {
            return "DELETE FROM " + _tableName + ";";
        }

        public string createInsertSqlQuery()
        {
            return "INSERT INTO " + _tableName + " VALUES (" + _insertData?.Select(d => "$" + d.Key) + ");" ;
        }

        public void executeAll(List<KeyValuePair<string, object?>> sqlInfo)
        {
            // toDo SQLiteを用いたDB操作
        }
    }
}
