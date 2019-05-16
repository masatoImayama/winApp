using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Commands;
using Prism.Mvvm;

namespace AporoKarte.Models
{
    class KarteDetailDto : BindableBase
    {
        private String _clientId = String.Empty;
        public String ClientId {
            get { return _clientId;  }
            set { SetProperty(ref _clientId, value); }
        }

        private String _clientName = String.Empty;
        public String ClientName
        {
            get { return _clientName; }
            set { SetProperty(ref _clientName, value); }
        }

        private String _clientNameKana = String.Empty;
        public String ClientNameKana
        {
            get { return _clientNameKana; }
            set { SetProperty(ref _clientNameKana, value); }
        }

        private String _introductorName = String.Empty;
        public String IntroductorName
        {
            get { return _introductorName; }
            set { SetProperty(ref _introductorName, value); }
        }

        private String _introductorNameKana = String.Empty;
        public String IntroductorNameKana
        {
            get { return _introductorNameKana; }
            set { SetProperty(ref _introductorNameKana, value); }
        }

        private String _postCode = String.Empty;
        public String PostCode
        {
            get { return _postCode; }
            set { SetProperty(ref _postCode, value); }
        }

        private String _addressPref = String.Empty;
        public String AddressPref
        {
            get { return _addressPref; }
            set { SetProperty(ref _addressPref, value); }
        }

        private String _addressCity = String.Empty;
        public String AddressCity
        {
            get { return _addressCity; }
            set { SetProperty(ref _addressCity, value); }
        }

        private String _addressOther = String.Empty;
        public String AddressOther
        {
            get { return _addressOther; }
            set { SetProperty(ref _addressOther, value); }
        }

        private String _addressKana = String.Empty;
        public String AddressKana
        {
            get { return _addressKana; }
            set { SetProperty(ref _addressKana, value); }
        }

        private String _tel = String.Empty;
        public String Tel
        {
            get { return _tel; }
            set { SetProperty(ref _tel, value); }
        }

        private String _fax = String.Empty;
        public String Fax
        {
            get { return _fax; }
            set { SetProperty(ref _fax, value); }
        }

        private String _consultationYmd = String.Empty;
        public String ConsultationYmd
        {
            get { return _consultationYmd; }
            set { SetProperty(ref _consultationYmd, value); }
        }

        private String _opinionYmd = String.Empty;
        public String OpinionYmd
        {
            get { return _opinionYmd; }
            set { SetProperty(ref _opinionYmd, value); }
        }


        private String _adjustPostCode = String.Empty;
        public String AdjustPostCode
        {
            get { return _adjustPostCode; }
            set { SetProperty(ref _adjustPostCode, value); }
        }

        private String _adjustAddressPref = String.Empty;
        public String AdjustAddressPref
        {
            get { return _adjustAddressPref; }
            set { SetProperty(ref _adjustAddressPref, value); }
        }

        private String _adjustAddressCity = String.Empty;
        public String AdjustAddressCity
        {
            get { return _adjustAddressCity; }
            set { SetProperty(ref _adjustAddressCity, value); }
        }

        private String _adjustAddressOther = String.Empty;
        public String AdjustAddressOther
        {
            get { return _adjustAddressOther; }
            set { SetProperty(ref _adjustAddressOther, value); }
        }

        private String _adjustAddressKana = String.Empty;
        public String AdjustAddressKana
        {
            get { return _adjustAddressKana; }
            set { SetProperty(ref _adjustAddressKana, value); }
        }

        private String _adjustYmd = String.Empty;
        public String AdjustYmd
        {
            get { return _adjustYmd; }
            set { SetProperty(ref _adjustYmd, value); }
        }

        private int _adjustAmount = 0;
        public int AdjustAmount
        {
            get { return _adjustAmount; }
            set { SetProperty(ref _adjustAmount, value); }
        }

        private String _confirmYmd = String.Empty;
        public String ConfirmYmd
        {
            get { return _confirmYmd; }
            set { SetProperty(ref _confirmYmd, value); }
        }

        private String _landArea = String.Empty;
        public String LandArea
        {
            get { return _landArea; }
            set { SetProperty(ref _landArea, value); }
        }

        private String _chainClientMemo = String.Empty;
        public String ChainClientMemo
        {
            get { return _chainClientMemo; }
            set { SetProperty(ref _chainClientMemo, value); }
        }

        private String _otherMemo = String.Empty;
        public String OtherMemo
        {
            get { return _otherMemo; }
            set { SetProperty(ref _otherMemo, value); }
        }

        private int _addressComparisonFlg = 0;
        public int AddressComparisonFlg
        {
            get { return _addressComparisonFlg; }
            set { SetProperty(ref _addressComparisonFlg, value); }
        }
    }
}
