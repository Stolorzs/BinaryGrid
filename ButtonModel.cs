using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;

namespace BinaryGrid.Model
{
    public class ButtonModel : INotifyPropertyChanged
    {
        private Brush _backgroundColor;
        // 公共属性（例如按钮显示文本、颜色等）
        public string Text => Number.ToString();

        private int _number;
        public int Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged(nameof(Number)); // 触发 Number 更新
                OnPropertyChanged(nameof(Text));// 触发 Text 更新
            }
        }



        // 支持变更通知的 BackgroundColor
        public Brush BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        // 按钮的唯一标识（可选）
        public int Id { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        //变更方法
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // 按钮的私有方法（可定义不同逻辑）
        private void ExecuteAction()
        {
            if (BackgroundColor == Brushes.Red)
            {
                BackgroundColor = Brushes.Green;
            }
            else
            {
                BackgroundColor = Brushes.Red;
            }
            // 根据 Id 或其他属性执行不同逻辑
            switch (Id)
            {
                case 1:
                    //       MethodForButton1();
                    break;
                case 2:
                    //     MethodForButton2();
                    break;
                    // ...其他按钮逻辑
            }
        }

        // 公共命令（绑定到按钮的点击事件）
        public ICommand ClickCommand => new RelayCommand(ExecuteAction);

        // 示例方法
        //   private void MethodForButton1() => MessageBox.Show("按钮1被点击");
        //   private void MethodForButton2() => MessageBox.Show("按钮2被点击");
    }

}
