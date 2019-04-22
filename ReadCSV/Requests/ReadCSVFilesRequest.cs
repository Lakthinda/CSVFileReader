using ReadCSV.Contracts;

namespace ReadCSV.Requests
{
    /// <summary>
    /// ReadCSVFilesRequest
    /// </summary>
    public class ReadCSVFilesRequest : IReadCSVFilesRequest
    {
        public string FolderPath { get; set; }
        public int Percentage { get; set; }
    }
}
