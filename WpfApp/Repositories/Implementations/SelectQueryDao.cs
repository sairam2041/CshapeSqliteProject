using WpfApp.Models;
using WpfApp.Repositories.Base;
using WpfApp.Repositories.Interfaces;
using WpfApp.Repositories.Sql;

namespace WpfApp.Repositories.Implementations
{
    public class SelectQueryDao : BaseSql, ICreateSqlInfo
    {
        public SelectQueryDao(string table, string schema) : base(table, schema)
        {
        }

        public string CreateQuey(object value, object? where, bool isAttach = false)
        {
            string _tableName = isAttach ? Schema + "." + TableName : TableName;
            return SqlBuilder.BuildSelectQuery(_tableName, (string[]?)value, (IDictionary<string, object>?)where);
        }

        public IEnumerable<SqlParameterSet> CreateParameterSets(object value, object? where)
        {
            throw new NotImplementedException();
        }
    }
}
