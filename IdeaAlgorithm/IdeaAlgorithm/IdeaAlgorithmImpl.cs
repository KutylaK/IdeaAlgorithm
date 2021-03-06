﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaAlgorithm
{
    public class IdeaAlgorithmImpl
    {
        static int rounds = 8;
        int[] keys;

        public IdeaAlgorithmImpl(byte[] Key, bool encrypt)
        {
            var tmpKeys = generateByteKey(Key);
            if (encrypt)
                keys = tmpKeys;
            else
                keys = invertSubKey(tmpKeys);
        }

        public byte[] crypt(byte[] data)
        {
            var dataList = data.ToList();
            while (dataList.Count % 8 != 0) dataList.Add((byte)0);

            var dataExpanded = dataList.ToArray();
            for (int i = 0; i < dataExpanded.Count(); i += 8)
            {
                cryptPart(dataExpanded, i);
            }
            return dataExpanded;
        }

        public void cryptPart(byte[] data, int offset)
        {
            int x1 = concat2Bytes(data[offset + 0], data[offset + 1]);
            int x2 = concat2Bytes(data[offset + 2], data[offset + 3]);
            int x3 = concat2Bytes(data[offset + 4], data[offset + 5]);
            int x4 = concat2Bytes(data[offset + 6], data[offset + 7]);
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
                                                    
            data[offset + 0] = (byte)(r0 >> 8);
            data[offset + 1] = (byte)r0;
            data[offset + 2] = (byte)(r1 >> 8);
            data[offset + 3] = (byte)r1;
            data[offset + 4] = (byte)(r2 >> 8);
            data[offset + 5] = (byte)r2;
            data[offset + 6] = (byte)(r3 >> 8);
            data[offset + 7] = (byte)r3;

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


        private static int[] invertSubKey(int[] key)
        {
            int[] invKey = new int[key.Length];
            int p = 0;
            int i = rounds * 6;
            invKey[i + 0] = mulInv(key[p++]);
            invKey[i + 1] = addInv(key[p++]);
            invKey[i + 2] = addInv(key[p++]);
            invKey[i + 3] = mulInv(key[p++]);
            for (int r = rounds - 1; r >= 0; r--)
            {
                i = r * 6;
                int m = r > 0 ? 2 : 1;
                int n = r > 0 ? 1 : 2;
                invKey[i + 4] = key[p++];
                invKey[i + 5] = key[p++];
                invKey[i + 0] = mulInv(key[p++]);
                invKey[i + m] = addInv(key[p++]);
                invKey[i + n] = addInv(key[p++]);
                invKey[i + 3] = mulInv(key[p++]);
            }
            return invKey;
        }

        public static byte[] makeKey(string charKey)
        {
            int nofChar = 0x7E - 0x21 + 1; 
            int[] a = new int[8];
            for (int p = 0; p < charKey.Length; p++)
            {
                int c = charKey[p];

                for (int i = a.Length - 1; i >= 0; i--)
                {
                    c += a[i] * nofChar;
                    a[i] = c & 0xFFFF;
                    c >>= 16;
                }
            }
            byte[] key = new byte[16];
            for (int i = 0; i < 8; i++)
            {
                key[i * 2] = (byte)(a[i] >> 8);
                key[i * 2 + 1] = (byte)a[i];
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
