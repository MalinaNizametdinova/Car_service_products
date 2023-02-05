using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp8.Pages
{
    /// <summary>
    /// Логика взаимодействия для Product.xaml
    /// </summary>
    public partial class Product : Page
    {
        public Product()
        {
            InitializeComponent();
            Sort.SelectedIndex = 0;
            Spisok.SelectedIndex = 0;
            var currentUsers = Entities.GetContext().Product.ToList();
            ListUser.ItemsSource = currentUsers;
            UpdateUsers();


        }


        private void UpdateUsers()
        {
            var currentUsers = Entities.GetContext().Product.ToList();

            currentUsers = currentUsers.Where(x => x.Title.ToLower().Contains(Sea.Text.ToLower())).ToList();

            if (Spisok.SelectedIndex != 0)
                currentUsers = currentUsers.Where(x => x.ManufacturerID == Spisok.SelectedIndex).ToList();

            if (Sort.SelectedIndex == 0)
                ListUser.ItemsSource = currentUsers.OrderBy(x => x.Cost).ToList();
            else
                ListUser.ItemsSource = currentUsers.OrderByDescending(x => x.Cost).ToList();
        }

      

        private void Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUsers();
        }

        private void Sea_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUsers();
        }

        private void Spisok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUsers();
        }

        private void ADD_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ADD(null));
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            var usersForRemoving = ListUser.SelectedItems.Cast<Product>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {usersForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                   // Entities.GetContext().Product.RemoveRange(usersForRemoving);
                    Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    ListUser.ItemsSource = Entities.GetContext().Product.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
        }
    }
}
