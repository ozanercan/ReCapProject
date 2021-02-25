using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessFileResult : FileResult
    {
        public SuccessFileResult() : base(true)
        {
        }
        public SuccessFileResult(string shortPath, string fullPath, string fileName) : base(true, shortPath, fullPath, fileName)
        {
        }
    }
}
