using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ReadCSV.Contracts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReadCSV.Services
{
    /// <summary>
    /// CSVFilesService - Read both CSV File types and process accordingly
    /// </summary>
    public class CSVFileService : ICSVFileService
    {
        private readonly int LPValueIndex;
        private readonly int LPDateTimeIndex;
        private readonly int TOUValueIndex;
        private readonly int TOUDateTimeIndex;
        private readonly AppSettings appSettings;

        public CSVFileService(IOptions<AppSettings> options)
        {
            if (options == null || options.Value == null)
            {
                throw new ArgumentNullException("Error retrieving AppSettings.");
            }

            appSettings = options.Value;            

            LPDateTimeIndex = appSettings.LPDateTimeIndex;
            LPValueIndex = appSettings.LPValueIndex;
            TOUDateTimeIndex = appSettings.TOUDateTimeIndex;
            TOUValueIndex = appSettings.TOUValueIndex;
        }

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
        public DisplayRecord GetRecords(IEnumerable<string[]> lineArray, string fileName,int percentage, int dateTimeIndex, int dataValueIndex, int skipRows = 1)
        {
            var DataValueArray = lineArray.Select(l => new Record { DateTime = l[dateTimeIndex], Value = l[dataValueIndex] }).Skip(skipRows).ToArray();

            var total = DataValueArray.Select(s => s.Value.TryGetDouble()).Sum();
            double median = total / DataValueArray.Length;

            var records = DataValueArray.Where(s => s.Value.TryGetDouble() > (median * (1+ percentage/100)) || 
                                                    s.Value.TryGetDouble() < (median * (1 - percentage / 100)));
            
            return new DisplayRecord
            {
                Records = records,
                Median = median,
                FileName = fileName
            };
        }

        /// <summary>
        /// Returns an array of strings after reading CSV files
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="percentage"></param>
        /// <returns></returns>
        public IEnumerable<DisplayRecord> ReadCSVFiles(string folderPath, int percentage)
        {            
            List<DisplayRecord> displayRecordList = new List<DisplayRecord>();
            IEnumerable<string[]> lineArray = new List<string[]>();
            DisplayRecord displayRecord = new DisplayRecord();

            try
            {
                // Take a snapshot of the file system.  
                DirectoryInfo dir = new DirectoryInfo(folderPath);

                // This method assumes that the application has discovery permissions  
                // for all folders under the specified path.  
                IEnumerable<FileInfo> fileList = dir.GetFiles("*.csv", SearchOption.AllDirectories);
            

                foreach(var fileInfo in fileList)
                {
                    // Read the file
                    lineArray = File.ReadLines(fileInfo.FullName)
                        .Select(a => a.Split(';'))
                        .SelectMany(lines => lines)
                        .Select(l => l.Split(','));

                    // Note: Both LP and TOU files Date/Time and Data Value / Energy column indexes are the same
                    // But following logic is used assuming that column indexes differ in two files.
                    if (fileInfo.Name.StartsWith("LP", System.StringComparison.OrdinalIgnoreCase))
                    {
                        // Get DataValueIndex = 5 & DateTime index = 3
                        displayRecord = GetRecords(lineArray, fileInfo.Name, percentage, LPDateTimeIndex, LPValueIndex);
                    }
                    else
                    {
                        // Get EnergyIndex = 5 & DateTime index = 3
                        displayRecord = GetRecords(lineArray, fileInfo.Name, percentage, TOUDateTimeIndex, TOUValueIndex);
                    }

                    displayRecordList.Add(displayRecord);
                }
            }
            catch (Exception e)
            {                
                throw new Exception("Error reading the file",e);
            }

            return displayRecordList;
        }
    }
}
