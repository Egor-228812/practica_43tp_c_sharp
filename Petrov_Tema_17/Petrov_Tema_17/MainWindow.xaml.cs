using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Petrov_Tema_17
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Room> rooms;
        private Room selectedRoom;

        public MainWindow()
        {
            InitializeComponent();
            LoadRooms();
            RoomsListBox.ItemsSource = rooms;
            Loaded += MainWindow_Loaded;
        }

        private void LoadRooms()
        {
            rooms = new ObservableCollection<Room>
    {
        new Room { RoomNumber = "101", Type = "Стандарт", Price = 80, IsBooked = false },
        new Room { RoomNumber = "102", Type = "Стандарт", Price = 80, IsBooked = true },
        new Room { RoomNumber = "103", Type = "Улучшенный", Price = 120, IsBooked = false },
        new Room { RoomNumber = "104", Type = "Люкс", Price = 180, IsBooked = false },
        new Room { RoomNumber = "105", Type = "Стандарт", Price = 80, IsBooked = true },
        new Room { RoomNumber = "106", Type = "Улучшенный", Price = 120, IsBooked = false }
    };
            foreach (var room in rooms)
                room.PropertyChanged += Room_PropertyChanged;
        }

        private void Room_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Room.IsBooked))
            {
                var room = sender as Room;
                var container = GetListBoxItemForRoom(room);
                if (container != null)
                {
                    var border = FindVisualChild<Border>(container, "RoomBorder");
                    if (room.IsBooked)
                        StartBorderAnimation(border);
                    else
                        StopBorderAnimation(border);
                }
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyAppearAnimationToAvailableRooms();
            ApplyBorderAnimationsForBookedRooms();
        }

        private void ApplyAppearAnimationToAvailableRooms()
        {
            foreach (var room in rooms.Where(r => !r.IsBooked))
            {
                var container = GetListBoxItemForRoom(room);
                if (container != null)
                {
                    var border = FindVisualChild<Border>(container, "RoomBorder");
                    if (border != null)
                    {
                        var sb = Resources["ScaleFadeInAnimation"] as Storyboard;
                        sb.Begin(border);
                    }
                }
            }
        }

        private void ApplyBorderAnimationsForBookedRooms()
        {
            foreach (var room in rooms.Where(r => r.IsBooked))
            {
                var container = GetListBoxItemForRoom(room);
                if (container != null)
                {
                    var border = FindVisualChild<Border>(container, "RoomBorder");
                    if (border != null)
                        StartBorderAnimation(border);
                }
            }
        }

        private void StartBorderAnimation(Border border)
        {
            if (border == null) return;
            var sb = Resources["BookedBorderAnimation"] as Storyboard;
            sb.Begin(border);
        }

        private void StopBorderAnimation(Border border)
        {
            if (border == null) return;
            var sb = Resources["BookedBorderAnimation"] as Storyboard;
            sb.Stop(border);
            border.BorderBrush = new SolidColorBrush(Colors.LightGray);
        }

        private ListBoxItem GetListBoxItemForRoom(Room room)
        {
            return RoomsListBox.ItemContainerGenerator.ContainerFromItem(room) as ListBoxItem;
        }

        private T FindVisualChild<T>(DependencyObject parent, string name) where T : FrameworkElement
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T tChild && tChild.Name == name)
                    return tChild;
                var result = FindVisualChild<T>(child, name);
                if (result != null)
                    return result;
            }
            return null;
        }

        private void RoomsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                selectedRoom = e.AddedItems[0] as Room;
                var container = RoomsListBox.ItemContainerGenerator.ContainerFromItem(selectedRoom) as ListBoxItem;
                if (container != null)
                {
                    var border = FindVisualChild<Border>(container, "RoomBorder");
                    if (border != null)
                    {
                        var pressAnimation = new Storyboard();
                        var scaleX = new DoubleAnimation(0.95, 1, new Duration(TimeSpan.FromSeconds(0.1)));
                        var scaleY = new DoubleAnimation(0.95, 1, new Duration(TimeSpan.FromSeconds(0.1)));
                        Storyboard.SetTarget(scaleX, border.RenderTransform);
                        Storyboard.SetTarget(scaleY, border.RenderTransform);
                        Storyboard.SetTargetProperty(scaleX, new PropertyPath("ScaleX"));
                        Storyboard.SetTargetProperty(scaleY, new PropertyPath("ScaleY"));
                        pressAnimation.Children.Add(scaleX);
                        pressAnimation.Children.Add(scaleY);
                        pressAnimation.Begin();
                    }
                }
                ShowRoomDetails(selectedRoom);
            }
            else
            {
                HideDetailsPanel();
            }
        }

        private void ShowRoomDetails(Room room)
        {
            if (DetailsPanel.Visibility != Visibility.Visible)
            {
                DetailsPanel.Visibility = Visibility.Visible;
                var showSb = Resources["ShowDetailsAnimation"] as Storyboard;
                showSb.Begin(DetailsPanel);
            }
            DetailRoomNumber.Text = $"Номер: {room.RoomNumber}";
            DetailType.Text = $"Тип: {room.Type}";
            DetailPrice.Text = $"Цена за ночь: {room.Price:C}";
            DetailStatus.Text = room.IsBooked ? "Статус: Забронирован" : "Статус: Доступен";
            BookingButton.Content = room.IsBooked ? "Отменить бронь" : "Забронировать";
            var fadeIn = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.2)));
            DetailsContent.BeginAnimation(UIElement.OpacityProperty, fadeIn);
        }

        private void HideDetailsPanel()
        {
            var hideSb = Resources["HideDetailsAnimation"] as Storyboard;
            hideSb.Completed += (s, e) => DetailsPanel.Visibility = Visibility.Collapsed;
            hideSb.Begin(DetailsPanel);
        }

        private void BookingButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRoom == null) return;
            selectedRoom.IsBooked = !selectedRoom.IsBooked;
            ShowRoomDetails(selectedRoom);
            var container = GetListBoxItemForRoom(selectedRoom);
            if (container != null)
            {
                var border = FindVisualChild<Border>(container, "RoomBorder");
                if (selectedRoom.IsBooked)
                    StartBorderAnimation(border);
                else
                {
                    StopBorderAnimation(border);
                    var sb = Resources["ScaleFadeInAnimation"] as Storyboard;
                    sb.Begin(border);
                }
            }
        }
    }
}