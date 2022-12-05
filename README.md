# WavesLabs.Node.Client
A .NET library for interacting with the Waves blockchain.

Supports node interaction, offline transaction signing and creating addresses and keys.

## Using in your project
Use the codes below to add *WavesLabs.Node.Client* as a dependency for your project.

### Requirements:
- [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet) or above

### Create a project:
Using *Visual Studio* or *Visual Studio Code* create a new console app.

### Install NuGet package:
#### Package manager
```
PM> NuGet\Install-Package WavesLabs.Node.Client
```
#### .NET CLI
```
> dotnet add package WavesLabs.Node.Client
```

More options [here](https://www.nuget.org/packages/WavesLabs.Node.Client).

## Usage example
We will initiate a create [alias transaction](https://dev.waves.tech/en/edu/lessons/learn-sdks-the-waves-signer-and-its-providers/work-with-accounts#create-alias-transaction):

1) Open the *Program.cs* file created by default and replace its content with the code below:
```csharp
using WavesLabs.Node.Transactions.Utils;

// Generate a random seed phrase
var seed = Crypto.GenerateRandomSeedPhrase();

// Print the generated seed
Console.WriteLine(seed);
```

2) Run the app.

In console window you will see the output of the generated seed phrase. There will be a similar set of words printed:

```
chunk jump trash fringe success avoid undo fatal clown learn attack month eyebrow sock repair
```
Save the generated seed phrase.

3) Replace the *Program.cs* file content with the code below.
Assign the generated seed phrase to the senderPrivateKey variable's method PrivateKey.FromSeed(). The code will create a new account from the generated seed phrase:
```csharp
using WavesLabs.Node.Client;
using WavesLabs.Node.Transactions.Common;
using WavesLabs.Node.Transactions;

var node = new Node(Profile.TestNet);

// Create a private key from the seed
var senderPrivateKey = PrivateKey.FromSeed("insert the generated seed phrase here");
/* It could look like this: 
var senderPrivateKey = PrivateKey.FromSeed("chunk jump trash fringe success avoid undo fatal clown learn attack month eyebrow sock repair");*/

// Create the public key from the private key
var senderPublicKey = senderPrivateKey.PublicKey;

// Get the account address from the public key in the same network as with the node instance
var senderAddress = Address.FromPublicKey(ChainIds.TestNet, senderPublicKey);

// Print the generated address
Console.WriteLine(senderAddress);
```

4) Run the app.

You will be able to see an account address in the output like this:
```
3N6Dbnr36oxZUcXXX7ifYbA6CSJf1ndg18s
```
Copy the generated account address from the terminal.

5) Top up the account balance.
Performing a transaction on the Waves blockchain incurs a fee. It will be vital to top up the balance to operate with transactions. Depending on the chosen node instance network (MainNet, TestNet, or StageNet), there are different ways of making it. Using the account address from the previous step:

* For **Mainnet**: Transfer the WAVES tokens to the address.
* For **Testnet**: Use [Faucet](https://testnet.wavesexplorer.com/faucet) to top up your balance for free.
* For **Stagenet**: Use [Faucet](https://testnet.wavesexplorer.com/faucet) to top up your balance for free.

6) Replace the *Program.cs* file content with the code below.
```csharp
using WavesLabs.Node.Client;
using WavesLabs.Node.Transactions.Common;
using WavesLabs.Node.Transactions;
using WavesLabs.Node.Client.Transactions;

//node instance creation in the given network
var node = new Node(Profile.TestNet);

// Create a private key from the seed
var senderPrivateKey = PrivateKey.FromSeed("insert the generated phrase here");
/* It could look like this: 
var senderPrivateKey = PrivateKey.FromSeed(
    "chunk jump trash fringe success avoid undo fatal clown learn attack month eyebrow sock repair"
);*/

// Create the public key from the private key
var senderPublicKey = senderPrivateKey.PublicKey;

// Get the account address from the public key in the same network as with the node instance
var senderAddress = Address.FromPublicKey(ChainIds.TestNet, senderPublicKey);

// Create an alias using the current system time
var alias = Alias.As("alias" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

// Create an alias transaction
var createAliasTx = CreateAliasTransactionBuilder
    .Params(alias) // alias
    .GetSignedWith(senderPrivateKey); // sign transaction with your private key

// Broadcast the transaction to a node and wait for it to be included in the blockchain
node.WaitForTransaction(node.Broadcast(createAliasTx).Id);

// Get information about the transaction from the node
var createAliasTxInfo = node.GetTransactionInfo<CreateAliasTransactionInfo>(createAliasTx.Id);

// Print the alias full representation
Console.WriteLine(createAliasTxInfo.Transaction.Alias.ToFullString());
```

7) Run the app.

