using System.Data.SQLite;
using WpfApp.Models;

namespace WpfApp.Repositories
{
    public class DbExecutor
    {
        private SQLiteConnection _conn;
        SQLiteTransaction _tran;
        
        public DbExecutor(SQLiteConnection conn, SQLiteTransaction tran)
        {
            _conn = conn;
            _tran = tran;
        }

        public void ExecuteAll(List<SqlInfoDto> sqlInfoList)
        {
            try
            {
                foreach (SqlInfoDto sqlInfo in sqlInfoList)
                {
                    using var command = _conn.CreateCommand();
                    command.CommandText = sqlInfo.SqlQuery;
                    command.Transaction = _tran;

                    if (sqlInfo.ValueParameterSets is null)
                    {
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        // SQL情報を解析して使いまわす
                        command.Prepare();
                        foreach (var data in sqlInfo.ValueParameterSets)
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
            }
            catch
            {
                _tran.Rollback();
                throw;
            }
        }
    }
}
