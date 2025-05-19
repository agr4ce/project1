using GameStarts.Classes;
using GameStarts.Data;
using GameStarts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GameStarts.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductCard.xaml
    /// </summary>
    public partial class ProductCard : Page
    {
        public ProductCard()
        {
            InitializeComponent();

            using (var context = new BoardGameContext())
            {
                var game = context.BoardGames.FirstOrDefault(g => g.IdBoardGame == CurrentProduct.IDBoardGame);
                if (game == null)
                {
                    MessageBox.Show("Кажется игра пропала ;-;", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (game.Picture == null)
                    ProductImg.Source = Img.ConvertImg(Properties.Resources.nullimg);
                else
                    ProductImg.Source = Img.ConvertImg(game.Picture);
                NameProduct.Text = game.Name;
                PriceTextBlock.Text = String.Concat("Цена: ", game.Price, " руб.");
                MinAgeTextBlock.Text = String.Concat("Минимальный возраст: ", game.MinAge);
                MinCountTextBlock.Text = String.Concat("Минимальное кол-во игроков: ", game.MinCountPlayers);
                MaxCountTextBlock.Text = String.Concat("Максимальное кол-во игроков: ", game.MaxCountPlayer == null ? "-" : game.MaxCountPlayer);
                AvrTimeTextBLock.Text = String.Concat("Среднее время игры: ", game.AverageTime);

                Description.Text = game.Description;

                var gameCat = context.BoardGames.Where(g => g.IdBoardGame == CurrentProduct.IDBoardGame).Include(c => c.IdCategories).ToList();
                foreach (var g in gameCat)
                {
                    foreach (var c in g.IdCategories)
                    {
                        StackPanel stackPanel = new StackPanel();
                        TextBlock nameCat = new TextBlock();
                        nameCat.TextWrapping = TextWrapping.Wrap;
                        nameCat.Text = c.Name;
                        nameCat.Style = (Style)nameCat.FindResource("RedTextBlock");
                        TextBlock description = new TextBlock();
                        description.TextWrapping = TextWrapping.Wrap;
                        description.Text = c.Description;
                        stackPanel.Children.Add(nameCat);
                        stackPanel.Children.Add(description);
                        CategoriesStackPanel.Children.Add(stackPanel);
                    }
                }

                var additions = context.Additions.Where(a => a.IdBoardGame == CurrentProduct.IDBoardGame).ToList();
                if (additions.Count == 0)
                {
                    TextBlock textBlock = new();
                    textBlock.Text = "К этой настольной игре нет дополненией";
                    AdditionStackPanel.Children.Add(textBlock);
                }
                else
                {
                    foreach (var item in additions)
                    {
                        StackPanel sp = new();
                        TextBlock nameAdd = new();
                        nameAdd.Text = item.Name;
                        nameAdd.Style = (Style)nameAdd.FindResource("RedTextBlock");
                        TextBlock priceAdd = new();
                        priceAdd.Text = String.Concat(item.Price, " руб.");
                        Button button = new();
                        button.Content = "Узнать больше";
                        button.Name = String.Concat("button_", item.IdAddition);
                        button.Click += AdditionButton_Click;
                        sp.Children.Add(nameAdd);
                        sp.Children.Add(priceAdd);
                        sp.Children.Add(button);
                        AdditionStackPanel.Children.Add(sp);
                    }
                }

                if (game.Amount == 0)
                {
                    AddToCartButton.IsEnabled = false;
                    AddToCartButton.Content = "Игры нет в наличии";
                }

                /*foreach (var c in game.IdCategories)
                {
                    StackPanel stackPanel = new StackPanel();
                    TextBlock nameCat = new TextBlock();
                    nameCat.Text = c.Name;
                    TextBlock description = new TextBlock();
                    description.Text = c.Description;
                    stackPanel.Children.Add(nameCat);
                    stackPanel.Children.Add(description);
                    CategoriesStackPanel.Children.Add(stackPanel);
                    MessageBox.Show(c.Name);
                }*/

            }
        }

        private void AdditionButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            CurrentProduct.IDAddition = Convert.ToInt32(button.Name.Replace("button_", ""));
            NavigationManager.MainFrame.Navigate(new AdditionCard());
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            if (Cart.IDBoardGame.Contains(CurrentProduct.IDBoardGame))
            {
                MessageBox.Show("Настольная игра уже есть в корзине", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Cart.IDBoardGame.Add(CurrentProduct.IDBoardGame);
            MessageBox.Show("Настольная игра добавлена в корзину","Оповещение",MessageBoxButton.OK,MessageBoxImage.Information);
        }
    }
}
