using WpfApp.Dto;
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

        public SqlInfoDto GenerateSqlInfo(string tableName, SqlQueryParametersDto dto)
        {
            return SqlInfoBuilder.BuildDeleteInfo(tableName, dto);
        }
    }
}
