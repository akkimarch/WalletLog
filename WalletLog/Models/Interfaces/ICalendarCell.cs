using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletLog.Models.Interfaces
{
    public interface ICalendarCell
    {
        /// <summary>
        /// 各セルの日付
        /// </summary>
        public DateTime? Date { get; set; }
    }
}
