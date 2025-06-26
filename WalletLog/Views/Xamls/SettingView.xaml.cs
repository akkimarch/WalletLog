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
            var vm = new SettingViewModel();
            this.DataContext = vm;
            //
            //// Loaded時 イベント追加 マウスダウンでPopupを閉じる
            //this.Loaded += (s, e) =>
            //{
            //    this.PreviewMouseDown += SettingView_PreviewMouseDown;
            //};

        }

        /// <summary>
        /// SettingViewのクローズ時イベント
        /// </summary>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            // ここで何か処理を行うことができます
            // 例えば、設定の保存など
            // 例: SaveSettings();
            // 例: e.Cancel = true; // クローズをキャンセルする場合
            var vm = this.DataContext as SettingViewModel;
            if (vm == null) return;

            vm.SaveExpenceCommand.Execute(null); // 保存コマンドを呼ぶ

        }


    }
}
