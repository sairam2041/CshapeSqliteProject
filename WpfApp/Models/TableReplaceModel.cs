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
            // 最終的には、複数DBファイルをアタッチしてコネクションを作成する形にしたい。
            string dbPath = "";

            foreach(var dao in _daoList)
            {
                dao.ReplaceTableData(dbPath);
            }
        }
    }
}
