using WavesLabs.Node.Transactions;
using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Tests.UT
{
    [TestClass]
    public class InvokeScriptTransactionTests
    {
        [TestMethod]
        public void InvokeScriptTransactionEqualsAndGetHashCodeTest()
        {
            var hs = new HashSet<InvokeScriptTransaction>();
            var tr1 = new InvokeScriptTransaction { Payment = new List<Amount> { Amount.As(10, Base58s.As("abc")) } };
            var tr2 = new InvokeScriptTransaction { Payment = new List<Amount> { Amount.As(10, Base58s.As("abc")) } };
            //Equals
            Assert.AreEqual(tr1, tr2);
            Assert.AreEqual(tr1, tr1);
            Assert.IsTrue(tr1 == tr2);
            Assert.IsFalse(tr1 != tr2);

            //GetHashCode equals
            hs.Add(tr1);
            hs.Add(tr2);
            Assert.AreEqual(1, hs.Count);


            //Not equals
            tr2.Payment.Add(Amount.As(5, Base58s.As("xyz")));
            Assert.AreNotEqual(tr1, tr2);
            Assert.IsFalse(tr1 == tr2);
            Assert.IsTrue(tr1 != tr2);
            InvokeScriptTransaction? nullTx = null;
            Assert.AreNotEqual(tr1, nullTx);

            var trx = new IssueTransaction();
            Assert.AreNotEqual(tr1, trx);
            Assert.IsFalse(tr1 == trx);
            Assert.IsTrue(tr1 != trx);

            //GetHashCode not equals
            hs.Add(tr1);
            hs.Add(tr2);
            Assert.AreEqual(2, hs.Count);
        }
    }
}