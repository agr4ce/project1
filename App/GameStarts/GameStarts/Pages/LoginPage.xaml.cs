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
using GameStarts.Classes;
using GameStarts.Data;
using GameStarts.Models;

namespace GameStarts.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using(var context = new UserContext())
            {
                var user = context.Users.FirstOrDefault(u=>u.Login == LoginTextBox.Text);
                if (user != null)
                {
                    if (user.Password == PasswordTextBox.Text)
                    {
                        MessageBox.Show("Успешный вход","Оповещение",MessageBoxButton.OK,MessageBoxImage.Information);
                        var role = context.RoleUsers.FirstOrDefault(r=>r.IdRoleUser == user.IdRoleUser);
                        if (role == null)
                        {
                            MessageBox.Show("Ошибка определения роли пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        CurrentUser.UserRole = role.NameRoleUser;
                        NavigationManager.MainFrame.Navigate(new UserPage());
                        return;
                    }
                }
                MessageBox.Show("Логин или пароль неверен","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
