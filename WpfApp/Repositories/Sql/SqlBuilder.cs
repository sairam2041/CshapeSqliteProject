using WpfApp.Models;

namespace WpfApp.Repositories.Sql
{
    public static class SqlBuilder
    {
        public static string BuildDeleteQuery(string tableName) => $"DELETE FROM {tableName};";

        public static string BuildInsertQuery(string tableName, IEnumerable<IDictionary<string, object>> insertData)
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

        public static List<SqlParameterSet> BuildPlaceholders(IEnumerable<IDictionary<string, object>> insertData)
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
