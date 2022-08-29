// See https://aka.ms/new-console-template for more 

using George.Services.Layer.EncryptionService;

var temp = AES_Cipher.EncryptText("Hello world");

Console.WriteLine("Hello, World! " + temp);
Console.WriteLine(AES_Cipher.DecryptText(temp));
