using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Tests
{
    public class TransactionBinarySerializerTestsBase
    {
        protected TransactionBinarySerializerFactory Factory { get; } = new TransactionBinarySerializerFactory();
        protected PrivateKey PrivateKey { get; } = new PrivateKey("5rRiArQQqc4mR91hfcNhDFneh5e5CC9ntVp27MsoqCAG");
    }
}