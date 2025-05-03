using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace BinaryGrid.Model
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ButtonModel> Buttons { get; } = new ObservableCollection<ButtonModel>();
        private string _someText;

        public event PropertyChangedEventHandler? PropertyChanged;

        private string _hexText;


        // 初始值确保必须选中一个模式
        private ModeType _selectedMode = ModeType.SignMagnitude;

        public ModeType SelectedMode
        {
            get => _selectedMode;
            set
            {
                if (_selectedMode != value)
                {
                    _selectedMode = value;
                    OnPropertyChanged(nameof(SelectedMode));
                    if (value == ModeType.SignMagnitude)
                    {
                        Buttons[0].Number = 128;
                    }
                    else if (value == ModeType.TwoComplement)
                    {
                        Buttons[0].Number = -128;
                    }
                }

            }
        }
        //十六进制显示文字
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
                    Number = 256 >> i,
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
                    displayResult.Add(Buttons[i].Number);
            }
            // 计算结果
            var process = string.Join("+", displayResult);
            var resDec = displayResult.Sum();
            var resHex = displayResult.Select(n => Math.Abs(n)).Sum();
            //发送信息使用MVVM将数据传递到View
            Messenger.Default.Send(new UpdateSomeTextMessage
            {
                UpdateDecText = process + "=" + resDec,
                UpdateHexText = Convert.ToString(resHex, 16).ToUpper().PadLeft(2, '0'),
            });
        }
        // 切换模式的命令
        public ICommand SetModeCommand => new RelayCommand<string>(mode =>
        {
            if (Enum.TryParse(mode, out ModeType newMode))
            {
                SelectedMode = newMode;
            }
        });


    }
}
