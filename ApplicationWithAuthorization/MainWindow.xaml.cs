using System.Windows;
using ApplicationWithAuthorization.ViewModel;

// Бэкграунд код внешней части MainWindow.xaml. MainWindow.xaml - стартовое окно при запуске программы.
// Так как применяется паттерн MVVM, то код-бехайнд для бэкграунд части окна отсутствует.
// Весь код применен в StartupWindowViewModel классе, что является представлением модели для данного окна.
// Поэтому свойству окна DataContext присвоена ссылка на StartupWindowViewModel.

namespace ApplicationWithAuthorization
{    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new StartupWindowViewModel(this);
        }
    }
}