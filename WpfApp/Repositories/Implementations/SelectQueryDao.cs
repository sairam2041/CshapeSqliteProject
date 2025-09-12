using WpfApp.Dto;
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

        public SqlInfoDto GenerateSqlInfo(string tableName, SqlQueryParametersDto dto)
        {
            return SqlInfoBuilder.BuildSelectInfo(tableName, dto);
        }
    }
}