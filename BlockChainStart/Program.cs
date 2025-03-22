internal class Program
    {
    public static int Port = 501;
    private static WebSocketServer? _server = null;
    private static WebSocketClientProject _client = new();
    public static Blockchain? AmirCoin = new();
    private static string _name = "Amir";

    private static void Main ( string[] args )
        {
        AmirCoin.InitializeChain();

        if (args.Length >= 1)
            Port = int.Parse(args[0]);
        if (args.Length >= 2)
            _name = args[1];

        if (Port > 0)
            {
            _server = new WebSocketServer();
            _server.Start();
            }
        if (_name != "Unknown")
            {
            Console.WriteLine($"Current user is {_name}");
            }

        Console.WriteLine("=========================");
        Console.WriteLine("1. Connect to a server");
        Console.WriteLine("2. Add a transaction");
        Console.WriteLine("3. Display Blockchain");
        Console.WriteLine("4. Exit");
        Console.WriteLine("=========================");

        var selection = 0;
        while (selection != 4)
            {
            switch (selection)
                {
                case 1:
                    Console.WriteLine("Please enter the server URL");
                    var serverUrl = Console.ReadLine();
                    _client.Connect($"{serverUrl}/Blockchain");
                    break;
                case 2:
                    Console.WriteLine("Please enter the receiver name");
                    var receiverName = Console.ReadLine();
                    Console.WriteLine("Please enter the amount");
                    var withdrawAmount = Console.ReadLine();
                    if (receiverName is not null)
                        AmirCoin.CreateTransaction(new Transaction(_name, receiverName, int.Parse(withdrawAmount)));
                    AmirCoin.ProcessPendingTransaction(_name);
                    _client.Broadcast(JsonConvert.SerializeObject(AmirCoin));
                    break;
                case 3:
                    Console.WriteLine("Blockchain");
                    Console.WriteLine(JsonConvert.SerializeObject(AmirCoin, Formatting.Indented));
                    break;

                }

            Console.WriteLine("Please select an action");
            var action = Console.ReadLine();
            if (action is not null) selection = int.Parse(action);
            }

        _client.Close();
        }
    }