using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DiffieHellman
{
	public class E2EEUtils
	{
		///Computes a diffie-hellman Secret given a remotePublicKey and a local private key
		public static BigInteger ComputeDiffieHellmanSecret(BigInteger prime, BigInteger remotePublicKey, BigInteger localPrivateKey)
		{
			var secret = BigInteger.ModPow(remotePublicKey, localPrivateKey, prime);
			return secret;
		}

		//secret strengthening. replace with stronger KDF, e.g: HKDF
		public static string SHA3KDF(BigInteger secret)
		{
			SHA384 sha3 = SHA384.Create(); // Use SHA384 for 224-bit output
			byte[] secretBytes = secret.ToByteArray();
			byte[] hashBytes = sha3.ComputeHash(secretBytes);
			string base64Hash = Convert.ToBase64String(hashBytes);
			// Remove padding '=' characters
			return base64Hash;
		}
	}
}
