using WpfApp.Repositories.Interfaces;
using WpfApp.Repositories.Base;

namespace WpfApp.Repositories.Implementations
{
    class SampleDao : BaseDao, ITableDataReplaceDao
    {
        protected List<KeyValuePair<string, object?>> sqlInfo;
        public SampleDao(IEnumerable<KeyValuePair<string, object>>? insertData) : base("sample", insertData)
        {
        }

        public void ReplaceTableData()
        {
            string dSql = createDeleteSqlQuery();
            sqlInfo.Add(new KeyValuePair<string, object?>(dSql, null));

            string iSql = createInsertSqlQuery();
            var insertData = createPlaceholderValue();
            sqlInfo.Add(new KeyValuePair<string, object?>(iSql, insertData));

            executeAll(sqlInfo);
        }
    }
}
