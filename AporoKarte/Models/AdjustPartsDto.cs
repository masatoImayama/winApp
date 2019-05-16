using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Commands;
using Prism.Mvvm;

namespace AporoKarte.Models
{
    class AdjustPartsDto : BindableBase
    {
        private String _clientId = String.Empty;
        public String ClientId {
            get { return _clientId;  }
            set { SetProperty(ref _clientId, value); }
        }

        private String _adjustYmd;
        public String AdjustYmd
        {
            get { return _adjustYmd; }
            set { SetProperty(ref _adjustYmd, value); }
        }

        private String _partsCode;
        public String PartsCode
        {
            get { return _partsCode; }
            set { SetProperty(ref _partsCode, value); }
        }

        private int _count;
        public int Count
        {
            get { return _count; }
            set { SetProperty(ref _count, value); }
        }

        private int _orderNo;
        public int OrderNo
        {
            get { return _orderNo; }
            set { SetProperty(ref _orderNo, value); }
        }
    }
}
