using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace BlockChainStart.BlockChains;

public class Block
    {
    public int Index { get; set; }
    public DateTime CreatedAt { get; set; }
    public string PreviousHash { get; set; }
    public string Hash { get; set; }
    public string Data { get; set; }

    #region constructor

    public Block ( DateTime createdAt, string previousHash, string data )
        {
        Index = 0;
        CreatedAt = createdAt;
        PreviousHash = previousHash;
        Hash = CalculateHash();
        Data = data;
        }

    #endregion

    public string CalculateHash ()
    {
        var sha256 = SHA256.Create();
        byte[] inputByte = Encoding.ASCII.GetBytes($"{CreatedAt}-{PreviousHash ?? ""}-{Data}");
        byte[] outputByte = sha256.ComputeHash(inputByte);
        var resultHash = Convert.ToBase64String(outputByte);
        return resultHash;
    }
    }