namespace BlockChainStart.BlockChains;

public class Blockchain
    {
    public IList<Block> Chain { get; set; }


    #region Constructor

    public Blockchain ()
        {

        InitializeChain();
        AddGenesisBlock();
        }


    #endregion

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
        var genesisBlock = new Block(DateTime.UtcNow, null, "{}");

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
        newBlock.Hash = newBlock.CalculateHash();
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
    }