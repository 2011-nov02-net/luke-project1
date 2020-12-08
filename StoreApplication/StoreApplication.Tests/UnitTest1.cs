using StoreApplication.WebApp.Models;
using System;
using Xunit;

namespace StoreApplication.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CreateCutomerTest()
        {
            var newCustomer = new Customer()
            {
                FirstName = "George",
                LastName = "Bush",
                Email = "gbush@email.com"
            };

            Assert.True(newCustomer.FirstName == "George" && newCustomer.LastName == "Bush" && newCustomer.Email == "gbush@email.com");
        }

        //[Fact]
        //public void CreateLocationTest()
        //{
        //    var newLocation = new Loc()
        //    {
        //        FirstName = "George",
        //        LastName = "Bush",
        //        Email = "gbush@email.com"
        //    };

        //    Assert.True(newCustomer.FirstName == "George" && newCustomer.LastName == "Bush" && newCustomer.Email == "gbush@email.com");
        //}
    }
}
