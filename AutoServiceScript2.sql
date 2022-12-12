CREATE DATABASE "AutoService"
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    CONNECTION LIMIT = 10;
	
	
create table RoleUser
(
	RoleName varchar(30) not null constraint PK_RoleNumber primary key unique
);


create or replace procedure RoleUser_Insert(p_RoleName varchar(30))
language plpgsql
as $$
DECLARE have_record int := count (*) FROM RoleUser
where RoleName = p_RoleName ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
insert into RoleUser(RoleName)
values (p_RoleName);
end if;
end;
$$;

call RoleUser_Insert('Администратор');
call RoleUser_Insert('Сотрудник отдела закупок');
call RoleUser_Insert('Клиент');
call RoleUser_Insert('Сотрудник склада');
call RoleUser_Insert('Сотрудник отдела продаж');
call RoleUser_Insert('Сотрудник ремонтного отдела');

create or replace procedure RoleUser_Update(p_RoleName varchar(30), p_newRoleName varchar(30) )
language plpgsql
as $$
DECLARE have_record int := count (*) FROM RoleUser
where RoleName = p_newRoleName ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
update RoleUser set
RoleName = p_newRoleName
where
RoleName = p_RoleName;
end if;
end;
$$;


create or replace procedure RoleUser_Delete(p_RoleName varchar(30))
language plpgsql
as $$
--DECLARE have_record int := count (*) FROM Таблица где используется данная таблица
--where RoleName = p_RoleName;
begin
--if have_record>0 then raise exception 'Данные нельзя удалить, так как они уже задействованы';
--else
delete from RoleUser
where
RoleName = p_RoleName;
--end if;
end;
$$;





create table Users
(
Email varchar(50) not null constraint PK_Email primary key unique,
Password varchar(30) not null,
LastName varchar(30) not null,
FirstName varchar(30) not null,
MiddleName varchar(30) not null,
RegistrationDate date not null default CURRENT_DATE,
RoleName varchar(30) not null,
foreign key (RoleName) references RoleUser (RoleName) on update cascade on delete cascade,
life boolean not null default True
);

INSERT INTO Users VALUES ('diagram@mail.ru', 'qwe123', 'Special','For','Diagramm','2019-10-10','Сотрудник склада');
INSERT INTO Users VALUES ('diagram2@mail.ru', 'qwe123', 'Special','For','Diagramm','2019-10-10','Сотрудник склада');
INSERT INTO Users VALUES ('diagram3@mail.ru', 'qwe123', 'Special','For','Diagramm','2019-10-10','Сотрудник отдела закупок');
INSERT INTO Users VALUES ('diagram4@mail.ru', 'qwe123', 'Special','For','Diagramm','2019-10-10','Сотрудник склада');
INSERT INTO Users VALUES ('diagram5@mail.ru', 'qwe123', 'Special','For','Diagramm','2019-10-10','Сотрудник отдела закупок');
INSERT INTO Users VALUES ('diagram6@mail.ru', 'qwe123', 'Special','For','Diagramm','2020-10-10','Клиент');
INSERT INTO Users VALUES ('diagram7@mail.ru', 'qwe123', 'Special','For','Diagramm','2020-10-10','Клиент');
INSERT INTO Users VALUES ('diagram8@mail.ru', 'qwe123', 'Special','For','Diagramm','2020-10-10','Клиент');
INSERT INTO Users VALUES ('diagram9@mail.ru', 'qwe123', 'Special','For','Diagramm','2020-10-10','Клиент');
INSERT INTO Users VALUES ('diagram10@mail.ru', 'qwe123', 'Special','For','Diagramm','2021-10-10','Сотрудник отдела продаж');
INSERT INTO Users VALUES ('diagram11@mail.ru', 'qwe123', 'Special','For','Diagramm','2021-10-10','Сотрудник отдела продаж');
INSERT INTO Users VALUES ('diagram12@mail.ru', 'qwe123', 'Special','For','Diagramm','2021-10-10','Сотрудник отдела продаж');
INSERT INTO Users VALUES ('diagram13@mail.ru', 'qwe123', 'Special','For','Diagramm','2021-10-10','Сотрудник отдела продаж');
INSERT INTO Users VALUES ('diagram14@mail.ru', 'qwe123', 'Special','For','Diagramm','2021-10-10','Сотрудник отдела продаж');
INSERT INTO Users VALUES ('diagram15@mail.ru', 'qwe123', 'Special','For','Diagramm','2021-10-10','Сотрудник ремонтного отдела');

create or replace view Users_View ("Email", "Пароль", "Фамилия", "Имя", "Отчество", "Дата регистрации","Роль")
as
select
email,
password,
lastname,
firstname,
middlename,
RegistrationDate,
rolename
from Users where life = true;


create or replace procedure Users_Insert(p_Email varchar(50), p_Password varchar(30), p_LastName varchar(30), p_FirstName varchar(30), p_MiddleName varchar(30), p_RoleName varchar(30))
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Users
where Email = p_Email and Password = p_Password and LastName = p_LastName and FirstName = p_FirstName and MiddleName = p_MiddleName and RoleName = p_RoleName ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
insert into Users(Email,Password,LastName,FirstName,MiddleName,RoleName)
values (p_Email,p_Password,p_LastName,p_FirstName,p_MiddleName,p_RoleName);
end if;
end;
$$;


