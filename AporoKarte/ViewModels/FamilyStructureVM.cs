using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AporoKarte.Commands;
using AporoKarte.Models;

using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;

namespace AporoKarte.ViewModels
{
    class FamilyStructureVM : BindableBase
    {
#region 定数定義
        private const String SETSUBUN_MONTH_DAY = "0204";
        private const int HONMEISEI_MOD_NUM = 9;
        private const String HONMEISEI_CODE_NINE = "9";
#endregion

        private bool _principalFlg;
        public bool PrincipalFlg
        {
            get { return _principalFlg; }
            set { SetProperty(ref _principalFlg, value); }
        }

        private String _name;
        public String Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value);
                IsChenged = true;
            }
        }

        private String _nameKana;
        public String NameKana
        {
            get { return _nameKana; }
            set { SetProperty(ref _nameKana, value);
                IsChenged = true;
            }
        }

        private String _birthday;
        public String Birthday
        {
            get { return _birthday; }
            set {

                SetProperty(ref _birthday, value);

                // 九星気学　本命星算出
                this.Honmeisei = this.CalcHonmeiseiCode(value);

                if (!String.Empty.Equals(this.Honmeisei)) {
                    this.HonmeiseiName = _honmeiseiDic[this.Honmeisei];
                } else
                {
                    this.HonmeiseiName = String.Empty;
                }

                IsChenged = true;
            }
        }

        //private ObservableCollection<ComboItem> _birthdayYear;
        //public ObservableCollection<ComboItem> BirthdayYear
        //{
        //    get { return _birthdayYear; }
        //    set
        //    {
        //        SetProperty(ref _birthdayYear, value);
        //        RaisePropertyChanged("BirthdayYearVal");
        //    }

        //}

        //private ObservableCollection<ComboItem> _birthdayMonth;
        //public ObservableCollection<ComboItem> BirthdayMonth
        //{
        //    get { return _birthdayMonth; }
        //    set
        //    {
        //        SetProperty(ref _birthdayMonth, value);
        //        RaisePropertyChanged("BirthdayMonthVal");
        //    }

        //}

        //private ObservableCollection<ComboItem> _birthdayDay;
        //public ObservableCollection<ComboItem> BirthdayDay
        //{
        //    get { return _birthdayDay; }
        //    set
        //    {
        //        SetProperty(ref _birthdayDay, value);
        //        RaisePropertyChanged("BirthdayDayVal");
        //    }

        //}

        private String _birthdayYearVal;
        public String BirthdayYearVal
        {
            get { return _birthdayYearVal; }
            set {
                SetProperty(ref _birthdayYearVal, value);
                ChangeBirthday();
            }
        }

        private String _birthdayMonthVal;
        public String BirthdayMonthVal
        {
            get { return _birthdayMonthVal; }
            set {
                SetProperty(ref _birthdayMonthVal, value);
                ChangeBirthday();
            }
        }

        private String _birthdayDayhVal;
        public String BirthdayDayVal
        {
            get { return _birthdayDayhVal; }
            set {
                SetProperty(ref _birthdayDayhVal, value);
                ChangeBirthday();
            }
        }

        private String _relation;
        public String Relation
        {
            get { return _relation; }
            set { SetProperty(ref _relation, value);
                IsChenged = true;
            }
        }

        private String _honmeisei;
        public String Honmeisei
        {
            get { return _honmeisei; }
            set { SetProperty(ref _honmeisei, value); }
        }

        private String _honmeiseiName;
        public String HonmeiseiName
        {
            get { return _honmeiseiName; }
            set { SetProperty(ref _honmeiseiName, value); }
        }

        private IDictionary<String, String> _honmeiseiDic = new Dictionary<String, String>();

        public bool IsChenged = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FamilyStructureVM()
        {
            _honmeiseiDic = GetHonmeiseiDic();
            FamilyStructure MdlFamilyStructure = new FamilyStructure();
            //this.BirthdayYear = MdlFamilyStructure.GetBirthdayYearComboList();
            //this.BirthdayMonth = MdlFamilyStructure.GetBirthdayMonthComboList();
            //this.BirthdayDay = MdlFamilyStructure.GetBirthdayDayComboList();
            BirthdayYearVal = "1970";
            BirthdayMonthVal = "01";
            BirthdayDayVal = "01";


        }

#region コマンド登録


#endregion


        /// <summary>
        /// 本命星コード算出
        /// </summary>
        /// <param name="birthday">生年月日（yyyyMMdd形式）</param>
        /// <returns></returns>
        public String CalcHonmeiseiCode(String birthday)
        {
            DateTime dt;
            String dtFormat = String.Empty;
            if (birthday.Length == 8)
            {
                dtFormat = birthday.Substring(0, 4) + "/" + birthday.Substring(4, 2) + "/" + birthday.Substring(6, 2);
            }else
            {
                return String.Empty;
            }

            if (DateTime.TryParse(dtFormat, out dt) == false)
            {
                return String.Empty; 
            }

            int rtnHonmeiseiCode = 0;
            int birthYear = int.Parse(birthday.Substring(0, 4));
            int birthMonthDay = int.Parse(birthday.Substring(4, 4));

            int calcHS1 = int.Parse(birthYear.ToString().Substring(0, 1)) + int.Parse(birthYear.ToString().Substring(1, 1)) + int.Parse(birthYear.ToString().Substring(2, 1)) + int.Parse(birthYear.ToString().Substring(3, 1));
            int calcHS2 = int.Parse(calcHS1.ToString().Substring(0, 1)) + int.Parse(calcHS1.ToString().Substring(1, 1));
            rtnHonmeiseiCode = 11 - calcHS2;

            // 節分前判定
            if (birthMonthDay < int.Parse(SETSUBUN_MONTH_DAY))
            {
                // 生年月日の月日が節分前の場合
                // 一つ前の本命星をセット
                rtnHonmeiseiCode = rtnHonmeiseiCode - 1;

                if (rtnHonmeiseiCode == 0)
                {
                    // 0の場合
                    // 本命星コードに９をセット
                    rtnHonmeiseiCode = int.Parse(HONMEISEI_CODE_NINE);
                }
            }

            return rtnHonmeiseiCode.ToString();
        }

        private IDictionary<String, String> GetHonmeiseiDic() {
            IDictionary<String, String> rtnHonmeiseiDic = new Dictionary<String, String>();

            rtnHonmeiseiDic.Add("1", "一白水星");
            rtnHonmeiseiDic.Add("2", "二黒土星");
            rtnHonmeiseiDic.Add("3", "三碧木星");
            rtnHonmeiseiDic.Add("4", "四緑木星");
            rtnHonmeiseiDic.Add("5", "五黄土星");
            rtnHonmeiseiDic.Add("6", "六白金星");
            rtnHonmeiseiDic.Add("7", "七赤金星");
            rtnHonmeiseiDic.Add("8", "八白土星");
            rtnHonmeiseiDic.Add("9", "九紫火星");

            return rtnHonmeiseiDic;
        }

        private void ChangeBirthday()
        {
            this.Birthday = this.BirthdayYearVal + this.BirthdayMonthVal + this.BirthdayDayVal;
        }
    }
}
