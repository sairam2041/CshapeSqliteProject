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

        public void ExecuteRelpace()
        {
            var dbFileName= ((BaseDao)_daoList.First()).SchemmaName;
            HashSet<string> dbFiles = new HashSet<string>() { dbFileName };

            //usingは１行で書けるし、こちらの方が見やすい。スコープを明示的にしたい時のみ{ }
            //の方使おう。
            string fullPath = Path.GetFullPath($"Db/{dbFileName}");

            // @を付けると日本語のエスケープが不要になる
            using var connection = new SQLiteConnection(@$"Data Source={fullPath};Version=3;");
            connection.Open();

            var tran = connection.BeginTransaction();

            foreach (var dao in _daoList)
            {
                var _dbFile = ((BaseDao)dao).SchemmaName;
                var needAttach = !dbFiles.Contains(_dbFile);
                if (needAttach) {
                    var attachCommand = new SQLiteCommand($"ATTACH DATABASE {_dbFile} AS {Path.GetFileNameWithoutExtension(_dbFile)}", connection);
                    attachCommand.ExecuteNonQuery();

                    dbFiles.Add(_dbFile);
                }

                dao.ReplaceTableData(connection, tran, needAttach);
            }
        }
    }
}
