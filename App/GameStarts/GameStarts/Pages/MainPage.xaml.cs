using GameStarts.Classes;
using GameStarts.Data;
using GameStarts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GameStarts.Pages
{
    public partial class MainPage : Page
    {
        int age = -1;
        int count = 0;
        List<int> categories = new List<int>();

        public MainPage()
        {
            InitializeComponent();
            
                using (var context = new BoardGameContext())
                {
                    LoginToPage(context);
                }
          
        }

        private void LoginToPage(BoardGameContext context)
        {
            string qwery = "SELECT BoardGame.IdBoardGame, BoardGame.Name, BoardGame.Price, BoardGame.MinAge, BoardGame.MinCountPlayers,BoardGame.MaxCountPlayer,BoardGame.Description, BoardGame.AverageTime,BoardGame.Picture,BoardGame.Amount FROM BoardGame RIGHT JOIN Top10BoardGame ON BoardGame.Name = Top10BoardGame.Name";
            var TopBoardGame = context.BoardGames.FromSqlRaw(qwery).ToList();
            AddGame(TopBoardGame);

            var category = context.Categories.Where(c => !c.Genre).ToList();
            var genre = context.Categories.Where(c => c.Genre).ToList();

            foreach (var item in category)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Tag = item.IdCategory;
                checkBox.Content = item.Name;
                checkBox.Checked += checkBox_Checked;
                checkBox.Unchecked += CheckBox_Unchecked;
                CategoryStackPanel.Children.Add(checkBox);
            }
            foreach (var item in genre)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Tag = item.IdCategory;
                checkBox.Content = item.Name;
                checkBox.Checked += checkBox_Checked;
                checkBox.Unchecked += CheckBox_Unchecked;
                GenreStackPanel.Children.Add(checkBox);
            }
        }

        private void ClearCategory()
        {
            while(CategoryStackPanel.Children.Count > 0)
                CategoryStackPanel.Children.RemoveAt(0);
            while(GenreStackPanel.Children.Count > 0)
                GenreStackPanel.Children.RemoveAt(0);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox chBox = (CheckBox)sender;
            categories.Remove((int)chBox.Tag);
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chBox = (CheckBox)sender;
            categories.Add((int)chBox.Tag);
        }

        private void ClearGame()
        {
            while (BoardGameWrapPanel.Children.Count > 0)
                BoardGameWrapPanel.Children.RemoveAt(0);
        }
        private void AddGame(List<BoardGame> TopBoardGame)
        {
            foreach (var game in TopBoardGame)
            {
                StackPanel sp = new StackPanel();
                BitmapImage img = new BitmapImage();
                Image image = new Image();
                image.Width = 200;
                image.Height = 200;
                if (game.Picture == null)
                    image.Source = Img.ConvertImg(Properties.Resources.nullimg);
                else
                    image.Source = Img.ConvertImg(game.Picture);
                TextBlock name = new TextBlock();
                name.Text = game.Name;
                name.Style = (Style)name.FindResource("RedTextBlock");
                TextBlock price = new TextBlock();
                price.Text = String.Concat(game.Price, " руб.");
                Button button = new Button();
                button.Content = "Узнать больше";
                button.Name = String.Concat("button_", game.IdBoardGame);
                button.Click += InfoProductButton_Click;
                sp.Children.Add(image);
                sp.Children.Add(name);
                sp.Children.Add(price);
                sp.Children.Add(button);
                BoardGameWrapPanel.Children.Add(sp);
            }
        }

        private void InfoProductButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            CurrentProduct.IDBoardGame = Convert.ToInt32(button.Name.Replace("button_",""));
            NavigationManager.MainFrame.Navigate(new ProductCard());
        }

        private void AgeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            age = (int)AgeSlider.Value;
            if(age == 18)
                AgeTextBlock.Text = String.Concat(age, "+");
            else if(age == -1)
                AgeTextBlock.Text = "Любой";
            else
                AgeTextBlock.Text = age.ToString();
        }

        private void CountSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            count = (int)CountSlider.Value;
            if(count == 18)
                CountTextBlock.Text = "Много";
            else if(count == 0)
                CountTextBlock.Text = "Любое";
            else
                CountTextBlock.Text = count.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InfoLabel.Content = "Найденные результаты: ";
            ClearGame();
            using (var context = new BoardGameContext())
            {
                string name = NameTextBox.Text.Trim().ToLower();
                var games = context.BoardGames.Where(bg => bg.Name.ToLower().Contains(NameTextBox.Text));
                if(age != -1)
                {
                    games = games.Where(g=>g.MinAge<=age);
                }
                if (count != 0)
                {
                    games = games.Where(g => g.MinCountPlayers <= count && g.MaxCountPlayer>=count);
                }
                if(categories.Count != 0)
                {
                    int currect = 0;
                    var catgames = games.Include(c => c.IdCategories).ToList();
                    List<BoardGame> bg = new List<BoardGame>();
                    foreach (var g in catgames)
                    {
                        foreach (var c in g.IdCategories)
                        {
                            foreach(var item in categories)
                            {
                                if (c == context.Categories.FirstOrDefault(ca => ca.IdCategory == (int)item)){
                                    currect++;
                                }
                            }
                        }
                        if(currect == categories.Count())
                        {
                            bg.Add(g);
                        }
                        currect = 0;
                    }
                    AddGame(bg);
                    return;
                }
                AddGame(games.ToList());
            }
        }

        private void ReLoadButton_Click(object sender, RoutedEventArgs e)
        {
            ClearGame();
            ClearCategory();
            categories.Clear();
            age = -1;
            AgeSlider.Value = age;
            count = 0;
            CountSlider.Value = count;
            InfoLabel.Content = "Самые популярные игры за последнее время:";
            using (var context = new BoardGameContext())
            {
                LoginToPage(context);
            }
        }
    }
}
