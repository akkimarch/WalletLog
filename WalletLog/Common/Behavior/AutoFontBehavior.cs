using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Xml.Linq;

namespace WalletLog
{
    public static class AutoFontBehavior
    {
        // 添付プロパティ EnableAutoFont を定義（TextBox に機能をON/OFFできる）
        // "EnableAutoFont" というプロパティに True がセットされたら、自動的に OnEnableAutoFontChanged(...) が呼ばれる
        public static readonly DependencyProperty EnableAutoFontProperty =
            DependencyProperty.RegisterAttached(
                "EnableAutoFont",                // プロパティ名
                typeof(bool),                    // 型：bool（ON/OFF）
                typeof(AutoFontBehavior),        // 所属クラス
                new PropertyMetadata(false, OnEnableAutoFontChanged)); // 値が変わった時の処理
    
        // 通常のプロパティの set 相当
        public static void SetEnableAutoFont(DependencyObject element, bool value)
            => element.SetValue(EnableAutoFontProperty, value);
    
        // 通常のプロパティの get 相当
        public static bool GetEnableAutoFont(DependencyObject element)
            => (bool)element.GetValue(EnableAutoFontProperty);
    
        // EnableAutoFont の値が変わったとき呼ばれる
        private static void OnEnableAutoFontChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // 対象が TextBox だった場合
            if (d is TextBox box)
            {
                if ((bool)e.NewValue)
                {
                    // 機能を有効化 → Textが変わったときにサイズ調整
                    box.TextChanged += Box_TextChanged;
    
                    // 初期表示時にも調整（Textが既に入っている場合）
                    box.Loaded += Box_Loaded;
                }
                else
                {
                    // 機能を無効化 → イベント解除
                    box.TextChanged -= Box_TextChanged;
                    box.Loaded -= Box_Loaded;
                }
            }
            else if (d is TextBlock textBlock)
            {  // 対象が TextBlock だった場合

                // 自動調整
                AdjustFontSize(textBlock);

                // イベントをフックして変化に対応（例：Loaded後にテキストが入る場合）
                textBlock.Loaded += (s, _) => AdjustFontSize(textBlock);
                textBlock.SizeChanged += (s, _) => AdjustFontSize(textBlock);
            }
            else
            {
                // 対象が TextBox でも TextBlock でもない場合は何もしない
                return;
            }
        }
    
        // TextBoxのTextが変更されたときに呼ばれる
        private static void Box_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox box)
            {
                AdjustFontSize(box);
            }
        }
    
        private static void Box_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox || sender is TextBlock)
            {
                AdjustFontSize((FrameworkElement)sender);
            }
        }

        // 実際にフォントサイズを調整する処理
        //private static void AdjustFontSize(TextBox box)
        //{
        //    double maxFontSize = 20;   // 最大フォントサイズ
        //    double minFontSize = 8;    // 最小フォントサイズ
        //    double currentSize = maxFontSize;


        //    // テキストの実際のサイズを測るためのオブジェクト
        //    var formattedText = new FormattedText(
        //        box.Text,
        //        CultureInfo.CurrentCulture,
        //        FlowDirection.LeftToRight,
        //        new Typeface(box.FontFamily.ToString()),
        //        currentSize,
        //        Brushes.Black,
        //        VisualTreeHelper.GetDpi(box).PixelsPerDip);


        //    // テキストがTextBoxの幅を超えていたら、サイズを少しずつ縮める
        //    while (formattedText.Width > box.ActualWidth - 4 && currentSize > minFontSize)
        //    {
        //        currentSize -= 1;
        //        formattedText.SetFontSize(currentSize);
        //    }

        //    // フォントサイズを設定
        //    box.FontSize = currentSize;
        //}

        private static void AdjustFontSize(FrameworkElement element)
        {
            double maxFontSize = 20; // 最大フォントサイズ
            double minFontSize = 8;  // 最小フォントサイズ
            double currentSize = maxFontSize;

            string text = "";
            FontFamily fontFamily;

            // 対象が TextBox または TextBlock かで分岐してテキストを取得
            if (element is TextBox textBox)
            {
                text = textBox.Text;
                fontFamily = textBox.FontFamily;
            }
            else if (element is TextBlock textBlock)
            {
                text = textBlock.Text;
                fontFamily = textBlock.FontFamily;
            }
            else
            {
                return; // どちらでもない場合は何もしない
            }


            // テキストサイズ計測用の FormattedText を作成
            var formattedText = new FormattedText(
                text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(fontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal),
                currentSize,
                Brushes.Black,
                VisualTreeHelper.GetDpi(element).PixelsPerDip);
        
            // テキストが幅を超える間、フォントサイズを1ずつ縮める
            while (formattedText.Width > element.ActualWidth - 4 && currentSize > minFontSize)
            {
                currentSize -= 1;
                formattedText.SetFontSize(currentSize);
            }
        
            // 実際にフォントサイズを適用（対象の型ごとに分けて設定）
            if (element is TextBox tb)
                tb.FontSize = currentSize;
            else if (element is TextBlock tblock)
                tblock.FontSize = currentSize;
        }



    }
}
