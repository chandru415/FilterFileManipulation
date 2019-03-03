using System;
using System.IO;
using System.Text;

namespace FFManipulation
{
    /// <summary>
    /// Log the details to Log file under C:\FFMLogs\Logs.txt
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Error Level Like Error, Info, Fatal, Warn
        /// </summary>
        public enum ErrorLevel
        {
            /// <summary>
            /// Error level - 0
            /// </summary>
            ERROR,
            /// <summary>
            /// Info level - 1
            /// </summary>
            INFO,
            /// <summary>
            /// Fatal level - 3
            /// </summary>
            FATAL,
            /// <summary>
            /// warn level - 3
            /// </summary>
            WARN
        }
        /// <summary>
        /// logs log into file
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        public static void Log(ErrorLevel level, string message)
        {
            try
            {
                var drive = Constants.logFile_Drive;
                var directory = Constants.logFile_Directory;
                var file = Constants.logFile_File;

                string directoryPathString = Path.Combine(drive + @":\", directory);
                string filePath = Path.Combine(directoryPathString + @"\", file);

                if (!Directory.Exists(directoryPathString))
                {
                    Directory.CreateDirectory(directoryPathString);
                }

                if (!File.Exists(filePath))
                {

#if HCORE
                    File.Create(filePath).Dispose();
#else
                    File.Create(filePath).Close();
#endif
                }
                using (StreamWriter logfile = File.AppendText(filePath))
                {
#if NET40
                    logfile.Write(String.Format("{0}\n", new StringBuilder().Append($"{level} {DateTime.Now} {message}").ToString()));
                    logfile.Write(Environment.NewLine);
#else
                    logfile.WriteAsync(String.Format("{0}\n", new StringBuilder().Append($"{level} {DateTime.Now} {message}").ToString()));
                    logfile.WriteAsync(Environment.NewLine);
#endif

                    logfile.Flush();
#if HCORE
                    logfile.Dispose();
#else
                    logfile.Close();
#endif
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
