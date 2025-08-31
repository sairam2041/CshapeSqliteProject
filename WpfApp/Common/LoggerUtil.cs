using System.IO;

namespace WpfApp.Common
{
    class LoggerUtil : IDisposable
    {
        private readonly StreamWriter _wirter;
        private bool _disposed = false;
        private readonly string logFilePath = "../Log/app.log";

        /// <summary>
        /// ログ出力Utilクラス。usingを使って呼び出せば解放処理を自動で行ってくれる。
        /// </summary>
        /// <param name="path">出力ログファイルのパス。ファイル名も記載する。相対パスでも絶対パスでも可。</param>
        public LoggerUtil(string? path = null)
        {
            string _path = Path.Combine(AppContext.BaseDirectory, path ?? logFilePath);
            createLogDirectory(_path);

            _wirter = new StreamWriter(_path, append: true)
            {
                AutoFlush = true
            };
        }

        public void Info(string message)
        {
            WriteLog("INFO", message);
        }

        public void Warn(string message)
        {
            WriteLog("WARN", message);
        }

        public void Error(string message)
        {
            WriteLog("ERROR", message);
        }

        private void WriteLog(string level, string message)
        {
            var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";
            _wirter.WriteLine(logEntry);
        }

        private void createLogDirectory(string path)
        {
            string fullPath = Path.GetFullPath(path);
            string? dirPath = Path.GetDirectoryName(fullPath);

            if (!string.IsNullOrEmpty(dirPath)) 
            { 
                Directory.CreateDirectory(dirPath);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            // これいるか？
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _wirter?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
