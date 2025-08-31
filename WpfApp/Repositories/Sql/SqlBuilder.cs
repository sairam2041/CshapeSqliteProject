using WpfApp.Models;

namespace WpfApp.Repositories.Sql
{
    public static class SqlBuilder
    {
        public static string BuildDeleteQuery(string tableName) => $"DELETE FROM {tableName};";

        public static string BuildInsertQuery(string tableName, IEnumerable<KeyValuePair<string, object>> insertData)
        {
            var placeholders = string.Join(", ", insertData.Select(kv => $"${kv.Key}"));
            return $"INSERT INTO {tableName} VALUES ({placeholders});";
        }

        public static List<SqlParameterSet> BuildPlaceholders(IEnumerable<KeyValuePair<string, object>> insertData)
        {
            var placeholderParams = insertData.Select(kv => new KeyValuePair<string, object>($"${kv.Key}", kv.Value)).ToList();
            
            return  new List<SqlParameterSet> {
                new SqlParameterSet { Parameters = placeholderParams }
            };
        }
    }
}
