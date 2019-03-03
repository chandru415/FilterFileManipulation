using System;
using System.IO;
using System.Linq;
using System.Text;

namespace FFManipulation
{
    /// <summary>
    /// T: EFActions
    /// </summary>
    public static class EFActions
    {
        static int count = 0;
#if FRAMECORE
        /**<summary>
            Perfrom Operations like COPY, MOVE, DELETE To a Specific File-Extension Files.
            <para><b>destinationPath</b> is optional.Option has value like: 1 - COPY, 2 - MOVE, 3 - DELETE</para>
            <param name = "option" > T:int, 1 - COPY, 2 - MOVE, 3 - DELETE</param>
            <param name = "sourcePath" ></param>
            <param name= "extension" ></param>
            <param name= "destinationPath" ></param>
        <returns>
            Operation Status, File Count as Tuple.
            T: CallResult for Framework 4.0 to 4.6
       </returns>
       <exception cref = "System.IO.IOException" >
            T:System.IO.IOException:
               An I/O error occurred.
        </exception>
        </summary>
      */
        public static CallResult FileManipulation(int option, string sourcePath, string extension, string destinationPath = null)
        {
#else

        /**<summary>
              Perfrom Operations like COPY, MOVE, DELETE To a Specific File-Extension Files.
              <para><b>destinationPath</b> is optional.Option has value like: 1 - COPY, 2 - MOVE, 3 - DELETE</para>
              <param name = "option" > T:int, 1 - COPY, 2 - MOVE, 3 - DELETE</param>
              <param name = "sourcePath" ></param>
              <param name= "extension" ></param>
              <param name= "destinationPath" ></param>
          <returns>
              Operation Status, File Count as Tuple.
              T: CallResult for Framework 4.0 to 4.6
         </returns>
         <exception cref = "System.IO.IOException" >
              T:System.IO.IOException:
                 An I/O error occurred.
          </exception>
          </summary>
        */
        public static (bool, int) FileManipulation(int option, string sourcePath, string extension, string destinationPath = null)
        {
#endif
            if (Validate(option, sourcePath, extension, destinationPath))
            {
                try
                {
                    switch (option)
                    {
                        // COPY 
                        case 1:
                            Directory.GetFiles(sourcePath, "*." + extension + "").ToList().ForEach(file =>
                            {
                                File.Copy(file, destinationPath + "\\" + Path.GetFileName(file), true);
                                count++;
                            });
                            Logger.Log(Logger.ErrorLevel.INFO, new StringBuilder().Append($"Files copied from {sourcePath} with extention: {extension} to {destinationPath} and file count: {count}").ToString());
                            break;
                        // MOVE 
                        case 2:
                            foreach (var file in Directory.GetFiles(sourcePath, "*." + extension + ""))
                            {
                                if (!IsFileInUse(file))
                                {
                                    if (File.Exists(destinationPath + "\\" + Path.GetFileName(file)))
                                    {

                                        File.Replace(file, destinationPath + "\\" + Path.GetFileName(file), destinationPath + "\\" + count + ".bac", false);
                                        File.Delete(destinationPath + "\\" + count + ".bac");
                                        count++;
                                    }
                                    else
                                    {
                                        File.Move(file, destinationPath + "\\" + Path.GetFileName(file));
                                        count++;
                                    }

                                }
                                else
                                {
                                    continue;
                                }
                            }
                            Logger.Log(Logger.ErrorLevel.INFO, new StringBuilder().Append($"Files moved from {sourcePath} with extention: {extension} to {destinationPath} and file count: {count}").ToString());
                            break;
                        // DELETE 
                        default:
                            foreach (var file in Directory.GetFiles(sourcePath, "*." + extension + ""))
                            {
                                if (!IsFileInUse(file))
                                {
                                    File.Delete(file);
                                    count++;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            Logger.Log(Logger.ErrorLevel.INFO, new StringBuilder().Append($"Files deleted from {sourcePath} and file count: {count}").ToString());
                            break;
                    }
#if FRAMECORE
                    return new CallResult()
                    {
                        Status = true,
                        Count = count

                    };
#else
                    return (true, count);
#endif
                }
                catch (Exception ex)
                {
                    Logger.Log(Logger.ErrorLevel.ERROR, new StringBuilder().Append($"{ex.Message} {ex.StackTrace}").ToString());
#if FRAMECORE
                    return new CallResult()
                    {
                        Status = false,
                        Count = count

                    };
#else
                    return (false, count);
#endif
                }
            }
            else
            {
                Logger.Log(Logger.ErrorLevel.ERROR, "Proccess input was due invalid parameters suppiled");
#if FRAMECORE
                return new CallResult()
                {
                    Status = false,
                    Count = count

                };
#else
                return (false, count);
#endif
            }
        }
        /// <summary>
        /// Validate Source, Destination Paths Are Exists or Not. And Files With Provided Extenstion
        /// </summary>
        /// <param name="option">T:int, 1 - COPY, 2 - MOVE, 3 - DELETE</param>
        /// <param name="sourcePath">String</param>
        /// <param name="extension">String</param>
        /// <param name="destinationPath">String</param>
        /// <returns></returns>
        public static bool Validate(int option, string sourcePath, string extension, string destinationPath = null)
        {
            if (Directory.Exists(sourcePath) && option > 2)
            {
                return Directory.GetFiles(sourcePath, "*." + extension + "").Length > 0;
            }
            else if (Directory.Exists(sourcePath) && Directory.Exists(destinationPath))
            {
                return Directory.GetFiles(sourcePath, "*." + extension + "").Length > 0;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks Wheater Proccessing File Is In Use or Not
        /// </summary>
        /// <param name="filePath">string path of the File</param>
        /// <returns>boolean</returns>
        public static bool IsFileInUse(string filePath)
        {
            try
            {
                var fileInfo = new FileInfo(filePath).Open(FileMode.Open);
#if HCORE
                fileInfo.Dispose();
#else
                fileInfo.Close();
#endif
                return false;
            }
            catch (IOException ex)
            {
                Logger.Log(Logger.ErrorLevel.ERROR, new StringBuilder().Append($"{filePath} {ex.Message} {ex.StackTrace}").ToString());
                return true;
            }
        }
    }
}
