using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class ProductTests
    {
        //These tests show the 3 ways we can initialize objects in C#
        [TestMethod()]
        //Initializing objects by setting properties is best when
        //    - When populating from database values
        //    - When modifying properties
        public void SayHelloTest()
        {
            //Arrange
            var currentProduct = new Product();
            currentProduct.ProductName = "Saw";
            currentProduct.ProductId = 1;
            currentProduct.Description = "15-inch steel blade hand saw";
            currentProduct.ProductVendor.CompanyName = "ABC Corp";
            var expected = "Hello Saw (1): 15-inch steel blade hand saw Available on: ";

            //Act
            var actual = currentProduct.SayHello();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        //Initializing objects by using a parameterized constructor is best when
        //    - When setting the basic set of properties
        public void SayHello_ParameterizedConstructor()
        {
            //Arrange
            var currentProduct = new Product(1, "Saw",
                                 "15-inch steel blade hand saw");

            var expected = "Hello Saw (1): 15-inch steel blade hand saw Available on: ";

            //Act
            var actual = currentProduct.SayHello();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        //Initializing objects by using an object initializer is best when
        //    - When readability is important
        //    - When initializing a subset or superset of properties
        public void SayHello_ObjectInitializer()
        {
            //Arrange
            var currentProduct = new Product
            {
                ProductId = 1,
                ProductName = "Saw",
                Description = "15-inch steel blade hand saw"
            };

            var expected = "Hello Saw (1): 15-inch steel blade hand saw Available on: ";

            //Act
            var actual = currentProduct.SayHello();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Product_Null()
        {
            //Arrange
            Product currentProduct = null;
            var companyName = currentProduct?.ProductVendor?.CompanyName; //C# 6 null checking syntax

            string expected = null;

            //Act
            var actual = companyName;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConvertMetersToInchesTest()
        {
            // Arrange
            var expected = 78.74;

            // Act
            var actual = 2 * Product.InchesPerMeter;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}