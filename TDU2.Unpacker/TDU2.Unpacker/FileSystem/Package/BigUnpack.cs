using System;
using System.IO;
using System.Collections.Generic;

namespace TDU2.Unpacker
{
    class BigUnpack
    {
        static List<MapEntry> m_EntryTable = new List<MapEntry>();

        public static void iDoIt(String m_Archive, String m_MapFile, String m_DstFolder)
        {
            BigHashList.iLoadProject();

            using (FileStream TMapStream = File.OpenRead(m_MapFile))
            {
                var m_Header = new MapHeader();

                m_Header.dwMagic = TMapStream.ReadUInt32();
                m_Header.dwVersion = TMapStream.ReadInt32();

                if (m_Header.dwMagic != 0x46424D58)
                {
                    throw new Exception("[ERROR]: Invalid magic of MAP index file!");
                }

                if (m_Header.dwVersion != 257)
                {
                    throw new Exception("[ERROR]: Invalid version of MAP index file!");
                }

                m_Header.dwStructureNamesOffset = TMapStream.ReadInt32(true);
                m_Header.dwStructureNamesTableOffset = TMapStream.ReadInt32(true);
                m_Header.dwSubHeaderOffset = TMapStream.ReadInt32(true);

                TMapStream.Seek(m_Header.dwSubHeaderOffset + 4, SeekOrigin.Begin);

                m_Header.dwTotalFiles = TMapStream.ReadInt32(true);
                m_Header.dwSubHeaderSize = TMapStream.ReadInt32(true);
                m_Header.dwNumRecords = TMapStream.ReadInt32(true);
                m_Header.dwRecordsOffset = TMapStream.ReadInt32(true);

                TMapStream.Seek(m_Header.dwSubHeaderOffset + m_Header.dwSubHeaderSize, SeekOrigin.Begin);

                m_EntryTable.Clear();
                for (Int32 i = 0; i < m_Header.dwTotalFiles; i++)
                {
                    UInt64 dwNameHash = TMapStream.ReadUInt64(true);

                    var TEntry = new MapEntry
                    {
                        dwNameHash = dwNameHash,
                        dwSize = 0,
                        dwOffset = 0,
                    };

                    m_EntryTable.Add(TEntry);
                }

                TMapStream.Seek(m_Header.dwRecordsOffset + m_Header.dwSubHeaderOffset, SeekOrigin.Begin);

                for (Int32 i = 0; i < m_Header.dwTotalFiles; i++)
                {
                    m_EntryTable[i].dwSize = TMapStream.ReadInt32(true);
                    m_EntryTable[i].dwOffset = TMapStream.ReadInt64(true);
                }

                TMapStream.Dispose();

                using (FileStream TBigStream = File.OpenRead(m_Archive))
                {
                    foreach (var m_Entry in m_EntryTable)
                    {
                        String m_FileName = BigHashList.iGetNameFromHashList(m_Entry.dwNameHash);
                        String m_FullPath = m_DstFolder + m_FileName;

                        Utils.iSetInfo("[UNPACKING]: " + m_FileName);
                        Utils.iCreateDirectory(m_FullPath);

                        TBigStream.Seek(m_Entry.dwOffset, SeekOrigin.Begin);

                        var lpBuffer = TBigStream.ReadBytes(m_Entry.dwSize);
                        lpBuffer = BigCipher.iDecryptData(lpBuffer);

                        File.WriteAllBytes(m_FullPath, lpBuffer);
                    }

                    TBigStream.Dispose();
                }
            }
        }
    }
}
