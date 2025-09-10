using WpfApp.Models;
using WpfApp.Repositories.Base;
using WpfApp.Repositories.Interfaces;
using WpfApp.Repositories.Sql;

namespace WpfApp.Repositories.Implementations
{
    public class InsertQueryDao : BaseSql, ICreateSqlInfo
    {
        public InsertQueryDao(string table, string schema) : base(table, schema)
        {
        }

        public string CreateQuey(object value, object? where, bool isAttach = false)
        {
            string _tableName = isAttach ? Schema + "." + TableName : TableName;
            return SqlBuilder.BuildInsertQuery(_tableName, (IEnumerable<IDictionary<string, object>>)value);
        }

        public IEnumerable<SqlParameterSet> CreateParameterSets(object value, object? where)
        {
            return SqlBuilder.BuildPlaceholders((IEnumerable<IDictionary<string, object>>)value);
        }
    }
}
