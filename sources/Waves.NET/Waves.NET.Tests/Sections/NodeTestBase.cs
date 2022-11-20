using System.Diagnostics;
using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;
using Waves.NET.Transactions.Info;
using Waves.NET.Transactions.Utils;

namespace Waves.NET.Tests.Sections
{
    public class NodeTestBase
    {
        private const int BlockInterval = 60;

        public const string FaucetSeedPhrase = "waves private node seed with waves tokens";

        public INodeClient Node { get; set; }
        public PrivateKey FaucetPrivateKey { get; set; }
        public Address FaucetAddress { get; set; }

        public NodeTestBase()
        {
            Node = NodeClient.Create(Profile.Private);
            FaucetPrivateKey = PrivateKey.FromSeed(FaucetSeedPhrase);
            FaucetAddress = Address.FromPublicKey(Node.ChainId, FaucetPrivateKey.PublicKey);
        }

        public (PrivateKey Pk, Address Addr) CreateAccount()
        {
            var account = PrivateKey.FromSeed(Crypto.GenerateRandomSeedPhrase());
            return (account, Address.FromPublicKey(Node.ChainId, account.PublicKey));
        }

        public (PrivateKey Pk, Address Addr) CreateAccountWithBalance(long wavesQuantity)
        {
            var account = PrivateKey.FromSeed(Crypto.GenerateRandomSeedPhrase());
            var accountAddress = Address.FromPublicKey(Node.ChainId, account.PublicKey);

            var tx = Node.Transactions.Broadcast(TransferTransactionBuilder.Params(accountAddress, wavesQuantity).GetSignedWith(FaucetPrivateKey));
            WaitForTransaction(tx.Id);

            return (account, accountAddress);
        }

        public void WaitForTransactions(ICollection<Base58s> transactionIds, uint waitForSeconds = BlockInterval)
        {
            const int PollingIntervalInMillis = 1000;
            uint waitForMillis = waitForSeconds * 1000;
            Exception? lastException = null;

            var sw = Stopwatch.StartNew();
            while (true)
            {
                try
                {
                    var statuses = Node.Transactions.GetTransactionsStatus(transactionIds);
                    if(statuses.All(x => x.Status.Equals("confirmed", StringComparison.OrdinalIgnoreCase))) return;
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    Thread.Sleep(PollingIntervalInMillis);
                }

                if (sw.ElapsedMilliseconds > waitForMillis)
                    throw new Exception($"WaitForTransaction: Operation timeout ({waitForSeconds} seconds). Last exception: {lastException}");
            }
        }

        public TransactionInfo WaitForTransaction(Base58s? transactionId, uint waitForSeconds = BlockInterval)
        {
            if (transactionId is null)
                throw new ArgumentNullException($"WaitForTransaction: transaction ID cannot be null");

            const int PollingIntervalInMillis = 100;
            uint waitForMillis = waitForSeconds * 1000;
            Exception? lastException;

            var sw = Stopwatch.StartNew();
            while (true)
            {
                try
                {
                    return Node.Transactions.GetTransactionInfo(transactionId);
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    Thread.Sleep(PollingIntervalInMillis);
                }

                if (sw.ElapsedMilliseconds > waitForMillis)
                    throw new Exception($"WaitForTransaction: Operation timeout ({waitForSeconds} seconds). Last exception: {lastException}");
            }
        }


        public int WaitForHeight(int target, uint waitForSeconds = BlockInterval)
        {
            int start = Node.Blocks.GetHeight();
            int prev = start;
            const int PollingIntervalInMillis = 100;
            uint waitForMillis = waitForSeconds * 1000;

            var sw = Stopwatch.StartNew();

            while (true)
            {
                try
                {
                    int current = Node.Blocks.GetHeight();

                    if (current >= target)
                    {
                        return current;
                    }
                    else if (current > prev)
                    {
                        prev = current;
                    }
                }
                catch
                {
                    Thread.Sleep(PollingIntervalInMillis);
                }

                if (sw.ElapsedMilliseconds > waitForMillis)
                    throw new Exception($"WaitForHeight: timeout reached ({waitForSeconds} seconds)");
            }
        }

        public int WaitBlocks(int blocksCount, uint waitingInSeconds = BlockInterval)
        {
            var height = Node.Blocks.GetHeight();
            return WaitForHeight(height + blocksCount, waitingInSeconds);
        }

        public int WaitBlocks(int blocksCount)
        {
            return WaitBlocks(blocksCount, BlockInterval * 3);
        }
    }
}