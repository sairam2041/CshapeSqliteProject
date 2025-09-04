using WpfApp.Repositories.Interfaces;
using System.Data.SQLite;
using System.IO;

namespace WpfApp.Models
{
    class TableReplaceModel
    {
        private readonly List<ITableDataReplaceDao> _daoList;

        public TableReplaceModel(List<ITableDataReplaceDao> daoList)
        {
            _daoList = daoList;
        }

        public void ExecuteRelpace()
        {
            // 最終的には、複数DBファイルをアタッチしてコネクションを作成する形にしたい。
            string dbPath = "Db/sample.db";
            //usingは１行で書けるし、こちらの方が見やすい。スコープを明示的にしたい時のみ{ }
            //の方使おう。
            string fullPath = Path.GetFullPath(dbPath);

            // @を付けると日本語のエスケープが不要になる
            using var connection = new SQLiteConnection(@$"Data Source={fullPath};Version=3;");
            connection.Open();

            var tran = connection.BeginTransaction();

            foreach (var dao in _daoList)
            {
                dao.ReplaceTableData(connection, tran);
            }
        }
    }
}
