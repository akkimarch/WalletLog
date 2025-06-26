using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WalletLog.Models;

namespace WalletLog.ViewModels.Interfaces
{
    public interface ISettingViewModel
    {
        CustomCommand<object> AddExpenceCommand { get; set; }

        CustomCommand<object> RemoveExpenceCommand { get; set; }

        ObservableCollection<ExpenseItem> Expences { get; set; }
        CustomCommand SaveExpenceCommand { get; }

    }


}
