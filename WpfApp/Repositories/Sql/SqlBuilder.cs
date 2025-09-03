using WpfApp.Models;

namespace WpfApp.Repositories.Sql
{
    public static class SqlBuilder
    {
        public static string BuildDeleteQuery(string tableName) => $"DELETE FROM {tableName};";

<<<<<<< HEAD
        public static string BuildInsertQuery(string tableName, IEnumerable<IDictionary<string, object>> insertData)
=======
        public static string BuildInsertQuery(string tableName, IEnumerable<Dictionary<string, object>> insertData)
>>>>>>> e9d79d644fc75ec0d4af6d9d843a1de5b305f45d
        {
            // 最初の1行目のキーを使ってプレースホルダーを構築
            var firstRow = insertData.FirstOrDefault();

            if (firstRow == null || firstRow.Count == 0)
            {
                throw new ArgumentException("insertData に有効なデータが含まれていません。");
            }

            var placeholders = string.Join(", ", firstRow.Keys.Select(key => $"${key}"));

            return $"INSERT INTO {tableName} VALUES ({placeholders});";
        }

<<<<<<< HEAD
        public static List<SqlParameterSet> BuildPlaceholders(IEnumerable<IDictionary<string, object>> insertData)
=======
        public static List<SqlParameterSet> BuildPlaceholders(IEnumerable<Dictionary<string, object>> insertData)
>>>>>>> e9d79d644fc75ec0d4af6d9d843a1de5b305f45d
        {
            var result = new List<SqlParameterSet>();

            foreach (var row in insertData)
            {
                var paramList = new List<Dictionary<string, object>>();

                foreach (var kv in row)
                {
                    paramList.Add(new Dictionary<string, object>
            {
                { $"${kv.Key}", kv.Value }
            });
                }

                result.Add(new SqlParameterSet
                {
                    Parameters = paramList
                });
            }

            return result;
        }
    }
}
