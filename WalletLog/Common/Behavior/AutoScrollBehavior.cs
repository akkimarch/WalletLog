using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace WalletLog
{
    /// <summary>
    /// ListBoxにアイテムが追加されたら、自動で一番下にスクロールする
    /// </summary>
    public static class AutoScrollBehavior
    {
        public static readonly DependencyProperty EnableAutoScrollProperty =
            DependencyProperty.RegisterAttached(
                "EnableAutoScroll",
                typeof(bool),
                typeof(AutoScrollBehavior),
                new PropertyMetadata(false, OnEnableAutoScrollChanged));
    
        public static bool GetEnableAutoScroll(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableAutoScrollProperty);
        }
    
        public static void SetEnableAutoScroll(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableAutoScrollProperty, value);
        }
    
        private static void OnEnableAutoScrollChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var listBox = d as ListBox;
            if (listBox == null) return;
    
            if ((bool)e.NewValue)
            {
                listBox.Loaded += (s, args) =>
                {
                    TryAttachCollectionChangedHandler(listBox);
                };
            }
        }

        private static void TryAttachCollectionChangedHandler(ListBox listBox)
        {
            if (listBox.ItemsSource is INotifyCollectionChanged notifyCollection)
            {
                notifyCollection.CollectionChanged += (s, args) =>
                {
                    if (args.Action == NotifyCollectionChangedAction.Add)
                    {
                        listBox.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            if (listBox.Items.Count > 0)
                                listBox.ScrollIntoView(listBox.Items[listBox.Items.Count - 1]);
                        }));
                    }
                };
            }
        }

    }
}
