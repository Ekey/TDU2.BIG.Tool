using System;

namespace TDU2.Unpacker
{
    class MapEntry
    {
        public UInt64 dwNameHash { get; set; }
        public Int32 dwSize { get; set; }
        public Int64 dwOffset { get; set; }
    }
}
