using GameStarts.Data;
using System;
using System.Windows;

namespace GameStarts
{
    /// <summary>
    /// Логика взаимодействия для SetterDialog.xaml
    /// </summary>
    public partial class SetterDialog : Window
    {
        public SetterDialog()
        {
            InitializeComponent();

            ServerNameTextBox.Text = Properties.Settings.Default.ServerName;
            BDNameTextBox.Text = Properties.Settings.Default.BDName;
            BdUserNameTextBox.Text = Properties.Settings.Default.BDUserLogin;
            PasswordTextBox.Text = Properties.Settings.Default.Password;
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ServerName = ServerNameTextBox.Text;
            Properties.Settings.Default.BDName = BDNameTextBox.Text;
            Properties.Settings.Default.BDUserLogin = BdUserNameTextBox.Text;
            Properties.Settings.Default.Password = PasswordTextBox.Text;
            Properties.Settings.Default.Save();
            try
            {
                using (var context = new BoardGameContext())
                {
                    var games = context.BoardGames;
                }
                this.DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Подключение не удалось\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
