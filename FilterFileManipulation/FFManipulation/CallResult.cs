using System;
using System.Collections.Generic;
using System.Text;

namespace FFManipulation
{
    /// <summary>
    /// return type to File manipulation
    /// </summary>
    public class CallResult
    {
        /// <summary>
        /// returns true if operation is successfull else false for failed
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// returns number of files proccessed
        /// </summary>
        public int Count { get; set; }
    }
}
