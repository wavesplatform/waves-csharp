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

Console.ReadLine();