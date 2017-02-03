using System;
using NUnit.Framework;
using HemtentaTdd2017.blog;

namespace Hemtenta_Test
{
    [TestFixture]
    public class Blog_Test
    {
        private IAuthenticator _authenticator;
        private IBlog blog;

        [Test]
        public void IfUserIsNullShouldThrowException()
        {
            blog = new Blog(_authenticator = new MockAuthenticator());

            //TODO Change to custom exception
            Assert.Throws<IncorrectUserException>(()=> blog.LoginUser(null), "Did not throw exception");
        }

        [TestCase("Malin", "malin")]
        public void UserIsFoundInDbSuccess(string username, string password)
        {
            blog = new Blog(_authenticator = new MockAuthenticator());

            blog.LoginUser(new User(username) { Password = password });
            var userIsLoggedIn = blog.UserIsLoggedIn;

            Assert.IsTrue(userIsLoggedIn, "The user should be logged in");
        }

        [TestCase(null, "malin")]
        [TestCase("", "malin")]
        public void IfUsernameIsNullOrEmptyShouldThrowException(string username, string password)
        {
            blog = new Blog(_authenticator = new MockAuthenticator());

            Assert.Throws<IncorrectUserException>(() => blog.LoginUser(new User(username) { Password = password }), "Did not throw exception");
        }

        [TestCase("Erik", "erik")]
        public void IfNoUserIsFoundInDbShouldThrowException(string username, string password)
        {
            blog = new Blog(_authenticator = new MockAuthenticator());

            Assert.Throws<NoUserFoundInDbException>(() => blog.LoginUser(new User(username) { Password = password }), "Did not throw exception");
        }

        [Test]
        public void IfNoUserIsLoggedInShouldBeFalse()
        {
            blog = new Blog(_authenticator = new MockAuthenticator());
            var userIsLoggedIn = blog.UserIsLoggedIn;

            Assert.IsFalse(userIsLoggedIn, "The user should not be logged in");
        }

        [TestCase("Malin", "malin")]
        public void IfNoUserIsLoggedInWhenLoggingOutShouldThrowException(string username, string password)
        {
            blog = new Blog(_authenticator = new MockAuthenticator());

            Assert.Throws<NoUserLoggedInException>(() => blog.LogoutUser(new User(username) { Password = password }));
        }

        [TestCase("Malin", "malin")]
        public void LogoutShouldSucceed(string username, string password)
        {
            blog = new Blog(_authenticator = new MockAuthenticator());

            blog.LoginUser(new User(username) { Password = password });
            blog.LogoutUser(new User(username) { Password = password });
            Assert.That(blog.UserIsLoggedIn == false);
        }

        [TestCase("Malin", "malin")]
        public void LogoutShouldFail(string username, string password)
        {
            blog = new Blog(_authenticator = new MockAuthenticator());

            blog.LoginUser(new User(username) { Password = password });
            blog.LogoutUser(new User(username) { Password = password });
            Assert.That(blog.UserIsLoggedIn == false);
        }

        [TestCase("Malin", "malin")]
        public void LogoutShouldThrowException(string username, string password)
        {
            blog = new Blog(_authenticator = new MockAuthenticator());

            blog.LoginUser(new User(username) { Password = password });

            Assert.Throws<IncorrectUserException>(() => blog.LogoutUser(null));
            Assert.Throws<IncorrectUserException>(() => blog.LogoutUser(new User(null) { Password = password }));
            Assert.Throws<IncorrectAuthenticationException>(() => blog.LogoutUser(new User(username) { Password = null }));
            Assert.Throws<IncorrectUserException>(() => blog.LogoutUser(new User("") { Password = "" }));

        }


        [TestCase("Malin", "malin", "Home Page", "Welcome to the home page")]
        public void PublishPageShouldSucceed(string username, string password, string title, string content)
        {
            blog = new Blog(_authenticator = new MockAuthenticator());

            blog.LoginUser(new User(username) { Password = password });

            var userIsLoggedIn = blog.UserIsLoggedIn;

            //Check to see that user is really logged in
            Assert.That(userIsLoggedIn == true, "User should be logged in");

            Assert.That(blog.PublishPage(new Page() { Title = title, Content = content }) == true, "The publish should have been succesful");
        }

        [TestCase("Malin", "malin", "Home Page", "Welcome to the home page")]
        public void PublishPageShouldFail(string username, string password, string title, string content)
        {
            blog = new Blog(_authenticator = new MockAuthenticator());

            blog.LoginUser(new User(username) { Password = password });

            var userIsLoggedIn = blog.UserIsLoggedIn;

            //Check to see that user is really logged in
            Assert.That(userIsLoggedIn == true, "User should be logged in");

            //Instead of page, send in null
            Assert.Throws<IncorrectPageException>(() => blog.PublishPage(null), "The publish should not have been succesful");

            blog.LogoutUser(new User(username) { Password = password });

            userIsLoggedIn = blog.UserIsLoggedIn;

            //Check to see that user is really logged out
            Assert.That(userIsLoggedIn == false, "User should be logged in");

            //Publish page when user is not logged in
            Assert.That(blog.PublishPage(new Page { Title = title, Content = content }) == false, "The user is not logged in, publish should fail");

        }

        [TestCase("Malin", "malin", "", "", "")]
        [TestCase("Malin", "malin", null, null, null)]
        [TestCase("Malin", "malin", "", "Hej på dig", null)]
        public void IfMailContentIsNullOrEmptySendEmailThrowException(string username, string password, string address, string caption, string body)
        {
            blog = new Blog(_authenticator = new MockAuthenticator());
            blog.LoginUser(new User(username) { Password = password });

            var userIsLoggedIn = blog.UserIsLoggedIn;

            //Check to see that user is really logged in
            Assert.That(userIsLoggedIn == true, "User should be logged in");

            Assert.Throws<IncorrectEmailException>(() => blog.SendEmail(address, caption, body));
        }

        [TestCase("m.classon@hotmail.com", "Hej", "Hej Malin", "Malin", "malin")]
        public void IfUserIsLoggedInSendEmailShouldReturnZero(string address, string caption, string body, string username, string password)
        {
            blog = new Blog(_authenticator = new MockAuthenticator());

            var userIsLoggedIn = blog.UserIsLoggedIn;

            //Check to see that no user is logged in
            Assert.That(userIsLoggedIn == false, "No user should be logged in");

            int result = blog.SendEmail(address, caption, body);

            // SendEmail returns 1 if email is sent and 0 on failure.
            Assert.That(result == 0, "The message should not have been sent, since no user is logged in");
        }



        [TestCase("m.classon@hotmail.com", "Hej", "Hej Malin", "Malin", "malin")]
        public void IfUserIsLoggedInSendEmailShouldReturnOne(string address, string caption, string body, string username, string password)
        {
            blog = new Blog(_authenticator = new MockAuthenticator());

            blog.LoginUser(new User(username) { Password = password });

            var userIsLoggedIn = blog.UserIsLoggedIn;

            //Check to see that user is really logged in
            Assert.That(userIsLoggedIn == true, "User should be logged in");

            var result = blog.SendEmail(address, caption, body);

            // SendEmail returns 1 if email is sent and 0 on failure.
            Assert.That(result == 1, "The message should have been sent, since user is logged in");
        }




    }
}
