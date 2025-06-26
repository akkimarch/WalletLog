using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Text.Json.Serialization;


namespace WalletLog.Models
{
    public class DailySet
    {
        // ItemsSourceからバインドできるようプロパティとして公開する
        public ObservableCollection<ExpenseItem> DailyExpenseItems { get; } = new ObservableCollection<ExpenseItem>();
        public DateTime? CurrentDate { get; set; }

        public void Clear()
        {
            DailyExpenseItems.Clear();
        }

        /// <summary>
        /// 当日分のタスクをロード
        /// </summary>
        /// <param name="date"></param>
        public void LoadExpencesForCurrentDate(DateTime date)
        {
            CurrentDate = date;

            const string settingFile = $"json\\expences.json";
            string currentDaysFile = $"json\\DailyFile\\daily_{CurrentDate:yyyyMMdd}.json";  // 選択日付の記録ファイル

            // 記録ファイルが既にあるなら既存ファイルから、ない(初めて開く)ならタスク一覧から読み出し
            string jsonSource = currentDaysFile;
            if (!File.Exists(currentDaysFile))
            {
                jsonSource = settingFile;
            }

            this.Clear();
            try
            {
                if (File.Exists(jsonSource))
                {
                    string json = File.ReadAllText(jsonSource);
                    List<ExpenseItem>? loadedExpences = new();

                    try
                    {
                        var loadedDailySet = JsonSerializer.Deserialize<List<ExpenseItem>>(json);
                        loadedExpences = loadedDailySet ?? null;
                    }
                    catch (JsonException)
                    {   // 旧型式
                    }

                    if (loadedExpences != null)
                    {
                        // 無条件追加
                        foreach (var Expences in loadedExpences)
                        {
                            this.DailyExpenseItems.Add(Expences);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"タスクリスト 読み込みエラー: {ex.Message}");
                return;
            }
        }



    }
}
