using WpfApp.Models;
using WpfApp.Repositories.Sql;
using System.IO;

namespace WpfApp.Repositories.Base
{
    public abstract class BaseDao
    {
        public string SchemmaName { get; init; }
        public string TableName { get; init; }
        private IEnumerable<IDictionary<string, object>>? InsertData { get; set; }

        public BaseDao(string schemaName, string tableName, IEnumerable<IDictionary<string, object>>? insertData)
        {
            SchemmaName = schemaName;
            TableName = tableName;
            InsertData = insertData;
        }

        public List<SqlParameterSet>? CreatePlaceholderValue() =>
            InsertData == null ? null : SqlBuilder.BuildPlaceholders(InsertData);

        public string CreateDeleteSqlQuery(bool isAttached = false) => SqlBuilder.BuildDeleteQuery(GetTableReference(isAttached));

        public string CreateInsertSqlQuery(bool isAttached = false) =>
            InsertData == null ? "" : SqlBuilder.BuildInsertQuery(GetTableReference(isAttached), InsertData);

        private string GetTableReference(bool isAttached) =>
             isAttached ? $"{Path.GetFileNameWithoutExtension(SchemmaName)}.{TableName}" : TableName;
    }
}
