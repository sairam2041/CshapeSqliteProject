using WpfApp.Models;
using WpfApp.Repositories.Sql;

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

        public List<SqlParameterSet>? CreatePlaceholderValue() =>
            _insertData == null ? null : SqlBuilder.BuildPlaceholders(_insertData);

        public string CreateDeleteSqlQuery() => SqlBuilder.BuildDeleteQuery(_tableName);

        public string CreateInsertSqlQuery() =>
            _insertData == null ? "" : SqlBuilder.BuildInsertQuery(_tableName, _insertData);
    }
}
