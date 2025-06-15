//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Input;

//namespace WalletLog.ViewModels.Interfaces
//{
//    public interface ICalendarViewModel
//    {

//        // カレンダーに表示するセルのリスト
//        public ObservableCollection<CalendarCellViewModel> CalendarDates { get; set; }

//        // 日付クリック時のコマンド
//        public ICommand DateClickCommand { get; set; }
//        public string CurrentMonthText => $"{CurrentYear}年{CurrentMonth}月";
        
//        public ICommand PreviousMonthCommand { get; set; }
//        public ICommand NextMonthCommand { get; set; }
//        public CustomCommand CalendarSelectCommand { get; }

//        public ICommand OpenMemoCommand{ get; set; }

//        private int CurrentYear { get; set; }
//        private int CurrentMonth { get; set; }

//        public List<int> Years { get; } = Enumerable.Range(2000, 50).ToList(); // 2000年〜2049年
//        public List<int> Months { get; } = Enumerable.Range(1, 12).ToList();
        
//        public int SelectedYear { get; set; }
//        public int SelectedMonth { get; set; }
        
//        //public ICommand ConfirmDateCommand { get; }

//        public bool IsCalendarOpen
//        {
//            get => _isCalendarOpen;
//            set
//            {
//                _isCalendarOpen = value;
//                OnPropertyChanged(nameof(IsCalendarOpen));
//                Console.WriteLine($"Popup open: {value}"); // ← ここで確認
//            }
//        }

//        public DateTime CurrentDate
//        {
//            get => new DateTime(SelectedYear, SelectedMonth, 1);
//            set
//            {
//                SelectedYear = value.Year;
//                SelectedMonth = value.Month;
//                OnPropertyChanged(nameof(SelectedYear));
//                OnPropertyChanged(nameof(SelectedMonth));
//            }
//        }

//        public CalendarViewModel()
//        {
//            CalendarDates = new ObservableCollection<CalendarCellViewModel>();
//            DateClickCommand = new CustomCommand<object>(OnDateClicked);
//            PreviousMonthCommand = new CustomCommand( ()=>ChangeMonth(-1));
//            NextMonthCommand = new CustomCommand( ()=>ChangeMonth(1));
//            CalendarSelectCommand = new CustomCommand(() => IsCalendarOpen = true);
//            //OpenMemoCommand = new CustomCommand<CalendarCellViewModel>(OpenMemo);

//            var today = DateTime.Today;
//            CurrentYear = today.Year;
//            CurrentMonth = today.Month;
        
//            GenerateCalendar(CurrentYear, CurrentMonth);
//        }


//    }
//}
