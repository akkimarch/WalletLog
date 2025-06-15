using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;
using WalletLog.Views;
using WalletLog.Models; /* あとで削除 */
using WalletLog.Models.Interfaces;
using WalletLog.ViewModels.Interfaces;

namespace WalletLog.ViewModels
{
    public class CalendarCellViewModel: ICalendarCellViewModel,INotifyPropertyChanged
    {
        #region public Properties(ICalendarViewModel)
        public CalendarCell? calendarDate { get; set; } = new CalendarCell();

        // ボタンに表示するテキスト（日にち or 空文字）
        public string DisplayDay => calendarDate?.Date?.Day.ToString() ?? string.Empty;

        // コマンド用のパラメータに使う
        public DateTime? ClickDate => calendarDate?.Date;

        public bool visibleMemo { get; set; } = false;
        public string MemoText { get; set; } = string.Empty;
        #endregion

        #region public Properties(INotifyPropertyChanged)
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region public Methods(INotifyPropertyChanged)
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion

    }
}
