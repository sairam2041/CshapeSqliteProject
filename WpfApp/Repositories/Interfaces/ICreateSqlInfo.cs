using WpfApp.Models;

namespace WpfApp.Repositories.Interfaces
{
    interface ICreateSqlInfo 
    {
        string CreateQuey(object value , object? where, bool isAttach = false);

        // setやwhereのパラメータが全て入った辞書を返却
        IEnumerable<SqlParameterSet> CreateParameterSets(object value, object? where);
    }
}
