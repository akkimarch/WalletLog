using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletLog
{
    /// <summary>
    /// VM-V イベント通知用
    /// </summary>
    public static class AppMessenger
    {
        public static event Action? TaskDataChanged;

        public static void NotifyTaskDataChanged() => TaskDataChanged?.Invoke();
    }
}
