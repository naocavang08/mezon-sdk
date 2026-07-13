using Mezon_sdk.Mmn.Models;
using SimpleBase;
using System.Numerics;
using System.Security.Cryptography;
using System.Text.Json;

namespace Mezon_sdk.Mmn.Utils
{
    public static class CryptoHelper
    {
        public const int Ed25519PublicKeySizeInBytes = 32;
        public const int Ed25519ExpandedPrivateKeySizeInBytes = 64;
        public const int Ed25519PrivateKeySeedSizeInBytes = 32;

        public static byte[] Serialize(Tx tx)
        {
            var extraInfo = tx.ExtraInfo ?? string.Empty;
            var textData = tx.TextData ?? string.Empty;
            var metadata = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "{0}|{1}|{2}|{3}|{4}|{5}|{6}",
                tx.Type, tx.Sender, tx.Recipient, tx.Amount, textData, tx.Nonce, extraInfo);
            Console.WriteLine($"Serialize metadata: {metadata}");
            return System.Text.Encoding.UTF8.GetBytes(metadata);
        }

        public static SignedTx SignTx(Tx tx, byte[] pubKey, byte[] privKey)
        {
            if (privKey.Length != 32)
            {
                throw new ArgumentException("Unsupported private key length. Seed must be 32 bytes.");
            }

            var privateKeyParams = new Org.BouncyCastle.Crypto.Parameters.Ed25519PrivateKeyParameters(privKey, 0);
            var txHash = Serialize(tx);
            
            var signer = new Org.BouncyCastle.Crypto.Signers.Ed25519Signer();
            signer.Init(true, privateKeyParams);
            signer.BlockUpdate(txHash, 0, txHash.Length);
            var signature = signer.GenerateSignature();

            if (tx.Type == (int)TxType.Faucet)
            {
                return new SignedTx
                {
                    Tx = tx,
                    Sig = Base58Encode(signature)
                };
            }

            var userSig = new UserSig
            {
                PubKey = pubKey,
                Sig = signature
            };

            var userSigBytes = JsonSerializer.SerializeToUtf8Bytes(userSig);
            return new SignedTx
            {
                Tx = tx,
                Sig = Base58Encode(userSigBytes)
            };
        }

        public static bool Verify(Tx tx, string sig)
        {
            var txHashBytes = Serialize(tx);
            if (tx.Type == (int)TxType.Faucet)
            {
                try
                {
                    var pubKeyBytes = Base58Decode(tx.Sender);
                    var signatureBytes = Base58Decode(sig);
                    
                    var publicKeyParams = new Org.BouncyCastle.Crypto.Parameters.Ed25519PublicKeyParameters(pubKeyBytes, 0);
                    var verifier = new Org.BouncyCastle.Crypto.Signers.Ed25519Signer();
                    verifier.Init(false, publicKeyParams);
                    verifier.BlockUpdate(txHashBytes, 0, txHashBytes.Length);
                    return verifier.VerifySignature(signatureBytes);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Faucet verify exception: {ex.Message}");
                    return false;
                }
            }

            try
            {
                var sigBytes = Base58Decode(sig);
                var userSig = JsonSerializer.Deserialize<UserSig>(sigBytes);
                if (userSig == null)
                {
                    Console.WriteLine("UserSig deserialization returned null");
                    return false;
                }

                var publicKeyParams = new Org.BouncyCastle.Crypto.Parameters.Ed25519PublicKeyParameters(userSig.PubKey, 0);
                var verifier = new Org.BouncyCastle.Crypto.Signers.Ed25519Signer();
                verifier.Init(false, publicKeyParams);
                verifier.BlockUpdate(txHashBytes, 0, txHashBytes.Length);
                return verifier.VerifySignature(userSig.Sig);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"User verify exception: {ex.Message}");
                return false;
            }
        }

        public static Tx BuildTransferTx(
            int txType,
            string sender,
            string recipient,
            BigInteger amount,
            ulong nonce,
            ulong timestamp,
            string textData,
            Dictionary<string, string>? extraInfo,
            string zkProof,
            string zkPub)
        {
            ValidationHelper.ValidateAddress(sender);
            ValidationHelper.ValidateAddress(recipient);
            ValidationHelper.ValidateAmount(amount);

            var serializedTxExtra = ValidationHelper.SerializeTxExtraInfo(extraInfo);

            return new Tx
            {
                Type = txType,
                Sender = sender,
                Recipient = recipient,
                Amount = amount,
                Nonce = nonce,
                Timestamp = timestamp,
                TextData = textData,
                ExtraInfo = serializedTxExtra,
                ZkProof = zkProof,
                ZkPub = zkPub
            };
        }

        public static byte[] Base58Decode(string input)
        {
            return Base58.Bitcoin.Decode(input).ToArray();
        }

        public static string Base58Encode(byte[] input)
        {
            return Base58.Bitcoin.Encode(input);
        }

        public static string GenerateAddress(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
                return Base58Encode(hash);
            }
        }

        public static (byte[] publicKey, byte[] privateKey) GenerateEd25519KeyPair()
        {
            var seed = new byte[Ed25519PrivateKeySeedSizeInBytes];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(seed);
            }

            var privateKeyParams = new Org.BouncyCastle.Crypto.Parameters.Ed25519PrivateKeyParameters(seed, 0);
            var publicKeyParams = privateKeyParams.GeneratePublicKey();
            var publicKey = publicKeyParams.GetEncoded();

            // Return the seed (32 bytes) as privateKey, not the expanded version (64 bytes)
            // This matches what SignTx expects
            return (publicKey, seed);
        }
    }
}
