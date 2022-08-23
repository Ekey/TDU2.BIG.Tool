using System;

namespace TDU2.Unpacker
{
    class BigCipher
    {
        private static Byte[] lpKey = new Byte[] {
           0xD7, 0xA8, 0xE2, 0xD4
        };

        public static Byte[] iDecryptData(Byte[] lpBuffer)
        {
            for (Int32 i = 0; i < lpBuffer.Length; i++)
            {
                lpBuffer[i] ^= lpKey[i % 4];
            }

            return lpBuffer;
        }
    }
}
