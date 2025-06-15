using GongSolutions.Wpf.DragDrop;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Resources;
using WalletLog.ViewModels.Interfaces;
using WalletLog.Views;

namespace WalletLog.ViewModels
{
    public class MainViewModel : IMainViewModel,INotifyPropertyChanged, IDropTarget
    {
        #region Constructor
        public MainViewModel()
        {
            // カスタムコマンドをバインド
            PreviousDayCommand = new CustomCommand(() => ChangeDate(-1));
            NextDayCommand = new CustomCommand(() => ChangeDate(1));

            CalendarSelectCommand = new CustomCommand(() => IsCalendarOpen = true);
            ChangeToSpecificDateCommand = new CustomCommand<DateTime>(date =>
            {
                CurrentDate = date;
            });

            OpenSettingCommand = new CustomCommand(OpenSetting);
            OpenFixedCostsSettingCommand = new CustomCommand(OpenFixedCostsSetting);
            OpenCalendarCommand = new CustomCommand(OpenCalendar);
            OpenBankCommand = new CustomCommand(OpenBank);
            OpenReadReceiptCommand = new CustomCommand(OpenReadReceipt);
            RegistExpenceCommand = new CustomCommand(RegistExpence);
        }

        #endregion

        #region public Method(IMainViewModel)

        public CustomCommand PreviousDayCommand {get; set; }
        public CustomCommand NextDayCommand { get; set; }
        
        public CustomCommand CalendarSelectCommand { get; set; }

        CustomCommand<DateTime> ChangeToSpecificDateCommand { get; set; }

        public CustomCommand OpenSettingCommand { get; set; }
        public CustomCommand OpenFixedCostsSettingCommand { get; set; }
        public CustomCommand OpenCalendarCommand { get; set; }
        public CustomCommand OpenBankCommand { get; set; }

        public CustomCommand OpenReadReceiptCommand { get; set; }

        public CustomCommand RegistExpenceCommand { get; set; }

        public IDisplayDaySet CurrentDaySet { get; } = new DisplayDaySet
        {
            DailyConsumption = new ObservableCollection<Consumption>()
        };

        public DateTime CurrentDate { get; set; }

        public bool IsCalendarOpen { get; set; }

        #endregion

        #region public Method(IDropTarget)
        /// <summary>
        /// ドラッグオーバー時処理
        /// </summary>
        /// <param name="dropInfo"></param>
        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.Effects = DragDropEffects.Move;
            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
        }

        /// <summary>
        /// ドロップ時処理
        /// </summary>
        /// <param name="dropInfo"></param>
        public void Drop(IDropInfo dropInfo)
        {
            //var source = dropInfo.Data as TaskItem;
            //var target = dropInfo.TargetItem as TaskItem;

            //if (source != null && target != null && source != target)
            //{
            //    int oldIndex = CurrentDaySet.DailyTasks.IndexOf(source);
            //    int newIndex = CurrentDaySet.DailyTasks.IndexOf(target);
            //    if (oldIndex >= 0 && newIndex >= 0)
            //    {
            //        CurrentDaySet.DailyTasks.Move(oldIndex, newIndex);
            //    }
            //}
        }

        #endregion

        #region public Method(INotifyPropertyChanged)
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        #endregion


        #region private Method

        /// <summary>
        /// 設定画面を開く
        /// </summary>
        private void OpenSetting()
        {
            var setting = new SettingView();
            setting.ShowDialog(); // モーダル表示
        }

        /// <summary>
        /// 
        /// OpenFixedCostsSetting 固定費設定ウィンドウを開く
        /// </summary>
        private void OpenFixedCostsSetting()
        {
            // 固定費設定ウィンドウを開く処理
            MessageBox.Show("固定費設定ウィンドウを開きます。");
        }


        /// <summary>
        /// カレンダーウィンドウを開く
        /// </summary>
        private void OpenCalendar()
        {
            var vm = new CalendarCellViewModel();
            var calendarWindow = new CalendarView { DataContext = vm };

            bool? result = calendarWindow.ShowDialog();
            // メモ内容取り込み
            if (result == true)
            {   // OKボタンで閉じた (Cancelならfalse)
                //CurrentDaySet.Memo = vm.MemoText;   // メモ保存
            }
        }

        /// <summary>
        /// OpenBank 銀行口座ウィンドウを開く
        /// </summary>
        private void OpenBank()
        {
            // 銀行口座ウィンドウを開く処理
            MessageBox.Show("銀行口座ウィンドウを開きます。");
        }

        /// <summary>
        /// OpenReadReceipt 読み取り確認ウィンドウを開く
        /// </summary>
        private void OpenReadReceipt()
        {
            // 読み取り確認ウィンドウを開く処理
            MessageBox.Show("読み取り確認ウィンドウを開きます。");
        }

        /// <summary>
        /// 日付を前後に変更する
        /// </summary>
        /// <param name="offset"></param>
        private void ChangeDate(int offset)
        {
            CurrentDate = CurrentDate.AddDays(offset);
            OnPropertyChanged(nameof(CurrentDate));
        }

        /// <summary>
        /// 支出を登録する
        /// </summary>
        private void RegistExpence()
        {
            // 支出登録処理
            MessageBox.Show("支出を登録します。");
        }
        #endregion


    }

}