using GameStarts.Classes;
using GameStarts.Data;
using GameStarts.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Collections.Generic;

namespace GameStarts.Pages
{
    /// <summary>
    /// Логика взаимодействия для CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        int IDsp = 0;
        List<StackPanel> stackPanels = new List<StackPanel>();
        public CartPage()
        {
            InitializeComponent();

            if (Cart.IDBoardGame.Count == 0 && Cart.IDAddition.Count == 0)
            {
                TextBlock textBlock = new();
                textBlock.Text = "Здесь пока ничего нет";
                MainCart.Children.Add(textBlock);
            }
            else
            {
                using (var context = new BoardGameContext())
                {
                    foreach (var IDgame in Cart.IDBoardGame)
                    {
                        Cart.CountBoardGame.Add(1);
                        var game = context.BoardGames.FirstOrDefault(g => g.IdBoardGame == IDgame);
                        if (game != null)
                        {
                            BitmapImage bitmapImage = new BitmapImage();
                            if (game.Picture == null)
                                bitmapImage = Img.ConvertImg(Properties.Resources.nullimg);
                            else
                                bitmapImage = Img.ConvertImg(game.Picture);
                            stackPanels.Add(AddProdutcToCart(String.Concat("game_", game.IdBoardGame),
                                             game.Name,
                                             bitmapImage,
                                             game.Price,
                                             game.Amount));
                        }
                    }
                    foreach (var IDaddition in Cart.IDAddition)
                    {
                        Cart.CountAddition.Add(1);
                        var addition = context.Additions.FirstOrDefault(a => a.IdAddition == IDaddition);
                        if (addition != null)
                        {
                            BitmapImage bitmapImage = new BitmapImage();
                            if (addition.Picture == null)
                                bitmapImage = Img.ConvertImg(Properties.Resources.nullimg);
                            else
                                bitmapImage = Img.ConvertImg(addition.Picture);
                            stackPanels.Add(AddProdutcToCart(String.Concat("addition_", addition.IdAddition),
                                             addition.Name,
                                             bitmapImage,
                                             addition.Price,
                                             addition.Amount));
                        }
                    }
                }
                UpdateMainCart();
            }
        }

        private void UpdateMainCart()
        {
            while (MainCart.Children.Count > 0)
                MainCart.Children.RemoveAt(0);
            foreach (var sp in stackPanels)
            {
                MainCart.Children.Add(sp);
            }
        }

        private StackPanel AddProdutcToCart(string nameButton, string nameProduct, BitmapImage picture, decimal priceProduct, short max)
        {
            StackPanel sp = new StackPanel();
            sp.VerticalAlignment = VerticalAlignment.Top;
            sp.Orientation = Orientation.Horizontal;
            sp.Name = String.Concat("sp_", IDsp);

            StackPanel rightsp = new StackPanel();
            BitmapImage img = new BitmapImage();
            Image image = new Image();
            image.Width = 200;
            image.Height = 200;
            image.Source = picture;
            TextBlock name = new TextBlock();
            name.Style = (Style)name.FindResource("RedTextBlock");
            name.Text = nameProduct;
            TextBlock price = new TextBlock();
           
            price.Text = String.Concat(priceProduct, " руб.");
            Button button = new Button();
            button.Content = "Узнать больше";
            button.Name = String.Concat("button_", nameButton);

            TextBlock text = new();
            text.Text = "Кол-во:";
            TextBox count = new();
            count.VerticalAlignment = VerticalAlignment.Top;
            count.Width = 100;
            count.Text = "1";
            count.LostFocus += Count_LostFocus; 
            count.Name = String.Concat("textbox_",nameButton, "_max_", max);
            TextBlock textMax = new();
            textMax.Text = String.Concat("/", max);

            Button delbutton = new Button();
            delbutton.Content = "Удалить из корзины";
            delbutton.Name = String.Concat("button_", nameButton, "_sp_", IDsp);
            delbutton.Click += Delbutton_Click;

            button.Click += InfoProductButton_Click;
            sp.Children.Add(image);
            rightsp.Children.Add(name);
            rightsp.Children.Add(price);
            rightsp.Children.Add(button);
            rightsp.Children.Add(delbutton);
            sp.Children.Add(rightsp);

            sp.Children.Add(text);
            sp.Children.Add(count);
            sp.Children.Add(textMax);

            IDsp++;
            return sp;
        }

