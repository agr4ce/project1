   M S B A M L   `   `   `  ����E  AGameStarts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GameStarts
GameStarts  5   	       GameStarts.SetterDialog   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:GameStarts     5   	     � Title$  SetterDialog��5   	   $	��250��6   $	��600��6+    � WindowStartupLocation$ CenterScreen=�67   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��Сервер:��6   $��0��6#   $��Center=�60      RedTextBlock#��[  6K   �� 5   
   $5��/Наименование базы данных:��6   $��1��65   $��Center=�6B   #��[  6]   �� 5   
   $+��%Имя пользователя БД:��6   $��2��60   $��Center=�6=   #��[  6X   �� 5   
   $��Пароль:��6   $��3��6#   $��Center=�60   #��[  6K   �� 5   
   -   5         ��Name$ ServerNameTextBox��5      $��1��6-   �� 5   
   -   5        $ BDNameTextBox��5      $��1��6)   $��1��69   �� 5   
   -   5        $ BdUserNameTextBox��5      $��1��6-   $��2��6=   �� 5   
   -   5        $ PasswordTextBox��5      $��1��6+   $��3��6;   �� 5   
   -   5         ��Name$ ConnectButton��5      $��Подключиться��6(   $��4��6?   $��1��6L   6\   5       5!      