If everything was done correctly, you will see your new account alias similar to this:
```
alias:T:alias1665584780791
```
It will be an indication that the library is installed correctly and you can continue to use it this way.

## Writing a smart asset script
1. Prepare the Ride script you would like to attach to an asset:
```
{-# STDLIB_VERSION 6 #-}
{-# CONTENT_TYPE EXPRESSION #-}
{-# SCRIPT_TYPE ASSET #-}

func trueReturner () = {
    true
}
trueReturner()
```

2. Follow these steps:
* Create an asset;
* Attach a smart account script to it;
* Send the transaction to the node.
Here is how to make this:
```csharp
using WavesLabs.Node.Client;
using WavesLabs.Node.Transactions.Common;
using WavesLabs.Node.Transactions;
using WavesLabs.Node.Transactions.Utils;

// Node instance creation in the given network (TESTNET, MAINNET, STAGENET)
var node = new Node(Profile.TestNet);

// Create a private key from a seed
var senderPrivateKey = PrivateKey.FromSeed("your seed phrase");

// Create an asset script
var txScript =
    "{-# STDLIB_VERSION 6 #-}\n" +
    "{-# CONTENT_TYPE EXPRESSION #-}\n" +
    "{-# SCRIPT_TYPE ASSET #-}\n" +
    "func trueReturner() = {\n" +
        "true\n" +
    "}\n" +
    "trueReturner()";

// Transform the Ride script to a base64 string
var script = node.CompileScript(txScript).Script;

// Create an issue transaction
var tx = IssueTransactionBuilder.Params(
      "sampleasset",                    // asset name
      1000,                             // asset quantity
      2                                 // decimal places number
    )
    .SetScript(script)                  // set our compiled script
    .SetReissuable(true)                // mark asset as reissuable
    .SetDescription("description")      // set description string
    .GetSignedWith(senderPrivateKey);   // sign transaction

// Broadcast the transaction to a node and wait for it to be included in the blockchain
node.WaitForTransaction(node.Broadcast(tx).Id);
```
## Setting a smart account script
```csharp
using WavesLabs.Node.Client;
using WavesLabs.Node.Transactions.Common;
using WavesLabs.Node.Transactions;
using WavesLabs.Node.Transactions.Utils;

// Node instance creation in the given network (TESTNET, MAINNET, STAGENET)
var node = new Node(Profile.TestNet);

// Create a private key from a seed
var senderPrivateKey = PrivateKey.FromSeed("seed phrase");

// Create a simple account script in Ride
var txScript = "{-# STDLIB_VERSION 6 #-}\n" +
   "{-# CONTENT_TYPE EXPRESSION #-}\n" +
   "{-# SCRIPT_TYPE ACCOUNT #-}\n" +
   "let cooperPubKey = base58'BVqYXrapgJP9atQccdBPAgJPwHDKkh6A8'\n" +
   "let BTCId = base58'8LQW8f7P5d5PZM7GtZEBgaqRPGSzS3DfPuiXrURJ4AJS'\n" +
   "match tx {\n" +
      "case o: Order =>\n" +
         "sigVerify(o.bodyBytes, o.proofs[0], cooperPubKey ) && \n" +
         "(o.assetPair.priceAsset == BTCId || o.assetPair.amountAsset == BTCId)\n" +
      "case _ => sigVerify(tx.bodyBytes, tx.proofs[0], cooperPubKey )\n" +
   "}";

// Compile the script to the Base64 format 
var compiledScript = node.CompileScript(txScript).Script;

// Create a set script transaction 
var setScriptTx = SetScriptTransactionBuilder.Params(compiledScript).GetSignedWith(senderPrivateKey);

// Broadcast the transaction to a node and wait for it to be included in the blockchain
node.WaitForTransaction(node.Broadcast(setScriptTx).Id);
```

