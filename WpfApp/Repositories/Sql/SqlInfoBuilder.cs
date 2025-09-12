using WpfApp.Dto;

namespace WpfApp.Repositories.Sql
{
    public class SqlQueryParametersDto
    {
        public IEnumerable<IDictionary<string, object>>? InsertValue { get; set; }
        public IEnumerable<string>? SelectColumn { get; set; }
        public IDictionary<string, object>? WhereValue  { get; set; }
        public IDictionary<string, object>? UpdateValue { get; set; }
    }

    public static class SqlInfoBuilder
    {
        private static string prefix = "$";
        public static SqlInfoDto BuildDeleteInfo(string tableName, SqlQueryParametersDto dto)
        {
            if (dto.WhereValue is not null)
            {
                var whereSet = dto.WhereValue.ToDictionary(pair => $"{prefix}{pair.Key}_w", pair => pair.Value);
                var whereValue = String.Join(" AND ", whereSet.ToList());

                return new SqlInfoDto { Query = $"DELETE FROM {tableName} WHERE {whereValue};", ValueSet = new List<IDictionary<string, object>>() { whereSet } };
            }
            return new SqlInfoDto() { Query = $"DELETE FROM {tableName};", ValueSet = null };
        }

        public static SqlInfoDto BuildInsertInfo(string tableName, SqlQueryParametersDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto.InsertValue);

            var valueSet = dto.InsertValue.Select(dict => dict.ToDictionary(pair => $"{prefix}{pair.Key}", pair => pair.Value)).ToList();

            var firstRow = valueSet.FirstOrDefault();
            if (firstRow == null || firstRow.Count == 0)
            {
                throw new ArgumentException("insertData に有効なデータが含まれていません。");
            }

            var placeholders = string.Join(", ", firstRow.Keys);

            return new SqlInfoDto { Query = $"INSERT INTO {tableName} VALUES ({placeholders});", ValueSet = valueSet };
        }

        public static SqlInfoDto BuildSelectInfo(string tableName, SqlQueryParametersDto dto)
        {
            var selectValue = dto.SelectColumn is null ? "*" : String.Join(", ", dto.SelectColumn);
           
            if(dto.WhereValue is not null)
            {
                var whereSet = dto.WhereValue.ToDictionary(pair => $"{prefix}{pair.Key}_w", pair => pair.Value);
                var whereValue = String.Join(" AND ", whereSet.ToList());

                return new SqlInfoDto { Query = $"SELECT {selectValue} FROM {tableName} WHERE {whereValue};", ValueSet = new List<IDictionary<string, object>>() { whereSet } };
            }
            return new SqlInfoDto { Query = $"SELECT {selectValue} FROM {tableName};", ValueSet = null };
        }

        public static SqlInfoDto BuildUpdateInfo(string tableName, SqlQueryParametersDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto.UpdateValue);

            var setValue = string.Join(", ", dto.UpdateValue.Select(kv => $"{kv.Key} = {prefix}{kv.Key}_s"));
            var setSet = dto.UpdateValue.ToDictionary(pair => $"{prefix}{pair.Key}_s", pair => pair.Value);
           
            if (dto.WhereValue is not null)
            {
                var whereSet = dto.WhereValue.ToDictionary(pair => $"{prefix}{pair.Key}_w", pair => pair.Value);
                var whereValue = String.Join(" AND ", whereSet.ToList());

                var mergeSet = setSet.Concat(whereSet).ToDictionary(pair => pair.Key, pair => pair.Value);

                return new SqlInfoDto { Query = $"UPDATE {tableName} SET {setValue} WHERE {whereValue};", ValueSet = new List<IDictionary<string, object>>() { mergeSet } };
            }
            return new SqlInfoDto { Query = $"UPDATE {tableName} SET {setValue};", ValueSet = new List<IDictionary<string, object>>() { setSet } };
        }
    }
}
