// See https://aka.ms/new-console-template for more information
using CW20.Business.Services;
using System.Text.Json;

Console.WriteLine("Hello, World!");

var test = new UserDapperService();
var ttt = await test.GetUserById(1);

Console.WriteLine(JsonSerializer.Serialize(ttt));



//push shode aya