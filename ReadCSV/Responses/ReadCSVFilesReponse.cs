using System.Collections.Generic;
using ReadCSV.Contracts;
using ReadCSV.Contracts.Models;

namespace ReadCSV.Responses
{
    /// <summary>
    /// ReadCSVFilesResponse
    /// </summary>
    public class ReadCSVFilesReponse : IReadCSVFilesResponse
    {
        public IEnumerable<DisplayRecord> Records { get; set; }
    }
}
