using System.Xml.Linq;
using Waves.NET.ReturnTypes;
using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;
using Waves.NET.Transactions.Utils;

namespace Waves.NET.Tests.Sections
{
    [TestClass]
    public class AssetsNodeTests : NodeTestBase
    {
        [TestMethod]
        public void DetailsTest() {
            var alice = CreateAccountWithBalance(1000000000);

            const string script = "{-# STDLIB_VERSION 5 #-}\n" +
                        "{-# CONTENT_TYPE EXPRESSION #-}\n" +
                        "{-# SCRIPT_TYPE ACCOUNT #-}\n" +
                        "sigVerify(tx.bodyBytes, tx.proofs[0], tx.senderPublicKey)";

            var compileScript = Node.CompileScript(script);

            var tx = Node.Broadcast(IssueTransactionBuilder.Params("Asset 1", 10000000, 2).SetScript(compileScript.Script).GetSignedWith(alice.Pk));
            var assetId1 = tx.AssetId;
            Node.WaitForTransaction(assetId1);

            var assetId2 = Node.Broadcast(IssueTransactionBuilder.Params("Asset 2", 10000000, 2).GetSignedWith(alice.Pk)).AssetId;
            Node.WaitForTransaction(assetId2);

            var height = Node.GetHeight();
            var expectedAsset1Details = new AssetDetails {
                AssetId = assetId1,
                Issuer = alice.Addr,
                Decimals = 2,
                IssuerPublicKey = alice.Pk.PublicKey,
                IssueHeight = height,
                Name = "Asset 1",
                IssueTimestamp = tx.Timestamp,
                Description = "",
                MinSponsoredAssetFee = null,
                OriginTransactionId = tx.Id!,
                Reissuable = true,
                ScriptDetails = new ScriptDetails { Script = compileScript.Script!.EncodedWithPrefix, ScriptComplexity = compileScript.Complexity },
                Scripted = true
            };

            var asset1Details = Node.GetAssetDetails(assetId1, true);
            Assert.IsNotNull(asset1Details);
            Assert.AreEqual(expectedAsset1Details, asset1Details);

            var assetsDetails = Node.GetAssetDetails(new[] { assetId1, assetId2 });
            Assert.IsNotNull(assetsDetails);
            Assert.AreEqual(2, assetsDetails.Count);
            Assert.IsNotNull(assetsDetails.FirstOrDefault(x => x.AssetId == assetId1!));
            Assert.IsNotNull(assetsDetails.FirstOrDefault(x => x.AssetId == assetId2!));
        }

        [TestMethod]
        public void BalanceTest() {
            var alice = CreateAccountWithBalance(1000000000);

            var assetId1 = Node.Broadcast(IssueTransactionBuilder.Params("Asset 1", 10000000, 2).GetSignedWith(alice.Pk)).AssetId;
            Node.WaitForTransaction(assetId1);

            var assetId2 = Node.Broadcast(IssueTransactionBuilder.Params("Asset 2", 10000000, 2).GetSignedWith(alice.Pk)).AssetId;
            Node.WaitForTransaction(assetId2);

            var assetBalance = Node.GetAssetsBalance(alice.Addr);
            var asset1Balance = Node.GetAssetsBalance(alice.Addr, assetId1);
            Assert.IsNotNull(assetBalance);
            Assert.IsNotNull(assetBalance.Balances);
            Assert.AreEqual(2, assetBalance.Balances.Count);

            var asset1Balance1 = assetBalance.Balances.FirstOrDefault(x => x.AssetId == assetId1!);
            Assert.IsNotNull(asset1Balance1);
            Assert.AreEqual(10000000, asset1Balance1.Balance);

            var asset2Balance1 = assetBalance.Balances.FirstOrDefault(x => x.AssetId == assetId2!);
            Assert.IsNotNull(asset2Balance1);
            Assert.AreEqual(10000000, asset2Balance1.Balance);
        }

        [TestMethod]
        public void DistributionTest()
        {
            var alice = CreateAccountWithBalance(1000000000);
            int recipientsNumber = 190;
            var assetId = Node.Broadcast(
                IssueTransactionBuilder.Params("Asset", Enumerable.Range(1, recipientsNumber).Sum(), 2).GetSignedWith(alice.Pk)).AssetId;

            Node.WaitForTransaction(assetId);

            var transfersToDistribute = Enumerable.Range(1, recipientsNumber).Select(x =>
                new Transfer
                {
                    Amount = x,
                    Recipient = Address.As(Crypto.CreateAddressFromPublicKey(Node.ChainId, PrivateKey.FromSeed(Crypto.GenerateRandomSeedPhrase()).PublicKey))
                }
            ).ToList();

            var transactions = new List<Transaction> {
                MassTransferTransactionBuilder.Params(transfersToDistribute.Take(recipientsNumber / 2).ToList()).SetAssetId(assetId).GetSignedWith(alice.Pk),
                MassTransferTransactionBuilder.Params(transfersToDistribute.Skip(recipientsNumber / 2).ToList()).SetAssetId(assetId).GetSignedWith(alice.Pk)
            };
            var transactionIds = transactions.Select(x => x.Id).ToList();

            foreach(var tx in transactions)
            {
                Node.Broadcast(tx);
            }

            Node.WaitForTransactions(transactionIds!);
            Node.WaitBlocks(1);

            var distributionPage1 = Node.GetAssetDistribution(assetId!, Node.GetHeight() - 1, 100);
            var distributionPage2 = Node.GetAssetDistribution(assetId!, Node.GetHeight() - 1, 100, distributionPage1.LastItem);

            Assert.IsTrue(distributionPage1.HasNext);
            Assert.IsFalse(distributionPage2.HasNext);
        }

        [TestMethod]
        public void CalcTransactionFeeTest()
        {
            var alice = CreateAccountWithBalance(1000000000);
            int recipientsNumber = 10;
            var assetId = Node.Broadcast(
                IssueTransactionBuilder.Params("Asset", Enumerable.Range(1, recipientsNumber).Sum(), 2).GetSignedWith(alice.Pk)).AssetId;

            Node.WaitForTransaction(assetId);

            var transfersToDistribute = Enumerable.Range(1, recipientsNumber).Select(x =>
                new Transfer
                {
                    Amount = x,
                    Recipient = Address.As(Crypto.CreateAddressFromPublicKey(Node.ChainId, PrivateKey.FromSeed(Crypto.GenerateRandomSeedPhrase()).PublicKey))
                }
            ).ToList();

            var tx = MassTransferTransactionBuilder.Params(transfersToDistribute.ToList()).SetAssetId(assetId).GetSignedWith(alice.Pk);
            var txFee = Node.CalculateTransactionFee(tx);
            Assert.AreEqual(tx.Fee, txFee.FeeAmount);
        }

        [TestMethod]
        public void GetNftTest()
        {
            var alice = CreateAccountWithBalance(1000000000);
            //TODO issue many NFTs

            try
            {
                var nfts = Node.GetNft(alice.Addr);
                nfts = Node.GetNft(alice.Addr, 1);
                nfts = Node.GetNft(alice.Addr, 1, "nft");
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}