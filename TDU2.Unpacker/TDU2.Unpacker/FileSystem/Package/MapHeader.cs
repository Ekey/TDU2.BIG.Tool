using System;

namespace TDU2.Unpacker
{
    class MapHeader
    {
        public UInt32 dwMagic { get; set; } // 0x46424D58 (XMBF)
        public Int32 dwVersion { get; set; } // 257
        public Int32 dwStructureNamesOffset { get; set; } // 28
        public Int32 dwStructureNamesTableOffset { get; set; } // 224
        public Int32 dwSubHeaderOffset { get; set; } // + 4
        public Int32 dwTotalFiles { get; set; }
        public Int32 dwSubHeaderSize { get; set; } // 24
        public Int32 dwNumRecords { get; set; } // equals dwTotalFiles
        public Int32 dwRecordsOffset { get; set; }
    }
}
