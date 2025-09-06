using WpfApp.Models;
using WpfApp.Repositories.Base;
using WpfApp.Repositories.Interfaces;
using WpfApp.Common;
using System.Data.SQLite;

namespace WpfApp.Repositories.Implementations
{
    class SampleDao : BaseDao, ITableDataReplaceDao
    {
        private List<SqlInfoDto> sqlInfo = new List<SqlInfoDto>();
        
        public SampleDao(string schema, string table, string csvFileName) : base(schema, table, ReadCsvData(csvFileName))
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

        private static IEnumerable<IDictionary<string, object>> ReadCsvData(string csvFileName)
        {
            var cUtil = new CsvReadUtil(csvFileName);
            return (IEnumerable<IDictionary<string, object>>)cUtil.CsvRead();
        }
    }
}
