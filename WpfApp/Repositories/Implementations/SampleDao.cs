using WpfApp.Repositories.Interfaces;
using WpfApp.Repositories.Base;
using WpfApp.Models;

namespace WpfApp.Repositories.Implementations
{
    class SampleDao : BaseDao, ITableDataReplaceDao
    {
        protected List<SqlInfoDto> sqlInfo = new List<SqlInfoDto>();
        public SampleDao(IEnumerable<KeyValuePair<string, object>>? insertData) : base("sample", insertData)
        {
        }

        public void ReplaceTableData(string dbPath)
        {
            string dSql = CreateDeleteSqlQuery();
            sqlInfo.Add(new SqlInfoDto { sqlQuery = dSql, ParameterSets = null });

            string iSql = CreateInsertSqlQuery();
            var insertData = CreatePlaceholderValue();
            sqlInfo.Add(new SqlInfoDto { sqlQuery = iSql, ParameterSets = insertData });

            var executor = new DbExecutor(dbPath);
            executor.ExecuteAll(sqlInfo);
        }
    }
}
