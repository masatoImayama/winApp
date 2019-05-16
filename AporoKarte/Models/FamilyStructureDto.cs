using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Commands;
using Prism.Mvvm;

namespace AporoKarte.Models
{
    class FamilyStructureDto : BindableBase
    {
        private String _clientId = String.Empty;
        public String ClientId {
            get { return _clientId;  }
            set { SetProperty(ref _clientId, value); }
        }

        private int _principalFlg;
        public int PrincipalFlg
        {
            get { return _principalFlg; }
            set { SetProperty(ref _principalFlg, value); }
        }

        private String _name;
        public String Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private String _nameKana;
        public String NameKana
        {
            get { return _nameKana; }
            set { SetProperty(ref _nameKana, value); }
        }

        private String _birthday;
        public String Birthday
        {
            get { return _birthday; }
            set
            {
                SetProperty(ref _birthday, value);
            }
        }

        private String _relation;
        public String Relation
        {
            get { return _relation; }
            set { SetProperty(ref _relation, value); }
        }

        private String _honmeisei;
        public String Honmeisei
        {
            get { return _honmeisei; }
            set { SetProperty(ref _honmeisei, value); }
        }

        private int _orderNo;
        public int OrderNo
        {
            get { return _orderNo; }
            set { SetProperty(ref _orderNo, value); }
        }
    }
}
