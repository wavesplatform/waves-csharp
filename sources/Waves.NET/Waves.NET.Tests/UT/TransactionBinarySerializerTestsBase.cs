using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Tests
{
    public class TransactionBinarySerializerTestsBase
    {
        protected TransactionBinarySerializerFactory Factory { get; }
        protected PrivateKey PrivateKey { get; }
        protected PublicKey PublicKey { get; }

        public TransactionBinarySerializerTestsBase()
        {
            Factory = new TransactionBinarySerializerFactory();
            PrivateKey = new PrivateKey("5rRiArQQqc4mR91hfcNhDFneh5e5CC9ntVp27MsoqCAG");
            PublicKey = PrivateKey.PublicKey;
        }
    }
}