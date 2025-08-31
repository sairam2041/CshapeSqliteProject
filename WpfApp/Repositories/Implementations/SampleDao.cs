using WpfApp.Repositories.Interfaces;
using WpfApp.Repositories.Base;

namespace WpfApp.Repositories.Implementations
{
    class SampleDao : BaseDao, ITableDataReplaceDao
    {
        protected List<KeyValuePair<string, object?>> sqlInfo = new List<KeyValuePair<string, object?>>();
        public SampleDao(IEnumerable<KeyValuePair<string, object>>? insertData) : base("sample", insertData)
        {
        }

        public void ReplaceTableData(string dbPath)
        {
            string dSql = CreateDeleteSqlQuery();
            sqlInfo.Add(new KeyValuePair<string, object?>(dSql, null));

            string iSql = CreateInsertSqlQuery();
            var insertData = CreatePlaceholderValue();
            sqlInfo.Add(new KeyValuePair<string, object?>(iSql, insertData));

            var executor = new DbExecutor(dbPath);
            executor.ExecuteAll(sqlInfo);
        }
    }
}
