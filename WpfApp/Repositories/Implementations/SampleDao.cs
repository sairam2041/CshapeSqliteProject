using WpfApp.Models;
using WpfApp.Repositories.Base;
using WpfApp.Repositories.Interfaces;
using WpfApp.Common;
using System.Data.SQLite;

namespace WpfApp.Repositories.Implementations
{
    class SampleDao : BaseDao, ITableDataReplaceDao
    {
        protected List<SqlInfoDto> sqlInfo = new List<SqlInfoDto>();
        public SampleDao() : base("sample.db", "sample", ReadTestData())
        {
        }

        public void ReplaceTableData(SQLiteConnection conn, SQLiteTransaction tran, bool isAttached = false)
        {
            string dSql = CreateDeleteSqlQuery(isAttached);
            sqlInfo.Add(new SqlInfoDto { sqlQuery = dSql, ParameterSets = null });

            string iSql = CreateInsertSqlQuery(isAttached);
            var insertData = CreatePlaceholderValue();
            sqlInfo.Add(new SqlInfoDto { sqlQuery = iSql, ParameterSets = insertData });

            var executor = new DbExecutor(conn, tran);
            executor.ExecuteAll(sqlInfo);
        }

        private static IEnumerable<IDictionary<string, object>> ReadTestData()
        {
            var cUtil = new CsvReadUtil("sample.csv");
            return (IEnumerable<IDictionary<string, object>>)cUtil.CsvRead();
        }
    }
}
