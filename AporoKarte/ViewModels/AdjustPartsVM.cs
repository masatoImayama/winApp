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
    class AdjustPartsVM : BindableBase
    {

        private String _adjustYmd;
        public String AdjustYmd
        {
            get { return _adjustYmd; }
            set
            {
                var dtFormat = String.Empty;
                if (value != null && !String.Empty.Equals(value))
                {
                    dtFormat = DateTime.Parse(value).ToShortDateString();
                } 

                SetProperty(ref _adjustYmd, dtFormat);
                this.IsChenged = true;
                RaisePropertyChanged("AdjustYmd");
            }
        }

        private String _partsCode;
        public String PartsCode
        {
            get { return _partsCode; }
            set { SetProperty(ref _partsCode, value);
                this.IsChenged = true;
            }
        }

        private String _partsName;
        public String PartsName
        {
            get { return _partsName; }
            set { SetProperty(ref _partsName, value); }
        }


        private int _count;
        public int Count
        {
            get { return _count; }
            set { SetProperty(ref _count, value);
                this.IsChenged = true;
            }
        }

        public bool IsChenged = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AdjustPartsVM()
        {
        }

        #region コマンド登録

        /// <summary>
        /// 生年月日変更
        /// </summary>
        private DelegateCommand _changeAdjustDateCommand;
        public DelegateCommand ChangeAdjustDateCommand
        {
            get { return _changeAdjustDateCommand = _changeAdjustDateCommand ?? new DelegateCommand(ChangeAdjustDate); }
        }
        #endregion

        public void ChangeAdjustDate()
        {
            MessageBox.Show("test2");
        }
    }
}
