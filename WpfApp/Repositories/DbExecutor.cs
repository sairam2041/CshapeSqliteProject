using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using WpfApp.Models;

namespace WpfApp.Repositories
{
    public class DbExecutor
    {
        private string _dbPath;
        public DbExecutor(string dbPath)
        {
            // 一旦文字列DBパス情報を保持する形に
            _dbPath = dbPath;
        }

        public void ExecuteAll(List<SqlInfoDto> sqlInfoList)
        {
            // usingは１行で書けるし、こちらの方が見やすい。スコープを明示的にしたい時のみ{}の方使おう。
            string fullPath = Path.GetFullPath(_dbPath);

            // @を付けると日本語のエスケープが不要になる
            using var connection = new SQLiteConnection(@$"Data Source={fullPath};Version=3;");
            connection.Open();

            var tran = connection.BeginTransaction();
            try
            {
                foreach (SqlInfoDto sqlInfo in sqlInfoList)
                {
                    using var command = connection.CreateCommand();
                    command.CommandText = sqlInfo.sqlQuery;
                    command.Transaction = tran;

                    if (sqlInfo.ParameterSets is null)
                    {
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        command.Prepare();
                        foreach (var data in sqlInfo.ParameterSets)
                        {
                            foreach (var param in data.Parameters)
                            {
                                foreach(var cl in param)
                                {
                                    command.Parameters.AddWithValue(cl.Key, cl.Value);
                                }
                            }
                            command.ExecuteNonQuery();
                        }
                    }
                }
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }
    }
}
