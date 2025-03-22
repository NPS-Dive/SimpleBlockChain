using BlockChainStart.Transactions;

namespace BlockChainStart.BlockChains;

public class Blockchain
    {
    public IList<Block> Chain { get; set; }
    public int Difficulty { get; set; } = 3;
    public double Reward { get; set; } = 1;

    public IList<Transaction> PendingTransactionsList = new List<Transaction>();



    #region Constructor

    public Blockchain ()
        {

        InitializeChain();
        AddGenesisBlock();
        }


    #endregion

    public void CreateTransaction ( Transaction transaction )
        {
        PendingTransactionsList.Add(transaction);
        }

    public void ProcessPendingTransaction ( string minerAddress )
        {
        var block = new Block(DateTime.UtcNow, GetLatestBlock().Hash, PendingTransactionsList);
        AddNewBlock(block);
        PendingTransactionsList = new List<Transaction>();
        CreateTransaction(new Transaction(null, minerAddress, Reward));
        }

    public void InitializeChain ()
        {
        Chain = new List<Block>();
        }

    public void AddGenesisBlock ()
        {
        Chain.Add(CreateGenesisBlock());
        }

    public Block CreateGenesisBlock ()
        {
        var genesisBlock = new Block(DateTime.UtcNow, null, PendingTransactionsList);
        genesisBlock.Mine(Difficulty);
        PendingTransactionsList = new List<Transaction>();
        return genesisBlock;
        }

    public Block GetLatestBlock ()
        {
        var latestBlock = Chain[^1];

        return latestBlock;
        }

    public void AddNewBlock ( Block newBlock )
        {
        var latestBlock = GetLatestBlock();
        newBlock.Index = latestBlock.Index + 1;
        newBlock.PreviousHash = latestBlock.Hash;
        newBlock.Mine(this.Difficulty);
        Chain.Add(newBlock);
        }

    public bool IsValid ()
        {
        for (var i = 1; i < Chain.Count; i++)
            {
            var currentBlock = Chain[i];
            var previousBlock = Chain[i - 1];

            if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                return false;
                }

            if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                return false;
                }
            }
        return true;
        }

    public double GetBalance ( string Address )
        {
        double balance = 0;
        foreach (var block in Chain)
        {
            foreach (var transaction in block.TransactionsList)
            {
                if (transaction.SenderAddress == Address)
                {
                    balance -= transaction.WithdrawalAmount;
                }
                else if (transaction.ReceiverAddress == Address)
                {
                    balance += transaction.WithdrawalAmount;
                }
            }

        }
        return balance;
        }



    }