using System.Text.RegularExpressions;
using Waves.NET.Addresses;
using Waves.NET.Addresses;
using Waves.NET.Debug;
using Waves.NET.Exceptions;
using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Tests.Sections
{
    [TestClass]
    public class AddressNodeTests : NodeTestBase
    {
        [TestMethod]
        public void GetAddressesTest()
        {
            var result = Node.Addresses.GetAddresses();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 1);
        }

        [TestMethod]
        public void GetAddressesRangeTest()
        {
            var result = Node.Addresses.GetAddresses(0, 100);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void BalanceTest()
        {
            long initBalance = 1000000;
            long balanceAfterLeaseOutFee = initBalance - LeaseTransaction.MinFee;
            long leasedIn = 30000;
            long leasedOut = 60000;

            var alice = CreateAccountWithBalance(initBalance);
            var bob = CreateAccountWithBalance(initBalance);

            WaitForTransaction(Node.Transactions.Broadcast(
                    LeaseTransactionBuilder.Params(alice.Addr, leasedIn).GetSignedWith(FaucetPrivateKey)).Id);

            int height = WaitForTransaction(Node.Transactions.Broadcast(
                    LeaseTransactionBuilder.Params(bob.Addr, leasedOut).GetSignedWith(alice.Pk)).Id).Height;

            Assert.AreEqual(balanceAfterLeaseOutFee, Node.Addresses.GetBalance(alice.Addr));

            var aliceBalanceDetails = Node.Addresses.GetBalanceDetails(alice.Addr);
            Assert.AreEqual(alice.Addr, aliceBalanceDetails.Address);
            Assert.AreEqual(balanceAfterLeaseOutFee - leasedOut, aliceBalanceDetails.Available);
            Assert.AreEqual(balanceAfterLeaseOutFee, aliceBalanceDetails.Regular);
            Assert.AreEqual(0, aliceBalanceDetails.Generating);
            Assert.AreEqual(balanceAfterLeaseOutFee - leasedOut + leasedIn, aliceBalanceDetails.Effective);

            Assert.IsTrue(Node.Addresses.GetBalanceDetails(FaucetAddress).Generating > 0);
            Assert.AreEqual(balanceAfterLeaseOutFee - leasedOut + leasedIn, Node.Addresses.GetEffectiveBalance(alice.Addr));

            var balances = Node.Addresses.GetBalances(new List<Address> { alice.Addr, bob.Addr });
            CollectionAssert.IsSubsetOf(balances.ToList(), new List<AddressBalance> {
                new AddressBalance { Address = alice.Addr, Balance = balanceAfterLeaseOutFee },
                new AddressBalance { Address = bob.Addr, Balance = initBalance },
            });

            balances = Node.Addresses.GetBalances(new List<Address> { alice.Addr, bob.Addr }, height);
            CollectionAssert.IsSubsetOf(balances.ToList(), new List<AddressBalance> {
                new AddressBalance { Address = alice.Addr, Balance = balanceAfterLeaseOutFee },
                new AddressBalance { Address = bob.Addr, Balance = initBalance },
            });

            WaitForHeight(height + 1);

            Assert.AreEqual(balanceAfterLeaseOutFee, Node.Addresses.GetBalance(alice.Addr, 1));
            Assert.AreEqual(balanceAfterLeaseOutFee - leasedOut + leasedIn, Node.Addresses.GetEffectiveBalance(alice.Addr, 1));
        }

        [TestMethod]
        public void WavesBalanceHistoryTest()
        {
            long initBalance = 1000000;
            long transferAmount = 50000;

            var alice = CreateAccount();
            int initHeight = WaitForTransaction(Node.Transactions.Broadcast(
                TransferTransactionBuilder.Params(alice.Addr, initBalance).GetSignedWith(FaucetPrivateKey)).Id).Height;
            WaitForHeight(initHeight + 1);

            int transferHeight = WaitForTransaction(Node.Transactions.Broadcast(
                TransferTransactionBuilder.Params(alice.Addr, transferAmount).GetSignedWith(FaucetPrivateKey)).Id).Height;
            WaitForHeight(transferHeight + 1);

            var balanceHistory = Node.Debug.GetBalanceHistory(alice.Addr);
            var expected = new List<HistoryBalance>
            {
                new HistoryBalance { Height = initHeight, Balance = initBalance },
                new HistoryBalance { Height = transferHeight, Balance = initBalance + transferAmount}
            };

            CollectionAssert.IsSubsetOf(balanceHistory.ToList(), expected);
        }

        [TestMethod]
        public void GetDataTest()
        {
            var alice = CreateAccountWithBalance(1000000000);
            var binaryValue = new Base64s(new byte[32767]);
            var stringWithMaxLength = new string('Ø', 32766 / 2) + "!";
            var expectedEntries = new EntryData[] {
                new BinaryEntry { Key = "bin-empty", Value = Base64s.As(new byte[0]) },
                new BinaryEntry { Key = "bin-max", Value = binaryValue },
                new BooleanEntry { Key = "bool-false", Value = false },
                new BooleanEntry { Key = "bool-true", Value = true },
                new IntegerEntry { Key = "int-min", Value = long.MinValue},
                new IntegerEntry { Key = "int-zero", Value = 0 },
                new IntegerEntry { Key = "int-max", Value = long.MaxValue },
                new StringEntry { Key = "str-empty", Value = "" },
                new StringEntry { Key = "str-max", Value = stringWithMaxLength }
            };

            WaitForTransaction(Node.Transactions.Broadcast(
                    DataTransactionBuilder.Params(expectedEntries).GetSignedWith(alice.Pk)).Id);

            CollectionAssert.IsSubsetOf(Node.Addresses.GetData(alice.Addr).ToList(), expectedEntries);
            Assert.AreEqual(new BinaryEntry { Key = "bin-max", Value = binaryValue }, Node.Addresses.GetData(alice.Addr, "bin-max"));

            CollectionAssert.IsSubsetOf(Node.Addresses.GetData(alice.Addr, new[] { "bin-max", "int-max" }).ToList(), new List<EntryData> {
                new BinaryEntry { Key = "bin-max", Value = binaryValue },
                new IntegerEntry { Key = "int-max", Value = long.MaxValue }
            });

            ;
            CollectionAssert.IsSubsetOf(Node.Addresses.GetData(alice.Addr, new Regex("int.+")).ToList(), new List<EntryData> {
                new IntegerEntry { Key = "int-min", Value = long.MinValue },
                new IntegerEntry { Key = "int-max", Value = long.MaxValue },
                new IntegerEntry { Key = "int-zero", Value = 0 }
            });
        }

        [TestMethod]
        public void ExpressionCompileAndScriptInfoTest()
        {
            const string script = "{-# STDLIB_VERSION 5 #-}\n" +
                        "{-# CONTENT_TYPE EXPRESSION #-}\n" +
                        "{-# SCRIPT_TYPE ACCOUNT #-}\n" +
                        "sigVerify(tx.bodyBytes, tx.proofs[0], tx.senderPublicKey)";

            var scriptInfo = Node.Utils.CompileScript(script);

            var expectedScript =
                new Base64s("BQkAAfQAAAADCAUAAAACdHgAAAAJYm9keUJ5dGVzCQABkQAAAAIIBQAAAAJ0eAAAAAZwcm9vZnMAAAAAAAAAAAAIBQAAAAJ0eAAAAA9zZW5kZXJQdWJsaWNLZXlzTh3b");

            var alice = CreateAccountWithBalance(SetScriptTransaction.MinFee);

            WaitForTransaction(Node.Transactions.Broadcast(SetScriptTransactionBuilder.Params(scriptInfo.Script!).GetSignedWith(alice.Pk)).Id);

            var si = Node.Addresses.GetScriptInfo(alice.Addr);
            Assert.AreEqual(scriptInfo, si);
            Assert.AreEqual(new ScriptInfo
            {
                Script = expectedScript,
                Complexity = 202,
                VerifierComplexity = 202,
                ExtraFee = 400000
            }, scriptInfo);

            try
            {
                Node.Addresses.GetScriptMeta(alice.Addr);
                Assert.Fail();
            }
            catch (NodeException ex)
            {
                Assert.IsTrue(ex.Message.Contains("ScriptParseError(Expected DApp)", StringComparison.OrdinalIgnoreCase));
            } //TODO waiting fix in Node. The scenario should work
        }

        [TestMethod]
        public void DAppCompileAndScriptInfoTest()
        {
            var compileScriptInfo = Node.Utils.CompileScript(
                "{-# STDLIB_VERSION 5 #-}\n" +
                "{-# CONTENT_TYPE DAPP #-}\n" +
                "{-# SCRIPT_TYPE ACCOUNT #-}\n" +
                "\n" +
                "@Callable(inv)\n" +
                "func foo() = nil\n" +
                "\n" +
                "@Callable(inv)\n" +
                "func bar(bin: ByteVector, bool: Boolean, int: Int, str: String, list: List[Int]) = nil");
            var expectedScript =
                new Base64s("AAIFAAAAAAAAAA0IAhIAEgcKBQIEAQgRAAAAAAAAAAIAAAADaW52AQAAAANmb28AAAAABQAAAANuaWwAAAADaW52AQAAAANiYXIAAAAFAAAAA2JpbgAAAARib29sAAAAA2ludAAAAANzdHIAAAAEbGlzdAUAAAADbmlsAAAAAHZW33I=");

            var expectedComplexities = new Dictionary<string, int> { { "foo", 1 }, {"bar", 1} };

            var expectedFunctions = new Dictionary<string, ICollection<NameTypePair>>
            {
                { "foo", new List<NameTypePair>() },
                { "bar", new List<NameTypePair> {
                    new NameTypePair { Name = "bin", Type = "ByteVector" },
                    new NameTypePair { Name = "bool", Type = "Boolean" },
                    new NameTypePair { Name = "int", Type = "Int" },
                    new NameTypePair { Name = "str", Type = "String" },
                    new NameTypePair { Name = "list", Type = "List[Int]" },
                } }
            };

            var alice = CreateAccountWithBalance(SetScriptTransaction.MinFee);

            WaitForTransaction(Node.Transactions.Broadcast(
                    SetScriptTransactionBuilder.Params(compileScriptInfo.Script!).GetSignedWith(alice.Pk)).Id);

            var actualScriptInfo = Node.Addresses.GetScriptInfo(alice.Addr);

            Assert.AreEqual(actualScriptInfo, new ScriptInfo {
                Script = expectedScript,
                Complexity = 1,
                VerifierComplexity = 0,
                CallableComplexities = expectedComplexities,
                ExtraFee = 0
            });

            Assert.AreEqual(Node.Addresses.GetScriptMeta(alice.Addr), new ScriptMeta { Version = 2, CallableFuncTypes = expectedFunctions });
            Assert.AreEqual(Node.Addresses.GetScriptInfo(alice.Addr), compileScriptInfo);
        }

        [TestMethod]
        public void CompileScriptCompactionTest()
        {
            var scriptText = "{-# STDLIB_VERSION 5 #-}\n" +
                "{-# SCRIPT_TYPE ACCOUNT #-}\n" +
                "{-# CONTENT_TYPE DAPP #-}\n" +
                "\n" +
                "func veryLongName0() = true\n" +
                "func veryLongName1() = if (veryLongName0()) then veryLongName0() else veryLongName0()\n" +
                "func veryLongName2() = if (veryLongName1()) then veryLongName0() else veryLongName0()\n" +
                "func veryLongName3() = if (veryLongName2()) then veryLongName0() else veryLongName0()\n" +
                "func veryLongName4() = if (veryLongName3()) then veryLongName0() else veryLongName0()\n" +
                "func veryLongName5() = if (veryLongName4()) then veryLongName0() else veryLongName0()\n" +
                "func veryLongName6() = if (veryLongName5()) then veryLongName0() else veryLongName0()";

            var fullScriptInfo = Node.Utils.CompileScript(scriptText, false);
            var compactScriptInfo = Node.Utils.CompileScript(scriptText, true);

            Assert.IsTrue(fullScriptInfo.Script!.Bytes.Length > compactScriptInfo.Script!.Bytes.Length);
        }
    }
}