call Users_insert('Admin', 'Admin', 'Admin', 'Admin', 'Admin', 'Администратор');

create or replace procedure Users_Update(p_Email varchar(50), p_Password varchar(30), p_LastName varchar(30), p_FirstName varchar(30), p_MiddleName varchar(30), p_RoleName varchar(30), p_newEmail varchar(50) )
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Users
where Email = p_newEmail and Password = p_Password and LastName = p_LastName and FirstName = p_FirstName and MiddleName = p_MiddleName and RoleName = p_RoleName ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
update Users set
Password = p_Password,
LastName = p_LastName,
FirstName = p_FirstName,
MiddleName = p_MiddleName,
RoleName = p_RoleName,
Email = p_newEmail
where
Email = p_Email;
end if;
end;
$$;


create or replace procedure Users_Delete(p_Email varchar(50))
language plpgsql
as $$
--DECLARE have_record int := count (*) FROM Таблица где используется данная таблица
--where Email = p_Email;
begin
--if have_record>0 then raise exception 'Данные нельзя удалить, так как они уже задействованы';
--else
delete from Users
where
Email = p_Email;
--end if;
end;
$$;





create table Client
(
ClientNumber varchar(10) not null constraint PK_ClientNumber primary key unique,
PassportSeries varchar(4) not null,
PassportNumber varchar(6) not null,
Email varchar(50) not null,
foreign key (Email) references Users (Email) on update cascade on delete cascade
);


create or replace procedure Client_Insert(p_ClientNumber varchar(10), p_PassportSeries varchar(4), p_PassportNumber varchar(6), p_Email varchar(50))
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Client
where ClientNumber = p_ClientNumber and PassportSeries = p_PassportSeries and PassportNumber = p_PassportNumber and Email = p_Email ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
insert into Client(ClientNumber,PassportSeries,PassportNumber,Email)
values (p_ClientNumber,p_PassportSeries,p_PassportNumber,p_Email);
end if;
end;
$$;


create or replace procedure Client_Update(p_ClientNumber varchar(10), p_PassportSeries varchar(4), p_PassportNumber varchar(6), p_Email varchar(50), p_newClientNumber varchar(10) )
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Client
where ClientNumber = p_newClientNumber and PassportSeries = p_PassportSeries and PassportNumber = p_PassportNumber and Email = p_Email ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
update Client set
PassportSeries = p_PassportSeries,
PassportNumber = p_PassportNumber,
Email = p_Email,
ClientNumber = p_newClientNumber
where
ClientNumber = p_ClientNumber;
end if;
end;
$$;


create or replace procedure Client_Delete(p_ClientNumber varchar(10))
language plpgsql
as $$
--DECLARE have_record int := count (*) FROM Таблица где используется данная таблица
--where ClientNumber = p_ClientNumber;
begin
--if have_record>0 then raise exception 'Данные нельзя удалить, так как они уже задействованы';
--else
delete from Client
where
ClientNumber = p_ClientNumber;
--end if;
end;
$$;





create or replace function Sign_IN(login varchar(30), pass varchar(30))
   returns varchar(30) 
   language plpgsql
  as
$$
DECLARE have_record int := count (*) FROM Users
where email = login and password = pass;
begin
	if have_record>0 then
		return rolename from users where email = login and password = pass; 
	else
		return 'No';
	end if;
end;
$$





create table PurchaseOrder
(
PurchaseOrderNumber varchar(35) not null constraint PK_PurchaseOrderNumber primary key unique,
PurchaseOrderDate date not null,
Quantity int not null,
RotalPrice decimal(38,2) not null,
Article varchar(35) not null
);

call PurchaseOrder_Insert('test2', '2022-10-10', '33', '38.2', '123456')

create or replace procedure PurchaseOrder_Insert(p_PurchaseOrderNumber varchar(35), p_PurchaseOrderDate date, p_Quantity int, p_RotalPrice decimal(38,2), p_Article varchar(35))
language plpgsql
as $$
DECLARE have_record int := count (*) FROM PurchaseOrder
where PurchaseOrderNumber = p_PurchaseOrderNumber;
begin
if have_record>0 then
raise exception 'Такой номер договора на поставку уже есть!';
else
insert into PurchaseOrder(PurchaseOrderNumber,PurchaseOrderDate,Quantity,RotalPrice,Article)
values (p_PurchaseOrderNumber,p_PurchaseOrderDate,p_Quantity,p_RotalPrice,p_Article);
end if;
end;
$$;


create or replace procedure PurchaseOrder_Update(p_PurchaseOrderNumber varchar(35), p_PurchaseOrderDate date, p_Quantity int, p_RotalPrice decimal(38,2), p_Article varchar(35), p_newPurchaseOrderNumber varchar(35) )
language plpgsql
as $$
DECLARE have_record int := count (*) FROM PurchaseOrder
where PurchaseOrderNumber = p_PurchaseOrderNumber and PurchaseOrderDate = p_PurchaseOrderDate and Quantity = p_Quantity and RotalPrice = p_RotalPrice and Article = p_Article;
begin
if have_record>0 then
raise exception 'Такой договор на поставку уже есть!';
else
update PurchaseOrder set
PurchaseOrderDate = p_PurchaseOrderDate,
Quantity = p_Quantity,
RotalPrice = p_RotalPrice,
Article = p_Article,
PurchaseOrderNumber = p_newPurchaseOrderNumber
where
PurchaseOrderNumber = p_PurchaseOrderNumber;
end if;
end;
$$;


