using ReadCSV.Contracts.Models;
using System.Collections.Generic;

namespace ReadCSV.Contracts
{
    public interface IReadCSVFilesResponse
    {
        IEnumerable<DisplayRecord> Records { get; set; }
    }
}
