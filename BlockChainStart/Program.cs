// See https://aka.ms/new-console-template for more information

using BlockChainStart.BlockChains;
using BlockChainStart.Transactions;
using Newtonsoft.Json;

var beginTime = DateTime.UtcNow.TimeOfDay;

var AmirCoin = new Blockchain();

AmirCoin.CreateTransaction(new Transaction("Amir","Mohammad",75.75));
AmirCoin.ProcessPendingTransaction("Taqi");
AmirCoin.CreateTransaction(new Transaction("Amir2", "Mohammad2", 70.70));
AmirCoin.CreateTransaction(new Transaction("Amir3", "Mohammad3", 80.80));
AmirCoin.CreateTransaction(new Transaction("Amir4", "Amir", 85.85));
AmirCoin.ProcessPendingTransaction("Kambiz");




var endTime = DateTime.UtcNow.TimeOfDay;

var elapsedTime= endTime - beginTime;

#region Before Manuipulation

var resultJson1 = JsonConvert.SerializeObject(AmirCoin, Formatting.Indented);

Console.WriteLine(resultJson1);

var hashValidation1 = $"Is Chain Valid:  -> {AmirCoin.IsValid()}";

Console.WriteLine(hashValidation1);
Console.WriteLine($"Duration:   {elapsedTime} ");
Console.WriteLine(AmirCoin.GetBalance("Amir"));
#endregion

       
Console.WriteLine();

Console.ReadKey();