create or replace procedure PurchaseOrder_Delete(p_PurchaseOrderNumber varchar(35))
language plpgsql
as $$
--DECLARE have_record int := count (*) FROM Таблица где используется данная таблица
--where PurchaseOrderNumber = p_PurchaseOrderNumber;
begin
--if have_record>0 then raise exception 'Данные нельзя удалить, так как они уже задействованы';
--else
delete from PurchaseOrder
where
PurchaseOrderNumber = p_PurchaseOrderNumber;
--end if;
end;
$$;





create table Suplier
(
INN varchar(10) not null constraint PK_INN primary key unique,
SuplierName varchar(30) not null,
PaymentAccount varchar(20) not null
);

call Suplier_Insert('1234567890', 'Petrovuch', '12345678901234567890')
create or replace procedure Suplier_Insert(p_INN varchar(10), p_SuplierName varchar(30), p_PaymentAccount varchar(20))
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Suplier
where INN = p_INN or SuplierName = p_SuplierName or PaymentAccount = p_PaymentAccount ;
begin
if have_record>0 then
raise exception 'Введенные данные поставщика не уникальны';
else
insert into Suplier(INN,SuplierName,PaymentAccount)
values (p_INN,p_SuplierName,p_PaymentAccount);
end if;
end;
$$;


create or replace procedure Suplier_Update(p_INN varchar(10), p_SuplierName varchar(30), p_PaymentAccount varchar(20), p_newINN varchar(10) )
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Suplier
where INN = p_newINN and SuplierName = p_SuplierName and PaymentAccount = p_PaymentAccount ;
begin
if have_record>0 then
raise exception 'Введенные данные поставщика не уникальны';
else
update Suplier set
SuplierName = p_SuplierName,
PaymentAccount = p_PaymentAccount,
INN = p_newINN
where
INN = p_INN;
end if;
end;
$$;


create or replace procedure Suplier_Delete(p_INN varchar(10))
language plpgsql
as $$
--DECLARE have_record int := count (*) FROM Таблица где используется данная таблица
--where INN = p_INN;
begin
--if have_record>0 then raise exception 'Данные нельзя удалить, так как они уже задействованы';
--else
delete from Suplier
where
INN = p_INN;
--end if;
end;
$$;





create table PurchaseContract
(
PurchaseContractNumber varchar(35) not null constraint PK_PurchaseContractNumber primary key unique,
PurchaseContractDate date not null,
PurchaseOrderNumber varchar(35) not null,
foreign key (PurchaseOrderNumber) references PurchaseOrder (PurchaseOrderNumber) on update cascade on delete cascade,
INN varchar(10) not null,
foreign key (INN) references Suplier (INN) on update cascade on delete cascade,
Email varchar(50) not null,
foreign key (Email) references Users (Email) on update cascade on delete cascade
);

call PurchaseContract_Insert('1345gfge', '2022-10-10', 'test2', '1234567890', 'Zak@mail.ru')
create or replace procedure PurchaseContract_Insert(p_PurchaseContractNumber varchar(35), p_PurchaseContractDate date, p_PurchaseOrderNumber varchar(35), p_INN varchar(10), p_Email varchar(50))
language plpgsql
as $$
DECLARE have_record int := count (*) FROM PurchaseContract
where PurchaseContractNumber = p_PurchaseContractNumber and PurchaseContractDate = p_PurchaseContractDate and PurchaseOrderNumber = p_PurchaseOrderNumber and INN = p_INN;
begin
if have_record>0 then
raise exception 'Такой Контракт на поставку уже есть';
else
insert into PurchaseContract(PurchaseContractNumber,PurchaseContractDate,PurchaseOrderNumber,INN,Email)
values (p_PurchaseContractNumber,p_PurchaseContractDate,p_PurchaseOrderNumber,p_INN,p_Email);
end if;
end;
$$;

select * from Users where (lastname ||' '|| firstname||' '|| middlename) = 'Special For Diagramm'
create or replace procedure PurchaseContract_Update(p_PurchaseContractNumber varchar(35), p_PurchaseContractDate date, p_PurchaseOrderNumber varchar(35), p_INN varchar(10), p_Email varchar(50), p_newPurchaseContractNumber varchar(35) )
language plpgsql
as $$
DECLARE have_record int := count (*) FROM PurchaseContract
where PurchaseContractNumber = p_PurchaseContractNumber ;
begin
if have_record>0 then
raise exception 'Контракт на поставку с таким номером уже есть';
else
update PurchaseContract set
PurchaseContractDate = p_PurchaseContractDate,
PurchaseOrderNumber = p_PurchaseOrderNumber,
INN = p_INN,
Email = p_Email,
PurchaseContractNumber = p_newPurchaseContractNumber
where
PurchaseContractNumber = p_PurchaseContractNumber;
end if;
end;
$$;


