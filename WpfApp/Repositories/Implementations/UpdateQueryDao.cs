using WpfApp.Models;
using WpfApp.Repositories.Base;
using WpfApp.Repositories.Interfaces;
using WpfApp.Repositories.Sql;

namespace WpfApp.Repositories.Implementations
{
    public class UpdateQueryDao : BaseSql, ICreateSqlInfo
    {
        public UpdateQueryDao(string table, string schema) : base(table, schema)
        {
        }

        public string CreateQuey(object value, object? where, bool isAttach = false)
        {
            string _tableName = isAttach ? Schema + "." + TableName : TableName;
            return SqlBuilder.BuildUpdateQuery(_tableName, (IDictionary<string, object>)value, (IDictionary<string, object>?)where);
        }

        public IEnumerable<SqlParameterSet> CreateParameterSets(object value, object? where)
        {
            throw new NotImplementedException();
        }
    }
}
