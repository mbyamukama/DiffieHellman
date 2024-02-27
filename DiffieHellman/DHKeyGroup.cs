﻿using System;
using System.Numerics;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography;

namespace DiffieHellman
{


	public class DHKeyGroup
	{
		public BigInteger Prime { get; private set; } = BigInteger.Zero;
		public BigInteger Generator { get; private set; } = BigInteger.Zero;

		public DHKeyGroup(int dhGroupId)
		{
			switch (dhGroupId)
			{
				case 0: //Curve25519 Elliptic Curve. This prime is 2^255-19 and is the prime used in the Signal protocol
					Prime = BigInteger.Parse("14781619447589544791020593568409986887264606134616475288964881837755586237401");
					Generator = new BigInteger(2);
					break;

				case 1: //768-bit MODP Group 1. (1st Oakley group) The prime is: 2^768 - 2 ^704 - 1 + 2^64 * { [2^638 pi] + 149686 }
					Prime = BigInteger.Parse("""
					0xFFFFFFFFFFFFFFFFC90FDAA22168C234C4C6628B80DC1CD129024E088A67CC74020BBEA63B139B22514A08798E3404DDEF9519B3CD3A431B302B0A6DF25F1437
					4FE1356D6D51C245E485B576625E7EC6F44C42E9A63A3620FFFFFFFFFFFFFFFF
					""", NumberStyles.HexNumber);
					Generator = new BigInteger(2);
					break;
				case 2: //1024-bit MODP Group id 2. (2nd Oakley group) The prime is: 2^1024 - 2^960 - 1 + 2^64 * { [2^894 pi] + 129093 }
					Prime = BigInteger.Parse("""
					0xFFFFFFFFFFFFFFFFC90FDAA22168C234C4C6628B80DC1CD129024E088A67CC74020BBEA63B139B22514A08798E3404DDEF9519B3CD3A431B302B0A6DF25F143
					74FE1356D6D51C245E485B576625E7EC6F44C42E9A637ED6B0BFF5CB6F406B7EDEE386BFB5A899FA5AE9F24117C4B1FE649286651ECE65381FFFFFFFFFFFFFFFF
					""", NumberStyles.HexNumber);
					Generator = new BigInteger(2);
					break;
				case 5: //1536-bit MODP Group id 5. The prime is: 2^1536 - 2^1472 - 1 + 2^64 * { [2^1406 pi] + 741804 }
					Prime = BigInteger.Parse("""
					0xFFFFFFFFFFFFFFFFC90FDAA22168C234C4C6628B80DC1CD129024E088A67CC74020BBEA63B139B22514A08798E3404DDEF9519B3CD3A431B302B0A6DF25F14
					374FE1356D6D51C245E485B576625E7EC6F44C42E9A637ED6B0BFF5CB6F406B7EDEE386BFB5A899FA5AE9F24117C4B1FE649286651ECE45B3DC2007CB8A163BF
					0598DA48361C55D39A69163FA8FD24CF5F83655D23DCA3AD961C62F356208552BB9ED529077096966D670C354E4ABC9804F1746C08CA237327FFFFFFFFFFFFFFFF
					""", NumberStyles.HexNumber);
					Generator = new BigInteger(2);
					break;
				case 14: //2048-bit MODP Group id 14   This prime is: 2 ^ 2048 - 2 ^ 1984 - 1 + 2 ^ 64 * { [2 ^ 1918 pi] +124476 }
					Prime = BigInteger.Parse("""
					0xFFFFFFFFFFFFFFFFC90FDAA22168C234C4C6628B80DC1CD129024E088A67CC74020BBEA63B139B22514A08798E3404DDEF9519B3CD3A431B302B0A6DF25F1437
					4FE1356D6D51C245E485B576625E7EC6F44C42E9A637ED6B0BFF5CB6F406B7EDEE386BFB5A899FA5AE9F24117C4B1FE649286651ECE45B3DC2007CB8A163BF05
					98DA48361C55D39A69163FA8FD24CF5F83655D23DCA3AD961C62F356208552BB9ED529077096966D670C354E4ABC9804F1746C08CA18217C32905E462E36CE3B
					E39E772C180E86039B2783A2EC07A28FB5C55DF06F4C52C9DE2BCBF6955817183995497CEA956AE515D2261898FA051015728E5A8AACAA68FFFFFFFFFFFFFFFF
					""", NumberStyles.HexNumber);
					Generator = new BigInteger(2);
					break;

				case 15: //3072-bit MODP Group id 15. This prime is: 2 ^ 3072 - 2 ^ 3008 - 1 + 2 ^ 64 * { [2 ^ 2942 pi] +1690314 }
					Prime = BigInteger.Parse("""
					0xFFFFFFFFFFFFFFFFC90FDAA22168C234C4C6628B80DC1CD129024E088A67CC74020BBEA63B139B22514A08798E3404DDEF9519B3CD3A431B302B0A6DF25F1437
					4FE1356D6D51C245E485B576625E7EC6F44C42E9A637ED6B0BFF5CB6F406B7EDEE386BFB5A899FA5AE9F24117C4B1FE649286651ECE45B3DC2007CB8A163BF05
					98DA48361C55D39A69163FA8FD24CF5F83655D23DCA3AD961C62F356208552BB9ED529077096966D670C354E4ABC9804F1746C08CA18217C32905E462E36CE3B
					E39E772C180E86039B2783A2EC07A28FB5C55DF06F4C52C9DE2BCBF6955817183995497CEA956AE515D2261898FA051015728E5A8AAAC42DAD33170D04507A33
					A85521ABDF1CBA64ECFB850458DBEF0A8AEA71575D060C7DB3970F85A6E1E4C7ABF5AE8CDB0933D71E8C94E04A25619DCEE3D2261AD2EE6BF12FFA06D98A0864
					D87602733EC86A64521F2B18177B200CBBE117577A615D6C770988C0BAD946E208E24FA074E5AB3143DB5BFCE0FD108E4B82D120A93AD2CAFFFFFFFFFFFFFFFF
					""", NumberStyles.HexNumber);

					Generator = new BigInteger(2);
					break;
				default:
					Prime = new DHKeyGroup(0).Prime;
					break;
			}
		}

		public BigInteger[] DeriveKeyPair()
		{
			var privateKey = RandomBigInteger(Prime);
			var publicKey = BigInteger.ModPow(Generator, privateKey, Prime); //g^pk % p
			return new BigInteger[] { privateKey, publicKey };
		}

		//generate a random big integer less than prime. This is an arbitrary implementation
		public static BigInteger RandomBigInteger(BigInteger prime)
		{
			long bitLength = prime.GetBitLength(); // Get bit length of the prime
			byte[] bytes = RandomNumberGenerator.GetBytes((int)(bitLength / 8));
			bytes[0] &= 0x7F; // Clear MSB (ensures < prime) 
			BigInteger randomInt = new BigInteger(bytes);
			if (randomInt < 0) randomInt *= -1; //ensures +ve
			return randomInt;
		}
	}
}