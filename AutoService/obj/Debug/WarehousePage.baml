   M S B A M L   `   `   `  ????O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      F BAutoService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:AutoServiceAutoService 5         AutoService.WarehousePage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9System&clr-namespace:System;assembly=mscorlib     5      +localclr-namespace:AutoService     5        N? Title$  WarehousePage??5
      .??5      ? -   5        $??grdWarehousePage??5       ? RowDefinitions 5   
   ?? 5      $	??20* ?6   ?? 5      $
??111* ?6   ?? 5      $	??20* ?6   ?? 5      $
??111* ?6    ? ColumnDefinitions 5   
   ?? 5      $??* ?6   ?? 5      $??* ?6   .+?5   
   ?? $??0??6   $??0??6   $??Center=?6-   $??Center=?6J   $??25(?6f   $??Склады??6t   ?? 5   
   $??2??6   $??2??6   $??Center=?61   $??Center=?6N   $??25(?6j   $??Накладная??6x   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgWarehouse??5   n   $??0??6   $??1??6#      AutoGenerateColumns .  
???   ?$??1,0,0,0q?6?   ??6L   ?? *	warehouse+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$# Название склада??6%      MinWidth$ 25Q?6>      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6L   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6V   ?? *warehousename+
 ?? UpdateSourceTrigger$
 PropertyChanged=?   EditingElementStyle 5       ?? 5!       ?? 
TargetType" ??6    .?5"      ??  ?? Property ?@??	MaxLength ?? Value$ 30??6:   5#      5%        5'      $ 
Адрес??6%   $ 60Q?64   $ * 6B   	 6L   ?? *warehouseaddress+$
 PropertyChanged=? 5(      ?? 5)      " ??6    .?5*      ??  ?@??	MaxLength$ 60??6:   5+      5-      4  .System.Windows.Controls.DataGridTemplateColumn  50      $
 auto 6)      CellTemplate 51      ?? 52      .R?53      ?? -   5        $??Удалить??53   %   68   54      56      58      ?? 5;   
   $??0??6   $??1??6   $??Center=?6-   $??Center=?6J   $??25(?6f   $??Ячейки??6t     5=   
   -   5        $ dgCells??5=   i   $??1??6   $??1??6#    .  
???   ?$??1,0,0,0q?6?   ??6L   ?? *cell+ 5>      4  .System.Windows.Controls.DataGridComboBoxColumn  5?      -   5        cbWarehouseName??$ 
Склад??5?   B   $ 60Q?6Q     5@      $ Номер ячейки??6%   $ 25Q?6;   $ * 6I   	 6S   ?? *
cellnumber+$
 PropertyChanged=? 5A      ?? 5B      " ??6    .?5C      ??  ?@??	MaxLength$ 5??6:   5D      5F        5I      $
 auto 6)    5J      ?? 5K      .R?5L      ?? -   5        $??Удалить??5L   %   68   5M      5O      5Q        5S   
   -   5        $ 	dgInvoice??5S   q   $??3??6   $??2??6!    .  
???   ?$??1,0,0,0q?6?   ??6Q   ?? *	invoice+ 5U        5V      -   5        cbseriesnumber??$6 0Серийный номер расходника??5V   A   $ 60Q?6d     5W      -	   5        cbcellnumber??$ Ячейка??5W   ?   $ 60Q?6O     5X      -
   5        cbcellemail??$ Сотрудник??5X   >   $ 60Q?6Q     5Y      $# Номер накладной??6%   $ 25Q?6>   $ * 6L   	 6V   ?? *invoicenmber+$
 PropertyChanged=? 5Z      ?? 5[      " ??6    .?5\      ??  ?@??	MaxLength$ 35??6:   5]      5_        5a      $
 auto 6)   $ Дата??66    5b      ?? 5c      .R?5d      (  "System.Windows.Controls.DatePicker     SelectedDate 6)   ?? *invoicedate+$
 PropertyChanged=?   DisplayDateStart 6s   ??  ?? Mode$ OneWay=? ?? Today ?? Source# Z 5e      5g        5i      $ Количество??6%   $ 25Q?69   $ * 6G   	 6Q   ?? *
quantity+$
 PropertyChanged=? 5j      ?? 5k      " ??6    .?5l      ??  ?@??	MaxLength$ 8??6:   5m      5o        5r      $
 auto 6)    5s      ?? 5t      .R?5u      ?? -   5        $??Удалить??5u   %   68   5v      5x      5z      5|      5}      