create or replace procedure PurchaseContract_Delete(p_PurchaseContractNumber varchar(35))
language plpgsql
as $$
--DECLARE have_record int := count (*) FROM Таблица где используется данная таблица
--where PurchaseContractNumber = p_PurchaseContractNumber;
begin
--if have_record>0 then raise exception 'Данные нельзя удалить, так как они уже задействованы';
--else
delete from PurchaseContract
where
PurchaseContractNumber = p_PurchaseContractNumber;
--end if;
end;
$$;


create or replace view PurchaseContract_View ("purchasecontractnumber", "purchasecontractdate", "purchaseordernumber", "inn", "email")
as
select
purchasecontractnumber,
purchasecontractdate,
purchaseordernumber,
supliername,
lastname ||' '|| firstname||' '|| middlename
from PurchaseContract
inner join Suplier on PurchaseContract.inn = Suplier.inn
inner join Users on PurchaseContract.email = Users.email;





create table Disposable
(
	SeriesNumber varchar(35) not null constraint PK_SeriesNumber primary key unique,
	DisposableName varchar(35) not null,
	DisposableWeight decimal(38,2) not null,
	DisposableColor varchar(20) not null,
	DisposableMaterial varchar(35) not null,
	DisposableSertificate varchar(35) not null,
	PurchaseContractNumber varchar(35) not null,
	foreign key (PurchaseContractNumber) references PurchaseContract (PurchaseContractNumber) on update cascade on delete cascade
);

call Disposable_Insert('iurrtuigjv8634673', 'iueryugeig', '45.6', 'Red', 'Materiable', 'oureouviu87er87','1345gfge')

create or replace procedure Disposable_Insert(p_SeriesNumber varchar(35), p_DisposableName varchar(35), p_DisposableWeight decimal(38,2), p_DisposableColor varchar(20), p_DisposableMaterial varchar(35), p_DisposableSertificate varchar(35), p_PurchaseContractNumber varchar(35))
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Disposable
where SeriesNumber = p_SeriesNumber  and DisposableSertificate = p_DisposableSertificate and PurchaseContractNumber = p_PurchaseContractNumber ;
begin
if have_record>0 then
raise exception 'Расходник с таким серийным номером или сертификатом или контрактом на поставку уже есть';
else
insert into Disposable(SeriesNumber,DisposableName,DisposableWeight,DisposableColor,DisposableMaterial,DisposableSertificate,PurchaseContractNumber)
values (p_SeriesNumber,p_DisposableName,p_DisposableWeight,p_DisposableColor,p_DisposableMaterial,p_DisposableSertificate,p_PurchaseContractNumber);
end if;
end;
$$;


create or replace procedure Disposable_Update(p_SeriesNumber varchar(35), p_DisposableName varchar(35), p_DisposableWeight decimal(38,2), p_DisposableColor varchar(20), p_DisposableMaterial varchar(35), p_DisposableSertificate varchar(35), p_PurchaseContractNumber varchar(35), p_newSeriesNumber varchar(35) )
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Disposable
where SeriesNumber = p_SeriesNumber  and DisposableSertificate = p_DisposableSertificate and PurchaseContractNumber = p_PurchaseContractNumber ;
begin
if have_record>0 then
raise exception 'Расходник с таким серийным номером или сертификатом или контрактом на поставку уже есть';
else
update Disposable set
DisposableName = p_DisposableName,
DisposableWeight = p_DisposableWeight,
DisposableColor = p_DisposableColor,
DisposableMaterial = p_DisposableMaterial,
DisposableSertificate = p_DisposableSertificate,
PurchaseContractNumber = p_PurchaseContractNumber,
SeriesNumber = p_newSeriesNumber
where
SeriesNumber = p_SeriesNumber;
end if;
end;
$$;


create or replace procedure Disposable_Delete(p_SeriesNumber varchar(35))
language plpgsql
as $$
--DECLARE have_record int := count (*) FROM Таблица где используется данная таблица
--where SeriesNumber = p_SeriesNumber;
begin
--if have_record>0 then raise exception 'Данные нельзя удалить, так как они уже задействованы';
--else
delete from Disposable
where
SeriesNumber = p_SeriesNumber;
--end if;
end;
$$;





create table Warehouse
(
WarehouseName varchar(30) not null constraint PK_WarehouseName primary key unique,
WarehouseAddress varchar(60) not null
);


create or replace procedure Warehouse_Insert(p_WarehouseName varchar(30), p_WarehouseAddress varchar(60))
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Warehouse
where WarehouseName = p_WarehouseName and WarehouseAddress = p_WarehouseAddress;
begin
if have_record>0 then
raise exception 'Уже существует';
else
insert into Warehouse(WarehouseName,WarehouseAddress)
values (p_WarehouseName,p_WarehouseAddress);
end if;
end;
$$;


create or replace procedure Warehouse_Update(p_WarehouseName varchar(30), p_WarehouseAddress varchar(60), p_newWarehouseName varchar(30) )
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Warehouse
where WarehouseName = p_newWarehouseName and WarehouseAddress = p_WarehouseAddress;
begin
if have_record>0 then
raise exception 'Уже существует';
else
update Warehouse set
WarehouseAddress = p_WarehouseAddress,
WarehouseName = p_newWarehouseName
where
WarehouseName = p_WarehouseName;
end if;
end;
$$;