## Setting a dApp script

```csharp
using WavesLabs.Node.Client;
using WavesLabs.Node.Transactions.Common;
using WavesLabs.Node.Transactions;
using WavesLabs.Node.Transactions.Utils;

// Node instance creation in the given network (TESTNET, MAINNET, STAGENET)
var node = new Node(Profile.TestNet);

// Create a private key from a seed
var senderPrivateKey = PrivateKey.FromSeed("seed phrase");

// Create a simple dApp script in Ride
var txScript = "{-# STDLIB_VERSION 3 #-}\n" +
    "{-# SCRIPT_TYPE ACCOUNT #-}\n" +
    "{-# CONTENT_TYPE DAPP #-}\n" +
    "let answersCount = 20\n" +

    "let answers =\n" +
    "['It is certain.',\n" +
    "'Yes - definitely.',\n" +
    "'You may rely on it.',\n" +
    "'As I see it, yes.',\n" +
    "'My reply is no.', \n" +
    "'My sources say no.',\n" +
    "'Very doubtful.']\n" +

    "func getAnswer (question,previousAnswer) = {\n" +
        "let hash = sha256(toBytes((question + previousAnswer)))\n" +
        "let index = toInt(hash)\n" +
        "answers[(index % answersCount)]\n" +
    "}\n" +

    "func getPreviousAnswer (address) = match getString(this, (address + '_a')) {\n" +
        "case a: String => a\n" +
        "case _ => address\n" +
    "}\n" +

    "@Callable(i)\n" +
    "func tellme (question) = {\n" +
        "let callerAddress = toBase58String(i.caller.bytes)\n" +
        "let answer = getAnswer(question, getPreviousAnswer(callerAddress))\n" +
        "WriteSet([DataEntry((callerAddress + '_q'), question), DataEntry((callerAddress + '_a'), answer)])\n" +
    "}";

// Transform the Ride script to a base64 string
var compiledScript = node.CompileScript(txScript).Script;

// Create a set script transaction 
var setScriptTx = SetScriptTransactionBuilder.Params(compiledScript).GetSignedWith(senderPrivateKey);

// Broadcast the transaction to a node and wait for it to be included in the blockchain
node.WaitForTransaction(node.Broadcast(setScriptTx).Id);
```

## More Examples

### Working with accounts
#### Private keys
1. Seed phrase
```csharp
// Create a private key from a seed
var senderPrivateKey = PrivateKey.FromSeed("seed phrase");
```
2. Random seed phrase

2.1 Without nonce parameter
```csharp
// Generate a random seed phrase
var seed = Crypto.GenerateRandomSeedPhrase();
// Create the private key from the seed
var senderPrivateKey = PrivateKey.FromSeed(seed);
```
2.2 With nonce 2
```csharp
// Generate a random seed phrase
var seed = Crypto.GenerateRandomSeedPhrase();
// Create the private key from the seed
var senderPrivateKey = PrivateKey.FromSeed(seed, 2);
```
3. From seed bytes
```csharp
// Create a seed phrase bytes set
byte[] seedBytes = { 21, 55, 87, 117, 8, 81, 77, 77, 99, 87, 23, 7, 116, 99, 20, 12, 4 };
// Create the private key from the seed phrase bytes set
var senderPrivateKey = PrivateKey.FromSeed(seedBytes);
```
4. From random generated seed bytes
```csharp
// Generate a random seed phrase bytes set
byte[] randomSeedBytes = Crypto.GenerateRandomSeedBytes();
// Create the private key from the random seed phrase bytes set
var senderPrivateKey = PrivateKey.FromSeed(randomSeedBytes);
```
5. From bytes
```csharp
// Create a bytes set
byte[] bytes = {56, 3, 37, 64, 2, 38, 78, 37, 98, 45, 23, 117, 14, 88, 20, 42, 9, 21, 55, 87, 117, 8, 81, 77, 77, 99, 87, 23, 7, 116, 99, 20};
// Create the private key from seed bytes
var senderPrivateKey = PrivateKey.As(bytes);
```
6. From encoded string
```csharp
// Create a Base58 encoded string
var base58PhraseEncoded = "8hVeUrGJqb2yvecqmssSc7MP9SwKLQYycW7H1Zz3omwA";
// Create the private key from the Base58 encoded string
var senderPrivateKey = PrivateKey.As(base58PhraseEncoded);
```

