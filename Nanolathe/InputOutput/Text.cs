using Nanolathe.Models.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanolathe.InputOutput
{
    internal class Text
    {
        private const char HeaderOpenBracket = '[';
        private const char HeaderCloseBracket = ']';
        private const char SectionOpenBrace = '{';
        private const char SectionCloseBrace = '}';
        private const char Delimiter = '=';
        private const char EndStatement = ';';
        private static readonly char[] Whitespace = ['\r', '\n', '\t', ' '];

        public static void Read(string path, string name)
        {
            using (StreamReader rd = new StreamReader($"{path}\\{name}"))
            {
                while (!rd.EndOfStream)
                {
                    char nextChar = (char)rd.Read();
                    if (nextChar == HeaderOpenBracket)
                    {
                        Section newSec = ReadSection(rd);
                        Trace.WriteLine(newSec.ToString());
                    }
                }
            }
        }

        private static Section ReadSection(StreamReader rd)
        {
            string sectionHeader = ReadPortion(rd, HeaderCloseBracket);

            Dictionary<string, Section> subsections = new Dictionary<string, Section>();
            Dictionary<string, string> variables = new Dictionary<string, string>();
            bool running = true;
            while (running)
            {
                char nextChar = (char)rd.Read();
                if (Whitespace.Contains(nextChar) || nextChar == SectionOpenBrace)
                {
                    continue;
                }

                if (nextChar != SectionCloseBrace)
                {
                    if (nextChar == HeaderOpenBracket)
                    {
                        subsections.Add("", ReadSection(rd));
                    }
                    ReadSetting(rd, variables, nextChar);
                }
                else
                {
                    running = false;
                }
            }
            return new Section(variables, subsections, sectionHeader);
        }

        private static void ReadSetting(StreamReader rd, Dictionary<string, string> varis, char firstChar = '\0')
        {
            string setting = ReadPortion(rd, EndStatement, firstChar);
            string[] split = setting.Split(Delimiter);
            varis.Add(split[0], split[1]);
        }
        
        private static string ReadPortion(StreamReader rd, char endChar, char firstChar = '\0')
        {
            StringBuilder text = new StringBuilder();
            if (firstChar != '\0')
                text.Append(firstChar);
            bool running = true;
            while (running)
            {
                char nextChar = (char)rd.Read();
                if (nextChar != endChar)
                {
                    text.Append(nextChar);
                }
                else
                {
                    running = false;
                }
            }
            return text.ToString();
        }
    }
}