create or replace procedure Warehouse_Delete(p_WarehouseName varchar(30))
language plpgsql
as $$
--DECLARE have_record int := count (*) FROM Таблица где используется данная таблица
--where WarehouseName = p_WarehouseName;
begin
--if have_record>0 then raise exception 'Данные нельзя удалить, так как они уже задействованы';
--else
delete from Warehouse
where
WarehouseName = p_WarehouseName;
--end if;
end;
$$;





create table Cell
(
CellNumber varchar(5) not null constraint PK_CellNumber primary key unique,
WarehouseName varchar(30) not null,
foreign key (WarehouseName) references Warehouse (WarehouseName) on update cascade on delete cascade
);

call Cell_Insert('345', 'lhloh')
create or replace procedure Cell_Insert(p_CellNumber varchar(5), p_WarehouseName varchar(30))
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Cell
where CellNumber = p_CellNumber and WarehouseName = p_WarehouseName ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
insert into Cell(CellNumber,WarehouseName)
values (p_CellNumber,p_WarehouseName);
end if;
end;
$$;


create or replace procedure Cell_Update(p_CellNumber varchar(5), p_WarehouseName varchar(30), p_newCellNumber varchar(5) )
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Cell
where CellNumber = p_newCellNumber and WarehouseName = p_WarehouseName ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
update Cell set
WarehouseName = p_WarehouseName,
CellNumber = p_newCellNumber
where
CellNumber = p_CellNumber;
end if;
end;
$$;


create or replace procedure Cell_Delete(p_CellNumber varchar(5))
language plpgsql
as $$
--DECLARE have_record int := count (*) FROM Таблица где используется данная таблица
--where CellNumber varchar(5) = p_CellNumber varchar(5);
begin
--if have_record>0 then raise exception 'Данные нельзя удалить, так как они уже задействованы';
--else
delete from Cell
where
CellNumber = p_CellNumber;
--end if;
end;
$$;





create table Invoice
(
InvoiceNmber varchar(35) not null constraint PK_InvoiceNmber primary key unique ,
InvoiceDate date not null,
Quantity int not null,
SeriesNumber varchar(35) not null,
foreign key (SeriesNumber) references Disposable (SeriesNumber) on update cascade on delete cascade,
CellNumber varchar(5) not null,
foreign key (cellnumber) references Cell (cellnumber) on update cascade on delete cascade,
Email varchar(50) not null,
foreign key (Email) references Users (Email) on update cascade on delete cascade
);


create or replace procedure Invoice_Insert(p_InvoiceNmber varchar(35), p_InvoiceDate date, p_Quantity int, p_SeriesNumber varchar(35), p_CellNumber varchar(5), p_Email varchar(50))
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Invoice
where InvoiceNmber = p_InvoiceNmber and InvoiceDate = p_InvoiceDate and Quantity = p_Quantity and SeriesNumber = p_SeriesNumber and CellNumber = p_CellNumber and Email = p_Email ;
begin
if have_record>0 then
raise exception 'Введеная накладная уже есть';
else
insert into Invoice(InvoiceNmber,InvoiceDate,Quantity,SeriesNumber,CellNumber,Email)
values (p_InvoiceNmber,p_InvoiceDate,p_Quantity,p_SeriesNumber,p_CellNumber,p_Email);
end if;
end;
$$;


create or replace procedure Invoice_Update(p_InvoiceNmber varchar(35), p_InvoiceDate date, p_Quantity int, p_SeriesNumber varchar(35), p_CellNumber varchar(5), p_Email varchar(50), p_newInvoiceNmber varchar(35) )
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Invoice
where InvoiceNmber = p_newInvoiceNmber and InvoiceDate = p_InvoiceDate and Quantity = p_Quantity and SeriesNumber = p_SeriesNumber and CellNumber = p_CellNumber and Email = p_Email ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
update Invoice set
InvoiceDate = p_InvoiceDate,
Quantity = p_Quantity,
SeriesNumber = p_SeriesNumber,
CellNumber = p_CellNumber,
Email = p_Email,
InvoiceNmber = p_newInvoiceNmber
where
InvoiceNmber = p_InvoiceNmber;
end if;
end;
$$;


create or replace procedure Invoice_Delete(p_InvoiceNmber varchar(35))
language plpgsql
as $$
--DECLARE have_record int := count (*) FROM Таблица где используется данная таблица
--where InvoiceNmber = p_InvoiceNmber;
begin
--if have_record>0 then raise exception 'Данные нельзя удалить, так как они уже задействованы';
--else
delete from Invoice
where
InvoiceNmber = p_InvoiceNmber;
--end if;
end;
$$;


create or replace view Invoice_View ("invoicenmber", "invoicedate", "quantity", "seriesnumber", "cellnumber", "email")
as
select
invoicenmber,
invoicedate,
quantity,
Disposable.seriesnumber,
Cell.warehousename || ' - '|| Cell.cellnumber,
Users.email
from Invoice
inner join Disposable on Invoice.seriesnumber = Disposable.seriesnumber
inner join Cell on Invoice.cellnumber = Cell.cellnumber
inner join Users on Invoice.email = Users.email;





create table Car
(
CarNumber varchar(6) not null constraint PK_CarNumber primary key unique,
CarBrand varchar(35) not null,
CarModel varchar(50) not null,
STS varchar(10) not null,
VIN varchar(17) not null,
ClientNumber varchar(10) not null,
foreign key (ClientNumber) references Client (ClientNumber) on update cascade on delete cascade
);


