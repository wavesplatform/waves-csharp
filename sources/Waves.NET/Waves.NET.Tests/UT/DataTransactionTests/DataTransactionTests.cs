using Waves.NET.Transactions;

namespace Waves.NET.Tests.UT
{
    [TestClass]
    public class DataTransactionTests
    {
        [TestMethod]
        public void DataTransactionEqualsAndGetHashCodeTest()
        {
            var hs = new HashSet<DataTransaction>();
            var tr1 = new DataTransaction { Data = new List<EntryData> { DataEntry.AsBoolean("key", false) } };
            var tr2 = new DataTransaction { Data = new List<EntryData> { DataEntry.AsBoolean("key", false) } };
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
            tr2.Data.Add(DataEntry.AsInteger("k", 1));
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