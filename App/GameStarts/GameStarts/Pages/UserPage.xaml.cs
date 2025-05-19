using GameStarts.Classes;
using GameStarts.Data;
using GameStarts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace GameStarts.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        int idBoardGame = 0;
        byte[] photos;
        byte[] photosAddition;
        List<int> idCategoryBoardGame = new List<int>();
        List<string> boardGames;

        public UserPage()
        {
            InitializeComponent();
            if (CurrentUser.UserRole == "Контент-менеджер")
            {
                ProductPage.Visibility = Visibility.Visible;
                AdditionPage.Visibility = Visibility.Visible;
                SetterPage.Visibility = Visibility.Hidden;
                StatisticPage.Visibility = Visibility.Hidden;
                CategoryPage.Visibility = Visibility.Hidden;
            }

            using (var context = new BoardGameContext())
            {
                GameAdd(context.BoardGames.OrderBy(g => g.Name).ToList());
                var orders = context.Purshases;
                AllOrderCountTextBlock.Text = orders.Count().ToString();
                AddCategoryBoardGame(context.BoardGames.OrderBy(g => g.Name).ToList());
            }
            GameImage.Source = Img.ConvertImg(Properties.Resources.nullimg);
            AdditionImage.Source = Img.ConvertImg(Properties.Resources.nullimg);
            using (var context = new OrderContext())
            {
                var orders = context.MonthOrders;
                MonthOrderCountTextBlock.Text = orders.Count().ToString();
                MonthBoardGameTextBlock.Text = orders.Sum(o => o.BoardGamesPrice).ToString();
                MonthAdditionTextBlock.Text = orders.Sum(o => o.AdditionGamesPrice).ToString();
                AllTextBlock.Text = orders.Sum(o => o.OrderPrice).ToString();
            }

            using (var context = new BoardGameContext())
            {
                boardGames = context.BoardGames.OrderBy(b => b.Name).Select(b => b.Name).ToList();
                BoardGameComboBox.ItemsSource = context.BoardGames.OrderBy(b => b.Name).Select(b => b.Name).ToList();
                BoardGameComboBox.SelectedIndex = 0;
                AdditionAdd(context.Additions.OrderBy(a => a.Name).ToList());

                var categories = context.Categories.OrderBy(b => b.Name).ToList();
                CategoryAdd(categories);
            }
            ServerName.Text = Properties.Settings.Default.ServerName;
            BDName.Text = Properties.Settings.Default.BDName;
            UserName.Text = Properties.Settings.Default.BDUserLogin;


        }

        private void AddCategoryBoardGame(List<BoardGame> boardGames)
        {
            while (BoardGameWithcategoryStackPanel.Children.Count > 0)
                BoardGameWithcategoryStackPanel.Children.RemoveAt(0);
            foreach (var boardGame in boardGames)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Name = $"CheckBox_{boardGame.IdBoardGame}";
                checkBox.Content = boardGame.Name;
                checkBox.Checked += CheckBox_Checked;
                checkBox.Unchecked += CheckBox_Unchecked;
                checkBox.IsChecked = idCategoryBoardGame.Contains(boardGame.IdBoardGame);
                BoardGameWithcategoryStackPanel.Children.Add(checkBox);
            }
            //BoardGameWithcategoryStackPanel
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            int idGame = Convert.ToInt32(checkBox.Name.Replace("CheckBox_", ""));
            if (idCategoryBoardGame.Contains(idGame))
            {
                idCategoryBoardGame.Remove(idGame);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            int idGame = Convert.ToInt32(checkBox.Name.Replace("CheckBox_", ""));
            if (!idCategoryBoardGame.Contains(idGame))
            {
                idCategoryBoardGame.Add(idGame);
            }
        }

        private void AdditionAdd(List<Addition> additions)
        {
            while (AdditionListStackPanel.Children.Count > 0)
                AdditionListStackPanel.Children.RemoveAt(0);
            foreach (var addition in additions)
            {
                Button additionButton = new();
                additionButton.Content = addition.Name;
                additionButton.Name = String.Concat("button_", addition.IdAddition);
                additionButton.Style = (Style)additionButton.FindResource("UpButton");
                additionButton.Click += AdditionButton_Click;
                AdditionListStackPanel.Children.Add(additionButton);
            }
        }

        private void CategoryAdd(List<Category> categories)
        {
            while (CategoryListStackPanel.Children.Count > 0)
                CategoryListStackPanel.Children.RemoveAt(0);
            foreach (var category in categories)
            {
                Button categoryButton = new();
                categoryButton.Content = category.Name;
                categoryButton.Name = String.Concat("button_", category.IdCategory);
                categoryButton.Style = (Style)categoryButton.FindResource("UpButton");
                categoryButton.Click += CategoryButton_Click;
                CategoryListStackPanel.Children.Add(categoryButton);
            }
        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            idCategoryBoardGame.Clear();
            using (var context = new BoardGameContext())
            {
                var category = context.Categories.FirstOrDefault(a => a.IdCategory == Convert.ToInt32(button.Name.Replace("button_", "")));
                if (category == null)
                {
                    MessageBox.Show("Кажется дополнение пропало", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                IDCategoryTextBox.Text = category.IdCategory.ToString();
                NameCategoryTextBox.Text = category.Name;
                CategoryOrGenreCheckBox.IsChecked = category.Genre;
                DescriptionCategoryRichTextBox.Document = new System.Windows.Documents.FlowDocument();
                DescriptionCategoryRichTextBox.Document.Blocks.Add(new Paragraph(new Run(category.Description)));
                var catgames = context.BoardGames.Include(c => c.IdCategories).ToList();
                List<BoardGame> bg = new List<BoardGame>();
                foreach (var g in catgames)
                {
                    foreach (var c in g.IdCategories)
                    {
                        if (c == context.Categories.FirstOrDefault(ca => ca.IdCategory == category.IdCategory))
                        {
                            idCategoryBoardGame.Add(g.IdBoardGame);
                        }
                    }
                }
                AddCategoryBoardGame(context.BoardGames.OrderBy(g => g.Name).ToList());
            }
        }

        private void AdditionButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            using (var context = new BoardGameContext())
            {
                var addition = context.Additions.FirstOrDefault(a => a.IdAddition == Convert.ToInt32(button.Name.Replace("button_", "")));
                if (addition == null)
                {
                    MessageBox.Show("Кажется дополнение пропало", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (addition.Picture == null)
                    AdditionImage.Source = Img.ConvertImg(Properties.Resources.nullimg);
                else
                    AdditionImage.Source = Img.ConvertImg(addition.Picture);
                IDAdditionTextBlock.Text = addition.IdAddition.ToString();
                NameAddition.Text = addition.Name;
                PriceAddition.Text = addition.Price.ToString();
                AmountAddition.Text = addition.Amount.ToString();
                DescriptionAddition.Document = new System.Windows.Documents.FlowDocument();
                DescriptionAddition.Document.Blocks.Add(new Paragraph(new Run(addition.Description)));
                MinAgeAddition.Text = addition.MinAge.ToString();
                var boardGameName = context.BoardGames.FirstOrDefault(g => g.IdBoardGame == addition.IdBoardGame);
                BoardGameComboBox.SelectedValue = boardGameName.Name;
            }
        }

        private void GameAdd(List<BoardGame> boardGames)
        {
            while (BoardGameListStackPanel.Children.Count > 0)
                BoardGameListStackPanel.Children.RemoveAt(0);
            foreach (var game in boardGames)
            {
                Button gameButton = new();
                gameButton.Content = game.Name;
                gameButton.Name = String.Concat("button_", game.IdBoardGame);
                gameButton.Style = (Style)gameButton.FindResource("UpButton");
                gameButton.Click += GameButton_Click;
                BoardGameListStackPanel.Children.Add(gameButton);
            }
        }

        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            using (var context = new BoardGameContext())
            {
                var game = context.BoardGames.FirstOrDefault(g => g.IdBoardGame == Convert.ToInt32(button.Name.Replace("button_", "")));
                if (game == null)
                {
                    MessageBox.Show("Кажется игра пропала", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (game.Picture == null)
                    GameImage.Source = Img.ConvertImg(Properties.Resources.nullimg);
                else
                    GameImage.Source = Img.ConvertImg(game.Picture);
                IDTextBlock.Text = game.IdBoardGame.ToString();
                NameBoardGame.Text = game.Name;
                PriceBoardGame.Text = game.Price.ToString();
                if (game.AverageTime != null)
                {
                    AvrTimeBoardGame.Text = game.AverageTime.ToString();
                }
                MinCountBoardGame.Text = game.MinCountPlayers.ToString();
                MaxCountBoardGame.Text = game.MaxCountPlayer.ToString();
                AmountBoardGame.Text = game.Amount.ToString();
                DescriptionBoardGame.Document = new System.Windows.Documents.FlowDocument();
                DescriptionBoardGame.Document.Blocks.Add(new Paragraph(new Run(game.Description)));
                MinAgeBoardGame.Text = game.MinAge.ToString();
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (var context = new BoardGameContext())
            {
                GameAdd(context.BoardGames.Where(g => g.Name.Contains(SearchTextBox.Text)).OrderBy(g => g.Name).ToList());
            }
        }

        private void SaveGameButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> errors = new List<string>();
            using (var context = new BoardGameContext())
            {
                if (NameBoardGame.Text.Trim() == "")
                {
                    errors.Add("Заполните поле Наименование.");
                }
                if (IDTextBlock.Text == "-//-")
                {
                    if (context.BoardGames.FirstOrDefault(g => g.Name == NameBoardGame.Text.Trim()) != null)
                    {
                        errors.Add("Имя настольной игры не должно совпадать с уже существующими.");
                    }
                }
                if (!Decimal.TryParse(PriceBoardGame.Text, out decimal price))
                {
                    errors.Add("Цена должна быть цифрой");
                }
                TimeSpan avrTime = new TimeSpan(0, 0, 0);
                if (AvrTimeBoardGame.Text != null && !TimeSpan.TryParse(AvrTimeBoardGame.Text, out avrTime))
                {
                    errors.Add("При указании времени используйте шаблон: чч:мм:сс");
                    AvrTimeBoardGame.Text = "00:00:00";
                }
                Byte maxCount = 20;
                if (MaxCountBoardGame.Text != null && !Byte.TryParse(MaxCountBoardGame.Text, out maxCount))
                {
                    errors.Add("При указании Макс кол-во игроков нужно использовать цифры");
                }
                if (!Byte.TryParse(MinCountBoardGame.Text, out Byte minCount))
                {
                    errors.Add("В поле Мин кол-во игроков нужно использовать цифры");
                }
                if (minCount >= maxCount)
                {
                    errors.Add("Неккоректное указание Мин и Макс кол-ва игроков");
                }
                if (!short.TryParse(AmountBoardGame.Text, out short amountGame))
                {
                    errors.Add("В поле Кол-во экземпляров нужно использовать цифры");
                }
                if (!Byte.TryParse(MinAgeBoardGame.Text, out Byte minAge))
                {
                    errors.Add("В поле Мин возраст нужно использовать цифры");
                }
                if (minAge > 18)
                {
                    errors.Add("Поле Мин возраст не может быть больше 18");
                }
                if (minAge < 0 || minCount < 0 || amountGame < 0 || maxCount < 0 || price < 0)
                {
                    errors.Add("Численные поля не могут быть отрицательными");
                }

                if (errors.Count() > 0)
                {
                    MessageBox.Show($"Следует исправить следующие ошибки:\n{String.Join("\n", errors)}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                TextRange textRange = new TextRange(
                    DescriptionBoardGame.Document.ContentStart,
                     DescriptionBoardGame.Document.ContentEnd
                );

                if (IDTextBlock.Text == "-//-")
                {
                    try
                    {
                        var newGame = new BoardGame
                        {
                            Picture = photos == null ? null : photos,
                            Name = NameBoardGame.Text.Trim(),
                            Price = price,
                            MinAge = minAge,
                            MinCountPlayers = minCount,
                            MaxCountPlayer = maxCount == 20 ? null : maxCount,
                            Description = textRange.Text == "" ? null : textRange.Text,
                            AverageTime = AvrTimeBoardGame.Text == "00:00:00" ? null : avrTime,
                            Amount = amountGame
                        };

                        context.BoardGames.Add(newGame);
                        context.SaveChanges();
                        IDTextBlock.Text = context.BoardGames.FirstOrDefault(g => g.Name == NameBoardGame.Text.Trim()).IdBoardGame.ToString();
                        MessageBox.Show($"Настольная игра сохранена с ID = {context.BoardGames.FirstOrDefault(g => g.Name == NameBoardGame.Text.Trim()).IdBoardGame}", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ой, что-то пошло не так\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    try
                    {
                        var boardGame = context.BoardGames.FirstOrDefault(g => g.IdBoardGame == Convert.ToInt32(IDTextBlock.Text));
                        boardGame.Picture = photos == null ? boardGame.Picture : photos;
                        boardGame.Name = NameBoardGame.Text.Trim();
                        boardGame.Price = price;
                        boardGame.MinAge = minAge;
                        boardGame.MinCountPlayers = minCount;
                        boardGame.MaxCountPlayer = maxCount == 20 ? null : maxCount;
                        boardGame.Description = textRange.Text == "" ? null : textRange.Text;
                        boardGame.AverageTime = AvrTimeBoardGame.Text == "00:00:00" ? null : avrTime;
                        boardGame.Amount = amountGame;
                        context.SaveChanges();
                        MessageBox.Show($"Настольная игра с ID = {boardGame.IdBoardGame} обновлена", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ой, что-то пошло не так\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void SetterButton_Click(object sender, RoutedEventArgs e)
        {
            SetterDialog setterDialog = new SetterDialog();
            if (setterDialog.ShowDialog() == true)
            {
                MessageBox.Show("Настройки успешно изменены", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AddBoardGameButton_Click(object sender, RoutedEventArgs e)
        {
            GameImage.Source = Img.ConvertImg(Properties.Resources.nullimg);
            IDTextBlock.Text = "";
            IDTextBlock.Text = "-//-";
            AvrTimeBoardGame.Text = "00:00:00";
            PriceBoardGame.Text = "0,00";
            MinCountBoardGame.Text = "1";
            MaxCountBoardGame.Text = "";
            AmountBoardGame.Text = "0";
            MinAgeBoardGame.Text = "0";
            DescriptionBoardGame.Document = new System.Windows.Documents.FlowDocument();

        }
        private void HTMLButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Документ HTML|*.html";
            if (dialog.ShowDialog() != true)
            {
                return;
            }
            using (var context = new OrderContext())
            {
                var orders = context.MonthOrders;
                File.AppendAllText(dialog.FileName, $"<h2>Месяц: {DateTime.Now:Y}</h2>");
                File.AppendAllText(dialog.FileName, "<Table border='1' cellspacing='0'><tr bgcolor='#7B2525' align='center' style='color: white;'><th>День</th><th>Кол-во заказов</th><th>Итог по настольным играм</th><th>Итог по дополнениям</th><th>Общий итог</th></tr>");
                var lastDay = context.MonthOrders.Max(o => o.DateTime);
                string text;
                for (int i = 1; i <= lastDay.Day; i++)
                {
                    var ordersDay = context.MonthOrders.Where(o => o.DateTime.Day == i);
                    if (ordersDay != null)
                    {
                        text = $"<tr><td>{i}</td><td>{ordersDay.Count()}</td><td>{ordersDay.Sum(o => o.BoardGamesPrice)}</td><td>{ordersDay.Sum(o => o.AdditionGamesPrice)}</td><td>{ordersDay.Sum(o => o.OrderPrice)}</td></tr>";
                    }
                    else
                    {
                        text = $"<tr><td>{i}</td><td>0</td><td>0,00</td><td>0,00</td><td>0,00</td></tr>";
                    }
                    File.AppendAllText(dialog.FileName, text);
                }
                File.AppendAllText(dialog.FileName, "</Table>");
                MessageBox.Show("Файл статистики успешно создан", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void NewPictureBoardGameButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл JPG|*.jpg|Файл PNG|*.png";
            if (dialog.ShowDialog() != true)
            {
                return;
            }
            photos = File.ReadAllBytes(dialog.FileName);
            GameImage.Source = Img.ConvertImg(photos);
        }

        private void ReLoadButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new BoardGameContext())
            {
                GameAdd(context.BoardGames.Where(g => g.Name.Contains(SearchTextBox.Text)).OrderBy(g => g.Name).ToList());
                BoardGameComboBox.ItemsSource = context.BoardGames.OrderBy(b => b.Name).Select(b => b.Name).ToList();
                BoardGameComboBox.SelectedIndex = 0;
            }
        }

        private void SearchAdditionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (var context = new BoardGameContext())
            {
                AdditionAdd(context.Additions.Where(a => a.Name.Contains(SearchAdditionTextBox.Text)).OrderBy(a => a.Name).ToList());
            }
        }

        private void AddAdditionButton_Click(object sender, RoutedEventArgs e)
        {
            AdditionImage.Source = Img.ConvertImg(Properties.Resources.nullimg);
            IDAdditionTextBlock.Text = "-//-";
            NameAddition.Text = "";
            PriceAddition.Text = "0,00";
            AmountAddition.Text = "0";
            DescriptionAddition.Document = new System.Windows.Documents.FlowDocument();
            DescriptionAddition.Document.Blocks.Add(new Paragraph(new Run()));
            MinAgeAddition.Text = "0";
        }

        private void ReLoadAdditionButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new BoardGameContext())
            {
                AdditionAdd(context.Additions.Where(a => a.Name.Contains(SearchAdditionTextBox.Text)).OrderBy(a => a.Name).ToList());
            }
        }

        private void NewPictureAdditionButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл JPG|*.jpg|Файл PNG|*.png";
            if (dialog.ShowDialog() != true)
            {
                return;
            }
            photosAddition = File.ReadAllBytes(dialog.FileName);
            AdditionImage.Source = Img.ConvertImg(photosAddition);
        }

        private void SaveAdditionButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> errors = new List<string>();
            using (var context = new BoardGameContext())
            {
                if (NameAddition.Text.Trim() == "")
                {
                    errors.Add("Заполните поле Наименование.");
                }
                if (IDAdditionTextBlock.Text == "-//-")
                {
                    if (context.Additions.FirstOrDefault(a => a.Name == NameAddition.Text.Trim()) != null)
                    {
                        errors.Add("Имя настольной игры не должно совпадать с уже существующими.");
                    }
                }
                if (!Decimal.TryParse(PriceAddition.Text, out decimal price))
                {
                    errors.Add("Цена должна быть цифрой");
                }
                if (!short.TryParse(AmountAddition.Text, out short amountAddition))
                {
                    errors.Add("В поле Кол-во экземпляров нужно использовать цифры");
                }
                if (!Byte.TryParse(MinAgeAddition.Text, out Byte minAge))
                {
                    errors.Add("В поле Мин возраст нужно использовать цифры");
                }
                if (minAge > 18)
                {
                    errors.Add("Поле Мин возраст не может быть больше 18");
                }
                if (minAge < 0 || amountAddition < 0 || price < 0)
                {
                    errors.Add("Численные поля не могут быть отрицательными");
                }

                if (errors.Count() > 0)
                {
                    MessageBox.Show($"Следует исправить следующие ошибки:\n{String.Join("\n", errors)}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var game = context.BoardGames.FirstOrDefault(g => g.Name == BoardGameComboBox.SelectedValue);

                TextRange textRange = new TextRange(
                    DescriptionAddition.Document.ContentStart,
                     DescriptionAddition.Document.ContentEnd
                );

                if (IDAdditionTextBlock.Text == "-//-")
                {
                    try
                    {
                        var newAddition = new Addition
                        {
                            Picture = photosAddition == null ? null : photosAddition,
                            Name = NameAddition.Text.Trim(),
                            Price = price,
                            MinAge = minAge,
                            Description = textRange.Text == "" ? null : textRange.Text,
                            Amount = amountAddition,
                            IdBoardGame = game.IdBoardGame
                        };

                        context.Additions.Add(newAddition);
                        context.SaveChanges();
                        IDAdditionTextBlock.Text = context.Additions.FirstOrDefault(a => a.Name == NameAddition.Text.Trim()).IdAddition.ToString();
                        MessageBox.Show($"Дополнение сохранено с ID = {context.Additions.FirstOrDefault(a => a.Name == NameAddition.Text.Trim()).IdAddition}", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ой, что-то пошло не так\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    try
                    {
                        var addition = context.Additions.FirstOrDefault(a => a.IdAddition == Convert.ToInt32(IDAdditionTextBlock.Text));
                        addition.Picture = photosAddition == null ? addition.Picture : photosAddition;
                        addition.Name = NameAddition.Text.Trim();
                        addition.Price = price;
                        addition.MinAge = minAge;
                        addition.Description = textRange.Text == "" ? null : textRange.Text;
                        addition.Amount = amountAddition;
                        addition.IdBoardGame = game.IdBoardGame;
                        context.SaveChanges();
                        MessageBox.Show($"Дополнение с ID = {addition.IdAddition} обновлено", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ой, что-то пошло не так\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            IDCategoryTextBox.Text = "-//-";
            NameCategoryTextBox.Text = "";
            CategoryOrGenreCheckBox.IsChecked = false;
            DescriptionCategoryRichTextBox.Document = new System.Windows.Documents.FlowDocument();
            DescriptionCategoryRichTextBox.Document.Blocks.Add(new Paragraph(new Run()));
        }

        private void SearchCategoryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (var context = new BoardGameContext())
            {
                CategoryAdd(context.Categories.Where(a => a.Name.Contains(SearchCategoryTextBox.Text)).OrderBy(a => a.Name).ToList());
            }
        }

        private void ReLoadCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new BoardGameContext())
            {
                CategoryAdd(context.Categories.Where(a => a.Name.Contains(SearchCategoryTextBox.Text)).OrderBy(a => a.Name).ToList());
            }
        }

        private void SaveCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new BoardGameContext())
            {
                TextRange textRange = new TextRange(
                    DescriptionCategoryRichTextBox.Document.ContentStart,
                    DescriptionCategoryRichTextBox.Document.ContentEnd
                   );
                if (IDCategoryTextBox.Text != "-//-")
                {
                    try
                    {
                        List<BoardGame> boardGame = new List<BoardGame>();
                        var category = context.Categories.FirstOrDefault(c => c.IdCategory == Convert.ToInt32(IDCategoryTextBox.Text));
                        category.Name = NameCategoryTextBox.Text;
                        category.Genre = CategoryOrGenreCheckBox.IsChecked == false ? false : true;
                        category.Description = textRange.Text;
                        foreach (var idGame in idCategoryBoardGame)
                        {
                            var game = context.BoardGames.Include(c => c.IdCategories).FirstOrDefault(g => g.IdBoardGame == idGame);
                            try
                            {
                                game.IdCategories.Add(category);
                                context.SaveChanges();
                            }
                            catch (Exception) { }
                        }
                        MessageBox.Show($"Категория с ID = {category.IdCategory} обновлена", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Что-то пошло не так\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    try
                    {
                        Category category = new Category
                        {
                            Name = NameCategoryTextBox.Text,
                            Genre = CategoryOrGenreCheckBox.IsChecked == false ? false : true,
                            Description = textRange.Text
                        };
                        context.Categories.Add(category);
                        context.SaveChanges();
                        foreach (var idGame in idCategoryBoardGame)
                        {
                            var game = context.BoardGames.Include(c => c.IdCategories).FirstOrDefault(g => g.IdBoardGame == idGame);
                            try
                            {
                                game.IdCategories.Add(category);
                                context.SaveChanges();
                            }
                            catch (Exception) { }
                        }
                        IDCategoryTextBox.Text = context.Categories.FirstOrDefault(c => c.Name == NameCategoryTextBox.Text.Trim()).IdCategory.ToString();
                        MessageBox.Show($"Категория создана с ID = {context.Categories.FirstOrDefault(c => c.Name == NameCategoryTextBox.Text.Trim()).IdCategory}", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Что-то пошло не так\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void DeleteCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (IDCategoryTextBox.Text != "-//-")
            {
                if (MessageBox.Show("Вы действительно хотите удалить категорию?","Потверждение",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    return;
                }
                using(var context = new BoardGameContext())
                {
                    var category = context.Categories.FirstOrDefault(c => c.IdCategory == Convert.ToInt32(IDCategoryTextBox.Text));
                    context.Categories.Remove(category);
                    context.SaveChanges();
                    IDCategoryTextBox.Text = "-//-";
                    MessageBox.Show("Категория удалена","Оповещение",MessageBoxButton.OK,MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Невозможно удалить то, чего нет", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}