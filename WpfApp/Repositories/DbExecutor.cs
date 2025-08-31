using System.Data.SQLite;
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
            using var connection = new SQLiteConnection(_dbPath);
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
                            foreach (var plh in data.Parameters)
                            {
                                command.Parameters.AddWithValue(plh.Key, plh.Value);
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
