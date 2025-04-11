using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Tests.UT
{
    [TestClass]
    public class MassTransferTransactionTests
    {
        [TestMethod]
        public void MassTransferTransactionEqualsAndGetHashCodeTest()
        {
            var hs = new HashSet<MassTransferTransaction>();
            var tr1 = new MassTransferTransaction { Transfers = new List<Transfer> { new Transfer { Amount = 100 } } };
            var tr2 = new MassTransferTransaction { Transfers = new List<Transfer> { new Transfer { Amount = 100 } } };
            //Equals
            Assert.AreEqual(tr1, tr2);
            Assert.AreEqual(tr1, tr1);
            Assert.AreNotEqual(tr1, null);
            Assert.IsTrue(tr1 == tr2);
            Assert.IsFalse(tr1 != tr2);

            //GetHashCode equals
            hs.Add(tr1);
            hs.Add(tr2);
            Assert.AreEqual(1, hs.Count);


            //Not equals
            tr2.Transfers.Add(new Transfer { Amount = 50 });
            Assert.AreNotEqual(tr1, tr2);
            Assert.IsFalse(tr1 == tr2);
            Assert.IsTrue(tr1 != tr2);

            var trx = new IssueTransaction();
            Assert.AreNotEqual(tr1, trx);

            //GetHashCode not equals
            hs.Add(tr1);
            hs.Add(tr2);
            Assert.AreEqual(2, hs.Count);
        }
    }
}