using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WalletLog.Models; /* あとで削除 */
using WalletLog.Models.Interfaces;
using WalletLog.ViewModels.Interfaces;

namespace WalletLog.ViewModels
{
    public class CalendarViewModel: INotifyPropertyChanged
    {
        // カレンダーに表示するセルのリスト
        public ObservableCollection<CalendarCellViewModel> CalendarDates { get; set; }

        // 日付クリック時のコマンド
        public ICommand DateClickCommand { get; set; }
        public string CurrentMonthText => $"{CurrentYear}年{CurrentMonth}月";
        
        public ICommand PreviousMonthCommand { get; set; }
        public ICommand NextMonthCommand { get; set; }
        public CustomCommand CalendarSelectCommand { get; }

        //public ICommand OpenMemoCommand{ get; set; }

        private int CurrentYear { get; set; }
        private int CurrentMonth { get; set; }

        public List<int> Years { get; } = Enumerable.Range(2000, 50).ToList(); // 2000年〜2049年
        public List<int> Months { get; } = Enumerable.Range(1, 12).ToList();
        
        public int SelectedYear { get; set; }
        public int SelectedMonth { get; set; }
        
        private bool _isCalendarOpen;
        public bool IsCalendarOpen
        {
            get => _isCalendarOpen;
            set
            {
                _isCalendarOpen = value;
                OnPropertyChanged(nameof(IsCalendarOpen));
                Console.WriteLine($"Popup open: {value}"); // ← ここで確認
            }
        }

        public DateTime CurrentDate
        {
            get => new DateTime(SelectedYear, SelectedMonth, 1);
            set
            {
                SelectedYear = value.Year;
                SelectedMonth = value.Month;
                OnPropertyChanged(nameof(SelectedYear));
                OnPropertyChanged(nameof(SelectedMonth));
            }
        }

        public CalendarViewModel()
        {
            CalendarDates = new ObservableCollection<CalendarCellViewModel>();
            DateClickCommand = new CustomCommand<object>(OnDateClicked);
            PreviousMonthCommand = new CustomCommand( ()=>ChangeMonth(-1));
            NextMonthCommand = new CustomCommand( ()=>ChangeMonth(1));
            CalendarSelectCommand = new CustomCommand(() => IsCalendarOpen = true);
            //OpenMemoCommand = new CustomCommand<CalendarCellViewModel>(OpenMemo);

            var today = DateTime.Today;
            CurrentYear = today.Year;
            CurrentMonth = today.Month;
        
            GenerateCalendar(CurrentYear, CurrentMonth);
        }

        private void OnDateClicked(object param)
        {
            if (param is DateTime date)
            {
                System.Windows.MessageBox.Show($"{date:yyyy年MM月dd日} がクリックされました。");
            }
        }


        private void ChangeMonth(int offset)
        {
            // 月をずらす
            var newDate = new DateTime(CurrentYear, CurrentMonth, 1).AddMonths(offset);
            CurrentYear = newDate.Year;
            CurrentMonth = newDate.Month;
            OnPropertyChanged(nameof(CurrentMonthText));
            GenerateCalendar(CurrentYear, CurrentMonth);
        }
        
        private void GenerateCalendar(int year, int month)
        {
            CalendarDates.Clear();
        
            var firstDay = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int startDayOfWeek = (int)firstDay.DayOfWeek;
        
            // 曜日に合わせた空白セル（null日付）
            for (int i = 0; i < startDayOfWeek; i++)
            {
                var calendarCell = new CalendarCellViewModel {calendarDate= null};
                CalendarDates.Add(calendarCell);
            }

            // 実際の日付を持つセルを追加
            for (int day = 1; day <= daysInMonth; day++)
            {
                var calendarCell = new CalendarCellViewModel { calendarDate = new CalendarCell { Date = new DateTime(year, month, day) } };

                // json\DailyFile\daily_yyyyMMdd.jsonを読み込み、メモがあれば visibleMemo = trueにする、スタンプ達成していれば isStamped = trueにする
                var date = new DateTime(year, month, day);
                string filename = $"daily_{year}{month:D2}{day:D2}.json";
                string filePath = System.IO.Path.Combine("json", "DailyFile", filename);
                //if (System.IO.File.Exists(filePath))
                //{
                //    // JSONファイルを読み込む
                //    string json = System.IO.File.ReadAllText(filePath);
                //    DailySet currentDaySet = new DailySet();
                //    currentDaySet.LoadTasksForCurrentDate(date);  // タスクリストをロード

                //    // メモがあれば visibleMemo = trueにする
                //    if (currentDaySet.Memo != null && !string.IsNullOrEmpty(currentDaySet.Memo))
                //    {
                //        calendarCell.visibleMemo = true;
                //        calendarCell.MemoText = currentDaySet.Memo;
                //    }

                //    // スタンプ達成していれば スタンプマーク表示
                //    calendarCell.isStamped = currentDaySet.isStamped;
                //}


                CalendarDates.Add(calendarCell);
            }
        }

        //private void OpenMemo(CalendarCellViewModel calendarCell)
        //{
        //    var vm = new MemoWindowViewModel();
        //    vm.MemoText = calendarCell.MemoText;
        //    var memoWindow = new MemoWindow { DataContext = vm };

        //    bool? result = memoWindow.ShowDialog(); // メモ内容表示 (いずれ書き込み不可ウィンドウにする)
        //}


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
