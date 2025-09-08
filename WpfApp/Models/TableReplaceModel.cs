using WpfApp.Repositories.Interfaces;
using System.Data.SQLite;
using System.IO;
using WpfApp.Repositories.Base;

namespace WpfApp.Models
{
    class TableReplaceModel
    {
        private readonly List<ITableDataReplaceDao> _daoList;

        public TableReplaceModel(List<ITableDataReplaceDao> daoList)
        {
            _daoList = daoList;
        }

        // これテーブル洗い変えのみしか対応していない。
        // コマンドとDB操作を分別すれば、他のコマンドも対応できるように出来る気がする
        public void ExecuteRelpace()
        {
            // mainのDBファイル名
            var mainDbFile = ((BaseDao)_daoList.First()).SchemmaName;
            HashSet<string> dbFiles = new HashSet<string>() { mainDbFile };

            //usingは１行で書けるし、こちらの方が見やすい。スコープを明示的にしたい時のみ{ }
            //の方使おう。
            string fullPath = Path.GetFullPath($"Db/{mainDbFile}");

            // @を付けると日本語のエスケープが不要になる
            using var connection = new SQLiteConnection(@$"Data Source={fullPath};Version=3;");
            connection.Open();

            var tran = connection.BeginTransaction();

            foreach (var dao in _daoList)
            {
                var dbFile = ((BaseDao)dao).SchemmaName;
                if (!dbFiles.Contains(dbFile)) {
                    var attachCommand = new SQLiteCommand($"ATTACH DATABASE '{Path.GetFullPath($"Db/{dbFile}")}' AS {Path.GetFileNameWithoutExtension(dbFile)};", connection);
                    attachCommand.ExecuteNonQuery();

                    dbFiles.Add(dbFile);
                }

                // mainDBファイルと異なる場合はアタッチしたDBファイルと見なす
                dao.ReplaceTableData(connection, tran, (mainDbFile != dbFile));
            }

            tran.Commit();
        }
    }
}
