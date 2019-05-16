
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using AporoKarte.ViewModels;

namespace AporoKarte.Views
{

    /// <summary>
    /// KarteDetail.xaml の相互作用ロジック
    /// </summary>
    public partial class KarteDetail : Window
    {
        public KarteDetail()
        {
            InitializeComponent();

            this.DataContext = new KarteDetailVM();
        }

    }
}