create or replace procedure Car_Insert(p_CarNumber varchar(6), p_CarBrand varchar(35), p_CarModel varchar(50), p_STS varchar(10), p_VIN varchar(17), p_ClientNumber varchar(10))
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Car
where CarNumber = p_CarNumber and CarBrand = p_CarBrand and CarModel = p_CarModel and STS = p_STS and VIN = p_VIN and ClientNumber = p_ClientNumber ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
insert into Car(CarNumber,CarBrand,CarModel,STS,VIN,ClientNumber)
values (p_CarNumber,p_CarBrand,p_CarModel,p_STS,p_VIN,p_ClientNumber);
end if;
end;
$$;


create or replace procedure Car_Update(p_CarNumber varchar(6), p_CarBrand varchar(35), p_CarModel varchar(50), p_STS varchar(10), p_VIN varchar(17), p_ClientNumber varchar(10), p_newCarNumber varchar(6) )
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Car
where CarNumber = p_newCarNumber and CarBrand = p_CarBrand and CarModel = p_CarModel and STS = p_STS and VIN = p_VIN and ClientNumber = p_ClientNumber ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
update Car set
CarBrand = p_CarBrand,
CarModel = p_CarModel,
STS = p_STS,
VIN = p_VIN,
ClientNumber = p_ClientNumber,
CarNumber = p_newCarNumber
where
CarNumber = p_CarNumber;
end if;
end;
$$;


create or replace procedure Car_Delete(p_CarNumber varchar(6))
language plpgsql
as $$
--DECLARE have_record int := count (*) FROM Таблица где используется данная таблица
--where CarNumber = p_CarNumber;
begin
--if have_record>0 then raise exception 'Данные нельзя удалить, так как они уже задействованы';
--else
delete from Car
where
CarNumber = p_CarNumber;
--end if;
end;
$$;





create table OrderClient
(
OrderNumber varchar(35) not null constraint PK_OrderNumber primary key unique,
OrderSum decimal(38,2) not null,
OrderDate date not null,
CarNumber varchar(6) not null,
foreign key (CarNumber) references Car (CarNumber) on update cascade on delete cascade
);


create or replace procedure OrderClient_Insert(p_OrderNumber varchar(35), p_OrderSum decimal(38,2), p_OrderDate date, p_ClientNumber varchar(10))
language plpgsql
as $$
DECLARE have_record int := count (*) FROM OrderClient
where OrderNumber = p_OrderNumber and OrderSum = p_OrderSum and OrderDate = p_OrderDate and CarNumber = p_ClientNumber ;
begin
if have_record>0 then
raise exception 'Уже записан';
else
insert into OrderClient(OrderNumber,OrderSum,OrderDate,CarNumber)
values (p_OrderNumber,p_OrderSum,p_OrderDate,p_ClientNumber);
end if;
end;
$$;


create or replace procedure OrderClient_Update(p_OrderNumber varchar(35), p_OrderSum decimal(38,2), p_OrderDate date, p_ClientNumber varchar(10), p_newOrderNumber varchar(35) )
language plpgsql
as $$
DECLARE have_record int := count (*) FROM OrderClient
where OrderNumber = p_newOrderNumber and OrderSum = p_OrderSum and OrderDate = p_OrderDate and CarNumber = p_ClientNumber ;
begin
if have_record>0 then
raise exception 'Уже записан';
else
update OrderClient set
OrderSum = p_OrderSum,
OrderDate = p_OrderDate,
CarNumber = p_ClientNumber,
OrderNumber = p_newOrderNumber
where
OrderNumber = p_OrderNumber;
end if;
end;
$$;


create or replace procedure OrderClient_Delete(p_OrderNumber varchar(35))
language plpgsql
as $$
--DECLARE have_record int := count (*) FROM Таблица где используется данная таблица
--where OrderNumber = p_OrderNumber;
begin
--if have_record>0 then raise exception 'Данные нельзя удалить, так как они уже задействованы';
--else
delete from OrderClient
where
OrderNumber = p_OrderNumber;
--end if;
end;
$$;




create table Service
(
ServiceName varchar(30) not null constraint PK_ServiceName primary key unique,
Price decimal(38,2) not null,
ServiceDescription text not null
);

call Service_Insert('Ремонт КПП', '45800', 'Если коробка переключения передач выходит из строя, не стоит даже пробовать выполнить ремонт своими руками, такой сложный агрегат, требует исключительную точность. Диагностика и ремонт. Мы проводим точную предварительную диагностику, что намного повышает качество ремонтных работ. Наши специалисты быстро и по доступной цене отремонтируют КПП')
create or replace procedure Service_Insert(p_ServiceName varchar(30), p_Price decimal(38,2), p_ServiceDescription text)
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Service
where ServiceName = p_ServiceName and Price = p_Price and ServiceDescription = p_ServiceDescription ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
insert into Service(ServiceName,Price,ServiceDescription)
values (p_ServiceName,p_Price,p_ServiceDescription);
end if;
end;
$$;


create or replace procedure Service_Update(p_ServiceName varchar(30), p_Price decimal(38,2), p_ServiceDescription text, p_newServiceName varchar(30) )
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Service
where ServiceName = p_newServiceName and Price = p_Price and ServiceDescription = p_ServiceDescription ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
update Service set
Price = p_Price,
ServiceDescription = p_ServiceDescription,
ServiceName = p_newServiceName
where
ServiceName = p_ServiceName;
end if;
end;
$$;


