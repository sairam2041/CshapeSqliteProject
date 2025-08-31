using WpfApp.Repositories.Interfaces;

namespace WpfApp.Models
{
    class TableReplaceModel
    {
        public string TableName { get; set; }
        public List<Dictionary<string, object>> InsertData { get; }

        private readonly ITableDataReplaceDao _dao;

        public TableReplaceModel(string tableName, List<Dictionary<string, object>> insertData, ITableDataReplaceDao dao)
        {
            TableName = tableName;
            InsertData = insertData;
            _dao = _dao;
        }

        public void ExecuteRelpace()
        {
            _dao.ReplaceTableData();
        }
    }
}
