# WavesLabs.Node.Client
A .NET library for interacting with the Waves blockchain.

Supports node interaction, offline transaction signing and creating addresses and keys.

## Using in your project
Use the codes below to add *WavesLabs.Node.Client* as a dependency for your project.

##### Requirements:
- [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet) or above

##### Create a project:
Using *Visual Studio* or *Visual Studio Code* create a new console app.

##### Install NuGet package:
```
TODO: place "install nuget package" command here
```
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
    2.2 With nonce = 2

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
