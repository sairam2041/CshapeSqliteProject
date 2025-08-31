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

        public void ExecuteAll(List<KeyValuePair<string, object?>> sqlInfo)
        {
            // SQLite接続と実行処理をここに記述
        }
    }
}