#### Public keys
To obtain a public key:
```csharp
// Create the public key from the private key
var senderPublicKey = senderPrivateKey.PublicKey;
```

#### Address
```csharp
/* Get the account address from the public key
Specify the same network as used with the node instance */
var senderAddress = Address.FromPublicKey(ChainIds.TestNet, senderPublicKey);
```

### Examples of usage node REST API implemented methods
#### Assets
##### GET /assets/{assetId}/distribution/{height}/limit/{limit}
```csharp
// Get asset balance distribution by account addresses
var assetDistribution = node.GetAssetDistribution(assetId, height);
var assetDistribution = node.GetAssetDistribution(assetId, height, limit);
var assetDistribution = node.GetAssetDistribution(assetId, height, limit, afterAddress);
```
##### GET /assets/balance/{address}
```csharp
// Get the account balance of all assets (excluding WAVES)
var assetBalances = node.GetAssetsBalance(address);
```
##### GET /assets/balance/{address}/{assetId}
```csharp
// Get the account balance of a given asset
var assetBalance = node.GetAssetBalance(address, assetId);
```
##### GET /assets/details/{assetId}
```csharp
// Get detailed information about a given asset
var assetDetails = node.GetAssetDetails(assetId);
```
##### GET /assets/nft/{address}/limit/{limit}
```csharp
// Get a list of non-fungible tokens at a given address.
var assetDetailsList = node.GetNft(address);
var assetDetailsList = node.GetNft(address, limit);
var assetDetailsList = node.GetNft(address, limit, afterAssetId);
```
#### Addresses
##### GET /addresses
```csharp
// Get a list of account addresses in the node wallet
var addresses = node.GetAddresses()
```
##### GET /addresses/seq/{from}/{to}
```csharp
// Get a list of account addresses in the node wallet
var addresses = node.GetAddresses(fromIndex, oIndex);
```
##### GET /addresses/balance/{address}
```csharp
// Get the regular balance in WAVES at a given address
var addressBalance = node.GetBalance(address)
```
##### GET /addresses/balance/{address}/{confirmations}
```csharp
// Get the regular balance in WAVES at a given address
var addressBalance = node.GetBalance(address, confirmations);
```
##### GET /addresses/balance/details/{address}
```csharp
// Get the available, regular, generating, and effective account balances
var balanceDetails = node.GetBalanceDetails(address);
```
##### POST /addresses/balance
```csharp
// Create a list of addresses
var addresses = new List<Address> {
    new Address("<insert base58 address>"),
    new Address("<insert base58 address>")
};
// Get regular balances for multiple addresses
var addressBalances = node.GetBalances(addresses);
```
##### GET /addresses/data/{address}
```csharp
// Read account data entries by given address
var dataEntries = node.GetData(address);
```
##### POST /addresses/data/{address}
```csharp
// Read account data entries by given keys
var dataEntries = node.GetData(address, keysList);
```
##### GET /addresses/data/{address}/{key}
```csharp
// Read account data entries by given keys
var dataEntries = node.GetData(address, key);
```
##### GET /addresses/scriptInfo/{address}
```csharp
// Get an account script or a dApp script information by a given address
var scriptInfo = node.GetScriptInfo(address);
```
##### GET /addresses/scriptInfo/{address}/meta
```csharp
// Get an account script meta data
var scriptMeta = node.GetScriptMeta(address);
```

### Examples of working with transactions

#### IssueTransaction
```csharp
// Create an issue transaction
var tx = IssueTransactionBuilder.Params(
  "sampleasset", // name
  1000, // quantity
  2, // decimals
  "description of the asset", // description
  false, // reissuable
  null // base64 compiled script or null
).GetSignedWith(senderPrivateKey);

// Broadcast the transaction to a node and wait for it to be included in the blockchain
Node.WaitForTransaction(Node.Broadcast(tx).Id);

// Get information about the transaction from the node
IssueTransactionInfo txInfo = Node.GetTransactionInfo<IssueTransactionInfo>(tx.Id);
```

