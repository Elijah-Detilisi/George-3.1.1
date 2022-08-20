// See https://aka.ms/new-console-template for more information

using George.Data.Layer.DataAccess;

DataAccess dataAccess = new DataAccess();

Console.WriteLine("Hello, World!");
dataAccess.RestoreDataBase();
Console.WriteLine("Bye, World!");

await dataAccess.SaveUserAccountAsync("sonto@gmail.com", "89ASD46ASD64SADSA46S4AD");
Console.WriteLine("Done writing");