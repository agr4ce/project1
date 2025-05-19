using GameStarts.Classes;
using GameStarts.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
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
using PdfSharp.Fonts;
using GameStarts.Models;

namespace GameStarts.Pages
{
    /// <summary>
    /// Логика взаимодействия для FinalPage.xaml
    /// </summary>
    public partial class FinalPage : Page
    {
        public FinalPage()
        {
            InitializeComponent();
        }

        private void ReturnToMainPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.MainFrame.Navigate(new MainPage());
        }

        private void CreatePdfButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Документ PDF|*.pdf";
            if (dialog.ShowDialog() != true)
            {
                return;
            }
            using (var context = new BoardGameContext())
            {
                var push = context.Purshases.FirstOrDefault(p=>p.IdPurshases == CurrentOrder.IdOrder);
                PdfDocument document = new();
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont verdanaBig = new XFont("Verdana", 21, XFontStyle.Bold);
                XFont verdanaSmall = new XFont("Verdana", 18, XFontStyle.Bold);
                XFont verdanaSmallNotBold = new XFont("Verdana", 15);
                gfx.DrawString($"Заказ №{push.IdPurshases}", verdanaBig, XBrushes.DarkRed, new XPoint(200,70));
                gfx.DrawString($"Имя заказчика: {push.BuyersSurname} {push.BuyersName} {push.BuyersPatronymic}", verdanaSmall, XBrushes.Black, new XPoint(30, 100));
                gfx.DrawLine(new XPen(XColor.FromArgb(0,0,0)), new XPoint(20,120), new XPoint(580, 120));
                gfx.DrawString("Состав:", verdanaSmall, XBrushes.Black, new XPoint(30, 140));
                int currectY = 170;
                int count;
                decimal sum = 0;
                if (Cart.IDBoardGame.Count + Cart.IDAddition.Count <=20)
                {
                    foreach (var IdGame in Cart.IDBoardGame)
                    {
                        count = Cart.CountBoardGame[Cart.IDBoardGame.IndexOf(IdGame)];
                        var game = context.BoardGames.FirstOrDefault(g => g.IdBoardGame == IdGame);
                        gfx.DrawString(game.Name, verdanaSmallNotBold, XBrushes.Black, new XPoint(30, currectY));
                        currectY += 22;
                        gfx.DrawString($"{game.Price} * {count}", verdanaSmallNotBold, XBrushes.Black, new XPoint(310, currectY));
                        gfx.DrawString($"{game.Price * count}", verdanaSmallNotBold, XBrushes.Black, new XPoint(450, currectY));
                        currectY += 22;
                        sum += game.Price * count;
                    }
                    foreach (var IdAddition in Cart.IDAddition)
                    {
                        count = Cart.CountAddition[Cart.IDAddition.IndexOf(IdAddition)];
                        var addition = context.Additions.FirstOrDefault(a => a.IdAddition == IdAddition);
                        gfx.DrawString(addition.Name, verdanaSmallNotBold, XBrushes.Black, new XPoint(30, currectY));
                        currectY += 22;
                        gfx.DrawString($"{addition.Price} * {count}", verdanaSmallNotBold, XBrushes.Black, new XPoint(310, currectY));
                        gfx.DrawString($"{addition.Price * count}", verdanaSmallNotBold, XBrushes.Black, new XPoint(450, currectY));
                        currectY += 22;
                        sum += addition.Price * count;
                    }
                }
                gfx.DrawLine(new XPen(XColor.FromArgb(0, 0, 0)), new XPoint(20, currectY), new XPoint(580, currectY));
                gfx.DrawString($"{sum}", verdanaSmall, XBrushes.Black, new XPoint(450, currectY+22));
                gfx.DrawString($"{DateTime.Now}", verdanaSmallNotBold, XBrushes.Black, new XPoint(30, currectY + 44));
                document.Save(dialog.FileName);
            }
            MessageBox.Show("Чек создан","Оповещение",MessageBoxButton.OK, MessageBoxImage.Information);
        
        }
    }
}