#### TransferTransaction
```csharp
// Define the recipient’s address
Address recipientAddress = new Address("insert the address");

/* Define the asset ID
In case of transfer in WAVES, use
var assetId = AssetId.Waves */
var assetId = AssetId.As("insert the asset ID");

// Create a transaction
TransferTransactionBuilder.Params(recipient, amount, assetId, feeAsset, attachment).GetSignedWith(senderPrivateKey);

// Broadcast the transaction to a node and wait for it to be included in the blockchain
node.WaitForTransaction(node.Broadcast(tx).Id);

// Get information about the transaction from the node
var txInfo = node.GetTransactionInfo<TransferTransactionInfo>(tx.Id);
```

#### ReissueTransaction
```csharp
/* Define the asset ID
In case of transfer in WAVES, use
var assetId = AssetId.Waves */
var assetId = AssetId.As("insert the asset ID");

// Create a transaction
ReissueTransactionBuilder.Params(assetId, quantity, reissuable).GetSignedWith(senderPrivateKey);

// Broadcast the transaction to a node and wait for it to be included in the blockchain
node.WaitForTransaction(node.Broadcast(tx).Id);

// Get information about the transaction from the node
var txInfo = node.GetTransactionInfo<ReissueTransactionInfo>(tx.Id);
```

#### BurnTransaction
```csharp
/* Define the asset ID
In case of transfer in WAVES, use
var assetId = AssetId.Waves */
var assetId = AssetId.As("insert the asset ID");

// Create a transaction
BurnTransactionBuilder.Params(assetId, amount).GetSignedWith(senderPrivateKey);

// Broadcast the transaction to a node and wait for it to be included in the blockchain
node.WaitForTransaction(node.Broadcast(tx).Id);

// Get information about the transaction from the node
var txInfo = node.GetTransactionInfo<BurnTransactionInfo>(tx.Id);
```

#### ExchangeTransaction
```csharp
// Define the recipient public key
var recipientPublicKey = PublicKey.As("insert the public key");
/* Define the asset ID
In case of transfer in WAVES, use
var assetId = AssetId.Waves */
var assetId = AssetId.As("insert the asset ID");

// Define the amount for the transaction
var amount = Amount.As(250);

// Define the price of the asset by its id
var price = Amount.As(100, assetId);

// Create matcher fee values
var buyMatcherFee = 300_000;
var sellMatcherFee = 350_000;

var assetPair = new AssetPair { AmountAsset = assetId, PriceAsset = assetId };

// Create orders; for example, let the sender be the matcher
var buyOrder = new OrderBuilder(
OrderType.Buy, // The order type can only be one of the two values: BUY or SELL
amount, // amount
price, // price
senderPublicKey, // sender public key
assetPair
).GetSignedWith(recipientPrivateKey);

var sellOrder = new OrderBuilder(
OrderType.Sell, // order type
amount, // amount
price, // price
senderPublicKey, // sender public key
assetPair
).GetSignedWith(senderPrivateKey);

// Create a transaction
ExchangeTransactionBuilder.Params(buyOrder, sellOrder, amount, price, buyMatcherFee, sellMatcherFee).GetSignedWith(senderPrivateKey);

// Broadcast the transaction to a node and wait for it to be included in the blockchain
node.WaitForTransaction(node.Broadcast(tx).Id);

// Get information about the transaction from the node
var txInfo = node.GetTransactionInfo<ExchangeTransactionInfo>(tx.Id);
```

#### LeaseTransaction
```csharp
// Define the recipient’s address
var recipientAddress = new Address("insert the address");

// Create a transaction
LeaseTransactionBuilder.Params(recipientAddress, amount).GetSignedWith(senderPrivateKey);

// Broadcast the transaction to a node and wait for it to be included in the blockchain
node.WaitForTransaction(node.Broadcast(tx).Id);

// Get information about the transaction from the node
var txInfo = node.GetTransactionInfo<LeaseTransactionInfo>(tx.Id);
```

