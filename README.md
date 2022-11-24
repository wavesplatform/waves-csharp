# Waves.NET
A .NET library for interacting with the Waves blockchain.

Supports node interaction, offline transaction signing and creating addresses and keys.

## Using in your project
Use the codes below to add WavesJ as a dependency for your project.

##### Requirements:
- .NET 6 or above

##### NuGet package:
```
TODO: place "install nuget package" command here
```
### Getting started
Create an account from a private key from random seed phrase:
```csharp
var privateKey = PrivateKey.FromSeed(Crypto.GenerateRandomSeedPhrase());
var publicKey = PublicKey.From(privateKey);
var address = Address.FromPublicKey(ChainIds.TestNet, publicKey);
```

Create a Node and learn a few things about blockchain:
```csharp
var node = NodeClient.Create(Profile.TestNet);
Console.WriteLine("Current height is " + node.GetHeight());
Console.WriteLine("My balance is " + node.GetBalance(address));
Console.WriteLine("With 100 confirmations: " + node.GetBalance(address, 100));
```

Send some money to a buddy:
```csharp
var buddy = new Address("3N9gDFq8tKFhBDBTQxR3zqvtpXjw5wW3syA");
node.Broadcast(TransferTransactionBuilder.Params(buddy, 100000000).GetSignedWith(privateKey));
```

Set a script on an account. Be careful with the script you pass here, as it may lock the account forever!
```csharp
var script = node.CompileScript("{-# CONTENT_TYPE EXPRESSION #-} sigVerify(tx.bodyBytes, tx.proofs[0], tx.senderPublicKey)").Script;
node.Broadcast(SetScriptTransactionBuilder.Params(script).GetSignedWith(privateKey));
```