using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using AporoKarte.Commands;
using AporoKarte.Models;

using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

using Microsoft.Office.Interop.Excel;

namespace AporoKarte.ViewModels
{
    class MainViewModel : BindableBase
    {
        public String ClientName { get; set; }

        private String _adjustDateStart = String.Empty;
        public String AdjustDateStart {
            get { return _adjustDateStart; }
            set
            {
                String rtnStr = String.Empty;
                if (value != null)
                {
                    rtnStr = value;
                }
                SetProperty(ref _adjustDateStart, rtnStr);
            }
        }

        private String _adjustDateEnd = String.Empty;
        public String AdjustDateEnd
        {
            get { return _adjustDateEnd; }
            set {
                String rtnStr = String.Empty;
                if (value != null) 
                {
                    rtnStr = value;
                }
                SetProperty(ref _adjustDateEnd, rtnStr);
            }
        }

        public Karte MdlKarteList;

        public ObservableCollection<KarteDetailDto> KarteListData { get; set; }

        private bool closeWindow;
        public bool CloseWindow
        {
            get { return closeWindow; }
            set { this.SetProperty(ref this.closeWindow, value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainViewModel()
        {
            MdlKarteList = new Karte();
            KarteListData = new ObservableCollection<KarteDetailDto>();

            // 検索条件初期化
            ClientName = String.Empty;
            String CurrentYear = DateTime.Today.ToString().Substring(0,4); 
            AdjustDateStart = CurrentYear + "/01/01";
            AdjustDateEnd = CurrentYear + "/12/31";

            // 初期検索
            this.Search();

        }


        /// <summary>
        /// 検索処理
        /// </summary>
        public void Search() {
            //検索条件チェック処理
            StringBuilder ErrMsg = new StringBuilder();

            // 日付大小チェック
            DateTime tryParseDt = new DateTime();
            if (!String.Empty.Equals(this.AdjustDateStart) && !DateTime.TryParse(this.AdjustDateStart, out tryParseDt)) {
                ErrMsg.AppendLine("調整日の開始が日付として正しくありません。");
            }

            if (!String.Empty.Equals(this.AdjustDateEnd) && !DateTime.TryParse(this.AdjustDateEnd, out tryParseDt))
            {
                ErrMsg.AppendLine("調整日の終了が日付として正しくありません。");
            }

            if (!String.Empty.Equals(this.AdjustDateStart) && !String.Empty.Equals(this.AdjustDateEnd) && 
                DateTime.Parse(this.AdjustDateEnd) < DateTime.Parse(this.AdjustDateStart)) {
                ErrMsg.AppendLine("調整日の開始と終了が逆転しています。");
            }

            if (ErrMsg.Length > 0)
            {
                MessageBox.Show(ErrMsg.ToString(),"検索条件エラー");
            } else
            {
                // 検索条件格納処理
                SearchCondDto searchCond = new SearchCondDto();
                searchCond.ClientId = String.Empty;
                searchCond.ClientName = this.ClientName;
                searchCond.AdjustYmdStart = this.DateFormat(this.AdjustDateStart);
                searchCond.AdjustYmdEnd = this.DateFormat(this.AdjustDateEnd);

                // 一覧検索
                KarteListData.Clear();
                //KarteListData = new ObservableCollection<KarteDetailDto>();
                ObservableCollection<KarteDetailDto> listVm = MdlKarteList.ListSearch(searchCond);
                foreach (KarteDetailDto item in listVm)
                {
                    KarteListData.Add(item);
                }
            }
        }

        #region コマンドの実装
        /// <summary>
        /// 検索ボタンコマンド
        /// </summary>
        private DelegateCommand _searchListCommand;
        public DelegateCommand SearchListCommand
        {
            get { return _searchListCommand = _searchListCommand ?? new DelegateCommand(this.SearchList); }
        }

        private void SearchList()
        {
            // 検索処理
            this.Search();
        }

        /// <summary>
        /// 登録ボタンコマンド
        /// </summary>
        private DelegateCommand _registKarteCommand;
        public DelegateCommand RegistKarteCommand
        {
            get { return _registKarteCommand = _registKarteCommand ?? new DelegateCommand(this.RegistKarte); }
        }

        private void RegistKarte()
        {
            // カルテ詳細画面遷移
            App app = App.Current as App;
            KarteDetailVM vm = new KarteDetailVM();
            vm.FamilyStructureData.Add(new FamilyStructureVM() { Relation = "本人", Birthday = "19700101" });

            // 画面遷移パラメータ設定
            vm.WindowParams.Add("mode", "NEW");

            app.ShowModalView(vm);

            // 再検索
            this.Search();
        }

        /// <summary>
        /// 一覧編集ボタンコマンド
        /// </summary>
        private DelegateCommand<String> _editKarteDetailCommand;
        public DelegateCommand<String> EditKarteDetailCommand
        {
            get { return _editKarteDetailCommand = _editKarteDetailCommand ?? new DelegateCommand<String>(this.ShowEditKarteDetail); }
        }

        private void ShowEditKarteDetail(String clientId)
        {

            App app = App.Current as App;
            KarteDetailVM vm = new KarteDetailVM();

            // 画面遷移パラメータ設定
            vm.WindowParams.Add("mode", "EDIT");
            vm.WindowParams.Add("clientId", clientId);

            vm.Search();
            app.ShowModalView(vm);

            // 再検索
            this.Search();
        }

        /// <summary>
        /// 終了ボタンコマンド
        /// </summary>
        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand
        {
            get { return _closeCommand = _closeCommand ?? new DelegateCommand(this.Close); }
        }

        /// <summary>
        /// 画面終了処理
        /// </summary>
        private void Close()
        {
            this.CloseWindow = true;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show(((System.Windows.Controls.Button)sender).CommandParameter.ToString());
        //}
        #endregion

        private String DateFormat(String sourceDate)
        {
            if (sourceDate != null && !sourceDate.Equals(String.Empty))
            {
                String[] splitDate = DateTime.Parse(sourceDate).ToShortDateString().Split(char.Parse("/"));
                String strYear = splitDate[0].Substring(0, 4);
                String strMonth = "0" + splitDate[1];
                String strDay = "0" + splitDate[2];
                return strYear + strMonth.Substring(strMonth.Length - 2, 2) + strDay.Substring(strDay.Length - 2, 2);
            }
            else
            {
                return String.Empty;
            }

        }
    }
}
