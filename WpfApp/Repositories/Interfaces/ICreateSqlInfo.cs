using WpfApp.Dto;
using WpfApp.Repositories.Sql;

namespace WpfApp.Repositories.Interfaces
{
    interface ICreateSqlInfo 
    {
        SqlInfoDto GenerateSqlInfo(string tableName, SqlQueryParametersDto dto);
    }
}