#### LeaseCancelTransaction
```csharp
// Define the lease transaction ID
var leaseTxId = new Base58s("insert the lease transaction ID");

// Create a transaction
LeaseCancelTransactionBuilder.Params(leaseTxId).GetSignedWith(senderPrivateKey);

// Broadcast the transaction to a node and wait for it to be included in the blockchain
node.WaitForTransaction(node.Broadcast(tx).Id);

// Get information about the transaction from the node
var txInfo = node.GetTransactionInfo<LeaseCancelTransactionInfo>(tx.Id);
```

#### CreateAliasTransaction
```csharp
// Create an alias using the current system time
var alias = Alias.As("alice_" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

// Create a transaction
CreateAliasTransactionBuilder.Params(alias).GetSignedWith(senderPrivateKey);

// Broadcast the transaction to a node and wait for it to be included in the blockchain
node.WaitForTransaction(node.Broadcast(tx).Id);

// Get information about the transaction from the node
var txInfo = node.GetTransactionInfo<CreateAliasTransactionInfo>(tx.Id);
```

#### MassTransferTransaction
```csharp
/* Define the asset ID
In case of transfer in WAVES, use
var assetId = AssetId.Waves */
var assetId = AssetId.As("insert the asset ID");

// Define the recipient addresses
var recipientAddress = new Address("insert the address");
var recipientAddressTwo = new Address("insert the address");
var recipientAddressThree = new Address("insert the address");

// Create a list of recipients and their respective sums of transfer
var transfers = new List<Transfer> {
    Transfer.To(recipientAddress, 1000),
    Transfer.To(recipientAddressTwo, 1000),
    Transfer.To(recipientAddressThree, 1000)
};

// Create a transaction
MassTransferTransactionBuilder.Params(transfers, assetId, attachment).GetSignedWith(senderPrivateKey);

// Broadcast the transaction to a node and wait for it to be included in the blockchain
node.WaitForTransaction(node.Broadcast(tx).Id);

// Get information about the transaction from the node
var txInfo = node.GetTransactionInfo<MassTransferTransactionInfo>(tx.Id);
```

#### DataTransaction
```csharp
// Create a data transaction
var dataTx = DataTransactionBuilder.Params(new List<EntryData> { // A list of BinaryEntry, BooleanEntry, IntegerEntry, StringEntry or null
		// `data.type` string with `data.key` "test key 1" with `data.value` equal to "test value"
		DataEntry.AsString("test key 1", "test value"),
		// `data.type` boolean with `data.key` "test key 2" with `data.value` equal to "true"
		DataEntry.AsBoolean("test key 2", true)
	}).GetSignedWith(senderPrivateKey);

// Broadcast the transaction to a node and wait for it to be included in the blockchain
node.WaitForTransaction(node.Broadcast(tx).Id);

// Get information about the transaction from the node
var txInfo = node.GetTransactionInfo<DataTransactionInfo>(tx.Id);
```

#### SetScriptTransaction
```csharp
// Create a simple script and compile it to the Base64 format
var txScript = "{-# STDLIB_VERSION 5 #-}\n" +
"{-# CONTENT_TYPE DAPP #-}\n" +
"{-# SCRIPT_TYPE ACCOUNT #-}\n" +
"@Callable(inv)\n" +
"func call(bv: ByteVector, b: Boolean, int: Int, str: String, list: List[Int]) = {\n" +
" let asset = Issue(\"Asset\", \"\", 1, 0, true)\n" +
" let assetId = asset.calculateAssetId()\n" +
" let lease = Lease(inv.caller, 7)\n" +
" let leaseId = lease.calculateLeaseId()\n" +
" [\n" +
" BinaryEntry(\"bin\", assetId),\n" +
" BooleanEntry(\"bool\", true),\n" +
" IntegerEntry(\"int\", 100000),\n" +
" StringEntry(\"assetId\", assetId.toBase58String()),\n" +
" StringEntry(\"leaseId\", leaseId.toBase58String()),\n" +
" StringEntry(\"del\", \"\"),\n" +
" DeleteEntry(\"del\"),\n" +
" asset,\n" +
" SponsorFee(assetId, 1),\n" +
" Reissue(assetId, 4, false),\n" +
" Burn(assetId, 3),\n" +
" ScriptTransfer(inv.caller, 2, assetId),\n" +
" lease,\n" +
" LeaseCancel(lease.calculateLeaseId())\n" +
" ]\n" +
"}";
var compiledScript = node.CompileScript(txScript).Script;

// Create a transaction
SetScriptTransactionBuilder.Params(compiledScript).GetSignedWith(senderPrivateKey);

// Broadcast the transaction to a node and wait for it to be included in the blockchain
node.WaitForTransaction(node.Broadcast(tx).Id);

// Get information about the transaction from the node
var txInfo = node.GetTransactionInfo<SetScriptTransactionInfo>(tx.Id);
```

