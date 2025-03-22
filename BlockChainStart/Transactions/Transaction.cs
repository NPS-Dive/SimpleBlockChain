namespace BlockChainStart.Transactions;

public class Transaction
{
    public string SenderAddress { get; set; }
    public string ReceiverAddress { get; set; }
    public double WithdrawalAmount { get; set; }

    public Transaction(
        string senderAddress, 
        string receiverAddress, 
        double withdrawalAmount)
    {
        SenderAddress = senderAddress;
        ReceiverAddress = receiverAddress;
        WithdrawalAmount = withdrawalAmount;
    }
}