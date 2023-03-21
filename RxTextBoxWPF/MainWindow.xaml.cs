using System.Windows;

namespace RxTextBoxWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // XAML上でDataContextを指定しない場合はインスタンスを生成する
            // XAML上で指定するとインテリセンスなどが働くが、伝搬を考えるとこちらのほうが良い？？
            //DataContext = new MainWindowViewModel();
        }
    }
}