create or replace procedure Service_Delete(p_ServiceName varchar(30))
language plpgsql
as $$
--DECLARE have_record int := count (*) FROM Таблица где используется данная таблица
--where ServiceName = p_ServiceName;
begin
--if have_record>0 then raise exception 'Данные нельзя удалить, так как они уже задействованы';
--else
delete from Service
where
ServiceName = p_ServiceName;
--end if;
end;
$$;





create table ServiceOrder
(
ID_ServiceOrder SERIAL PRIMARY KEY,
ServiceName varchar(30) not null,
foreign key (ServiceName) references Service (ServiceName) on update cascade on delete cascade,
OrderNumber varchar(35) not null,
foreign key (OrderNumber) references OrderClient (OrderNumber) on update cascade on delete cascade
);


create or replace procedure ServiceOrder_Insert(p_ServiceName varchar(30), p_OrderNumber varchar(35))
language plpgsql
as $$
DECLARE have_record int := count (*) FROM ServiceOrder
where ServiceName = p_ServiceName and OrderNumber = p_OrderNumber ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
insert into ServiceOrder(ServiceName,OrderNumber)
values (p_ServiceName,p_OrderNumber);
end if;
end;
$$;


create or replace procedure ServiceOrder_Update(p_ID_ServiceOrder int, p_ServiceName varchar(30), p_OrderNumber varchar(35), p_newID_ServiceOrder int )
language plpgsql
as $$
DECLARE have_record int := count (*) FROM ServiceOrder
where ServiceName = p_ServiceName and OrderNumber = p_OrderNumber ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
update ServiceOrder set
ServiceName = p_ServiceName,
OrderNumber = p_OrderNumber
where
ID_ServiceOrder = p_ID_ServiceOrder;
end if;
end;
$$;


create or replace procedure ServiceOrder_Delete(p_ID_ServiceOrder int)
language plpgsql
as $$
--DECLARE have_record int := count (*) FROM Таблица где используется данная таблица
--where ID_ServiceOrder = p_ID_ServiceOrder;
begin
--if have_record>0 then raise exception 'Данные нельзя удалить, так как они уже задействованы';
--else
delete from ServiceOrder
where
ID_ServiceOrder = p_ID_ServiceOrder;
--end if;
end;
$$;




create table Contract
(
ContractNumber varchar(30) not null constraint PK_ContractNumber primary key unique,
DateComclusion date not null,
OrderNumber varchar(35) not null,
foreign key (OrderNumber) references orderclient (OrderNumber) on update cascade on delete cascade,
ClientNumber varchar(10) not null,
foreign key (ClientNumber) references client (ClientNumber) on update cascade on delete cascade,
Email varchar(50) not null,
foreign key (Email) references Users (Email) on update cascade on delete cascade
);


create or replace procedure Contract_Insert(p_ContractNumber varchar(30), p_DateComclusion date, p_OrderNumber varchar(35), p_ClientNumber varchar(10), p_Email varchar(50))
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Contract
where ContractNumber = p_ContractNumber and DateComclusion = p_DateComclusion and OrderNumber = p_OrderNumber and ClientNumber = p_ClientNumber and Email = p_Email ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
insert into Contract(ContractNumber,DateComclusion,OrderNumber,ClientNumber,Email)
values (p_ContractNumber,p_DateComclusion,p_OrderNumber,p_ClientNumber,p_Email);
end if;
end;
$$;


create or replace procedure Contract_Update(p_ContractNumber varchar(30), p_DateComclusion date, p_OrderNumber varchar(35), p_ClientNumber varchar(10), p_Email varchar(50), p_newContractNumber varchar(30) )
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Contract
where ContractNumber = p_newContractNumber and DateComclusion = p_DateComclusion and OrderNumber = p_OrderNumber and ClientNumber = p_ClientNumber and Email = p_Email ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
update Contract set
DateComclusion = p_DateComclusion,
OrderNumber = p_OrderNumber,
ClientNumber = p_ClientNumber,
Email = p_Email,
ContractNumber = p_newContractNumber
where
ContractNumber = p_ContractNumber;
end if;
end;
$$;


create or replace procedure Contract_Delete(p_ContractNumber varchar(30))
language plpgsql
as $$
--DECLARE have_record int := count (*) FROM Таблица где используется данная таблица
--where ContractNumber = p_ContractNumber;
begin
--if have_record>0 then raise exception 'Данные нельзя удалить, так как они уже задействованы';
--else
delete from Contract
where
ContractNumber = p_ContractNumber;
--end if;
end;
$$;





create table Diagnostics
(
DiagnosticsNumber varchar(30) not null constraint PK_DiagnosticsNumber primary key unique,
DiagnosticsDate date not null,
DiagnosticsResults text not null,
DiagnosticsDescription text not null,
CarNumber varchar(6) not null,
foreign key (CarNumber) references car (CarNumber) on update cascade on delete cascade,
ContractNumber varchar(30) not null,
foreign key (ContractNumber) references Contract (ContractNumber) on update cascade on delete cascade,
Email varchar(50) not null,
foreign key (Email) references users (Email) on update cascade on delete cascade
);

