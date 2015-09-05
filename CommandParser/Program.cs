using System;
using System.Collections.Generic;
using System.Linq;


namespace CommandParser
{
    public class CommandParser
    {
        static List<string> Tags = new List<string>() { "help", "k", "ping", "print" };
        static void Main(string[] args)
        {
            //test
            /*TestFunc();
            Console.ReadLine();
            return;
            */

            Console.WriteLine("I wish to be a part of study group");
            if (args != null)
            {
                if (!HasWrongTags(args))
                    ProcessCmd(args);
            }
            while (true)
            {
                var line = Console.ReadLine();
                string[] cmd = null;
                try
                {
                    cmd = ToCmdParams(line);
                    if (!HasWrongTags(cmd))
                        ProcessCmd(cmd);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static string[] ToCmdParams(string line)
        {
            string[] cmdParams = new string[0];
            List<string> list = new List<string>();
            line = line.Trim();
            var li = 0; // line index
            while(li < line.Length)
            {
                try {
                    while (line[li] == ' ')
                        li++;
                    if (line[li] == '\"')
                    {
                        var le = line.IndexOf('\"', li + 1);
                        if (le == -1)
                            throw new Exception();
                        list.Add(line.Substring(li, le - li+1)); 
                        li = le + 1;
                    }
                    else
                    {
                        var le = line.IndexOf(' ', li + 1);
                        if (le == -1)
                            le = line.Length;
                        list.Add(line.Substring(li, le - li));
                        li = le + 1;
                    }
                }
                catch(Exception ex)
                { throw new ArgumentException("Unexpected arguments. Use /? to see set of allowed commands and arguments"); }
            }
            return list.ToArray();
        }

        public static bool HasWrongTags(string[] cmdParams)
        {
            foreach (var p in cmdParams)
            {
                if (p.StartsWith("-"))
                {
                    if (!Tags.Contains(p.Substring(1)))
                    {
                        Console.WriteLine(String.Format("Command \"{0}\" is not supported, use /? to see set of allowed commands", p.Substring(1)));
                        return true;
                    }
                }
            }
            return false;
        }

 
        public static void ProcessCmd(string[] cmdParams)
        {
            bool cmndPresent = false;
            if (cmdParams.Contains("/?") || cmdParams.Contains("-help") || (cmdParams.Contains("/h")))
            {
                CmndPrintHelp();
                return;
            }
            
            var i = 0;
            try
            {
                while (i < cmdParams.Count())
                {
                    if (cmdParams[i].StartsWith("-"))
                    {
                        var key = cmdParams[i].Substring(1);
                        switch (key)
                        {
                            case "ping":
                                CmndPing();
                                i++;
                                break;
                            case "print":
                                i++;
                                List<string> pParams = GetCmndArgs(cmdParams, i);
                                CmndPrintMessage(pParams);
                                i = i + pParams.Count();
                                break;
                            case "k":
                                i++;
                                List<string> kParams = GetCmndArgs(cmdParams, i);
                                CmndK(kParams);
                                i = i + kParams.Count();
                                break;
                        }
                    }
                    else throw new ArgumentException("Unexpected arguments. Use /? to see set of allowed commands and arguments");

                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static List<string> GetCmndArgs(string[] cmndLine, int pos)
        {
            List<string> args = new List<string>();

            while (pos < cmndLine.Length && !(cmndLine[pos]).StartsWith("-"))
            {
                args.Add(cmndLine[pos]);
                pos++;
            }
            return args;
        }

        static void CmndPrintHelp()
        {
            Console.WriteLine("Commands to use [-k key value] [-ping] [-print message] [-help]");
            Console.WriteLine("-k key value - to print a table, each row contains key - value");
            Console.WriteLine("-ping - to beep and print \"Pinging...\"");
            Console.WriteLine("-print message - to print message");
            Console.WriteLine("-help /h /? - to print this help message");
        }
        static void CmndPing()
        {
            Console.Beep(300, 300);
            Console.WriteLine("Pinging...");
        }
        static void CmndPrintMessage(List<string> message)
        {
            if (message.Count == 0)
                throw new ArgumentException("Error: No arguments for command -print. Use /? to see set of allowed commands and arguments");
            if (message.Count == 1)
            {
                Console.WriteLine(message[0].Trim('"'));
                return;
            }
            for (int i = 0; i < message.Count - 1; i++)
                Console.Write(message[i] + " ");
            Console.WriteLine(message[message.Count - 1]);
        }
        static void CmndK(List<string> list)
        {
            if (list.Count == 0)
                throw new ArgumentException("Error: Incorrect input arguments for command -k");
            if (list.Count % 2 == 0)
                for (int i = 0; i < list.Count / 2; i++)
                    Console.WriteLine(String.Format("{0} - {1}", list[2 * i].Trim('"'), list[2 * i + 1].Trim('"')));
            else
            {
                for (int i = 0; i < list.Count / 2; i++)
                    Console.WriteLine(String.Format("{0} - {1}", list[2 * i].Trim('"'), list[2 * i + 1].Trim('"')));
                Console.WriteLine(String.Format("{0} - null", list[list.Count() - 1].Trim('"')));
            }
        }

        static void TestFunc()
        {
            //Test.StringToCmnParamArrayTest();
            //Test.CorrectCmndLineTest();
            Test.ProcTest();
        }
    }
}
