using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Tests.UT
{
    [TestClass]
    public class IssueTransactionTests
    {
        [TestMethod]
        public void IssueTransactionEqualsAndGetHashCodeTest()
        {
            var hs = new HashSet<IssueTransaction>();
            var tr1 = new IssueTransaction { Proofs = new List<Base58s> { Base58s.As("abcd") } };
            var tr2 = new IssueTransaction { Proofs = new List<Base58s> { Base58s.As("abcd") } };
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
            tr2.Proofs.Add(Base58s.As("xyz"));
            Assert.AreNotEqual(tr1, tr2);
            Assert.IsFalse(tr1 == tr2);
            Assert.IsTrue(tr1 != tr2);

            var trx = new BurnTransaction();
            Assert.AreNotEqual(tr1, trx);

            //GetHashCode not equals
            hs.Add(tr1);
            hs.Add(tr2);
            Assert.AreEqual(2, hs.Count);
        }
    }
}