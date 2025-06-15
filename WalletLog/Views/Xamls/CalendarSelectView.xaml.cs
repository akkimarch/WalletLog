using WalletLog.ViewModels;
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

namespace WalletLog.Views
{
    /// <summary>
    /// CalendarSelectView.xaml の相互作用ロジック
    /// </summary>
    public partial class CalendarSelectView : Window
    {
        public CalendarSelectView()
        {
            InitializeComponent();

            // DataContextが埋め込まれたときに、ViewModelのCloseActionを設定する
            //this.DataContextChanged += (s, e) =>
            //{
            //    if (e.NewValue is CalendarSelectViewModel vm)
            //    {
            //        vm.CloseAction = this.Close;
            //    }
            //};
        }
    }
}
