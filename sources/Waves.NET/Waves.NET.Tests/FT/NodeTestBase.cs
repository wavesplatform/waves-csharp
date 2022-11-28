using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;
using Waves.NET.Transactions.Utils;

namespace Waves.NET.Tests.FT
{
    public class NodeTestBase
    {
        public const string FaucetSeedPhrase = "waves private node seed with waves tokens";

        public INode Node { get; set; }
        public PrivateKey FaucetPrivateKey { get; set; }
        public Address FaucetAddress { get; set; }

        public NodeTestBase()
        {
            Node = NET.Node.Create(Profile.Private);
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

            var tx = Node.Broadcast(TransferTransactionBuilder.Params(accountAddress, wavesQuantity).GetSignedWith(FaucetPrivateKey));
            Node.WaitForTransaction(tx.Id);

            return (account, accountAddress);
        }
    }
}