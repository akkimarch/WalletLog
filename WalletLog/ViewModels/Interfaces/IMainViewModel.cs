using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace WalletLog.ViewModels.Interfaces
{
    internal interface IMainViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        CustomCommand OpenCalendarCommand { get; set; }

        CustomCommand PreviousDayCommand { get; set; }
        CustomCommand NextDayCommand { get; set; }
        CustomCommand CalendarSelectCommand { get; set; }
        CustomCommand OpenFixedCostsSettingCommand { get; set; }
        
        CustomCommand OpenBankCommand { get; set; }
        CustomCommand OpenSettingCommand { get; set; }
        CustomCommand OpenReadReceiptCommand {get; set; }
        CustomCommand RegistExpenceCommand { get; set; }

        IDisplayDaySet CurrentDaySet { get; }

        DateTime CurrentDate { get; set; }

        bool IsCalendarOpen { get; set; }

    }

    public class DisplayDaySet : IDisplayDaySet
    {  
       public ObservableCollection<Consumption> DailyConsumption { get; set; } = new ObservableCollection<Consumption>();

    }

    public interface IDisplayDaySet
    {
        ObservableCollection<Consumption> DailyConsumption { get; set; }
    }

    public class Consumption
    {
        string CategoryName { get; set; } = string.Empty;
        string Amount { get; set; } = string.Empty;

    }

    public interface IConsumption
    {
        string CategoryName { get; set; }
        string Amount { get; set; }
    }



}
