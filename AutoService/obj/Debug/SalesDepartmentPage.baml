   M S B A M L   `   `   `  ????F  BAutoService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:AutoServiceAutoService  5      %    AutoService.SalesDepartmentPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:AutoService     5        N? Title$  SalesDepartmentPage??5	      .??5      ? -   5        $??grdSalesDepartament??5       ? ColumnDefinitions 5   
   ?? 5      $??* ?6   ?? 5      $??* ?6    ? RowDefinitions 5   
   ?? 5      $
??0.2* ?6   ?? 5      $??* ?6   ?? 5      $
??0.2* ?6   ?? 5      $??* ?6   .+?5   
   ?? $:??4Заказы ожидающие оформления??6   $??25(?66   $??Center=?6D   ?? 5   
   $??Оформленные??6   $??25(?6&   $??1??64   $??Center=?6D   &   System.Windows.Controls.DataGrid  5   
   -   5        $??dgOrders??5         AutoGenerateColumns .  $??1??6c   ??6#   ?? *orderclient+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Номер заказа??6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6;   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6E   ?? *ordernumber+ ?? UpdateSourceTrigger$ PropertyChanged=?4  .System.Windows.Controls.DataGridTemplateColumn  5      $ Дата??6)   $ * 67   	   CellTemplate	 5      ?? 5      .R?5       ?? $
??Wrap=?6H   $??Center=?6\   $??Center=?6y   ??6(   ?? 
 ?? Path$
 	orderdate?6?   5!      5#        5%      $ Машина??6)   $ * 69   	 5&      ?? 5'      .R?5(      ?? $
??Wrap=?6H   $??Center=?6\   $??Center=?6y   ??6(   ?? $
 	carnumber?6?   5)      5+        5-      $ Стоимость??6)   $ * 6<   	 5.      ?? 5/      .R?50      ?? $
??Wrap=?6G   $??Center=?6[   $??Center=?6x   ??6(   ?? $
 ordersum?6?   51      53        55      $
 auto 6)   	 56      ?? 57      .R?58      ?? -   5        $??Оформить??58   %   69   59      5;        5=      $
 auto 6)   	 5>      ?? 5?      .R?5@      ?? -   5        $??Отменить??5@   %   69   5A      5C      5E        5H   
   -   5        $??dgContracts??5H   0   $??1??6   $??1??6     .  ??6`   ?? *
contract+ 5I        5J      $# Номер контракта??6%   $ * 6>    6H   ?? *contractnumber+$ PropertyChanged=?  5K      $! Дата заклюения??6%   $ * 6=    6G   ?? *datecomclusion+$ PropertyChanged=?  5L      $ Клиент??6%   $ * 65    6?   ?? *clientnumber+$ PropertyChanged=?  5M      $ Оформил??6%   $ * 66    6@   ?? *email+$ PropertyChanged=?  5N      $
 auto 6)   	 5O      ?? 5P      .R?5Q      ?? -   5        $??Отменить??5Q   %   69   5R      5T      5V      ?? 5X   
   $??2??6   $??2??6   $+??%Предлогаемые услуги??61   $??25(?6O   $??Center=?6]     5Y   
   -   5          Name$ 
dgServices??5Y   ?   $??2??6=   $??3??6Q    .  
???   ?$??1,0,0,0q?6?   ??6z   ?? *	warehouse+ 5[        5\      $# Название услуги??6%      MinWidth$ 25Q?6>   $ * 6L    6V   ?? *servicename+$ PropertyChanged=?   EditingElementStyle 5]      ?? 5^       ?? 
TargetType" ??6    .?5_      ??  ?? Property ?@??	MaxLength ?? Value$ 30??6:   5`      5b        5d      $ Цена??6%   $ 60Q?63   $ * 6A    6K   ?? *price+$ PropertyChanged=? 5e      ?? 5f      " ??6    .?5g      ??  ?@??	MaxLength$ 30??6:   5h      5j        5l      $ Описание??6%   $ 60Q?67   $ * 6E    6O   ?? *servicedescription+$ PropertyChanged=? 5m      ?? 5n      " ??6    .?5o      ??  ?@??	MaxLength$	 500??6:   5p      5r        5u      $
 auto 6)   	 5v      ?? 5w      .R?5x      ?? -   5        $??Удалить??5x   %   68   5y      5{      5}      5      5?      