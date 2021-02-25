using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class FileResult : IFileResult
    {
        public FileResult(bool success)
        {
            Success = success;
        }
        public FileResult(bool success, string shortPath, string fullPath, string fileName) : this(success)
        {
            ShortPath = shortPath;
            FullPath = fullPath;
            FileName = fileName;
        }

        public string ShortPath { get; }
        public string FullPath { get; }
        public string FileName { get; }
        public bool Success { get; }
    }
}
