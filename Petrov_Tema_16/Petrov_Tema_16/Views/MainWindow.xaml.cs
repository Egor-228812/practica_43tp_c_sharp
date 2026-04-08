using System.Windows;
using Petrov_Tema_16.Services;
using Petrov_Tema_16.ViewModels;

namespace Petrov_Tema_16
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IDataService dataService = new JsonDataService();
            AuthService authService = new AuthService(dataService);
            BookingService bookingService = new BookingService(dataService, authService);
            DataContext = new HotelViewModel(dataService, bookingService, authService);
        }
    }
}