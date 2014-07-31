using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;

namespace MSRA.SpringField.Components
{
    public static class GlobalHelper
    {
        /// <summary>
        /// Method to make sure that user's inputs are not malicious
        /// </summary>
        /// <param name="text">User's Input</param>
        /// <param name="maxLength">Maximum length of input</param>
        /// <param name="allowMutilSpace">Whether we allow multi space</param>
        /// <returns>The cleaned up version of the input</returns>
        public static string ClearInput(string text, int maxLength, bool allowMutilSpace)
        {
            text = text.Trim();
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            if (text.Length > maxLength)
                text = text.Substring(0, maxLength);
            if (!allowMutilSpace)
            { 
                text = Regex.Replace(text, "[\\s]{2,}", " ");	//two or more spaces       
            }
            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");	//<br>
            text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//&nbsp;
            text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);	//any other tags

            /**
             * What's the meaning of this statement?
             * This statement will add an additional ' to content(Bug #25987). So it was removed.
             */
            //text = text.Replace("'", "''");
            return text;
        }

        public static string FormatOutput(string rawString)
        {
            //rawString = rawString.Replace("  ", "&nbsp; ");
            //rawString = rawString.Replace("\n", "<br />");
            rawString = "<pre>" + rawString + "</pre>";
            return rawString;
        }

        public static List<string> FormatAlias(string alias)
        {
            List<string> aliasArr = new List<string>();

            alias = alias.Trim().ToLower();
            if(string.IsNullOrEmpty(alias))
            {
                return null;
            }

            alias = ClearInput(alias, 1024, false); //clear html
            alias = Regex.Replace(alias, "[\\s]{1,}", string.Empty); //clear space and break

            //string[] emailAlias = Regex.Split(alias, ";", RegexOptions.IgnorePatternWhitespace);
            string[] emailAlias = alias.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string curAlias in emailAlias)
            { 
                if(string.IsNullOrEmpty(curAlias))
                {
                    //throw new Exception("Illegal mail alias dected!");
                    break;
                }

                Regex regexEmail = new Regex(@"^([\w-]+(\.[\w-]+)*)@[\w-]+(\.[\w-]+)+$");
                Regex regexAlias = new Regex(@"^[\w-]+[^@:;'\+\{\}\(\)\^\#\!\%\&\*\\\|\`\~\[\]\,\.\?\/]$");
                
                if(regexEmail.IsMatch(curAlias) ^ regexAlias.IsMatch(curAlias))
                {
                    if (!regexEmail.IsMatch(curAlias))
                    {
                        aliasArr.Add(curAlias);
                    }
                    else
                    {
                        aliasArr.Add(regexEmail.Match(curAlias).Groups[1].Value);
                    }
                }
                else
                {
                    throw new Exception("Illegal mail alias detected!");
                }
            }

            return aliasArr;
            //"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$"
        }

        public static bool ValidateEmail(string mailStr)
        {
            Regex regexEmail = new Regex(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
            return regexEmail.IsMatch(mailStr);
        }

        public static string PasswordGenerator(int length, bool enableNum, bool enableLowercase, bool enableUppercase, bool enableSpecialChar, bool enableRepeat)
        {
            if (!enableNum && !enableLowercase && !enableUppercase && !enableSpecialChar)
            {
                throw new Exception("no sample series selected");
            }

            string numbers = "0123456789";
            string lowercase = "abcdefghijklmnopqrstuvwxyz";
            string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string specialChars = "_-<>*^;%/*$";
            StringBuilder sampleSeries = new StringBuilder(string.Empty);
            StringBuilder rndPwd = new StringBuilder(length);

            if (enableNum)
            {
                sampleSeries.Append(numbers);
            }

            if (enableLowercase)
            {
                sampleSeries.Append(lowercase);
            }

            if (enableUppercase)
            {
                sampleSeries.Append(uppercase);
            }

            if (enableSpecialChar)
            {
                sampleSeries.Append(specialChars);
            }

            //avoid the same seed
            Thread.Sleep(1);
            Random rndObj = new Random();

            if (length > sampleSeries.Length)
            {
                enableRepeat = true;
            }

            if (enableRepeat)
            {
                for (int i = 0; i < length; i++)
                {
                    int nextLoc = rndObj.Next(sampleSeries.Length);
                    rndPwd.Append(sampleSeries[nextLoc]);
                }
            }
            else
            {
                //if (length > sampleSeries.Length)
                //{
                //    throw new Exception("random string length should less than sample series length");
                //}

                for (int count = 0; count < (sampleSeries.Length * 2); count++)
                { 
                    int i = rndObj.Next(sampleSeries.Length);
                    int j = rndObj.Next(sampleSeries.Length);

                    //exchange sampleSeries[i] and sampleSeries[j]
                    char temp = sampleSeries[i];
                    sampleSeries[i] = sampleSeries[j];
                    sampleSeries[j] = temp;
                }

                char[] rndChars = new char[length];
                sampleSeries.CopyTo(0, rndChars, 0, length);
                rndPwd.Append(rndChars);
            }

            return rndPwd.ToString();
        }

        public static byte[] GetFileImage(string ResumePath)
        {
            // open the file
            FileStream Stream = new FileStream(ResumePath, FileMode.Open, FileAccess.Read);

            // create a buffer which can hold the whole file
            Byte[] FileData = new byte[Stream.Length];

            // read all the data into the buffer and close the stream
            Stream.Read(FileData, 0, Convert.ToInt32(Stream.Length));
            Stream.Close();

            return FileData;
        }
    }
}
