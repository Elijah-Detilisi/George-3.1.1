using George.Control.Layer;

AccountController accountController = new AccountController();
var result = accountController.LoginToInbox("hello@hotmail.com", "hello");

Console.WriteLine(result);
