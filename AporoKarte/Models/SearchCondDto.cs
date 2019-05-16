using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Commands;
using Prism.Mvvm;

namespace AporoKarte.Models
{
    class SearchCondDto : BindableBase
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

        private String _adjustYmdStart = String.Empty;
        public String AdjustYmdStart
        {
            get { return _adjustYmdStart; }
            set { SetProperty(ref _adjustYmdStart, value); }
        }

        private String _adjustYmdEnd = String.Empty;
        public String AdjustYmdEnd
        {
            get { return _adjustYmdEnd; }
            set { SetProperty(ref _adjustYmdEnd, value); }
        }
    }
}
