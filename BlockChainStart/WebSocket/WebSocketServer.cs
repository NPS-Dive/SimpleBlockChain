using BlockChainStart.BlockChains;
using BlockChainStart.Transactions;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace BlockChainStart.WebSocket
    {
    public class WebSocketServerProject : WebSocketBehavior
        {
        private bool _chainSynched = false;
        private WebSocketSharp.Server.WebSocketServer _webSocketSocketServer = null;

        public void Start ()
            {
            _webSocketSocketServer = new WebSocketSharp.Server.WebSocketServer($"ws://127.0.0.1:{Program.Port}");
            _webSocketSocketServer.AddWebSocketService<WebSocketServerProject>("/Blockchain");
            _webSocketSocketServer.Start();
            Console.WriteLine($"Started server at ws://127.0.0.1:{Program.Port}");
            }

        protected override void OnMessage ( MessageEventArgs e )
            {
            if (e.Data == "Hi Server")
            {
                Console.WriteLine(e.Data);
                Send($"Hi Client");
            }
            else
            {
                var newChain = JsonConvert.DeserializeObject<Blockchain>(e.Data);

                if (newChain is not null
                    && newChain.IsValid()
                    && Program.AmirCoin is not null
                    && newChain.Chain.Count > Program.AmirCoin.Chain.Count)
                {
                    var newTransactions = new List<Transaction>();
                    newTransactions.AddRange(newChain.PendingTransactionsList);
                    newTransactions.AddRange(Program.AmirCoin.PendingTransactionsList);

                    newChain.PendingTransactionsList = newTransactions;
                    Program.AmirCoin = newChain;
                }

                if (_chainSynched) return;

                Send(JsonConvert.SerializeObject(Program.AmirCoin));
                _chainSynched = true;
            }
            }
        }
    }
