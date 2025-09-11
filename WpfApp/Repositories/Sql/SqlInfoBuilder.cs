using WpfApp.Dto;

namespace WpfApp.Repositories.Sql
{
    public static class SqlInfoBuilder
    {
        private static string prefix = "$";
        public static SqlInfoDto BuildDeleteInfo(string tableName) => new SqlInfoDto() { Query = $"DELETE FROM {tableName};", ValueSet = null };

        public static SqlInfoDto BuildInsertInfo(string tableName, IEnumerable<IDictionary<string, object>> value)
        {
            var valueSet = value.Select(dict => dict.ToDictionary(pair => $"{prefix}{pair.Key}", pair => pair.Value)).ToList();

            var firstRow = valueSet.FirstOrDefault();
            if (firstRow == null || firstRow.Count == 0)
            {
                throw new ArgumentException("insertData に有効なデータが含まれていません。");
            }

            var placeholders = string.Join(", ", firstRow.Keys);

            return new SqlInfoDto { Query = $"INSERT INTO {tableName} VALUES ({placeholders});", ValueSet = valueSet };
        }

        public static SqlInfoDto BuildSelectInfo(string tableName, IEnumerable<string>? column, IDictionary<string, object>? where)
        {
            var selectValue = column is null ? "*" : String.Join(", ", column);
           
            if(where is not null)
            {
                var whereSet = where.ToDictionary(pair => $"{prefix}{pair.Key}_w", pair => pair.Value);
                var whereValue = String.Join(" AND ", whereSet.ToList());

                return new SqlInfoDto { Query = $"SELECT {selectValue} FROM {tableName} WHERE {whereValue};", ValueSet = new List<IDictionary<string, object>>() { whereSet } };
            }
            return new SqlInfoDto { Query = $"SELECT {selectValue} FROM {tableName};", ValueSet = null };
        }

        public static SqlInfoDto BuildUpdateInfo(string tableName, IDictionary<string, object> set, IDictionary<string, object>? where)
        {
            var setValue = string.Join(", ", set.Select(kv => $"{kv.Key} = {prefix}{kv.Key}_s"));
            var setSet = set.ToDictionary(pair => $"{prefix}{pair.Key}_s", pair => pair.Value);
           
            if (where is not null)
            {
                var whereSet = where.ToDictionary(pair => $"{prefix}{pair.Key}_w", pair => pair.Value);
                var whereValue = String.Join(" AND ", whereSet.ToList());

                var mergeSet = setSet.Concat(whereSet).ToDictionary(pair => pair.Key, pair => pair.Value);

                return new SqlInfoDto { Query = $"UPDATE {tableName} SET {setValue} WHERE {whereValue};", ValueSet = new List<IDictionary<string, object>>() { mergeSet } };
            }
            return new SqlInfoDto { Query = $"UPDATE {tableName} SET {setValue};", ValueSet = new List<IDictionary<string, object>>() { setSet } };
        }
    }
}
