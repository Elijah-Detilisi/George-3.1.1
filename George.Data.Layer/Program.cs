// See https://aka.ms/new-console-template for more information

using George.Data.Layer.DataAccess;

DataAccess dataAccess = new DataAccess();

//await dataAccess.SaveUserAccountAsync("detilisi@gmail.com", "89ASD46ASD64SADSA46S4AD");
Console.WriteLine("Done writing");

var result = dataAccess.GetEmailSettingsAsync("sonto@gmail.com");
Console.WriteLine(result);