using BlockChainStart.BlockChains;
using BlockChainStart.Transactions;
using Newtonsoft.Json;

namespace BlockChainStart.WebSocket
    {
    public class WebSocketClientProject
        {
        private IDictionary<string, WebSocketSharp.WebSocket> WebSocketDictionary { get; set; } = new Dictionary<string, WebSocketSharp.WebSocket>();

        public void Connect ( string url )
            {
            if (WebSocketDictionary.ContainsKey(url)) return;

            var ws = new WebSocketSharp.WebSocket(url);
            ws.OnMessage += ( sender, e ) =>
            {
                if (e.Data == "Guten Tag Client")
                {
                    Console.WriteLine(e.Data);
                }
                else
                {
                    var newChain = JsonConvert.DeserializeObject<Blockchain>(e.Data);

                    if (newChain is null
                        || !newChain.IsValid()
                        || Program.AmirCoin is null
                        || newChain.Chain.Count <= Program.AmirCoin.Chain.Count) return;

                    var newTransactions = new List<Transaction>();
                    newTransactions.AddRange(newChain.PendingTransactionsList);
                    newTransactions.AddRange(Program.AmirCoin.PendingTransactionsList);

                    newChain.PendingTransactionsList = newTransactions;
                    Program.AmirCoin = newChain;
                }
            };
            ws.Connect();
            ws.Send($"Guten Tag Server");
            ws.Send(JsonConvert.SerializeObject(Program.AmirCoin));
            WebSocketDictionary.Add(url, ws);
            }

        public void Send ( string url, string data )
            {
            foreach (var item in WebSocketDictionary)
            {
                if (item.Key == url)
                {
                    item.Value.Send(data);
                }
            }
            }

            public void Broadcast ( string data )
            {
                foreach (var item in WebSocketDictionary)
                {
                    item.Value.Send(data);
                }
            }

            public IList<string> GetServers ()
            {
                var resultList = WebSocketDictionary.Select(item => item.Key).ToList();
                return resultList;
            }

            public void Close ()
            {
                foreach (var item in WebSocketDictionary)
                {
                    item.Value.Close();
                }
            }
        }
    }