#### SponsorFeeTransaction
```csharp
/* Define the asset ID
In case of transfer in WAVES, use
var assetId = AssetId.Waves */
var assetId = AssetId.As("insert the asset ID");

// Create a transaction
SponsorFeeTransactionBuilder.Params(assetId, minSponsoredAssetFee).GetSignedWith(senderPrivateKey);

// Broadcast the transaction to a node and wait for it to be included in the blockchain
node.WaitForTransaction(node.Broadcast(tx).Id);

// Get information about the transaction from the node
var txInfo = node.GetTransactionInfo<SponsorFeeTransactionInfo>(tx.Id);
```

#### SetAssetScriptTransaction
```csharp
/* Define the asset ID
In case of transfer in WAVES, use
var assetId = AssetId.Waves */
var assetId = AssetId.As("insert the asset ID");

/* Transforming the ride script to a base64 string
Make sure to insert your ride script between the brackets below */
var script = node.CompileScript("INSERT YOUR DAPP SCRIPT HERE").Script;

// Create a transaction
SetAssetScriptTransactionBuilder.Params(assetId, script).GetSignedWith(senderPrivateKey);

// Broadcast the transaction to a node and wait for it to be included in the blockchain
node.WaitForTransaction(node.Broadcast(tx).Id);

// Get information about the transaction from the node
var txInfo = node.GetTransactionInfo<SetAssetScriptTransactionInfo>(tx.Id);
```

#### InvokeScriptTransaction
```csharp
// Define the dApp address
var dAppAddress = Address.As("insert the address");

/* Define the asset ID
In case of transfer in WAVES, use
var assetId = AssetId.Waves */
var assetId = AssetId.As("insert the asset ID");
var assetIdTwo = AssetId.As("insert the asset ID");
var assetIdThree = AssetId.As("insert the asset ID");

// Create a list of different payments
var payments = new List<Amount> {
    Amount.As(1005, assetId),
    Amount.As(1005, assetIdTwo),
    Amount.As(1005, assetIdThree)
};

var call = new Call {
    Function = "fname",
    Args = new List<CallArg> {
        // `call.args.type` binary with `call.args.value` equal to `senderAddress.bytes()`
        CallArg.AsBinary(senderAddress.Bytes),
        // `call.args.type` boolean with `call.args.value` equal to `true`
        CallArg.AsBoolean(true),
        // `call.args.type` integer with `call.args.value` equal to `100000`
        CallArg.AsInteger(100000),
        // `call.args.type` string with `call.args.value` equal to `string value`
        CallArg.AsString("string value"),
        // `call.args.type` list with `call.args.value` equal to `100000`
        CallArg.AsList(new [] {CallArg.AsInteger(100000) })
    }
};

// Create a transaction
InvokeScriptTransactionBuilder.Params(dAppAddress, payments, call).GetSignedWith(senderPrivateKey);

// Broadcast the transaction to a node and wait for it to be included in the blockchain
node.WaitForTransaction(node.Broadcast(tx).Id);

// Get information about the transaction from the node
var txInfo = node.GetTransactionInfo<InvokeScriptTransactionInfo>(tx.Id);
```

#### UpdateAssetInfoTransaction
```csharp
/* Define the asset ID
In case of transfer in WAVES, use
var assetId = AssetId.Waves */
var assetId = AssetId.As("insert the asset ID");

// Create a transaction
UpdateAssetInfoTransactionBuilder.Params(assetId, name, description).GetSignedWith(senderPrivateKey);

// Broadcast the transaction to a node and wait for it to be included in the blockchain
node.WaitForTransaction(node.Broadcast(tx).Id);

// Get information about the transaction from the node
var txInfo = node.GetTransactionInfo<UpdateAssetInfoTransactionInfo>(tx.Id);
```
