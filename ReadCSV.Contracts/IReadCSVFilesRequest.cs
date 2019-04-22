namespace ReadCSV.Contracts
{
    public interface IReadCSVFilesRequest
    {
        string FolderPath { get; set; }
        int Percentage { get; set; }
    }
}
