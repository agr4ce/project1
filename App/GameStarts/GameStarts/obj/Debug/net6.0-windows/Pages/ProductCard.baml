   M S B A M L   `   `   `  ����E  AGameStarts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GameStarts.PagesGameStarts.Pages  5      "    GameStarts.Pages.ProductCard   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:GameStarts.Pages     5        N� Title$  ProductCard��5	      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      �� 5      �� 5      $
��0.6* �6    � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      .+�5   
   �� -   5         ��Name$ NameProduct��5         BigRedTextBlock#��[  6)   �� 5   
   -   5         ��Name$ 
ProductImg��5      $��1��6$   $��2��61   �� 5   
   $
��Auto=�6   $��3��6;   $��2��6H   .��5      �� -   5        $ Description��5      $
��Wrap=�6-     JustText#��[ 6A   5      �� 5   
   $
��Auto=�6   $��Disabled=�6<   $��4��6c   $��3��6p   .��5      �� -   5         ��Name$ AdditionStackPanel��5       �� Orientation$ 
Horizontal=�65   $��Center=�6N   5      �� 5    
   $
��Auto=�6   $��2��6:   $��2��6J   $��2��6W   .��5!      �� -   5        $ CategoriesStackPanel��5!      5"      �� 5#   
   $��1��6   $��1��6"   $��2��62   .+�5$      �� -   5        $ PriceTextBlock��5$      $��Цена: 0 руб.��60   �� 5%      -   5        $ MinAgeTextBlock��5%      $.��(Минимальный возраст: 0��61   �� 5&      -   5        $ MinCountTextBlock��5&      $:��4Минимальное кол-во игроков: 1��63   �� 5'      -	   5        $ MaxCountTextBlock��5'      $<��6Максимальное кол-во игроков: -��63   �� 5(      -
   5        $ AvrTimeTextBLock��5(      $1��+Среднее время игры: 0:00:00��62   5)      �� 5*   
   -   5         ��Name$ AddToCartButton��5*      $��В корзину��6*   $��1��6>   $��2��6K   6[   5+      5,      