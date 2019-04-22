using ReadCSV.Contracts.Models;
using System.Collections.Generic;

namespace ReadCSV.Services
{
    public interface ICSVFileService
    {
        /// <summary>
        /// Returns DisplayRecord result from given list
        /// </summary>
        /// <param name="lineArray"></param>
        /// <param name="fileName"></param>
        /// <param name="percentage"></param>
        /// <param name="dateTimeIndex"></param>
        /// <param name="dataValueIndex"></param>
        /// <param name="skipRows"></param>
        /// <returns></returns>
        DisplayRecord GetRecords(IEnumerable<string[]> lineArray, string fileName,int percentage, int dateTimeIndex, int dataValueIndex, int skipRows = 1);

        /// <summary>
        /// Returns an array of strings after reading CSV files
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="percentage"></param>
        /// <returns></returns>
        IEnumerable<DisplayRecord> ReadCSVFiles(string folderPath, int percentage);
    }
}
