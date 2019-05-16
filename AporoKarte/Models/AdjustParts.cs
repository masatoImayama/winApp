using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLiteManager;
using AporoKarte.ViewModels;

namespace AporoKarte.Models
{
    class AdjustParts
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AdjustParts()
        {
            String AppPath = Common.GetCurrentAppDir();
            SQLiteAdapter.Connect(AppPath + Properties.Settings.Default.DBPath);
        }

        /// <summary>
        /// 一覧検索
        /// </summary>
        public ObservableCollection<AdjustPartsVM> ListSearch(SearchCondDto searchCond)
        {
            AdjustPartsVM AdjustPartsData = new AdjustPartsVM();
            ObservableCollection<AdjustPartsVM> AdjustPartsListData = new ObservableCollection<AdjustPartsVM>();

            // 一覧取得
            // SQL文生成
            StringBuilder SqlStb = new StringBuilder();
            SqlStb.Append(" SELECT adjustYmd,partsCode,count FROM adjustParts ");

            List<Object> prmsList = new List<Object>();
            List<String> whereList = new List<string>();

            if (!searchCond.ClientId.Equals(String.Empty))
            {
                whereList.Add(" clientId = ? ");
                prmsList.Add(searchCond.ClientId);
            }
            else
            {
                return AdjustPartsListData;
            }


            if (whereList.Count > 0)
            {
                SqlStb.Append(" WHERE ");
                SqlStb.Append(string.Join(" AND ", whereList));
            }

            //ORDERBY句生成
            SqlStb.Append(" ORDER BY orderNo ASC");

            //取得処理
            String[][] result = SQLiteAdapter.Instance.ExecuteReader(SqlStb.ToString(), prmsList.ToArray());

            // 調整備品ディクショナリ取得
            IDictionary<String,String> adjustPartsDic = this.GetAdjustPartsDic();

            foreach (var item in result)
            {
                AdjustPartsData = new AdjustPartsVM();

                AdjustPartsData.AdjustYmd = this.DateFormatSlush(item[0].Replace("/", ""));
                AdjustPartsData.PartsCode = item[1];
                AdjustPartsData.Count = int.Parse(item[2]);
                AdjustPartsData.IsChenged = false;
                AdjustPartsData.PartsName = adjustPartsDic[AdjustPartsData.PartsCode];

                AdjustPartsListData.Add(AdjustPartsData);
            }

            return AdjustPartsListData;
        }

        public void RegistAdjustParts(IList<AdjustPartsDto> AdjustPartsData)
        {
            IDictionary<String, object> insDtDic;
            foreach (AdjustPartsDto item in AdjustPartsData)
            {
                insDtDic = new Dictionary<string, object>();
                insDtDic.Add("clientId", item.ClientId);
                insDtDic.Add("adjustYmd", item.AdjustYmd);
                insDtDic.Add("partsCode", item.PartsCode);
                insDtDic.Add("count", item.Count);
                insDtDic.Add("orderNo", item.OrderNo);

                // SQL文生成
                StringBuilder SqlStb = new StringBuilder();
                SqlStb.Append("INSERT INTO adjustParts (");
                SqlStb.Append(String.Join(",", insDtDic.Keys.ToArray()));
                SqlStb.Append(") VALUES (");
                SqlStb.Append(SQLiteAdapter.Instance.ChainSeparateStr(insDtDic.Count, "?", ","));
                SqlStb.Append(" )");

                SQLiteAdapter.Instance.ExecuteInsert(SqlStb.ToString(), insDtDic.Values.ToArray());
            }
        }

        public void DeleteAdjustParts(int clientId)
        {
            StringBuilder SqlStb = new StringBuilder();
            SqlStb.Append("DELETE FROM adjustParts WHERE clientId = ? ");
            SQLiteAdapter.Instance.ExecuteNonQuery(SqlStb.ToString(), new object[] { clientId });

        }

        public ObservableCollection<ComboItem> GetAdjustPartsComboItems()
        {
            ObservableCollection<ComboItem>  rtnAdjustPartsComboItems = new ObservableCollection<ComboItem>();
            List<Object> prmsList = new List<Object>();

            // 一覧取得
            // SQL文生成
            StringBuilder SqlStb = new StringBuilder();
            SqlStb.Append(" SELECT partsCode,partsName FROM partsMaster ORDER BY orderNo ASC");

            //取得処理
            String[][] result = SQLiteAdapter.Instance.ExecuteReader(SqlStb.ToString(), prmsList.ToArray());

            foreach (var item in result)
            {
                rtnAdjustPartsComboItems.Add(new ComboItem { Value = item[0], Display = item[1] });
            }

            return rtnAdjustPartsComboItems;
        }

        /// <summary>
        /// 備品コンボ取得
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ComboItem> GetAdjustPartsComboList()
        {
            ObservableCollection<ComboItem> adjustPartsComboItems = new ObservableCollection<ComboItem>();

            // 備品マスタから取得してデータ作成
            adjustPartsComboItems = this.GetAdjustPartsComboItems();

            return adjustPartsComboItems;
        }


        /// <summary>
        /// 備品ディクショナリ取得
        /// </summary>
        /// <returns></returns>
        public IDictionary<String,String> GetAdjustPartsDic()
        {
            IDictionary<String,String> adjustPartsDic = new Dictionary<String,String>();

            // 備品マスタから取得してデータ作成
            foreach (ComboItem item in this.GetAdjustPartsComboItems())
            {
                adjustPartsDic.Add(item.Value, item.Display);
            }

            return adjustPartsDic;
        }

        private String DateFormatSlush(String sourceDate)
        {
            if (sourceDate != null && !sourceDate.Equals(String.Empty))
            {
                return sourceDate.Substring(0, 4) + "/" + sourceDate.Substring(4, 2) + "/" + sourceDate.Substring(6, 2);
            }
            else
            {
                return String.Empty;
            }

        }
    }
}