        private void Delbutton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы дeйствительно хотите удалить из корзины данную игру?", "Потверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }
            Button button = (Button)sender;
            int IDsp = Convert.ToInt32(button.Name.Substring(button.Name.LastIndexOf("_") + 1));
            for (int i = 0; i < stackPanels.Count; i++)
            {
                if (stackPanels[i].Name == String.Concat("sp_", IDsp))
                {
                    stackPanels.RemoveAt(i);
                    break;
                }
            }
            UpdateMainCart();
            if (button.Name.Contains("game"))
            {
                string IDBoardGameF = button.Name.Substring(button.Name.LastIndexOf("e_") + 2);
                int IDBoardGame = Convert.ToInt32(IDBoardGameF.Replace($"_sp_{IDsp}", ""));
                Cart.CountBoardGame.RemoveAt(Cart.IDBoardGame.IndexOf(IDBoardGame));
                Cart.IDBoardGame.Remove(IDBoardGame);
            }
            else
            {
                string IDAdditionF = button.Name.Substring(button.Name.LastIndexOf("n_") + 2);
                int IDAddition = Convert.ToInt32(IDAdditionF.Replace($"_sp_{IDsp}", ""));
                Cart.CountAddition.RemoveAt(Cart.IDAddition.IndexOf(IDAddition));
                Cart.IDAddition.Remove(IDAddition);
            }
        }

        private void Count_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!Int32.TryParse(textBox.Text, out int count))
            {
                MessageBox.Show("Вы должны ввести цифру", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                textBox.Text = "1";
                return;
            }
            int max = Convert.ToInt32(textBox.Name.Substring(textBox.Name.LastIndexOf("_") + 1));
            if (count > max)
            {
                MessageBox.Show("Такого кол-ва нет на складе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                textBox.Text = "1";
                return;
            }
            int buyId;
            if (textBox.Name.Contains("game_"))
            {
                string boardGameF = textBox.Name.Substring(textBox.Name.LastIndexOf("e_") + 2);
                buyId = Convert.ToInt32(boardGameF.Replace($"_max_{max}", ""));
                Cart.CountBoardGame[Cart.IDBoardGame.IndexOf(buyId)] = count;
            }
            else
            {
                string additionF = textBox.Name.Substring(textBox.Name.LastIndexOf("n_") + 2);
                buyId = Convert.ToInt32(additionF.Replace($"_max_{max}", ""));
                Cart.CountAddition[Cart.IDAddition.IndexOf(buyId)] = count;
            }
        }


        private void InfoProductButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Name.Contains("game"))
            {
                CurrentProduct.IDBoardGame = Convert.ToInt32(button.Name.Replace("button_game_", ""));
                NavigationManager.MainFrame.Navigate(new ProductCard());
            }
            else
            {
                CurrentProduct.IDAddition = Convert.ToInt32(button.Name.Replace("button_addition_", ""));
                NavigationManager.MainFrame.Navigate(new AdditionCard());
            }
        }

        private void ClearCart_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы дeйствительно хотите отчистить корзину?","Потверждение", MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }
            while (MainCart.Children.Count > 0)
                MainCart.Children.RemoveAt(0);
            Cart.IDBoardGame.Clear();
            Cart.IDAddition.Clear();
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            if (Cart.IDBoardGame.Count == 0 && Cart.IDAddition.Count == 0 )
            {
                MessageBox.Show("Вы не можете купить ничего","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            NavigationManager.MainFrame.Navigate(new OrderingPage());
        }
    }
}
