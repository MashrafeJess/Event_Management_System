using Business;
using Business.FakeForm;
using Database;
using Microsoft.AspNetCore.Identity;

namespace BusinessTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //RegistrationTest();
            LoginTest();
        }
        #region UserService
        static void RegistrationTest()
        {
            MockForm userForm = new MockForm();
            userForm.UserName = Console.ReadLine();
            userForm.Email = Console.ReadLine();
            userForm.Password = Console.ReadLine();
            Result result = new UserService().Registration(userForm);
            Console.WriteLine(result.Message);
        }
        static void LoginTest()
        {
            FakeLoginForm loginForm = new FakeLoginForm();
            Console.WriteLine("Email");
            loginForm.Email = Console.ReadLine();
            Console.WriteLine("Password");
            loginForm.Password = Console.ReadLine();
            Result result = new UserService().Login(loginForm);
            Console.WriteLine(result.Message);
        }
        #endregion
    }
}