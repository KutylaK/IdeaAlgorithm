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
        List<int> keys;

        public IdeaAlgorithmImpl(string stringKey)
        {
            byte[] key = generateByteKey(stringKey);
        }

        private byte[] generateByteKey(string stringKey)
        {
            int nofChar = 0x7E - 0x21 + 1;    // Number of different valid characters
            int[] a = new int[8];
            for (int p = 0; p < stringKey.Length; p++)
            {
                int c = stringKey[p];

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
    }
}
