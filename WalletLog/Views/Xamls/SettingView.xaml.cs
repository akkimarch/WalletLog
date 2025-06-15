using WalletLog.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WalletLog.Views
{
    /// <summary>
    /// SettingView.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingView : Window
    {
        public SettingView()
        {
            InitializeComponent();

            // binding
            //var vm = new SettingViewModel();
            //this.DataContext = vm;
            //
            //// Loaded時 イベント追加 マウスダウンでPopupを閉じる
            //this.Loaded += (s, e) =>
            //{
            //    this.PreviewMouseDown += SettingView_PreviewMouseDown;
            //};

        }
    }
}
