using System;
using System.IO;

namespace TDU2.Unpacker
{
    class Program
    {
        private static String m_Title = "Test Drive Unlimited 2 BIG Unpacker";

        static void Main(String[] args)
        {
            Console.Title = m_Title;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(m_Title);
            Console.WriteLine("(c) 2022 Ekey (h4x0r) / v{0}\n", Utils.iGetApplicationVersion());
            Console.ResetColor();

            if (args.Length != 2)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[Usage]");
                Console.WriteLine("    TDU2.Unpacker <m_File> <m_Directory>\n");
                Console.WriteLine("    m_File - Source of BIG archive file");
                Console.WriteLine("    m_Directory - Destination directory\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[Examples]");
                Console.WriteLine("    TDU2.Unpacker E:\\Games\\TDU2\\bigfile_EU_1.big D:\\Unpacked");
                Console.ResetColor();
                return;
            }

            String m_BigFile = args[0];
            String m_Output = Utils.iCheckArgumentsPath(args[1]);
            String m_MapFile = m_BigFile.Replace(".big", ".map");

            if (!File.Exists(m_BigFile))
            {
                Utils.iSetError("[ERROR]: Input BIG file -> " + m_BigFile + " <- does not exist");
                return;
            }

            if (!File.Exists(m_MapFile))
            {
                Utils.iSetError("[ERROR]: Input MAP file -> " + m_BigFile + " <- does not exist");
                return;
            }

            BigUnpack.iDoIt(m_BigFile, m_MapFile, m_Output);
        }
    }
}
