using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameStarts.Pages;
using GameStarts.Classes;

namespace GameStarts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            NavigationManager.MainFrame = MainFrame;
            try
            {
                NavigationManager.MainFrame.Navigate(new MainPage());
            }
            catch(Exception)
            {
                SetterDialog setterDialog = new();
                if(setterDialog.ShowDialog() == true)
                {
                    NavigationManager.MainFrame.Navigate(new MainPage());
                }
                else
                {
                    Close();
                }
            }
            BitmapImage img = new BitmapImage();
            LogoImg.Source = Img.ConvertImg(Properties.Resources.logo);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if(NavigationManager.MainFrame.CanGoBack)
                NavigationManager.MainFrame.GoBack();
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.MainFrame.Navigate(new CartPage());
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.MainFrame.Navigate(new LoginPage());
        }
    }
}
