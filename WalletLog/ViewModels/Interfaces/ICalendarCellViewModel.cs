using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletLog.Models;

namespace WalletLog.ViewModels.Interfaces
{
    public interface ICalendarCellViewModel
    {
        /// <summary>
        /// セルの日付
        /// </summary>
        CalendarCell? calendarDate { get; set; }

        /// <summary>
        /// ボタンに表示するテキスト（日にち or 空文字）
        /// </summary>
        string DisplayDay { get; }

        /// <summary>
        /// クリックした日付
        /// </summary>
        DateTime? ClickDate { get; }

        /// <summary>
        /// メモアイコンを表示するか
        /// </summary>
        bool visibleMemo { get; set; }

        /// <summary>
        /// メモテキスト
        /// </summary>
        string MemoText { get; set; }

    }
}
