using WpfApp.Models;
using WpfApp.Repositories.Base;
using WpfApp.Repositories.Interfaces;
using WpfApp.Repositories.Sql;

namespace WpfApp.Repositories.Implementations
{
    public class DeleteQueryDao : BaseSql, ICreateSqlInfo
    {
        public DeleteQueryDao(string table, string schema) : base(table, schema)
        {
        }

        public string CreateQuey(object value, object? where, bool isAttach = false)
        {
            string _tableName = isAttach ? Schema + "." + TableName : TableName;
            return SqlBuilder.BuildDeleteQuery(_tableName);
        }

        public IEnumerable<SqlParameterSet> CreateParameterSets(object value, object? where)
        {
            throw new NotImplementedException();
        }
    }
}
