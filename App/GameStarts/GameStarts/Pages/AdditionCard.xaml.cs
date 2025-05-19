using GameStarts.Classes;
using GameStarts.Data;
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

namespace GameStarts.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdditionCard.xaml
    /// </summary>
    public partial class AdditionCard : Page
    {
        public AdditionCard()
        {
            InitializeComponent();

            using(var context = new BoardGameContext())
            {
                var addition = context.Additions.FirstOrDefault(a=>a.IdAddition == CurrentProduct.IDAddition);
                if (addition == null)
                {
                    MessageBox.Show("Кажется дополнение пропало ;-;", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (addition.Picture == null)
                    AdditionImg.Source = Img.ConvertImg(Properties.Resources.nullimg);
                else
                    AdditionImg.Source = Img.ConvertImg(addition.Picture);

                NameAddition.Text = addition.Name;
                PriceTextBlock.Text = String.Concat("Цена: ", addition.Price, " руб.");
                MinAgeTextBlock.Text = String.Concat("Минимальный возраст: ", addition.MinAge);
                Description.Text = addition.Description;

                if(addition.Amount == 0)
                {
                    AddToCartButton.IsEnabled = false;
                    AddToCartButton.Content = "Дополнения нет в наличии";
                }
            }
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            if (Cart.IDAddition.Contains(CurrentProduct.IDAddition))
            {
                MessageBox.Show("Дополнение уже есть в корзине", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Cart.IDAddition.Add(CurrentProduct.IDAddition);
            MessageBox.Show("Дополнение добавлено в корзину", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
