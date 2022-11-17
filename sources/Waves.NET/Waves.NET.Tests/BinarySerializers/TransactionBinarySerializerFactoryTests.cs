using Waves.NET.Transactions.TransactionData;

namespace Waves.NET.Tests
{
    [TestClass]
    public class TransactionBinarySerializerFactoryTests : TransactionBinarySerializerTestsBase
    {
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TransactionBinarySerializerFactoryNotSupportedTransactionTypeTest()
        {
            Factory.Get(TransactionType.Unknown);
        }
    }
}