   M S B A M L   `   `   `  ����E  AGameStarts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GameStarts.PagesGameStarts.Pages  5          GameStarts.Pages.MainPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:GameStarts.Pages     5        N� Title$  MainPage��5	      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5       � ColumnDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $��2* �6   .+�5   
   �� $��0��6   $��1��6"   .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��Поиск: ��6      JustText#��[  6+   �� 5      -   5         ��Name$ NameTextBox��5      $	��300��6/   �� 5      -   5        $��Найти>��5        SmallUpButton#��[ 6?   5      �� 5      -   5         ��Name$ 	InfoLabel��5      $R��LСамые популярные игры за последнее время:��6'   5      �� 5   
   $��1��6   $��1��6$   $
��Auto=�64   .��5      � -   5         �Name$ BoardGameWrapPanel��5      5      �� 5   
   $��2��6   $
��Auto=�6(   .��5       �� $��2��6   ���	#FF7B2525.+�5!      �� $��Категории:��6     WhiteTextBlock#��[ 6.   �� 5"      -   5         ��Name$ CategoryStackPanel��5"      �� 5#      $��Жанры:��6   #��[ 6*   �� 5$      -   5        $ GenreStackPanel��5$      �� 5%      $!��Кол-во игроков:��6   #��[ 63   �� 5&      -   5         ��Name$ CountTextBlock��5&      $��
Любое��64   $��Right=�6A   #��[ 6]   �� 5'      -   5        	 ��Name$	 CountSlider��5'      
 � Minimum$
 0Q�6.    � Maximum$ 18Q�6:   6G   �� 5(      $$��Возраст игроков:��6   #��[ 64   �� 5)      -	   5        $ AgeTextBlock��5)      $��
Любой��62   $��Right=�6?   #��[ 6[   �� 5*      -
   5        $	 	AgeSlider��5*      $
 -1Q�6,   $ 18Q�69    � Value$ -1Q�6l   �� 5+      -   5        $��Найти>��5+        UpButton#��[ 6?   �� 5,      -   5         ��Name$ ReLoadButton��5,      $��Обновить��6/   #��[ 6B   6d   5-      5.      �� 5/   
   $��2��6   50      51      