namespace Core.Utilities.Results
{
    public interface IFileResult
    {
        public string ShortPath { get; }
        public string FullPath { get; }
        public string FileName { get; }
        public bool Success { get; }
    }
}
