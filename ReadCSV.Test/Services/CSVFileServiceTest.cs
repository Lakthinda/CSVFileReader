using Microsoft.Extensions.Options;
using Moq;
using ReadCSV.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ReadCSV.Test.Services
{
    public class CSVFileServiceTest
    {
        private readonly ICSVFileService sut;
        private readonly Mock<IOptions<AppSettings>> options;

        public CSVFileServiceTest()
        {
            options = new Mock<IOptions<AppSettings>>();
            options.Setup(s => s.Value)
                   .Returns(new AppSettings());
            sut = new CSVFileService(options.Object);
        }
                
        [Fact]
        public void CSVFileService_With_Null_Config_Throws_Exception()
        {
            // Arrange

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new CSVFileService(null));
        }

        [Fact]
        public void CSVFileService_ReadCSVFiles_With_Incorrect_FolderPath_Throws_Exception()
        {
            // Arrange
            var folderPath = "wrongFolderPath";
            var percentage = 10;

            // Act

            // Assert
            Assert.Throws<Exception>(() => sut.ReadCSVFiles(folderPath, percentage));
        }

        [Fact]
        public void CSVFileService_GetRecords_Correct_Params_Return_Correct_Results()
        {
            // Arrange
            var lineArray = GetLPList();
            var fileName = "testFilve.csv";
            var percentage = 10;
            int dateTimeIndex = 3;
            int valueIndex = 5;


            // Act
            var result = sut.GetRecords(lineArray,
                                        fileName,
                                        percentage,
                                        dateTimeIndex,
                                        valueIndex,
                                        1);

            // Assert
            Assert.True(result.Records.ToList().Count() == 2); // Expected no of Records = 2
            Assert.True(result.Median == 0); // Expected median = 0
            Assert.Equal(result.FileName, fileName);
        }

        private IEnumerable<string[]> GetLPList()
        {
            List<string[]> LPList = new List<string[]>();

            string[] strArr1 = new string[]{
                "MeterPoint Code",
                "Serial Number",
                "Plant Code",
                "Date/Time",
                "Data Type",
                "Data Value",
                "Units",
                "Status"
            };

            string[] strArr2 = new string[]{
                "",
                "",
                "",
                "31/08/2015 00:45:00",
                "",
                "10.000000",
                "",
                ""
            };

            string[] strArr3 = new string[]{
                "",
                "",
                "",
                "30/08/2015 00:45:00",
                "",
                "-10.000000",
                "",
                ""
            };

            LPList.Add(strArr1);
            LPList.Add(strArr2);
            LPList.Add(strArr3);

            return LPList;
        }
    }
}
