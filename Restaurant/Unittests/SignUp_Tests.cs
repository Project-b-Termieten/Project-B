using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace RestaurantAltaaf.Tests
{
    [TestClass]
    public class SignUpTests
    {
        private Signup usersignup;

        [TestInitialize]
        public void Setup()
        {
            usersignup = new Signup();
        }

        [TestMethod]
        public void SignUp_PasswordLengthTen_Valid()
        {
            // Arrange
            string name = "John Doe";
            string email = "john@example.com";
            string password = "abcdefghij"; // 10 characters password

            // Redirect Console input
            using (StringReader stringReader = new StringReader($"{name}\n{email}\n{password}\n"))
            {
                Console.SetIn(stringReader);

                // Act
                User newUser = usersignup.SignUp(false);

                // Assert
                Assert.IsNotNull(newUser);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Object reference not set to an instance of an object.")]
        public void SignUp_PasswordLengthThree_Invalid_Cancel()
        {
            // Arrange
            string name = "Jane Doe";
            string email = "jane@example.com";
            string password = "abc"; // 3 characters password

            // Redirect Console input
            using (StringReader stringReader = new StringReader($"{name}\n{email}\n{password}\nno\n"))
            {
                Console.SetIn(stringReader);

                // Act
                User newUser = usersignup.SignUp(false);
            }
        }
    }
}
