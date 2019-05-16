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
using System.Data.SQLiteManager;
using System.Windows;
using Microsoft.Win32;

namespace AporoKarte.ViewModels
{
    class KarteDetailVM : BindableBase
    {
#region 定数定義

#endregion

        public KarteDetailDto KarteDetailData { get; set; }

        public ObservableCollection<FamilyStructureVM> FamilyStructureData { get; set; }
        public ObservableCollection<AdjustPartsVM> AdjustPartsData { get; set; }

        //モデルクラス
        private Karte MdlKarte;
        private FamilyStructure MdlFamilyStructure;
        private AdjustParts MdlAdjustParts;
        private bool IsChenged = false;
        
        //画面遷移パラメータ
        private IDictionary<String,object> _windowParams;
        public IDictionary<String, object> WindowParams
        {
            get { return _windowParams; }
            set { SetProperty(ref _windowParams, value); }
        }

        private String _clientId = String.Empty;
        public String ClientId
        {
            get { return _clientId; }
            set { SetProperty(ref _clientId, value); }
        }

        private String _clientName = String.Empty;
        public String ClientName
        {
            get { return _clientName; }
            set { SetProperty(ref _clientName, value);
                IsChenged = true;
            }
        }

        private String _clientNameKana = String.Empty;
        public String ClientNameKana
        {
            get { return _clientNameKana; }
            set { SetProperty(ref _clientNameKana, value);
                IsChenged = true;
            }
        }

        private String _introductorName = String.Empty;
        public String IntroductorName
        {
            get { return _introductorName; }
            set { SetProperty(ref _introductorName, value);
                IsChenged = true;
            }
        }

        private String _introductorNameKana = String.Empty;
        public String IntroductorNameKana
        {
            get { return _introductorNameKana; }
            set { SetProperty(ref _introductorNameKana, value);
                IsChenged = true;
            }
        }

        private String _postCode = String.Empty;
        public String PostCode
        {
            get { return _postCode; }
            set { SetProperty(ref _postCode, value);
                IsChenged = true;
            }
        }

        private String _addressPref = String.Empty;
        public String AddressPref
        {
            get { return _addressPref; }
            set { SetProperty(ref _addressPref, value);
                IsChenged = true;
            }
        }

        private String _addressCity = String.Empty;
        public String AddressCity
        {
            get { return _addressCity; }
            set { SetProperty(ref _addressCity, value);
                IsChenged = true;
            }
        }

        private String _addressOther = String.Empty;
        public String AddressOther
        {
            get { return _addressOther; }
            set { SetProperty(ref _addressOther, value);
                IsChenged = true;
            }
        }

        private String _addressKana = String.Empty;
        public String AddressKana
        {
            get { return _addressKana; }
            set { SetProperty(ref _addressKana, value);
                IsChenged = true;
            }
        }

        private String _tel = String.Empty;
        public String Tel
        {
            get { return _tel; }
            set { SetProperty(ref _tel, value);
                IsChenged = true;
            }
        }

        private String _fax = String.Empty;
        public String Fax
        {
            get { return _fax; }
            set { SetProperty(ref _fax, value);
                IsChenged = true;
            }
        }

        private String _consultationYmd = String.Empty;
        public String ConsultationYmd
        {
            get { return _consultationYmd; }
            set { SetProperty(ref _consultationYmd, value);
                IsChenged = true;
            }
        }

        private String _opinionYmd = String.Empty;
        public String OpinionYmd
        {
            get { return _opinionYmd; }
            set { SetProperty(ref _opinionYmd, value);
                IsChenged = true;
            }
        }


        private String _adjustPostCode = String.Empty;
        public String AdjustPostCode
        {
            get { return _adjustPostCode; }
            set { SetProperty(ref _adjustPostCode, value);
                IsChenged = true;
            }
        }

        private String _adjustAddressPref = String.Empty;
        public String AdjustAddressPref
        {
            get { return _adjustAddressPref; }
            set { SetProperty(ref _adjustAddressPref, value);
                IsChenged = true;
            }
        }

        private String _adjustAddressCity = String.Empty;
        public String AdjustAddressCity
        {
            get { return _adjustAddressCity; }
            set { SetProperty(ref _adjustAddressCity, value);
                IsChenged = true;
            }
        }

        private String _adjustAddressOther = String.Empty;
        public String AdjustAddressOther
        {
            get { return _adjustAddressOther; }
            set { SetProperty(ref _adjustAddressOther, value);
                IsChenged = true;
            }
        }

        private String _adjustAddressKana = String.Empty;
        public String AdjustAddressKana
        {
            get { return _adjustAddressKana; }
            set { SetProperty(ref _adjustAddressKana, value);
                IsChenged = true;
            }
        }

