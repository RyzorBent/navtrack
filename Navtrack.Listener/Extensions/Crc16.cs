﻿using System.Linq;

namespace Navtrack.Listener.Extensions
{
    public static class Crc16
    {
        private const int HashSize = 16;
        private const int Init = 0xFFFF;
        private const int Mask = 0xFFFF;
        private const int Poly = 0x1021;
    
        // ReSharper disable once IdentifierTypo
        public static int Ccitt(int[] bytes)
        {
            int crc = Init;
            int[] table = Enumerable.Range(0, 256).Select(CreateTableEntry).ToArray();

            int toRight = HashSize - 8;
            toRight = toRight < 0 ? 0 : toRight;
            
            foreach (int t in bytes)
            {
                crc = table[((crc >> toRight) ^ t) & 0xFF] ^ (crc << 8);
                crc &= Mask;
            }

            return crc;
        }

        private static int CreateTableEntry(int index)
        {
            int r = index;
            r <<= HashSize - 8;

            const int lastBit = 1 << (HashSize - 1);

            for (int i = 0; i < 8; i++)
            {
                if ((r & lastBit) != 0)
                {
                    r = (r << 1) ^ Poly;
                }
                else
                {
                    r <<= 1;
                }
            }

            return r & Mask;
        }
    }
}