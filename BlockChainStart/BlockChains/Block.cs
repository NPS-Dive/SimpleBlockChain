using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using BlockChainStart.Transactions;
using Newtonsoft.Json;

namespace BlockChainStart.BlockChains;

public class Block
    {
    public int Index { get; set; }
    public DateTime CreatedAt { get; set; }
    public string PreviousHash { get; set; }
    public string Hash { get; set; }
    public IList<Transaction> TransactionsList { get; set; }
    public int Nonce { get; set; }

    #region constructor

    public Block ( DateTime createdAt, string previousHash, IList<Transaction> transactionsList )
        {
        Index = 0;
        CreatedAt = createdAt;
        PreviousHash = previousHash;
        Hash = CalculateHash();
        TransactionsList = transactionsList;
        }

    #endregion

    public string CalculateHash ()
        {
        var sha256 = SHA256.Create();
        var jsonTransactions = JsonConvert.SerializeObject(TransactionsList);
        byte[] inputByte = Encoding.ASCII.GetBytes($"{CreatedAt}-{PreviousHash ?? ""}-{jsonTransactions}-{Nonce}");
        byte[] outputByte = sha256.ComputeHash(inputByte);
        var resultHash = Convert.ToBase64String(outputByte);
        return resultHash;
        }

        public void Mine(int difficulty)
        {
            var leadingZeros = new string('0', difficulty);
            while (this.Hash is null || this.Hash[..difficulty] != leadingZeros)
            {
                this.Nonce++;
                this.Hash = this.CalculateHash();
            }
        }

    }