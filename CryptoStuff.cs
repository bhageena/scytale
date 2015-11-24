using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Scytale
{
    static class Crypto
    {
        // Use the password to generate key bytes.
        private static void MakeKeyAndIV(string password, byte[] salt, int key_size_bits, int block_size_bits, out byte[] key, out byte[] iv)
        {
            Rfc2898DeriveBytes derive_bytes = new Rfc2898DeriveBytes(password, salt, 1000);

            key = derive_bytes.GetBytes(key_size_bits / 8);
            iv = derive_bytes.GetBytes(block_size_bits / 8);
        }

        #region "Encrypt Files and Streams"

        // Encrypt or decrypt a file, saving the results in another file.
        public static void EncryptFile(string password, string in_file, string out_file)
        {
            CryptFile(password, in_file, out_file, true);
        }
        public static void DecryptFile(string password, string in_file, string out_file)
        {
            CryptFile(password, in_file, out_file, false);
        }
        public static void CryptFile(string password, string in_file, string out_file, bool encrypt)
        {
            // Create input and output file streams.
            using (FileStream in_stream = new FileStream(in_file, FileMode.Open, FileAccess.Read))
            {
                using (FileStream out_stream = new FileStream(out_file, FileMode.Create, FileAccess.Write))
                {
                    // Encrypt/decrypt the input stream into the output stream.
                    CryptStream(password, in_stream, out_stream, encrypt);
                }
            }
        }

        // Encrypt the data in the input stream into the output stream.
        public static void CryptStream(string password, Stream in_stream, Stream out_stream, bool encrypt)
        {
            // Make an AES service provider.
            AesCryptoServiceProvider aes_provider = new AesCryptoServiceProvider();

            // Find a valid key size for this provider.
            int key_size_bits = 0;
            for (int i = 1024; i > 1; i--)
            {
                if (aes_provider.ValidKeySize(i))
                {
                    key_size_bits = i;
                    break;
                }
            }
            Debug.Assert(key_size_bits > 0);

            // Get the block size for this provider.
            int block_size_bits = aes_provider.BlockSize;

            // Generate the key and initialization vector.
            byte[] key = null;
            byte[] iv = null;
            byte[] salt = { 0x0, 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0xF1, 0xF0, 0xEE, 0x21, 0x22, 0x45 };
            MakeKeyAndIV(password, salt, key_size_bits, block_size_bits, out key, out iv);

            // Make the encryptor or decryptor.
            ICryptoTransform crypto_transform;
            if (encrypt)
            {
                crypto_transform = aes_provider.CreateEncryptor(key, iv);
            }
            else
            {
                crypto_transform = aes_provider.CreateDecryptor(key, iv);
            }

            // Attach a crypto stream to the output stream.
            // Closing crypto_stream sometimes throws an
            // exception if the decryption didn't work
            // (e.g. if we use the wrong password).
            try
            {
                using (CryptoStream crypto_stream = new CryptoStream(out_stream, crypto_transform, CryptoStreamMode.Write))
                {
                    // Encrypt or decrypt the file.
                    const int block_size = 1024;
                    byte[] buffer = new byte[block_size];
                    int bytes_read;
                    while (true)
                    {
                        // Read some bytes.
                        bytes_read = in_stream.Read(buffer, 0, block_size);
                        if (bytes_read == 0) break;

                        // Write the bytes into the CryptoStream.
                        crypto_stream.Write(buffer, 0, bytes_read);
                    }
                } // using crypto_stream 
            }
            catch (CryptographicException e)
            {
                throw e;
            }
            catch
            {
                // Re-throw this exception.
                throw;
            }

            crypto_transform.Dispose();
        }

        #endregion // Encrypt Files and Streams"

        #region "Encrypt Strings and Byte[]"
        // Note that extension methods must be defined in a non-generic static class.

        // Encrypt or decrypt the data in in_bytes[] and return the result.
        public static byte[] CryptBytes(string password, byte[] in_bytes, bool encrypt)
        {
            // Make an AES service provider.
            AesCryptoServiceProvider aes_provider = new AesCryptoServiceProvider();

            // Find a valid key size for this provider.
            int key_size_bits = 0;
            for (int i = 1024; i > 1; i--)
            {
                if (aes_provider.ValidKeySize(i))
                {
                    key_size_bits = i;
                    break;
                }
            }
            Debug.Assert(key_size_bits > 0);

            // Get the block size for this provider.
            int block_size_bits = aes_provider.BlockSize;

            // Generate the key and initialization vector.
            byte[] key = null;
            byte[] iv = null;
            byte[] salt = { 0x0, 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0xF1, 0xF0, 0xEE, 0x21, 0x22, 0x45 };
            MakeKeyAndIV(password, salt, key_size_bits, block_size_bits, out key, out iv);

            // Make the encryptor or decryptor.
            ICryptoTransform crypto_transform;
            if (encrypt)
            {
                crypto_transform = aes_provider.CreateEncryptor(key, iv);
            }
            else
            {
                crypto_transform = aes_provider.CreateDecryptor(key, iv);
            }

            // Create the output stream.
            using (MemoryStream out_stream = new MemoryStream())
            {
                // Attach a crypto stream to the output stream.
                using (CryptoStream crypto_stream = new CryptoStream(out_stream,
                    crypto_transform, CryptoStreamMode.Write))
                {
                    // Write the bytes into the CryptoStream.
                    crypto_stream.Write(in_bytes, 0, in_bytes.Length);
                    try
                    {
                        crypto_stream.FlushFinalBlock();
                    }
                    catch (CryptographicException e)
                    {
                        throw e;
                    }
                    catch
                    {
                        // Re-throw this exception.
                        throw;
                    }

                    // return the result.
                    return out_stream.ToArray();
                }
            }
        }

        // String extensions to encrypt and decrypt strings.
        public static byte[] Encrypt(this string the_string, string password)
        {
            System.Text.ASCIIEncoding ascii_encoder = new System.Text.ASCIIEncoding();
            byte[] plain_bytes = ascii_encoder.GetBytes(the_string);
            return CryptBytes(password, plain_bytes, true);
        }
        public static string Decrypt(this byte[] the_bytes, string password)
        {
            byte[] decrypted_bytes = CryptBytes(password, the_bytes, false);
            System.Text.ASCIIEncoding ascii_encoder = new System.Text.ASCIIEncoding();
            return ascii_encoder.GetString(decrypted_bytes);
        }
        public static string CryptString(string password, string in_string, bool encrypt)
        {
            // Make a stream holding the input string.
            byte[] in_bytes = Encoding.ASCII.GetBytes(in_string);
            using (MemoryStream in_stream = new MemoryStream(in_bytes))
            {
                // Make an output stream.
                using (MemoryStream out_stream = new MemoryStream())
                {
                    // Encrypt.
                    CryptStream(password, in_stream, out_stream, true);

                    // Return the result.
                    out_stream.Seek(0, SeekOrigin.Begin);
                    using (StreamReader stream_reader = new StreamReader(out_stream))
                    {
                        return stream_reader.ReadToEnd();
                    }
                }
            }
        }

        // Convert a byte array into a readable string of hexadecimal values.
        public static string ToHex(this byte[] the_bytes)
        {
            return ToHex(the_bytes, false);
        }
        public static string ToHex(this byte[] the_bytes, bool add_spaces)
        {
            string result = "";
            string separator = "";
            if (add_spaces) separator = " ";
            for (int i = 0; i < the_bytes.Length; i++)
            {
                result += the_bytes[i].ToString("x2") + separator;
            }
            return result;
        }

        // Convert a string containing 2-digit hexadecimal values into a byte array.
        public static byte[] ToBytes(this string the_string)
        {
            List<byte> the_bytes = new List<byte>();
            the_string = the_string.Replace(" ", "");

            for (int i = 0; i < the_string.Length; i += 2)
            {
                the_bytes.Add(
                    byte.Parse(the_string.Substring(i, 2),
                        System.Globalization.NumberStyles.HexNumber));
            }
            return the_bytes.ToArray();
        }

        #endregion // Encrypt Strings and Byte[]

    }
}
