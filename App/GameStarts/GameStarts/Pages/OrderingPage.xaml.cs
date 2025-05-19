using GameStarts.Classes;
using GameStarts.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;
using System;
using GameStarts.Models;

namespace GameStarts.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderingPage.xaml
    /// </summary>
    public partial class OrderingPage : Page
    {
        public OrderingPage()
        {
            InitializeComponent();

            decimal itog = 0;

            using (var context = new BoardGameContext())
            {
                int count;
                decimal price;
                foreach (var idGame in Cart.IDBoardGame)
                {
                    var game = context.BoardGames.FirstOrDefault(g => g.IdBoardGame == idGame);
                    if (game != null)
                    {
                        TextBlock name = new();
                        name.Text = game.Name;
                        NameProduct.Children.Add(name);

                        TextBlock priceText = new();
                        price = game.Price;
                        priceText.Text = price.ToString();
                        OnePrice.Children.Add(priceText);

                        TextBlock countText = new();
                        count = Cart.CountBoardGame[Cart.IDBoardGame.IndexOf(idGame)];
                        countText.Text = count.ToString();
                        CountProduct.Children.Add(countText);

                        TextBlock secondPrice = new();
                        secondPrice.Text = (count * price).ToString();
                        ItogPrice.Children.Add(secondPrice);

                        itog += count * price;
                    }
                }
                foreach (var idAddition in Cart.IDAddition)
                {
                    var addition = context.Additions.FirstOrDefault(a => a.IdAddition == idAddition);
                    if (addition != null)
                    {
                        TextBlock name = new();
                        name.Text = addition.Name;
                        NameProduct.Children.Add(name);

                        TextBlock priceText = new();
                        price = addition.Price;
                        priceText.Text = price.ToString();
                        OnePrice.Children.Add(priceText);

                        TextBlock countText = new();
                        count = Cart.CountAddition[Cart.IDAddition.IndexOf(idAddition)];
                        countText.Text = count.ToString();
                        CountProduct.Children.Add(countText);

                        TextBlock secondPrice = new();
                        secondPrice.Text = (count * price).ToString();
                        ItogPrice.Children.Add(secondPrice);

                        itog += count * price;
                    }
                }
                TextBlock result = new();
                result.Text = itog.ToString();
                ItogPrice.Children.Add(result);
            }

        }

        private void SetOrderButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> errors = new List<string>();
            string pattern = ".*[0-9].*";
            Regex regex = new Regex(pattern);
            if (SurnameTextBox.Text.Trim() == "")
            {
                errors.Add("Поле Фамилия должно быть заполнено");
            }
            if (NameTextBox.Text.Trim() == "")
            {
                errors.Add("Поле Имя должно быть заполнено");
            }
            if (regex.IsMatch(SurnameTextBox.Text) || regex.IsMatch(NameTextBox.Text) || regex.IsMatch(Patronymic.Text))
            {
                errors.Add("В полях Фамилия/Имя/Отчество не должно быть цифр");
            }
            if (AddressTextBox.Text.Trim() == "")
            {
                errors.Add("Поле Адрес должно быть заполнено");
            }
            pattern = @"^(?i)[a-z0-9_-]+@([a-z0-9]+\.)+[a-z]+$";
            regex = new Regex(pattern);
            if (!regex.IsMatch(EmailTextBox.Text) && EmailTextBox.Text.Trim() != "")
            {
                errors.Add("Введён некоррeктный Email");
            }
            if (NumberTextBox.Text.Trim() == "")
            {
                errors.Add("Поле Номер телефона должно быть заполнено");
            }
            pattern = @"^[0-9]{11}$";
            regex = new Regex(pattern);
            if (!regex.IsMatch(NumberTextBox.Text))
            {
                errors.Add("Поле Номер телефона должно состоять из 11 цифр");
            }
            if(errors.Count > 0)
            {
                MessageBox.Show($"Упс! Надо исправить следующие ошибки:\n{String.Join("\n",errors)}","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            if (MessageBox.Show("Вы уверены, что хотите продолжить?\nЗаказ будет оформлен сразу после нажатия","Потверждение", MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }
            using (var context = new BoardGameContext())
            {
                try
                {
                    Purshase purshase = new Purshase
                    {
                        BuyersSurname = SurnameTextBox.Text.Trim(),
                        BuyersName = NameTextBox.Text.Trim(),
                        BuyersPatronymic = Patronymic.Text.Trim() == "" ? null : Patronymic.Text.Trim(),
                        DeliveryAddress = AddressTextBox.Text.Trim(),
                        Email = EmailTextBox.Text.Trim() == "" ? null : EmailTextBox.Text.Trim(),
                        Phone = NumberTextBox.Text.Trim()
                    };
                    context.Purshases.Add(purshase);
                    context.SaveChanges();
                    short count;

                    var purs = context.Purshases.FirstOrDefault(p=>p.DateTime == purshase.DateTime);
                    CurrentOrder.IdOrder = purs.IdPurshases;
                    foreach (var idGame in Cart.IDBoardGame)
                    {
                        count = (short)Cart.CountBoardGame[Cart.IDBoardGame.IndexOf(idGame)];
                        PurshasedBoardGame purshasedBoardGame = new PurshasedBoardGame
                        {
                            IdBoardGame = idGame,
                            IdPurshases = purs.IdPurshases,
                            Amount = count
                        };
                        context.PurshasedBoardGames.Add(purshasedBoardGame);
                    }
                    foreach (var idAddition in Cart.IDAddition)
                    {
                        count = (short)Cart.CountAddition[Cart.IDAddition.IndexOf(idAddition)];
                        PurshasedAddition purshasedAddition = new PurshasedAddition
                        {
                            IdAddition = idAddition,
                            IdPurshases = purs.IdPurshases,
                            Amount = count
                        };
                        context.PurshasedAdditions.Add(purshasedAddition);
                    }
                    context.SaveChanges();
                    MessageBox.Show("Заказ успешно оформлен\nС Вами свяжуться по номеру телефона в ближайщее время", "Успех", MessageBoxButton.OK,MessageBoxImage.Information);
                    NavigationManager.MainFrame.RemoveBackEntry();
                    NavigationManager.MainFrame.Navigate(new FinalPage());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Упс! Что-то пошло не так\n {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