        private String _adjustYmd = String.Empty;
        public String AdjustYmd
        {
            get { return _adjustYmd; }
            set { SetProperty(ref _adjustYmd, value);
                IsChenged = true;
            }
        }

        private int _adjustAmount = 0;
        public int AdjustAmount
        {
            get { return _adjustAmount; }
            set { SetProperty(ref _adjustAmount, value);
                IsChenged = true;
            }
        }

        private String _confirmYmd = String.Empty;
        public String ConfirmYmd
        {
            get { return _confirmYmd; }
            set { SetProperty(ref _confirmYmd, value);
                IsChenged = true;
            }
        }

        private String _landArea = String.Empty;
        public String LandArea
        {
            get { return _landArea; }
            set { SetProperty(ref _landArea, value);
                IsChenged = true;
            }
        }

        private String _chainClientMemo = String.Empty;
        public String ChainClientMemo
        {
            get { return _chainClientMemo; }
            set { SetProperty(ref _chainClientMemo, value);
                IsChenged = true;
            }
        }

        private String _otherMemo = String.Empty;
        public String OtherMemo
        {
            get { return _otherMemo; }
            set { SetProperty(ref _otherMemo, value);
                IsChenged = true;
            }
        }

        private bool _addressComparisonFlg = false;
        public bool AddressComparisonFlg
        {
            get { return _addressComparisonFlg; }
            set { SetProperty(ref _addressComparisonFlg, value);
                IsChenged = true;
            }
        }

        private bool closeWindow;
        public bool CloseWindow
        {
            get { return closeWindow; }
            set { this.SetProperty(ref this.closeWindow, value); }
        }


        //コンボボックス格納用
        private ObservableCollection<ComboItem> _addressPrefComboList;
        public ObservableCollection<ComboItem> AddressPrefComboList
        {
            get { return _addressPrefComboList; }
            set { SetProperty(ref _addressPrefComboList, value); }
        }

        private ObservableCollection<ComboItem> _adjustAddressPrefComboList;
        public ObservableCollection<ComboItem> AdjustAddressPrefComboList
        {
            get { return _adjustAddressPrefComboList; }
            set { SetProperty(ref _adjustAddressPrefComboList, value); }
        }

        private ObservableCollection<ComboItem> _adjustPartsComboList;
        public ObservableCollection<ComboItem> AdjustPartsComboList
        {
            get { return _adjustPartsComboList; }
            set { SetProperty(ref _adjustPartsComboList, value); }
        }

        private ObservableCollection<ComboItem> _birthdayYear;
        public ObservableCollection<ComboItem> BirthdayYear
        {
            get { return _birthdayYear; }
            set { SetProperty(ref _birthdayYear, value);
                RaisePropertyChanged("BirthdayYearVal");
            }

        }

        private ObservableCollection<ComboItem> _birthdayMonth;
        public ObservableCollection<ComboItem> BirthdayMonth
        {
            get { return _birthdayMonth; }
            set { SetProperty(ref _birthdayMonth, value);
                RaisePropertyChanged("BirthdayMonthVal");
            }

        }

        private ObservableCollection<ComboItem> _birthdayDay;
        public ObservableCollection<ComboItem> BirthdayDay
        {
            get { return _birthdayDay; }
            set { SetProperty(ref _birthdayDay, value);
                RaisePropertyChanged("BirthdayDayVal");
            }

        }

        private int _familyStructureSelectedIndex;
        public int FamilyStructureSelectedIndex
        {
            get { return _familyStructureSelectedIndex; }
            set { SetProperty(ref _familyStructureSelectedIndex, value); }
        }

