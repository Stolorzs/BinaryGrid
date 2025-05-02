using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight.Messaging;

namespace BinaryGrid.Model
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ButtonModel> Buttons { get; } = new ObservableCollection<ButtonModel>();
        private string _someText;

        public event PropertyChangedEventHandler? PropertyChanged;

        private string _hexText;

        public string HexText
        {
            get => _hexText;
            set
            {
                _hexText = value;
                OnPropertyChanged(nameof(HexText)); // 触发属性变更通知
            }
        }

        public string SomeText
        {
            get => _someText;
            set
            {
                _someText = value;
                OnPropertyChanged(nameof(SomeText)); // 触发属性变更通知
            }
        }
        public MainViewModel()
        {
            SomeText = "Start to use";
            // 订阅消息
            Messenger.Default.Register<UpdateSomeTextMessage>(this, message =>
            {
                SomeText = message.UpdateDecText;
                HexText = message.UpdateHexText;
            });
            // 初始化 8 个按钮

            for (int i = 1; i <= 8; i++)
            {
                var button = new ButtonModel
                {
                    Id = i,
                    Text = $"{256 >> i}",
                    BackgroundColor = Brushes.Red,
                };
                button.PropertyChanged += OnButtonPropertyChanged; // 监听属性变化
                Buttons.Add(button);
            }
            //初始化HexText
            HexText = "00";
        }
        // 实现 INotifyPropertyChanged 接口

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // 当按钮属性变化时触发
        private void OnButtonPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var displayResult = new List<int>();
            for (int i = 0; i < Buttons.Count; i++)
            {
                if (Buttons[i].BackgroundColor == Brushes.Green)
                    displayResult.Add(256 >> i+1);
            }
            var process = string.Join("+", displayResult);
            var res = displayResult.Sum();
            Messenger.Default.Send(new UpdateSomeTextMessage
            {
                UpdateDecText = process + "=" + res,
                UpdateHexText = Convert.ToString(res, 16).PadLeft(2, '0'),
            });

            
        }
    }
}
