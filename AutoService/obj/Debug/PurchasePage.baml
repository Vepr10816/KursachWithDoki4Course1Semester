   M S B A M L   `   `   `  ????O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      F BAutoService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:AutoServiceAutoService 5         AutoService.PurchasePage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=4.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      3System&clr-namespace:System;assembly=mscorlib  5      %localclr-namespace:AutoService  5        N? Title$  PurchasePage??5      .??5      ? -   5        $??grdPurchase??5       ? RowDefinitions 5   
   ?? 5      $	??20* ?6   ?? 5      $
??111* ?6   ?? 5      $	??20* ?6   ?? 5      $
??111* ?6    ? ColumnDefinitions 5   
   ?? 5      $??* ?6   ?? 5      $??* ?6   .+?5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dgPurchaseOrder??5   r   $??0??6   $??1??6#      AutoGenerateColumns .  
???   ?$??0,0,1,0q?6?   ??6L   ?? *purchaseOrder+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$3 -Номер заказа на поставку??6%      MinWidth$ 25Q?6G      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6U   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6_   ?? *purchaseordernumber+
 ?? UpdateSourceTrigger$
 PropertyChanged=?   EditingElementStyle 5      ?? 5       ?? 
TargetType" ??6    .?5      ??  ?? Property ?@??	MaxLength ?? Value$ 35??6:   5       5"      4  .System.Windows.Controls.DataGridTemplateColumn  5$      $
 auto 6)   $ Дата??66      CellTemplate 5%      ?? 5&      .R?5'      (  "System.Windows.Controls.DatePicker     SelectedDate 6)   ?? *purchaseorderdate+$
 PropertyChanged=?   DisplayDateStart 6y   ??  ?? Mode$ OneWay=? ?? Today ?? Source# Z 5(      5*        5-      $ Количество??6%   $ 60Q?69   $ * 6G   	 6Q   ?? *
quantity+$
 PropertyChanged=? 5.      ?? 5/      " ??6    .?50      ??  ?@??	MaxLength$ 7??6:   51      54        55      $ Итоговая цена??6%   $	 100Q?6<   $ * 6K   	 6U   ?? *
rotalprice+$
 PropertyChanged=? 56      ?? 57      " ??6    .?58      ??  ?@??	MaxLength$ 9??6:   59      5;        5<      $! Артикул товара??6%   $	 140Q?6=   $ * 6L   	 6V   ?? *	article+$
 PropertyChanged=? 5=      ?? 5>      " ??6    .?5?      ??  ?@??	MaxLength$ 35??6:   5@      5B        5D      $
 auto 6)    5E      ?? 5F      .R?5G      ?? -   5        $??Удалить??5G   %   68   5H      5J      5L        5O   
   -   5        $ 	dgSuplier??5O   l   $??1??6   $??1??6#    .  
???   ?$??1,0,0,0q?6?   ??6L   ?? *	suplier+ 5Q        5R      $! ИНН поставщика??6)      CellEditingTemplate 5S      ?? 5T      .R?5U      ?? -   5         ?? 	MaxLength$ 10??5U   ?   ??6&   ??  ?? Path$	 inn?$ TwoWay=?$
 PropertyChanged=?6?   5V       5X      ?? 5Y      .R?5Z      ?? ??6(   ?? $	 inn?$ TwoWay=?$
 PropertyChanged=?5[      5]        5i      $ Наименование??6%   $ 60Q?6;   	 6J   ?? *supliername+$
 PropertyChanged=? 5j      ?? 5k      " ??6    .?5l      ??  ?@??	MaxLength$ 30??6:   5m      5p        5y      $! Расчетный счет??6)    5z      ?? 5{      .R?5|      ?? -   5        $ 20??5|   ?   ??6&   ?? $ paymentaccount?$ TwoWay=?$
 PropertyChanged=?6?   5}       5      ?? 5?      .R?5?      ?? ??6(   ?? $ paymentaccount?$ TwoWay=?$
 PropertyChanged=?5?      5?        5?      $
 auto 6)    5?      ?? 5?      .R?5?      ?? -   5        $??Удалить??5?   %   68   5?      5?      5?        5?   
   -   5        $ dgPurchaseContract??5?   u   $??0??6   $??3??6#    .  
???   ?$??0,0,1,0q?6?   ??6L   ?? *purchasecontract+ 5?        5?      $
 auto 6)   $ Дата??66    5?      ?? 5?      .R?5?         6)   ?? *purchasecontractdate+$
 PropertyChanged=? 6|   ?? $ OneWay=?# Z 5?      5?      4  .System.Windows.Controls.DataGridComboBoxColumn  5?      -	   5        cbPurchaseOrder??$3 -Номер заказа на поставку??5?   B   $ 60Q?6d     5?      -
   5        	cbSuplier??$ Поставщик??5?   <   $ 60Q?6O     5?      -   5        
cbEmployee??$ Сотрудник??5?   =   $ 60Q?6P     5?      $9 3Номер контракта на поставку??6%   $ 25Q?6J   	 6Y   ?? *purchasecontractnumber+$
 PropertyChanged=? 5?      ?? 5?      " ??6    .?5?      ??  ?@??	MaxLength$ 35??6:   5?      5?        5?      $
 auto 6)    5?      ?? 5?      .R?5?      ?? -   5        $??Удалить??5?   %   68   5?      5?      5?        5?   
   -   5        $ dgDisposable??5?   o   $??1??6   $??3??6#    .  
???   ?$??1,0,0,0q?6?   ??6L   ?? *
disposable+ 5?        5?      $6 0Серийный номер расходника??6%   $ 25Q?6H   $ * 6V   	 6`   ?? *seriesnumber+$
 PropertyChanged=? 5?      ?? 5?      " ??6    .?5?      ??  ?@??	MaxLength$ 35??6:   5?      5?        5?      -   5        cbPurchaseContractNumber??$9 3Номер контракта на поставку??5?   K   $ 60Q?6p     5?      $+ %Название расходника??6%   $ 60Q?6B   $ * 6P   	 6Z   ?? *disposablename+$
 PropertyChanged=? 5?      ?? 5?      " ??6    .?5?      ??  ?@??	MaxLength$ 7??6:   5?      5?        5?      $ Вес??6%   $	 100Q?62   $ * 6A   	 6K   ?? *disposableweight+$
 PropertyChanged=? 5?      ?? 5?      " ??6    .?5?      ??  ?@??	MaxLength$ 9??6:   5?      5?        5?      $ Цвет??6%   $	 140Q?63   $ * 6B   	 6L   ?? *disposablecolor+$
 PropertyChanged=? 5?      ?? 5?      " ??6    .?5?      ??  ?@??	MaxLength$ 20??6:   5?      5?        5?      $ Метериал??6%   $	 140Q?67   $ * 6F   	 6P   ?? *disposablematerial+$
 PropertyChanged=? 5?      ?? 5?      " ??6    .?5?      ??  ?@??	MaxLength$ 35??6:   5?      5?        5?      $' !Номер сертификата??6%   $	 140Q?6@   $ * 6O   	 6Y   ?? *disposablesertificate+$
 PropertyChanged=? 5?      ?? 5?      " ??6    .?5?      ??  ?@??	MaxLength$ 35??6:   5?      5?        5?      $
 auto 6)    5?      ?? 5?      .R?5?      ?? -   5        $??Удалить??5?   %   68   5?      5?      5?      ?? 5  
   $??0??6   $??0??6   $??Center=?6-   $??Center=?6J   $??25(?6f   $(??"Заказы на поставку??6t   ?? 5  
   $??0??6   $??1??6   $??Center=?6-   $??Center=?6J   $??25(?6f   $??Поставщики??6t   ?? 5  
   $??2??6   $??0??6   $??Center=?6-   $??Center=?6J   $??25(?6f   $.??(Контракты на поставку??6t   ?? 5  
   $??2??6   $??1??6   $??Center=?6-   $??Center=?6J   $??25(?6f   $??Расходники??6t   5     5     