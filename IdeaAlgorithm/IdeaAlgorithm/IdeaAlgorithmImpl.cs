using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaAlgorithm
{
    public class IdeaAlgorithmImpl
    {
        int rounds = 8;
        int[] keys;

        public IdeaAlgorithmImpl(byte[] Key, bool encrypt)
        {
            var tmpKeys = generateByteKey(Key);
            if (encrypt)
                keys = tmpKeys;
            else
                keys = invertSubkey(tmpKeys);
        }

        public byte[] crypt(byte[] data)
        {
            var result = new List<byte>();
            for (int i = 0; i < data.Length-7; i+=8)
            {
                var part = data.Skip(i).Take(8).ToList();

                if (part.Count() != 8)
                {
                    while (part.Count != 8)
                        part.Add((byte)0);
                }

                result.AddRange(cryptPart(part.ToArray()));

            }

            return result.ToArray() ;
        }

        byte[] cryptPart(byte[] data)
        {
            int x1 = concat2Bytes(data[0], data[1]);
            int x2 = concat2Bytes(data[2], data[3]);
            int x3 = concat2Bytes(data[4], data[5]);
            int x4 = concat2Bytes(data[6], data[7]);
            int k = 0; 
            for (int round = 0; round < rounds; round++)
            {
                int y1 = mul(x1, keys[k++]);         
                int y2 = add(x2, keys[k++]);         
                int y3 = add(x3, keys[k++]);         
                int y4 = mul(x4, keys[k++]);         
                int y5 = y1 ^ y3;                    
                int y6 = y2 ^ y4;                    
                int y7 = mul(y5, keys[k++]);         
                int y8 = add(y6, y7);                
                int y9 = mul(y8, keys[k++]);         
                int y10 = add(y7, y9);               
                x1 = y1 ^ y9;                        
                x2 = y3 ^ y9;                        
                x3 = y2 ^ y10;                       
                x4 = y4 ^ y10;                       
            }

            int r0 = mul(x1, keys[k++]);             
            int r1 = add(x3, keys[k++]);             
            int r2 = add(x2, keys[k++]);             
            int r3 = mul(x4, keys[k]);               
                                                     
            data[0] = (byte)(r0 >> 8);
            data[1] = (byte)r0;
            data[2] = (byte)(r1 >> 8);
            data[3] = (byte)r1;
            data[4] = (byte)(r2 >> 8);
            data[5] = (byte)r2;
            data[6] = (byte)(r3 >> 8);
            data[7] = (byte)r3;

            return data;

        }

        private int[] generateByteKey(byte[] userKey)
        {
            if (userKey.Length != 16)
                throw new Exception($"Klucz ma niepoprawną długość {userKey.Length}");

            int[] key = new int[52];

            int b1, b2;
            for (int i = 0; i < userKey.Length / 2; i++)
            {
                key[i] = concat2Bytes(userKey[2 * i], userKey[2 * i + 1]);
            }

            for (int i = userKey.Length / 2; i < key.Length; i++)
            {
                b1 = key[(i + 1) % 8 != 0 ? i - 7 : i - 15] << 9;
                b2 = (int)((uint)key[(i + 2) % 8 < 2 ? i - 14 : i - 6] >> 7); 
                key[i] = (b1 | b2) & 0xFFFF;
            }
            return key;
        }


        static int concat2Bytes(int b1, int b2)
        {
            b1 = (b1 & 0xFF) << 8;  
            b2 = b2 & 0xFF;         
            return (b1 | b2);       
        }


        public static byte[] concat2Bytes(byte[] b1, byte[] b2)
        {
            byte[] output = new byte[b1.Length + b2.Length];
            int i = 0;
            foreach (byte aB1 in b1)
            {
                output[i++] = aB1;
            }
            foreach(byte aB2 in b2)
            {
                output[i++] = aB2;
            }
            return output;
        }

        private  int[] invertSubkey(int[] subkey)
        {
            int[] invSubkey = new int[subkey.Length];
            int p = 0;
            int i = rounds * 6;

            invSubkey[i] = mulInv(subkey[p++]);     
            invSubkey[i + 1] = addInv(subkey[p++]); 
            invSubkey[i + 2] = addInv(subkey[p++]); 
            invSubkey[i + 3] = mulInv(subkey[p++]); 
                                                    
            for (int r = rounds - 1; r > 0; r--)
            {
                i = r * 6;
                invSubkey[i + 4] = subkey[p++];        
                invSubkey[i + 5] = subkey[p++];        
                invSubkey[i] = mulInv(subkey[p++]); 
                invSubkey[i + 2] = addInv(subkey[p++]);
                invSubkey[i + 1] = addInv(subkey[p++]);
                invSubkey[i + 3] = mulInv(subkey[p++]);
            }

            invSubkey[4] = subkey[p++];                
            invSubkey[5] = subkey[p++];                
            invSubkey[0] = mulInv(subkey[p++]);        
            invSubkey[1] = addInv(subkey[p++]);        
            invSubkey[2] = addInv(subkey[p++]);        
            invSubkey[3] = mulInv(subkey[p]);          
            return invSubkey;
        }

        public static byte[] makeKey(string charKey, int size)
        {
            byte[] key = new byte[size];
            int i, j;
            for (j = 0; j < key.Length; ++j)
            {
                key[j] = 0;
            }
            for (i = 0, j = 0; i < charKey.Length; i++, j = (j + 1) % key.Length)
            {
                key[j] ^= (byte)charKey.ElementAt(i);
            }
            return key;
        }
        private static int add(int x, int y) => (x + y) & 0xFFFF;
        
        private static int addInv(int x) => (0x10000 - x) & 0xFFFF;
        

        private static int mul(int x, int y)
        {
            long m = (long)x * y;
            if (m != 0)
            {
                return (int)(m % 0x10001) & 0xFFFF;
            }
            else
            {
                if (x != 0 || y != 0)
                {
                    return (1 - x - y) & 0xFFFF;
                }
                return 1;
            }
        }

        public static void xor(byte[] a, int pos, byte[] b, int blockSize)
        {
            for (int p = 0; p < blockSize; p++)
            {
                a[pos + p] ^= b[p];
            }
        }

        private static int mulInv(int x)
        {
            if (x <= 1)
            {
                return x;
            }
            try
            {
                int y = 0x10001;
                int t0 = 1;
                int t1 = 0;
                while (true)
                {
                    t1 += y / x * t0;
                    y %= x;
                    if (y == 1)
                    {
                        return (1 - t1) & 0xffff;
                    }
                    t0 += x / y * t1;
                    x %= y;
                    if (x == 1)
                    {
                        return t0;
                    }
                }
            }
            catch (ArithmeticException e)
            {
                return 0;
            }
        }
    }
}
