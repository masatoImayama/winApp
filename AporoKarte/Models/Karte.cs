using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLiteManager;
using Microsoft.Office.Interop.Excel;
using AporoKarte.ViewModels;
using System.Runtime.InteropServices;

namespace AporoKarte.Models
{
    class Karte
    {
        private FamilyStructure MdlFamilyStructure;
        private AdjustParts MdlAdjustParts;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Karte()
        {
            String AppPath = Common.GetCurrentAppDir();
            SQLiteAdapter.Connect(AppPath + Properties.Settings.Default.DBPath);
            MdlFamilyStructure = new FamilyStructure();
            MdlAdjustParts = new AdjustParts();
        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        ~Karte()
        {
            //SQLiteAdapter.Disconnect();
        }

        /// <summary>
        /// 一覧検索
        /// </summary>
        public ObservableCollection<KarteDetailDto> ListSearch(SearchCondDto searchCond)
        {
            KarteDetailDto KarteDetailData = new KarteDetailDto();
            ObservableCollection<KarteDetailDto> KarteListData = new ObservableCollection<KarteDetailDto>();

            // 一覧取得
            // SQL文生成
            StringBuilder SqlStb = new StringBuilder();
            SqlStb.Append(" SELECT clientId,clientName,introductorName,addressPref,addressCity,tel,adjustYmd,confirmYmd,adjustAmount FROM client ");

            List<Object> prmsList = new List<Object>();
            List<String> whereList = new List<string>();

            if (!searchCond.ClientName.Equals(String.Empty))
            {
                whereList.Add(" (clientName LIKE ? OR clientNameKana LIKE ?) ");
                prmsList.Add("%" + searchCond.ClientName + "%");
                prmsList.Add("%" + searchCond.ClientName + "%");
            }

            if (!searchCond.AdjustYmdStart.Equals(String.Empty) || !searchCond.AdjustYmdEnd.Equals(String.Empty))
            {
                String AdjustYmdStart = "19700101";
                String AdjustYmdEnd = "29991231";
                if (!searchCond.AdjustYmdStart.Equals(String.Empty)) { AdjustYmdStart = searchCond.AdjustYmdStart.Replace("/", ""); }
                if (!searchCond.AdjustYmdEnd.Equals(String.Empty)) { AdjustYmdEnd = searchCond.AdjustYmdEnd.Replace("/", ""); }
                whereList.Add(" adjustYmd BETWEEN  ? AND ? ");
                prmsList.Add(AdjustYmdStart);
                prmsList.Add(AdjustYmdEnd);
            }

            if (whereList.Count > 0)
            {
                SqlStb.Append(" WHERE ");
                SqlStb.Append(string.Join(" AND ", whereList));
            }

            //ORDERBY句生成
            SqlStb.Append(" ORDER BY adjustYmd DESC, ClientNameKana ASC");

            //取得処理
            String[][] result = SQLiteAdapter.Instance.ExecuteReader(SqlStb.ToString(), prmsList.ToArray());

            foreach (var item in result)
            {
                KarteDetailData = new KarteDetailDto();
                KarteDetailData.ClientId = item[0];
                KarteDetailData.ClientName = item[1];
                KarteDetailData.IntroductorName = item[2];
                KarteDetailData.AddressPref = item[3];
                KarteDetailData.AddressCity = item[4];
                KarteDetailData.Tel = item[5];
                KarteDetailData.AdjustYmd = this.DateFormatSlush(item[6]);
                KarteDetailData.ConfirmYmd = this.DateFormatSlush(item[7]);
                KarteDetailData.AdjustAmount = int.Parse(item[8]);

                KarteListData.Add(KarteDetailData);
            }

            return KarteListData;
        }

        public KarteDetailDto SearchKarteDetail(SearchCondDto searchCond)
        {
            KarteDetailDto KarteDetailData = new KarteDetailDto();

            // 詳細取得
            // SQL文生成
            StringBuilder SqlStb = new StringBuilder();
            SqlStb.Append(" SELECT * FROM client ");

            List<Object> prmsList = new List<Object>();
            List<String> whereList = new List<string>();

            if (!searchCond.ClientId.Equals(String.Empty))
            {
                whereList.Add(" clientId = ? ");
                prmsList.Add(searchCond.ClientId);
            }
            else
            {
                return KarteDetailData;
            }

            if (whereList.Count > 0)
            {
                SqlStb.Append(" WHERE ");
                SqlStb.Append(string.Join(" AND ", whereList));
            }

            //取得処理
            String[][] result = SQLiteAdapter.Instance.ExecuteReader(SqlStb.ToString(), prmsList.ToArray());

            if (result.Length > 0)
            {
                KarteDetailData.ClientId = result[0][0];
                KarteDetailData.ClientName = result[0][1];
                KarteDetailData.ClientNameKana = result[0][2];
                KarteDetailData.IntroductorName = result[0][3];
                KarteDetailData.IntroductorNameKana = result[0][4];
                KarteDetailData.PostCode = result[0][5];
                KarteDetailData.AddressPref = result[0][6];
                KarteDetailData.AddressCity = result[0][7];
                KarteDetailData.AddressOther = result[0][8];
                KarteDetailData.AddressKana = result[0][9];
                KarteDetailData.Tel = result[0][10];
                KarteDetailData.Fax = result[0][11];
                KarteDetailData.ConsultationYmd = result[0][12];
                KarteDetailData.OpinionYmd = result[0][13];

                KarteDetailData.AdjustPostCode = result[0][14];
                KarteDetailData.AdjustAddressPref = result[0][15];
                KarteDetailData.AdjustAddressCity = result[0][16];
                KarteDetailData.AdjustAddressOther = result[0][17];
                KarteDetailData.AdjustAddressKana = result[0][18];
                KarteDetailData.AdjustYmd = result[0][19];
                KarteDetailData.AdjustAmount = int.Parse(result[0][20]);
                KarteDetailData.ConfirmYmd = result[0][21];
                KarteDetailData.LandArea = result[0][22];
                KarteDetailData.ChainClientMemo = result[0][23];
                KarteDetailData.OtherMemo = result[0][24];
                KarteDetailData.AddressComparisonFlg = int.Parse(result[0][25]);
            }

            return KarteDetailData;
        }

        public int RegistKarte(KarteDetailDto karteDetailData)
        {
            int incrementClientId = 0;

            IDictionary<String, object> insDtDic = new Dictionary<string, object>();
            insDtDic.Add("clientName", karteDetailData.ClientName);
            insDtDic.Add("clientNameKana", karteDetailData.ClientNameKana);
            insDtDic.Add("introductorName", karteDetailData.IntroductorName);
            insDtDic.Add("introductorNameKana", karteDetailData.IntroductorNameKana);
            insDtDic.Add("postCode", karteDetailData.PostCode);
            insDtDic.Add("addressPref", karteDetailData.AddressPref);
            insDtDic.Add("addressCity", karteDetailData.AddressCity);
            insDtDic.Add("addressOther", karteDetailData.AddressOther);
            insDtDic.Add("addressKana", karteDetailData.AddressKana);
            insDtDic.Add("tel", karteDetailData.Tel);
            insDtDic.Add("fax", karteDetailData.Fax);
            insDtDic.Add("consultationYmd", karteDetailData.ConsultationYmd);
            insDtDic.Add("opinionYmd", karteDetailData.OpinionYmd);
            insDtDic.Add("adjustPostCode", karteDetailData.PostCode);
            insDtDic.Add("adjustAddressPref", karteDetailData.AddressPref);
            insDtDic.Add("adjustAddressCity", karteDetailData.AddressCity);
            insDtDic.Add("adjustAddressOther", karteDetailData.AddressOther);
            insDtDic.Add("adjustAddressKana", karteDetailData.AddressKana);
            insDtDic.Add("adjustYmd", karteDetailData.AdjustYmd);
            insDtDic.Add("adjustAmount", karteDetailData.AdjustAmount);
            insDtDic.Add("confirmYmd", karteDetailData.ConfirmYmd);
            insDtDic.Add("landArea", karteDetailData.LandArea);
            insDtDic.Add("chainClientMemo", karteDetailData.ChainClientMemo);
            insDtDic.Add("otherMemo", karteDetailData.OtherMemo);
            insDtDic.Add("addressCamparisonFlg", karteDetailData.AddressComparisonFlg);

            // SQL文生成
            StringBuilder SqlStb = new StringBuilder();
            SqlStb.Append("INSERT INTO client (");
            SqlStb.Append(String.Join(",", insDtDic.Keys.ToArray()));
            SqlStb.Append(") VALUES (");
            SqlStb.Append(SQLiteAdapter.Instance.ChainSeparateStr(insDtDic.Count, "?", ","));
            SqlStb.Append(" )");

            incrementClientId = SQLiteAdapter.Instance.ExecuteInsert(SqlStb.ToString(), insDtDic.Values.ToArray());

            return incrementClientId;
        }
        public void UpdateKarte(KarteDetailDto karteDetailData)
        {

            IDictionary<String, object> updDtDic = new Dictionary<string, object>();
            updDtDic.Add("clientName", karteDetailData.ClientName);
            updDtDic.Add("clientNameKana", karteDetailData.ClientNameKana);
            updDtDic.Add("introductorName", karteDetailData.IntroductorName);
            updDtDic.Add("introductorNameKana", karteDetailData.IntroductorNameKana);
            updDtDic.Add("postCode", karteDetailData.PostCode);
            updDtDic.Add("addressPref", karteDetailData.AddressPref);
            updDtDic.Add("addressCity", karteDetailData.AddressCity);
            updDtDic.Add("addressOther", karteDetailData.AddressOther);
            updDtDic.Add("addressKana", karteDetailData.AddressKana);
            updDtDic.Add("tel", karteDetailData.Tel);
            updDtDic.Add("fax", karteDetailData.Fax);
            updDtDic.Add("consultationYmd", karteDetailData.ConsultationYmd);
            updDtDic.Add("opinionYmd", karteDetailData.OpinionYmd);
            updDtDic.Add("adjustPostCode", karteDetailData.PostCode);
            updDtDic.Add("adjustAddressPref", karteDetailData.AddressPref);
            updDtDic.Add("adjustAddressCity", karteDetailData.AddressCity);
            updDtDic.Add("adjustAddressOther", karteDetailData.AddressOther);
            updDtDic.Add("adjustAddressKana", karteDetailData.AddressKana);
            updDtDic.Add("adjustYmd", karteDetailData.AdjustYmd);
            updDtDic.Add("adjustAmount", karteDetailData.AdjustAmount);
            updDtDic.Add("confirmYmd", karteDetailData.ConfirmYmd);
            updDtDic.Add("landArea", karteDetailData.LandArea);
            updDtDic.Add("chainClientMemo", karteDetailData.ChainClientMemo);
            updDtDic.Add("otherMemo", karteDetailData.OtherMemo);
            updDtDic.Add("addressCamparisonFlg", karteDetailData.AddressComparisonFlg);

            // SQL文生成
            StringBuilder SqlStb = new StringBuilder();
            SqlStb.Append("UPDATE client SET ");
            IList<String> setsList = new List<String>();
            foreach (String key in updDtDic.Keys)
            {
                setsList.Add(key + " = ?");
            }
            SqlStb.Append(String.Join(",", setsList.ToArray()));
            SqlStb.Append(" WHERE clientId = ?");

            updDtDic.Add("ClientId", karteDetailData.ClientId);

            SQLiteAdapter.Instance.ExecuteNonQuery(SqlStb.ToString(), updDtDic.Values.ToArray());
        }

        public void DeleteKarte(int clientId)
        {
            StringBuilder SqlStb = new StringBuilder();
            SqlStb.Append("DELETE FROM client WHERE clientId = ? ");
            SQLiteAdapter.Instance.ExecuteNonQuery(SqlStb.ToString(), new object[] { clientId });

        }

        /// <summary>
        /// カルテ詳細Excelファイル出力
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="clientId"></param>
        public void CreateKarteExcel(String fileName, int clientId)
        {

            SearchCondDto searchCondDto = new SearchCondDto();
            KarteDetailDto karteDetailData = new KarteDetailDto();
            ObservableCollection<FamilyStructureVM> familyStructureData = new ObservableCollection<FamilyStructureVM>();
            ObservableCollection<AdjustPartsVM> adjustPartsData = new ObservableCollection<AdjustPartsVM>();
            Application xlApp = new Application();
            Workbook xlBook;
            Worksheet xlSheet;
            String AppPath = Common.GetCurrentAppDir();
            xlBook = xlApp.Workbooks.Add(AppPath + Properties.Settings.Default.KarteTemplate);
            xlSheet = (Worksheet)xlBook.Worksheets[1];
            xlApp.DisplayAlerts = false;

            try
            {

                if (xlApp != null)
                {
                    searchCondDto.ClientId = clientId.ToString();

                    // 詳細検索
                    karteDetailData = this.SearchKarteDetail(searchCondDto);

                    // 家族構成検索
                    familyStructureData = new ObservableCollection<FamilyStructureVM>();
                    familyStructureData = MdlFamilyStructure.ListSearch(searchCondDto);

                    // 調整備品検索
                    adjustPartsData = new ObservableCollection<AdjustPartsVM>();
                    adjustPartsData = MdlAdjustParts.ListSearch(searchCondDto);

                    // Excelファイル作成
                    xlSheet.Range["E1"].Value = karteDetailData.ClientId;   // 顧客ID
                    xlSheet.Range["H1"].Value = DateTime.Today.ToShortDateString();   // 出力日
                    xlSheet.Range["B5"].Value = karteDetailData.ClientName;  // 申込者
                    xlSheet.Range["B4"].Value = karteDetailData.ClientNameKana;  // 申込者ふりがな
                    xlSheet.Range["F5"].Value = karteDetailData.IntroductorName;  // 紹介者
                    xlSheet.Range["F4"].Value = karteDetailData.IntroductorNameKana;  // 紹介者ふりがな

                    StringBuilder address = new StringBuilder();
                    address.AppendLine("〒" + karteDetailData.PostCode);
                    address.Append(karteDetailData.AddressPref + karteDetailData.AddressCity + karteDetailData.AddressOther);

                    xlSheet.Range["B7"].Value = address.ToString().Replace("\r\n", Environment.NewLine);  // 住所
                    xlSheet.Range["B6"].Value = karteDetailData.AddressKana;  // 住所ふりがな
                    xlSheet.Range["B8"].Value = karteDetailData.Tel;  // 電話番号
                    xlSheet.Range["F8"].Value = karteDetailData.Fax;  // ＦＡＸ番号
                    xlSheet.Range["B9"].Value = DateFormatSlush(karteDetailData.ConsultationYmd);  // ご相談日
                    xlSheet.Range["F9"].Value = DateFormatSlush(karteDetailData.OpinionYmd);  // 鑑定日

                    if (karteDetailData.AddressComparisonFlg == 0)
                    {
                        StringBuilder adjustAddress = new StringBuilder();
                        adjustAddress.AppendLine("〒" + karteDetailData.AdjustPostCode);
                        adjustAddress.Append(karteDetailData.AdjustAddressPref + karteDetailData.AdjustAddressCity + karteDetailData.AdjustAddressOther);

                        xlSheet.Range["B11"].Value = adjustAddress.ToString().Replace("\r\n", Environment.NewLine);  // 鑑定場所
                        xlSheet.Range["B10"].Value = karteDetailData.AdjustAddressKana;  // 鑑定場所ふりがな
                    }
                    else
                    {
                        xlSheet.Range["B11"].Value = "同上";  // 鑑定場所
                    }

                    xlSheet.Range["B12"].Value = karteDetailData.AdjustAmount;  // 調整代金
                    xlSheet.Range["B13"].Value = DateFormatSlush(karteDetailData.AdjustYmd);  // 調整日
                    xlSheet.Range["F13"].Value = DateFormatSlush(karteDetailData.ConfirmYmd);  // 確認日
                    xlSheet.Range["B14"].Value = karteDetailData.ChainClientMemo;  // 関連顧客メモ
                    xlSheet.Range["B15"].Value = karteDetailData.OtherMemo;  // その他メモ

                    // 調整備品出力
                    int i = 19;
                    foreach (AdjustPartsVM item in adjustPartsData)
                    {
                        xlSheet.Range["A" + i.ToString()].Value = item.AdjustYmd;  // 調整日
                        xlSheet.Range["C" + i.ToString()].Value = item.PartsName;  // 備品名
                        xlSheet.Range["F" + i.ToString()].Value = item.Count;  // 箇所数
                        i++;
                    }
                    // 家族構成出力
                    i = 37;
                    foreach (FamilyStructureVM item in familyStructureData)
                    {
                        xlSheet.Range["A" + i.ToString()].Value = item.Name;  // 名前
                        xlSheet.Range["C" + i.ToString()].Value = item.Birthday.Substring(0, 4) + "/" + item.Birthday.Substring(4, 2) + "/" + item.Birthday.Substring(6, 2);  // 生年月日
                        xlSheet.Range["E" + i.ToString()].Value = item.Relation;  // 続柄
                        xlSheet.Range["F" + i.ToString()].Value = item.HonmeiseiName;  // 本命星

                        i++;
                    }

                    xlBook.SaveAs(fileName);

                    xlApp.ActiveWorkbook.Close();

                }


                }catch( Exception e)
            {
                throw e;
            }
            finally
            {
                xlApp.Quit();
                Marshal.ReleaseComObject(xlSheet);
                Marshal.ReleaseComObject(xlBook);
                Marshal.ReleaseComObject(xlApp);
            }
           
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
