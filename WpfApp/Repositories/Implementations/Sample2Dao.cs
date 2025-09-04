using WpfApp.Models;
using WpfApp.Repositories.Base;
using WpfApp.Repositories.Interfaces;
using WpfApp.Common;
using System.Data.SQLite;

namespace WpfApp.Repositories.Implementations
{
    class Sample2Dao : BaseDao, ITableDataReplaceDao
    {
        protected List<SqlInfoDto> sqlInfo = new List<SqlInfoDto>();
        public Sample2Dao() : base("sample2", ReadTestData())
        {
        }

        public void ReplaceTableData(SQLiteConnection conn, SQLiteTransaction tran)
        {
            string dSql = CreateDeleteSqlQuery();
            sqlInfo.Add(new SqlInfoDto { sqlQuery = dSql, ParameterSets = null });

            string iSql = CreateInsertSqlQuery();
            var insertData = CreatePlaceholderValue();
            sqlInfo.Add(new SqlInfoDto { sqlQuery = iSql, ParameterSets = insertData });

            var executor = new DbExecutor(conn, tran);
            // 動的にcommitの判断をしたい
            executor.ExecuteAll(sqlInfo, true);
        }

        private static IEnumerable<IDictionary<string, object>> ReadTestData()
        {
            var cUtil = new CsvReadUtil("sample.csv");
            return (IEnumerable<IDictionary<string, object>>)cUtil.CsvRead();
        }
    }
}
