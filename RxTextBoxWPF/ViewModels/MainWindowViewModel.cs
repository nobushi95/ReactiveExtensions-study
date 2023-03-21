using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;

namespace RxTextBoxWPF.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _input = "Input";
        private string _output = "Output: ";

        public string Input
        {
            get => _input;
            set
            {
                _input = value;
                OnPropertyChanged(nameof(Input));
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Input))); // 上と等価？
            }
        }

        public string Output
        {
            get => _output;
            set
            {
                _output = value;
                OnPropertyChanged(nameof(Output));
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Output))); // 上と等価？
            }
        }

        public MainWindowViewModel()
        {
            Observable.FromEvent<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                action => (sender, e) => action(e), // conversion
                eventHandler => this.PropertyChanged += eventHandler, // addHandler
                eventHandler => this.PropertyChanged -= eventHandler // removeHandler
                )
                .Where(eventArgs => eventArgs.PropertyName == nameof(Input)) // 変更されたか確認するものをInputにしぼる
                .Select(_ => Input) // 変更対象はInputの値
                /* .ObserveOnDispatcher() // スレッドをUIスレッドに変更(WPFでは不要で必要な場合は例外発生？) */
                .Subscribe(x => Output = $"Output: {x}");
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler is not null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
