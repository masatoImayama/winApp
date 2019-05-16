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
    class FamilyStructure
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FamilyStructure()
        {
            String AppPath = Common.GetCurrentAppDir();
            SQLiteAdapter.Connect(AppPath + Properties.Settings.Default.DBPath);
        }

        /// <summary>
        /// 一覧検索
        /// </summary>
        public ObservableCollection<FamilyStructureVM> ListSearch(SearchCondDto searchCond)
        {
            FamilyStructureVM FamilyStructureData = new FamilyStructureVM();
            ObservableCollection<FamilyStructureVM> FamilyStructureListData = new ObservableCollection<FamilyStructureVM>();

            // 一覧取得
            // SQL文生成
            StringBuilder SqlStb = new StringBuilder();
            SqlStb.Append(" SELECT principalFlg,name,nameKana,relationshipName,birthday,honmeiseiCode FROM familyStructure ");

            List<Object> prmsList = new List<Object>();
            List<String> whereList = new List<string>();

            if (!searchCond.ClientId.Equals(String.Empty))
            {
                whereList.Add(" clientId = ? ");
                prmsList.Add(searchCond.ClientId);
            }
            else
            {
                return FamilyStructureListData;
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

            foreach (var item in result)
            {
                FamilyStructureData = new FamilyStructureVM();

                if (item[0] == "0")
                {
                    FamilyStructureData.PrincipalFlg = false;
                }
                else
                {
                    FamilyStructureData.PrincipalFlg = true;
                }

                FamilyStructureData.Name = item[1];
                FamilyStructureData.NameKana = item[2];
                FamilyStructureData.Relation = item[3];
                FamilyStructureData.Birthday = this.DateFormatSlush(item[4]);
                FamilyStructureData.BirthdayYearVal = item[4].Substring(0, 4);
                FamilyStructureData.BirthdayMonthVal = item[4].Substring(4, 2);
                FamilyStructureData.BirthdayDayVal = item[4].Substring(6, 2);
                FamilyStructureData.Honmeisei = item[5];
                FamilyStructureData.IsChenged = false;
                FamilyStructureListData.Add(FamilyStructureData);
            }

            return FamilyStructureListData;
        }

        public void RegistFamilyStructure(IList<FamilyStructureDto> FamilyStructureData)
        {
            IDictionary<String, object> insDtDic;
            foreach (FamilyStructureDto item in FamilyStructureData)
            {
                insDtDic = new Dictionary<string, object>();
                insDtDic.Add("clientId", item.ClientId);
                insDtDic.Add("principalFlg", item.PrincipalFlg);
                insDtDic.Add("name", item.Name);
                insDtDic.Add("nameKana", item.NameKana);
                insDtDic.Add("relationshipName", item.Relation);
                insDtDic.Add("birthday", item.Birthday);
                insDtDic.Add("honmeiseiCode", item.Honmeisei);
                insDtDic.Add("orderNo", item.OrderNo);

                // SQL文生成
                StringBuilder SqlStb = new StringBuilder();
                SqlStb.Append("INSERT INTO familyStructure (");
                SqlStb.Append(String.Join(",", insDtDic.Keys.ToArray()));
                SqlStb.Append(") VALUES (");
                SqlStb.Append(SQLiteAdapter.Instance.ChainSeparateStr(insDtDic.Count, "?", ","));
                SqlStb.Append(" )");

                SQLiteAdapter.Instance.ExecuteInsert(SqlStb.ToString(), insDtDic.Values.ToArray());
            }
        }

        public void DeleteFamilyStructure(int clientId)
        {
            StringBuilder SqlStb = new StringBuilder();
            SqlStb.Append("DELETE FROM familyStructure WHERE clientId = ? ");
            SQLiteAdapter.Instance.ExecuteNonQuery(SqlStb.ToString(), new object[] { clientId });

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

        /// <summary>
        /// 生年月日_年コンボ取得
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ComboItem> GetBirthdayYearComboList()
        {
            ObservableCollection<ComboItem> birthdayYearComboItems = new ObservableCollection<ComboItem>();

            int i = 0;
            String tmpStr = String.Empty;
            int startYear = 1920;
            int endYear = DateTime.Today.Year;
            for (i = startYear; i <= endYear; i++)
            {
                tmpStr = i.ToString();
                birthdayYearComboItems.Add(new ComboItem { Value = tmpStr, Display = i.ToString() + "年" });
            }

            return birthdayYearComboItems;
        }

        /// <summary>
        /// 生年月日_月コンボ取得
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ComboItem> GetBirthdayMonthComboList()
        {
            ObservableCollection<ComboItem> birthdayMonthComboItems = new ObservableCollection<ComboItem>();

            int i = 0;
            String tmpStr = String.Empty;
            for (i = 1; i <= 12; i++)
            {
                tmpStr = "0" + i.ToString();
                birthdayMonthComboItems.Add(new ComboItem { Value = tmpStr.Substring(tmpStr.Length - 2, 2), Display = i.ToString() + "月" });
            }

            return birthdayMonthComboItems;
        }

        /// <summary>
        /// 生年月日_日コンボ取得
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ComboItem> GetBirthdayDayComboList()
        {
            ObservableCollection<ComboItem> birthdayDayComboItems = new ObservableCollection<ComboItem>();

            int i = 0;
            String tmpStr = String.Empty;
            for (i = 1; i <= 31; i++)
            {
                tmpStr = "0" + i.ToString();
                birthdayDayComboItems.Add(new ComboItem { Value = tmpStr.Substring(tmpStr.Length - 2, 2), Display = i.ToString() + "日" });
            }

            return birthdayDayComboItems;
        }
    }
}
