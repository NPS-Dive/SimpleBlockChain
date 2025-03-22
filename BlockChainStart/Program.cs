// See https://aka.ms/new-console-template for more information

using BlockChainStart.BlockChains;
using Newtonsoft.Json;

var AmirCoin = new Blockchain();
AmirCoin.AddNewBlock(new Block(DateTime.UtcNow, null, "Sender:Amir1, Receiver: Mohammad1, Withdrawal: 100, Details: 1details of withdrawal1"));
AmirCoin.AddNewBlock(new Block(DateTime.UtcNow, null, "Sender:Amir2, Receiver: Mohammad2, Withdrawal: 200, Details: 2details of withdrawal2"));
AmirCoin.AddNewBlock(new Block(DateTime.UtcNow, null, "Sender:Amir3, Receiver: Mohammad3, Withdrawal: 300, Details: 3details of withdrawal3"));
AmirCoin.AddNewBlock(new Block(DateTime.UtcNow, null, "Sender:Amir4, Receiver: Mohammad4, Withdrawal: 400, Details: 4details of withdrawal4"));
AmirCoin.AddNewBlock(new Block(DateTime.UtcNow, null, "Sender:Amir5, Receiver: Mohammad5, Withdrawal: 500, Details: 5details of withdrawal5"));
AmirCoin.AddNewBlock(new Block(DateTime.UtcNow, null, "Sender:Amir6, Receiver: Mohammad6, Withdrawal: 600, Details: 6details of withdrawal6"));




#region Before Manuipulation

var resultJson1 = JsonConvert.SerializeObject(AmirCoin, Formatting.Indented);

Console.WriteLine(resultJson1);

var hashValidation1 = $"Is Chain Valid:  -> {AmirCoin.IsValid()}";

Console.WriteLine(hashValidation1);
#endregion



#region Manipulation

Console.WriteLine("--- manipulation is coming ---");

//manipulating the blockchain data
AmirCoin.Chain[4].Data = "Sender:Amir4, Receiver: Myself, Withdrawal: 75000, Details: 4details of withdrawal4";
#endregion


#region Check Manipulation

var resultJson2 = JsonConvert.SerializeObject(AmirCoin, Formatting.Indented);
Console.WriteLine(resultJson2);
var hashValidation2 = $"Is Chain Valid:  -> {AmirCoin.IsValid()}";
Console.WriteLine(hashValidation2);

#endregion



Console.WriteLine();

Console.ReadKey();