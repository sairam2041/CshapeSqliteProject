using WpfApp.Models;
using WpfApp.Repositories.Sql;

namespace WpfApp.Repositories.Base
{
    public abstract class BaseDao
    {
        protected string _tableName;
<<<<<<< HEAD
        protected IEnumerable<IDictionary<string, object>>? _insertData;

        public BaseDao(string tableName, IEnumerable<IDictionary<string, object>>? insertData)
=======
        protected IEnumerable<Dictionary<string, object>>? _insertData;

        public BaseDao(string tableName, IEnumerable<Dictionary<string, object>>? insertData)
>>>>>>> e9d79d644fc75ec0d4af6d9d843a1de5b305f45d
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