        private int _adjustPartsSelectedIndex;
        public int AdjustPartsSelectedIndex
        {
            get { return _adjustPartsSelectedIndex; }
            set { SetProperty(ref _adjustPartsSelectedIndex, value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KarteDetailVM()
        {
            MdlKarte = new Karte();
            MdlFamilyStructure = new FamilyStructure();
            MdlAdjustParts = new AdjustParts();

            WindowParams = new Dictionary<String, object>();

            KarteDetailData = new KarteDetailDto();

            // 一覧初期化
            this.FamilyStructureData = new ObservableCollection<FamilyStructureVM>();
            this.AdjustPartsData = new ObservableCollection<AdjustPartsVM>();

            // コンボボックス初期化
            this.AddressPrefComboList = this.GetPrefComboList();
            this.AdjustAddressPrefComboList = this.GetPrefComboList();
            this.AdjustPartsComboList = MdlAdjustParts.GetAdjustPartsComboList();
            this.BirthdayYear = MdlFamilyStructure.GetBirthdayYearComboList();
            this.BirthdayMonth = MdlFamilyStructure.GetBirthdayMonthComboList();
            this.BirthdayDay = MdlFamilyStructure.GetBirthdayDayComboList();

        }

        #region コマンド登録
        /// <summary>
        /// 登録ボタン押下
        /// </summary>
        private DelegateCommand _registKarteCommand;
        public DelegateCommand RegistKarteCommand
        {
            get { return _registKarteCommand = _registKarteCommand ?? new DelegateCommand(RegistKarte); }
        }

        /// <summary>
        /// 削除ボタン押下
        /// </summary>
        private DelegateCommand _deleteKarteCommand;
        public DelegateCommand DeleteKarteCommand
        {
            get { return _deleteKarteCommand = _deleteKarteCommand ?? new DelegateCommand(DeleteKarte); }
        }

        /// <summary>
        /// キャンセルボタン押下
        /// </summary>
        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand
        {
            get { return _cancelCommand = _cancelCommand ?? new DelegateCommand(Cancel); }
        }

        /// <summary>
        /// 家族構成追加ボタン押下
        /// </summary>
        private DelegateCommand _familyStructureAddCommand;
        public DelegateCommand FamilyStructureAddCommand
        {
            get { return _familyStructureAddCommand = _familyStructureAddCommand ?? new DelegateCommand(FamilyStructureAdd); }
        }

        /// <summary>
        /// 家族構成削除ボタン押下
        /// </summary>
        private DelegateCommand _familyStructureRemoveCommand;
        public DelegateCommand FamilyStructureRemoveCommand
        {
            get { return _familyStructureRemoveCommand = _familyStructureRemoveCommand ?? new DelegateCommand(FamilyStructureRemove); }
        }

        /// <summary>
        /// 調整備品追加ボタン押下
        /// </summary>
        private DelegateCommand _adjustPartsAddCommand;
        public DelegateCommand AdjustPartsAddCommand
        {
            get { return _adjustPartsAddCommand = _adjustPartsAddCommand ?? new DelegateCommand(AdjustPartsAdd); }
        }

        /// <summary>
        /// 調整備品削除ボタン押下
        /// </summary>
        private DelegateCommand _adjustPartsRemoveCommand;
        public DelegateCommand AdjustPartsRemoveCommand
        {
            get { return _adjustPartsRemoveCommand = _adjustPartsRemoveCommand ?? new DelegateCommand(AdjustPartsRemove); }
        }

        /// <summary>
        /// カルテ出力ボタン押下
        /// </summary>
        private DelegateCommand _karteOutCommand;
        public DelegateCommand KarteOutCommand
        {
            get { return _karteOutCommand = _karteOutCommand ?? new DelegateCommand(KarteOut); }
        }
        #endregion


        #region コアロジック
        /// <summary>
        /// 検索処理
        /// </summary>
        public void Search()
        {
            // 検索条件格納処理
            SearchCondDto searchCond = new SearchCondDto();
            searchCond.ClientId = WindowParams["clientId"].ToString();

            // 詳細検索
            KarteDetailData = new KarteDetailDto();
            KarteDetailData = MdlKarte.SearchKarteDetail(searchCond);

            // 詳細データ移送
            this.ItemTransfarNo1(KarteDetailData);

            // 家族構成検索
            FamilyStructureData = new ObservableCollection<FamilyStructureVM>();
            FamilyStructureData = MdlFamilyStructure.ListSearch(searchCond);

            // 調整備品検索
            AdjustPartsData = new ObservableCollection<AdjustPartsVM>();
            AdjustPartsData = MdlAdjustParts.ListSearch(searchCond);

            this.IsChenged = false;
        }

        /// <summary>
        /// カルテ登録
        /// </summary>
        public void RegistKarte()
        {
            // 入力チェック
            if (CheckRegist() == false)
            {
                return;
            }

            // 確認
            MessageBoxResult resultDelete = MessageBox.Show("登録してよろしいですか？", "登録確認", MessageBoxButton.OKCancel);
            if (resultDelete == MessageBoxResult.Cancel)
            {
                return;
            }

            //KarteDetailDto karteDetailData = new KarteDetailDto();
            IList<FamilyStructureDto> familyStructureData;
            IList<AdjustPartsDto> adjustPartsData;

            int clientId = 0;

            ItemTransfarNo2();

            SQLiteAdapter.Instance.AutoTransaction = false;
            SQLiteAdapter.Instance.TransactionStart();

            // カルテ詳細登録(client)
            switch (WindowParams["mode"])
            {
                case "NEW":
                    clientId = MdlKarte.RegistKarte(KarteDetailData);
                    
                    // 家族構成登録(familyStructure)
                    familyStructureData = ItemTransfarNo3(clientId);
                    MdlFamilyStructure.RegistFamilyStructure(familyStructureData);

                    // 調整備品登録(adjustParts)
                    adjustPartsData = ItemTransfarNo4(clientId);
                    MdlAdjustParts.RegistAdjustParts(adjustPartsData);

                    break;
                case "EDIT":
                    clientId = int.Parse(this.ClientId);
                    MdlKarte.UpdateKarte(KarteDetailData);

                    // 家族構成削除(familyStructure)
                    MdlFamilyStructure.DeleteFamilyStructure(clientId);

                    // 家族構成登録(familyStructure)
                    familyStructureData = ItemTransfarNo3(clientId);
                    MdlFamilyStructure.RegistFamilyStructure(familyStructureData);

                    // 調整備品削除(adjustParts)
                    MdlAdjustParts.DeleteAdjustParts(clientId);

                    // 調整備品登録(adjustParts)
                    adjustPartsData = ItemTransfarNo4(clientId);
                    MdlAdjustParts.RegistAdjustParts(adjustPartsData);

                    break;
                default:
                    break;
            }


            SQLiteAdapter.Instance.TransactionCommit();

            MessageBox.Show("登録完了しました", "登録完了");

            this.Close();
        }

        /// <summary>
        /// カルテ削除
        /// </summary>
        public void DeleteKarte()
        {
            int clientId = int.Parse(this.ClientId);

            // 確認
            MessageBoxResult resultDelete = MessageBox.Show("削除すると元に戻せません。削除してよろしいですか？","削除確認",MessageBoxButton.OKCancel);
            if(resultDelete == MessageBoxResult.Cancel)
            {
                return;
            }

            SQLiteAdapter.Instance.AutoTransaction = false;
            SQLiteAdapter.Instance.TransactionStart();

            // カルテ削除
            MdlKarte.DeleteKarte(clientId);

            // 家族構成削除(familyStructure)
            MdlFamilyStructure.DeleteFamilyStructure(clientId);

            // 調整備品削除(adjustParts)
            MdlAdjustParts.DeleteAdjustParts(clientId);

            SQLiteAdapter.Instance.TransactionCommit();

            MessageBox.Show("削除完了しました", "削除完了");

            this.Close();
        }

        /// <summary>
        /// キャンセル処理
        /// </summary>
        public void Cancel()
        {
            // 編集中チェック
            bool isChanged = this.ChkChanged();
            if(isChanged == true)
            {
                // 確認
                MessageBoxResult resultDelete = MessageBox.Show("編集中の内容が保存されません。画面を閉じてよろしいですか？", "確認", MessageBoxButton.OKCancel);
                if (resultDelete == MessageBoxResult.Cancel)
                {
                    return;
                }
            }

            // 画面を閉じる
            this.Close();
        }

        /// <summary>
        /// 画面終了処理
        /// </summary>
        private void Close()
        {
            this.CloseWindow = true;
        }

        /// <summary>
        /// 家族構成追加
        /// </summary>
        public void FamilyStructureAdd()
        {
            if (FamilyStructureData.Count < 10)
            {
                FamilyStructureVM vm = new FamilyStructureVM();
                FamilyStructureData.Add(vm);
                IsChenged = true;
            }
        }

        /// <summary>
        /// 登録入力チェック
        /// </summary>
        /// <returns></returns>
        public bool CheckRegist()
        {
            StringBuilder ErrMsg = new StringBuilder();

            // 申込者名
            if (CheckEmptyVal(ClientName) == false)
            {
                ErrMsg.AppendLine("【申込者名】を入力してください。");
            }

            // 申込者名かな
            if (CheckEmptyVal(ClientNameKana) == false)
            {
                ErrMsg.AppendLine("【申込者名】の「ふりがな」を入力してください。");
            }

            // 住所
            // 　郵便番号
            if (CheckEmptyVal(PostCode) == false)
            {
                ErrMsg.AppendLine("【住所】の「郵便番号」を入力してください。");
            }

            // 　都道府県
            if (CheckEmptyVal(AddressPref) == false)
            {
                ErrMsg.AppendLine("【住所】の「都道府県」を選択してください。");
            }

            // 　市区町村
            if (CheckEmptyVal(AddressCity) == false)
            {
                ErrMsg.AppendLine("【住所】の「市区町村」を入力してください。");
            }

            // 　その他住所
            if (CheckEmptyVal(AddressOther) == false)
            {
                ErrMsg.AppendLine("【住所】の「その他住所」を入力してください。");
            }

            // 　住所かな
            if (CheckEmptyVal(AddressKana) == false)
            {
                ErrMsg.AppendLine("【住所】の「ふりがな」を入力してください。");
            }

            // 電話番号
            if (CheckEmptyVal(Tel) == false)
            {
                ErrMsg.AppendLine("【電話番号】を入力してください。");
            }

            // 鑑定場所住所
            if (AddressComparisonFlg == false)
            {
                // 　郵便番号
                if (CheckEmptyVal(AdjustPostCode) == false)
                {
                    ErrMsg.AppendLine("【鑑定場所】の「郵便番号」を入力してください。");
                }

                // 　都道府県
                if (CheckEmptyVal(AdjustAddressPref) == false)
                {
                    ErrMsg.AppendLine("【鑑定場所】の「都道府県」を選択してください。");
                }

                // 　市区町村
                if (CheckEmptyVal(AdjustAddressCity) == false)
                {
                    ErrMsg.AppendLine("【鑑定場所】の「市区町村」を入力してください。");
                }

                // 　その他住所
                if (CheckEmptyVal(AdjustAddressOther) == false)
                {
                    ErrMsg.AppendLine("【鑑定場所】の「その他住所」を入力してください。");
                }

                // 　住所かな
                if (CheckEmptyVal(AdjustAddressKana) == false)
                {
                    ErrMsg.AppendLine("【鑑定場所】の「ふりがな」を入力してください。");
                }
            }

            int i;
            // 家族構成
            i = 1;
            foreach (FamilyStructureVM item in FamilyStructureData)
            {
                // 氏名
                if (CheckEmptyVal(item.Name) == false)
                {
                    ErrMsg.AppendLine("【家族構成】" + i.ToString() + "行目:「氏名」を入力してください。");
                }

                // 生年月日
                if (CheckEmptyVal(item.Birthday) == false)
                {
                    ErrMsg.AppendLine("【家族構成】" + i.ToString() +"行目:「生年月日」を入力してください。");
                }

                // 日付不正チェック
                DateTime dt;
                String validBirthday = item.Birthday.Substring(0, 4) + "/" + item.Birthday.Substring(4, 2) + "/" + item.Birthday.Substring(6, 2);
                if (DateTime.TryParse(validBirthday, out dt) == false)
                {
                    ErrMsg.AppendLine("【家族構成】" + i.ToString() + "行目:「生年月日」は実在する日付で入力してください。");
                }
                i++;
            }

            // 調整備品
            i = 1;
            foreach (AdjustPartsVM item in AdjustPartsData)
            {
                // 調整日
                if (CheckEmptyVal(item.AdjustYmd) == false)
                {
                    ErrMsg.AppendLine("【調整備品】" + i.ToString() + "行目:「調整日」を入力してください。");
                }

                // 備品名
                if (CheckEmptyVal(item.PartsCode) == false)
                {
                    ErrMsg.AppendLine("【調整備品】" + i.ToString() + "行目:「備品名」を選択してください。");
                }

                // 箇所数
                if (item.Count == 0)
                {
                    ErrMsg.AppendLine("【調整備品】" + i.ToString() + "行目:「箇所数」を入力してください。");
                }
                i++;
            }

            if (ErrMsg.Length > 0)
            {
                MessageBox.Show(ErrMsg.ToString(), "入力エラー");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 家族構成削除
        /// </summary>
        public void FamilyStructureRemove()
        {
            if (FamilyStructureSelectedIndex >= 0 && FamilyStructureData.Contains(FamilyStructureData[FamilyStructureSelectedIndex]) == true)
            {
                FamilyStructureData.Remove(FamilyStructureData[FamilyStructureSelectedIndex]);
                IsChenged = true;
            }
        }

        /// <summary>
        /// 調整備品追加
        /// </summary>
        public void AdjustPartsAdd()
        {
            if (AdjustPartsData.Count < 10) { }
            {
                AdjustPartsVM vm = new AdjustPartsVM();
                vm.AdjustYmd = this.AdjustYmd;
                vm.PartsCode = this.AdjustPartsComboList.First().Value;
                vm.Count = 1;
                AdjustPartsData.Add(vm);
                IsChenged = true;
            }
        }

        /// <summary>
        /// 調整備品削除
        /// </summary>
        public void AdjustPartsRemove()
        {
            if (AdjustPartsSelectedIndex >= 0 && AdjustPartsData.Contains(AdjustPartsData[AdjustPartsSelectedIndex]) == true)
            {
                AdjustPartsData.Remove(AdjustPartsData[AdjustPartsSelectedIndex]);
                IsChenged = true;
            }
        }

        /// <summary>
        /// カルテ出力
        /// </summary>
        public void KarteOut()
        {
            if(this.ClientId == null || String.Empty.Equals(this.ClientId))
            {
                MessageBox.Show("登録前のため出力できません。登録してから出力してください", "確認");
                return;
            }

            // 編集中チェック
            bool isChanged = this.ChkChanged();
            if (isChanged == true)
            {
                // 確認
                MessageBox.Show("編集中のため出力できません。登録してから出力してください", "確認");
                return;
            }

            //カルテ出力
            String fileName = String.Empty;

            // 出力先選択(ファイル選択ダイアログ)
            fileName = GetFileName(int.Parse(this.ClientId));
            
            if (String.Empty.Equals(fileName))
            {
                MessageBox.Show("カルテ保存先を指定してください", "確認");
                return;
            }

            MdlKarte.CreateKarteExcel(fileName, int.Parse(this.ClientId));
            MessageBox.Show("出力完了しました", "出力完了");
        }

        #endregion

        /// <summary>
        /// 項目移送No1 詳細データを画面プロパティに移送する
        /// </summary>
        /// <param name="karteData"></param>
        private void ItemTransfarNo1(KarteDetailDto karteData)
        {
            this.ClientId = karteData.ClientId;
            this.ClientName = karteData.ClientName;
            this.ClientNameKana = karteData.ClientNameKana;
            this.IntroductorName = karteData.IntroductorName;
            this.IntroductorNameKana = karteData.IntroductorNameKana;
            this.PostCode = karteData.PostCode;
            this.AddressPref = karteData.AddressPref;
            this.AddressCity = karteData.AddressCity;
            this.AddressOther = karteData.AddressOther;
            this.AddressKana = karteData.AddressKana;
            this.Tel = karteData.Tel;
            this.Fax = karteData.Fax;
            this.ConsultationYmd = this.DateFormatSlush(karteData.ConsultationYmd);
            this.OpinionYmd = this.DateFormatSlush(karteData.OpinionYmd);
            this.AdjustPostCode = karteData.AdjustPostCode;
            this.AdjustAddressPref = karteData.AdjustAddressPref;
            this.AdjustAddressCity = karteData.AdjustAddressCity;
            this.AdjustAddressOther = karteData.AdjustAddressOther;
            this.AdjustAddressKana = karteData.AdjustAddressKana;
            this.AdjustYmd = this.DateFormatSlush(karteData.AdjustYmd);
            this.AdjustAmount = karteData.AdjustAmount;
            this.ConfirmYmd = this.DateFormatSlush(karteData.ConfirmYmd);
            this.LandArea = karteData.LandArea;
            this.ChainClientMemo = karteData.ChainClientMemo;
            this.OtherMemo = karteData.OtherMemo;

            if (karteData.AddressComparisonFlg == 0)
            {
                this.AddressComparisonFlg = false;
            }
            else
            {
                this.AddressComparisonFlg = true;
            }

        }

        /// <summary>
        /// 項目移送No2 画面プロパティ値をDTOに移送する
        /// </summary>
        private void ItemTransfarNo2()
        {

            KarteDetailData.ClientId = this.ClientId;
            KarteDetailData.ClientName = this.ClientName;
            KarteDetailData.ClientNameKana = this.ClientNameKana;
            KarteDetailData.IntroductorName = this.IntroductorName;
            KarteDetailData.IntroductorNameKana = this.IntroductorNameKana;
            KarteDetailData.PostCode = this.PostCode;
            KarteDetailData.AddressPref = this.AddressPref;
            KarteDetailData.AddressCity = this.AddressCity;
            KarteDetailData.AddressOther = this.AddressOther;
            KarteDetailData.AddressKana = this.AddressKana;
            KarteDetailData.Tel = this.Tel;
            KarteDetailData.Fax = this.Fax;
            KarteDetailData.ConsultationYmd = this.DateFormatNonSlushFromPicker(this.ConsultationYmd);
            KarteDetailData.OpinionYmd = this.DateFormatNonSlushFromPicker(this.OpinionYmd);
            KarteDetailData.AdjustPostCode = this.AdjustPostCode;
            KarteDetailData.AdjustAddressPref = this.AdjustAddressPref;
            KarteDetailData.AdjustAddressCity = this.AdjustAddressCity;
            KarteDetailData.AdjustAddressOther = this.AdjustAddressOther;
            KarteDetailData.AdjustAddressKana = this.AdjustAddressKana;
            KarteDetailData.AdjustYmd = this.DateFormatNonSlushFromPicker(this.AdjustYmd);
            KarteDetailData.AdjustAmount = this.AdjustAmount;
            KarteDetailData.ConfirmYmd = this.DateFormatNonSlushFromPicker(this.ConfirmYmd);
            KarteDetailData.LandArea = this.LandArea;
            KarteDetailData.ChainClientMemo = this.ChainClientMemo;
            KarteDetailData.OtherMemo = this.OtherMemo;

            if (this.AddressComparisonFlg == false)
            {
                KarteDetailData.AddressComparisonFlg = 0;
            }
            else
            {
                KarteDetailData.AddressComparisonFlg = 1;
            }
        }

        /// <summary>
        /// 項目移送No3 家族構成一覧 画面プロパティ値をDTOに移送する
        /// </summary>
        private IList<FamilyStructureDto> ItemTransfarNo3(int clientId)
        {
            FamilyStructureDto dt;
            IList<FamilyStructureDto> listDt = new List<FamilyStructureDto>();

            int i = 0;
            foreach (FamilyStructureVM item in this.FamilyStructureData)
            {
                i++;
                dt = new FamilyStructureDto();
                dt.ClientId = clientId.ToString();

                if (item.PrincipalFlg == true)
                {
                    dt.PrincipalFlg = 1;
                }
                else
                {
                    dt.PrincipalFlg = 0;
                }

                dt.Name = SQLiteAdapter.Instance.ChkEmptyVal(item.Name);
                dt.NameKana = SQLiteAdapter.Instance.ChkEmptyVal(item.NameKana);
                dt.Relation = SQLiteAdapter.Instance.ChkEmptyVal(item.Relation);
                dt.Birthday = SQLiteAdapter.Instance.ChkEmptyVal(item.Birthday);
                dt.Honmeisei = SQLiteAdapter.Instance.ChkEmptyVal(item.Honmeisei);
                dt.OrderNo = i;
                listDt.Add(dt);
            }

            return listDt;
        }

        /// <summary>
        /// 項目移送No4 調整備品一覧 画面プロパティ値をDTOに移送する
        /// </summary>
        private IList<AdjustPartsDto> ItemTransfarNo4(int clientId)
        {
            AdjustPartsDto dt;
            IList<AdjustPartsDto> listDt = new List<AdjustPartsDto>();

            int i = 0;
            foreach (AdjustPartsVM item in this.AdjustPartsData)
            {
                i++;
                dt = new AdjustPartsDto();
                dt.ClientId = clientId.ToString();
                dt.AdjustYmd = SQLiteAdapter.Instance.ChkEmptyVal(this.DateFormatNonSlushFromPicker(item.AdjustYmd));
                dt.PartsCode = SQLiteAdapter.Instance.ChkEmptyVal(item.PartsCode);
                dt.Count = item.Count;
                dt.OrderNo = i;
                listDt.Add(dt);
            }

            return listDt;
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

        private String DateFormatSlushFromPicker(String sourceDate)
        {
            if (sourceDate != null && !sourceDate.Equals(String.Empty))
            {
                String[] splitDate = DateTime.Parse(sourceDate).ToShortDateString().Split(char.Parse("/"));
                String strYear = splitDate[0].Substring(0, 4);
                String strMonth = "0" + splitDate[1];
                String strDay = "0" + splitDate[2];
                return strYear + "/" + strMonth.Substring(strMonth.Length - 2, 2) + "/" + strDay.Substring(strDay.Length - 2, 2);
            }
            else
            {
                return String.Empty;
            }

        }

        private String DateFormatNonSlushFromPicker(String sourceDate)
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

        /// <summary>
        /// 都道府県コンボ取得
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<ComboItem> GetPrefComboList()
        {
            ObservableCollection<ComboItem> prefComboItems = new ObservableCollection<ComboItem>();

            prefComboItems.Add(new ComboItem { Value = "北海道", Display = "北海道" });
            prefComboItems.Add(new ComboItem { Value = "青森県", Display = "青森県" });
            prefComboItems.Add(new ComboItem { Value = "岩手県", Display = "岩手県" });
            prefComboItems.Add(new ComboItem { Value = "宮城県", Display = "宮城県" });
            prefComboItems.Add(new ComboItem { Value = "秋田県", Display = "秋田県" });
            prefComboItems.Add(new ComboItem { Value = "山形県", Display = "山形県" });
            prefComboItems.Add(new ComboItem { Value = "福島県", Display = "福島県" });
            prefComboItems.Add(new ComboItem { Value = "茨城県", Display = "茨城県" });
            prefComboItems.Add(new ComboItem { Value = "栃木県", Display = "栃木県" });
            prefComboItems.Add(new ComboItem { Value = "群馬県", Display = "群馬県" });
            prefComboItems.Add(new ComboItem { Value = "埼玉県", Display = "埼玉県" });
            prefComboItems.Add(new ComboItem { Value = "千葉県", Display = "千葉県" });
            prefComboItems.Add(new ComboItem { Value = "東京都", Display = "東京都" });
            prefComboItems.Add(new ComboItem { Value = "神奈川県", Display = "神奈川県" });
            prefComboItems.Add(new ComboItem { Value = "新潟県", Display = "新潟県" });
            prefComboItems.Add(new ComboItem { Value = "富山県", Display = "富山県" });
            prefComboItems.Add(new ComboItem { Value = "石川県", Display = "石川県" });
            prefComboItems.Add(new ComboItem { Value = "福井県", Display = "福井県" });
            prefComboItems.Add(new ComboItem { Value = "山梨県", Display = "山梨県" });
            prefComboItems.Add(new ComboItem { Value = "長野県", Display = "長野県" });
            prefComboItems.Add(new ComboItem { Value = "岐阜県", Display = "岐阜県" });
            prefComboItems.Add(new ComboItem { Value = "静岡県", Display = "静岡県" });
            prefComboItems.Add(new ComboItem { Value = "愛知県", Display = "愛知県" });
            prefComboItems.Add(new ComboItem { Value = "三重県", Display = "三重県" });
            prefComboItems.Add(new ComboItem { Value = "滋賀県", Display = "滋賀県" });
            prefComboItems.Add(new ComboItem { Value = "京都府", Display = "京都府" });
            prefComboItems.Add(new ComboItem { Value = "大阪府", Display = "大阪府" });
            prefComboItems.Add(new ComboItem { Value = "兵庫県", Display = "兵庫県" });
            prefComboItems.Add(new ComboItem { Value = "奈良県", Display = "奈良県" });
            prefComboItems.Add(new ComboItem { Value = "和歌山県", Display = "和歌山県" });
            prefComboItems.Add(new ComboItem { Value = "鳥取県", Display = "鳥取県" });
            prefComboItems.Add(new ComboItem { Value = "島根県", Display = "島根県" });
            prefComboItems.Add(new ComboItem { Value = "岡山県", Display = "岡山県" });
            prefComboItems.Add(new ComboItem { Value = "広島県", Display = "広島県" });
            prefComboItems.Add(new ComboItem { Value = "山口県", Display = "山口県" });
            prefComboItems.Add(new ComboItem { Value = "徳島県", Display = "徳島県" });
            prefComboItems.Add(new ComboItem { Value = "香川県", Display = "香川県" });
            prefComboItems.Add(new ComboItem { Value = "愛媛県", Display = "愛媛県" });
            prefComboItems.Add(new ComboItem { Value = "高知県", Display = "高知県" });
            prefComboItems.Add(new ComboItem { Value = "福岡県", Display = "福岡県" });
            prefComboItems.Add(new ComboItem { Value = "佐賀県", Display = "佐賀県" });
            prefComboItems.Add(new ComboItem { Value = "長崎県", Display = "長崎県" });
            prefComboItems.Add(new ComboItem { Value = "熊本県", Display = "熊本県" });
            prefComboItems.Add(new ComboItem { Value = "大分県", Display = "大分県" });
            prefComboItems.Add(new ComboItem { Value = "宮崎県", Display = "宮崎県" });
            prefComboItems.Add(new ComboItem { Value = "鹿児島県", Display = "鹿児島県" });
            prefComboItems.Add(new ComboItem { Value = "沖縄県", Display = "沖縄県" });

            return prefComboItems;
        }

        /// <summary>
        /// 編集中チェック
        /// </summary>
        /// <returns></returns>
        private bool ChkChanged()
        {
            bool isChanged = this.IsChenged;
            foreach (FamilyStructureVM item in this.FamilyStructureData)
            {
                if (item.IsChenged == true)
                {
                    isChanged = true;
                }
            }
            foreach (AdjustPartsVM item in this.AdjustPartsData)
            {
                if (item.IsChenged == true)
                {
                    isChanged = true;
                }
            }
            return isChanged;
        }

        /// <summary>
        /// ファイル保存先選択
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        private String GetFileName(int clientId)
        {
            String fileName = String.Empty;

            //SaveFileDialogクラスのインスタンスを作成
            SaveFileDialog sfd = new SaveFileDialog();

            try
            {
                //はじめのファイル名を指定する
                //はじめに「ファイル名」で表示される文字列を指定する
                sfd.FileName = "顧客カルテ_" + clientId.ToString() + ".xlsx";

                //はじめに表示されるフォルダを指定する
                sfd.InitialDirectory = @"C:\";

                //[ファイルの種類]に表示される選択肢を指定する
                //指定しない（空の文字列）の時は、現在のディレクトリが表示される
                sfd.Filter = "EXCELファイル(*.xlsx;*.xls)|*.xlsx;*.xls";

                //[ファイルの種類]ではじめに選択されるものを指定する
                sfd.FilterIndex = 1;

                //タイトルを設定する
                sfd.Title = "保存先のファイルを選択してください";

                //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
                sfd.RestoreDirectory = true;

                //既に存在するファイル名を指定したとき警告する
                //デフォルトでTrueなので指定する必要はない
                sfd.OverwritePrompt = true;

                //存在しないパスが指定されたとき警告を表示する
                //デフォルトでTrueなので指定する必要はない
                sfd.CheckPathExists = true;

                //ダイアログを表示する
                if (sfd.ShowDialog() == true)
                {
                    //OKボタンがクリックされたとき、選択されたファイル名を表示する
                    fileName = sfd.FileName;
                }

            }
            catch (Exception e)
            {
                throw e;
            }

   
            return fileName;
        }

        /// <summary>
        /// 空チェック
        /// </summary>
        /// <param name="argStr"></param>
        /// <returns></returns>
        private bool CheckEmptyVal(String argStr)
        {
            if (argStr == null || String.Empty.Equals(argStr))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
