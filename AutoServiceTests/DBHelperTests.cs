using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AutoService;
namespace AutoServiceTests
{
    [TestClass]
    public class DBHelperTests
    {
        string email = "";
        string password = "";
        string lastName = "";
        string firstName = "";
        string middleName = "";
        string post = "";
        DBHelper dBHelper = new DBHelper();

        [TestMethod]
        public void Authorization_AdminandAdmin_Administratorreturned()
        {
            //arange
            email = "Admin";
            password = "Admin";
            string expected = "Администратор";
            //act
            string actual = dBHelper.Authorization(email, password);
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UsersInserInto_EmailandPasswordandFIandPost_1_returned()
        {
            //arange
            email = "user@mail.ru";
            password = "User!123";
            lastName = "LastName";
            firstName = "FirstName";
            middleName = "MiddleName";
            post = "Администратор";
            string expected = "1";
            string[] atributes = { email, password, lastName, firstName, middleName, post };
            //act
            dBHelper.InsertInto("Users", atributes);
            string actual = dBHelper.EecuteQueryReaderOne($@"select count(Email) from Users where Email = '{email}'", "count");
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UsersUpdateInto_EmailandPasswordandFIandPost_1_returned()
        {
            //arange
            string newEmail = "newuser@mail.ru";
            email = "user@mail.ru";
            password = "User!123";
            lastName = "LastName2";
            firstName = "FirstName2";
            middleName = "MiddleName2";
            post = "Клиент";
            string expected = "1";
            string[] atributes = { email, password, lastName, firstName, middleName, post, newEmail };
            //act
            dBHelper.UpdateInto("Users", atributes);
            string actual = dBHelper.EecuteQueryReaderOne($@"select count(Email) from Users where Email = '{newEmail}' and Password = '{password}' and LastName = '{lastName}' and FirstName = '{firstName}' and MiddleName = '{middleName}' and RoleName = '{post}'", "count");
            //assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void UsersDeleteInto_Email_1_returned()
        {
            //arange
            email = "newuser@mail.ru";
            string expected = "0";
            //act
            dBHelper.DeleteInto("Users", email);
            string actual = dBHelper.EecuteQueryReaderOne($@"select count(Email) from Users where Email = '{email}'", "count");
            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
