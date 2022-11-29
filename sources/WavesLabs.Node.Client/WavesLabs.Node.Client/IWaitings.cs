using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Client
{
    public interface IWaitings
    {
        void WaitForTransactions(ICollection<Base58s> transactionIds, uint waitForSeconds = 60);
        TransactionInfo WaitForTransaction(Base58s? transactionId, uint waitForSeconds = 60);
        T WaitForTransaction<T>(Base58s? transactionId, uint waitForSeconds = 60) where T : TransactionInfo;
        int WaitForHeight(int target, uint waitForSeconds = 60);
        int WaitBlocks(int blocksCount, uint waitingInSeconds = 60);
        int WaitBlocks(int blocksCount);
    }
}
