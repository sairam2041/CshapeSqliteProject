using WpfApp.Repositories.Interfaces;

namespace WpfApp.Models
{
    class TableReplaceModel
    {
        private readonly List<ITableDataReplaceDao> _daoList;

        public TableReplaceModel(List<ITableDataReplaceDao> daoList)
        {
            _daoList = daoList;
        }

        public void ExecuteRelpace()
        {
            foreach(var dao in _daoList)
            {
                dao.ReplaceTableData();
            }
        }
    }
}
