using System.Data.SQLite;

namespace WpfApp.Repositories.Interfaces
{
    interface ITableDataReplaceDao
    {
        public void ReplaceTableData(SQLiteConnection conn, SQLiteTransaction tran, bool isAttached = false);
    }
}
