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
    /// Логика взаимодействия для ADD.xaml
    /// </summary>
    public partial class ADD : Page
    {
        private Product _currentProduct = new Product ();
        public ADD(Product selectedProduct)
        {
            InitializeComponent();
            if (selectedProduct != null)
            {
                _currentProduct = selectedProduct;
            }
            DataContext = _currentProduct;
        }

        

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Product());
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
           // if (_currentProduct.ID == 0)
             //   Entities.GetContext().Product.Add(_currentProduct);
            try
            {
                
                Entities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            NavigationService.Navigate(new Product());


        }


    }
}
