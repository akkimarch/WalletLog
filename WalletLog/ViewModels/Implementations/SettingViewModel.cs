using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.IO;
using WalletLog.Models;
using WalletLog.ViewModels.Interfaces;

namespace WalletLog.ViewModels
{
    public class SettingViewModel : ISettingViewModel
    {
        public CustomCommand<object> AddExpenceCommand { get; set; }

        public CustomCommand<object> RemoveExpenceCommand { get; set; }

        public ObservableCollection<ExpenseItem> Expences { get; set; }
        public CustomCommand SaveExpenceCommand { get; }

        public SettingViewModel()
        {
            // コマンドの初期化
            AddExpenceCommand = new CustomCommand<object>(AddExpence);
            RemoveExpenceCommand = new CustomCommand<object>(RemoveExpence);
            SaveExpenceCommand = new CustomCommand(SaveExpence);

            // 初期化
            Expences = new ObservableCollection<ExpenseItem>();
        }

        private void AddExpence(object? obj)
        {
            // 新しいExpenseItemを追加
            Expences.Add(new ExpenseItem { Name = "New Expense", Amount = 0 });
        }

        private void RemoveExpence(object? obj)
        {
            // 選択されたExpenseItemを削除
            if (obj is ExpenseItem item && Expences.Contains(item))
            {
                Expences.Remove(item);
            }

        }

        // ExpencesSettings の一覧を JSON ファイルに保存する
        private void SaveExpence()
        {
            // タスク名重複チェック
            foreach (var expence in Expences)
            {
                if (Expences.Count(t => t.Name == expence.Name) > 1)
                {   // 重複あり
                    MessageBox.Show($"エラー タスク名に重複があります");
                    return;
                }
            }

            // json書き込み
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true, // 見やすく整形
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                string json = JsonSerializer.Serialize(
                                            Expences.ToList(), // ObservableCollectionはシリアライズできないので、Listに変換
                                            options);

                File.WriteAllText(settingFile, json);

                // メイン画面に通知
                AppMessenger.NotifyTaskDataChanged();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存エラー: {ex.Message}");
            }
        }

        private const string settingFile = $"json\\expences.json";



    }
}