call Diagnostics_Insert('jkdfjkfdjk', '2022.01.01', 'kjevjnrjr', 'knerjknvkj', '344343','398883312', 'diagram15@mail.ru')
select * from diagnostics where carnumber = '344343'
create or replace procedure Diagnostics_Insert(p_DiagnosticsNumber varchar(30), p_DiagnosticsDate date, p_DiagnosticsResults text, p_DiagnosticsDescription text, p_CarNumber varchar(6), p_ContractNumber varchar(30), p_Email varchar(50))
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Diagnostics
where DiagnosticsNumber = p_DiagnosticsNumber and DiagnosticsDate = p_DiagnosticsDate and DiagnosticsResults = p_DiagnosticsResults and DiagnosticsDescription = p_DiagnosticsDescription and CarNumber = p_CarNumber and Email = p_Email ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
insert into Diagnostics(DiagnosticsNumber,DiagnosticsDate,DiagnosticsResults,DiagnosticsDescription,CarNumber,ContractNumber,Email)
values (p_DiagnosticsNumber,p_DiagnosticsDate,p_DiagnosticsResults,p_DiagnosticsDescription,p_CarNumber,p_ContractNumber,p_Email);
end if;
end;
$$;


create or replace procedure Diagnostics_Update(p_DiagnosticsNumber varchar(30), p_DiagnosticsDate date, p_DiagnosticsResults text, p_DiagnosticsDescription text, p_CarNumber varchar(6), p_ContractNumber varchar(30),p_Email varchar(50), p_newDiagnosticsNumber varchar(30) )
language plpgsql
as $$
DECLARE have_record int := count (*) FROM Diagnostics
where DiagnosticsNumber = p_newDiagnosticsNumber and DiagnosticsDate = p_DiagnosticsDate and DiagnosticsResults = p_DiagnosticsResults and DiagnosticsDescription = p_DiagnosticsDescription and CarNumber = p_CarNumber and Email = p_Email ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
update Diagnostics set
DiagnosticsDate = p_DiagnosticsDate,
DiagnosticsResults = p_DiagnosticsResults,
DiagnosticsDescription = p_DiagnosticsDescription,
CarNumber = p_CarNumber,
ContractNumber = p_ContractNumber,
Email = p_Email,
DiagnosticsNumber = p_newDiagnosticsNumber
where
DiagnosticsNumber = p_DiagnosticsNumber;
end if;
end;
$$;


create or replace procedure Diagnostics_Delete(p_DiagnosticsNumber varchar(30))
language plpgsql
as $$
--DECLARE have_record int := count (*) FROM Таблица где используется данная таблица
--where DiagnosticsNumber = p_DiagnosticsNumber;
begin
--if have_record>0 then raise exception 'Данные нельзя удалить, так как они уже задействованы';
--else
delete from Diagnostics
where
DiagnosticsNumber = p_DiagnosticsNumber;
--end if;
end;
$$;







create table StatusCar
(
ID_StatusCar SERIAL PRIMARY KEY,
StatusCar varchar(30) not null,
StatusTime timestamp not null,
DiagnosticsNumber varchar(30) not null,
foreign key (DiagnosticsNumber) references Diagnostics (DiagnosticsNumber) on update cascade on delete cascade
);


create or replace procedure StatusCar_Insert(p_StatusCar varchar(30), p_StatusTime timestamp, p_DiagnosticsNumber varchar(30))
language plpgsql
as $$
DECLARE have_record int := count (*) FROM StatusCar
where StatusCar = p_StatusCar and StatusTime = p_StatusTime and DiagnosticsNumber = p_DiagnosticsNumber ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
insert into StatusCar(StatusCar,StatusTime,DiagnosticsNumber)
values (p_StatusCar,p_StatusTime,p_DiagnosticsNumber);
end if;
end;
$$;


create or replace procedure StatusCar_Update(p_ID_StatusCar int, p_StatusCar varchar(30), p_StatusTime timestamp, p_DiagnosticsNumber varchar(30) )
language plpgsql
as $$
DECLARE have_record int := count (*) FROM StatusCar
where StatusCar = p_StatusCar and StatusTime = p_StatusTime and DiagnosticsNumber = p_DiagnosticsNumber ;
begin
if have_record>0 then
raise exception 'Уже существует';
else
update StatusCar set
StatusCar = p_StatusCar,
StatusTime = p_StatusTime,
DiagnosticsNumber = p_DiagnosticsNumber
where
ID_StatusCar = p_ID_StatusCar;
end if;
end;
$$;


create or replace procedure StatusCar_Delete(p_ID_StatusCar int)
language plpgsql
as $$
--DECLARE have_record int := count (*) FROM Таблица где используется данная таблица
--where ID_StatusCar = p_ID_StatusCar;
begin
--if have_record>0 then raise exception 'Данные нельзя удалить, так как они уже задействованы';
--else
delete from StatusCar
where
ID_StatusCar = p_ID_StatusCar;
--end if;
end;
$$;

call statuscar_Update('Продиагностирована2','30.11.2022 12:27:57','lkjrj;rehj');

select (statustime || ': ' || StatusCar) as aaa from statuscar where diagnosticsnumber = 'доломуошщуощ'

select diagnosticsnumber from diagnostics where contractnumber = '801212994'
