using System;
using System.Collections.Generic;

namespace CommandParser
{
    public class Test
    {
        public static void StringToCmnParamArrayTest()
        {
            var line = " -p   l p";
            OneStrToArr(line);

            line = "  -help    -h  \" [";
            OneStrToArr(line);
            line = "  -help    -h  \" 12 3 \" [";
            OneStrToArr(line);
            line = "-k  \"-dc\" ok \"cd\" bad \" \"   -print \"  the message ! \" \"next sentence\"  and another one ";
            OneStrToArr(line);
        }

        public static void CorrectCmndLineTest()
        {
            var line = "-hepl -h";
            OneCorrCmndLine(line);
            line = " /h ";
            OneCorrCmndLine(line);
            line = " /? ";
            OneCorrCmndLine(line);
            line = " /help ";
            OneCorrCmndLine(line);
            line = " -k   jdfkl \"-asdas\" -pl ";
            OneCorrCmndLine(line);
            line = " -k  ";
            OneCorrCmndLine(line);
            line = " -k  gh ";
            OneCorrCmndLine(line);
            line = " -k   -print \"\" ";
            OneCorrCmndLine(line);
            line = " -k  sdhg -print \"\" ";
            OneCorrCmndLine(line);
            line = " -k   -print \"\" ";
            OneCorrCmndLine(line);
        }

        public static void ProcTest()
        {
            List<string> list = new List<string>();
            list.Add(" -help ");
            list.Add(" -k   jdfkl \"-asdas\" -pl ");
            list.Add(" -k  gh -h -print");
            list.Add(" -k   -print \"\" ");
            list.Add("");
            list.Add(" -k  \"-dc\" ok \"cd\" bad \" \"   - print \"\" ");
            list.Add(" -k  \"-dc\" ok \"cd\" bad \" \"   -print \"\" ");
            list.Add("-print \"  the message ! \" ");
            list.Add("  \"as \"   -print \"  the message ! \" -ping");
            list.Add(" -k  1 ok 2 bad \"3 \"   -print \"  the message ! \" \"next sentence\"  and another one ");
            list.Add(" -k  \"-dc\" ok \"cd\" bad \" \" -ping j  -print \"  the message ! \" \"next sentence\"  and another -ping ");
            list.Add("-ping -print hello everybody! -ping");
            list.Add("-k \"half life 3\" \"hl 3\"");
            foreach (var line in list)
            {
                {
                    string[] cmd = null;
                    Console.WriteLine(line);
                    try
                    {
                        cmd = CommandParser.ToCmdParams(line);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    if (!CommandParser.HasWrongTags(cmd))
                        CommandParser.ProcessCmd(cmd);
                    Console.WriteLine();
                }
            }
        }

        private static void OneProcLine(string line)
        {
            
        }
        private static void OneCorrCmndLine(string line)
        {
            try
            {
                Console.WriteLine(line);
                var cmnd = CommandParser.ToCmdParams(line);
                CommandParser.HasWrongTags(cmnd);
            }
            catch (ArgumentException ex)
            { Console.WriteLine(ex.Message); }
        }

        private static void OneStrToArr(string line)
        {
            Console.WriteLine(line);
            try
            {
                var ar = CommandParser.ToCmdParams(line);
                PrintArr(ar);
            }
            catch (ArgumentException ex)
            { Console.WriteLine(ex.Message); }
        }
        public static void PrintArr(string[] arr)
        {
            foreach (var el in arr)
                Console.WriteLine(el);
            Console.WriteLine();
        }
    }
}
