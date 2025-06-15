using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WalletLog.ViewModels;

namespace WalletLog.Views
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            // binding
            var vm = new MainViewModel();
            this.DataContext = vm;
        }

        /// <summary>
        /// クローズ時処理 (サマリ情報 JSON書き込み)
        /// メイン画面で「登録」押したときも書き込みする(スタンプ画面にすぐ反映させたい)が、
        /// そのままスタンプ画面から入力するごほうび内容が反映漏れるため、閉じる際に裏で再書き込みする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var vm = this.DataContext as MainViewModel;
            if(vm == null) return;

        }
    }
}
