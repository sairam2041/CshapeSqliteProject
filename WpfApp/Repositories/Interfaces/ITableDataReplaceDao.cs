using System.Data.SQLite;

namespace WpfApp.Repositories.Interfaces
{
    interface ITableDataReplaceDao
    {
        // 最終的にはDBコネクションを渡す形にしたい
        public void ReplaceTableData(SQLiteConnection conn, SQLiteTransaction tran);
    }
}
