using System.Numerics;

namespace DiffieHellman
{
	internal class Program
	{
		
		static void Main(string[] args)
		{
			DHKeyGroup group = new DHKeyGroup(0);

				BigInteger[] aliceKeyPair = group.DeriveKeyPair();
				BigInteger[] bobKeyPair = group.DeriveKeyPair();

				BigInteger aliceSecret = E2EEUtils.ComputeDiffieHellmanSecret(group.Prime, bobKeyPair[1], aliceKeyPair[0] );
				BigInteger bobSecret = E2EEUtils.ComputeDiffieHellmanSecret(group.Prime, aliceKeyPair[1], bobKeyPair[0] );

				Console.WriteLine("Alice == Bob? : " + (aliceSecret == bobSecret));

				//convert secrets into strong passwords for storage with KDF
				Console.WriteLine("Alice: " + E2EEUtils.SHA3KDF(aliceSecret));
				Console.WriteLine("Bob : "  + E2EEUtils.SHA3KDF(bobSecret));

		}
	}
}
