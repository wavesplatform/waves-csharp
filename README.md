# WavesLabs.Node.Client
A .NET library for interacting with the Waves blockchain.

Supports node interaction, offline transaction signing and creating addresses and keys.

## Using in your project
Use the codes below to add WavesLabs.Node.Client as a dependency for your project.

##### Requirements:
- [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet) or above

##### Create a project:
Using Visual Studio or Visual Studio Code create a new console app.

##### Install NuGet package:
```
TODO: place "install nuget package" command here
```
### Usage example
We will initiate a create [alias transaction](https://dev.waves.tech/en/edu/lessons/learn-sdks-the-waves-signer-and-its-providers/work-with-accounts#create-alias-transaction):
1. Open the Program.cs file created by default and replace its content with the code below:
```csharp
using WavesLabs.Node.Transactions.Utils;

// Generate a random seed phrase
var seed = Crypto.GenerateRandomSeedPhrase();

// Print the generated seed
Console.WriteLine(seed);
```
2. Run the program.
In console window you will see the output of the generated seed phrase. There will be a similar set of words printed:

```
chunk jump trash fringe success avoid undo fatal clown learn attack month eyebrow sock repair
```
Save the generated seed phrase.

3. Replace the Program.cs file content with the code below.
Assign the generated seed phrase to the senderPrivateKey variable's method PrivateKey.fromSeed().  The code will create a new account from the generated seed phrase:
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
Run the Main() class and save the output.
You will be able to see an account address in the output like this:
```
3N6Dbnr36oxZUcXXX7ifYbA6CSJf1ndg18s
```
Copy the generated account address from the terminal.
