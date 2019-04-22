using Moq;
using ReadCSV.Contracts.Models;
using ReadCSV.Requests;
using ReadCSV.Responses;
using ReadCSV.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ReadCSV.Test
{
    public class ServiceManagerTest
    {
        private readonly ServiceManager sut;
        private readonly Mock<ICSVFileService> cSVFileService;

        public ServiceManagerTest()
        {
            cSVFileService = new Mock<ICSVFileService>();
            sut = new ServiceManager(cSVFileService.Object);
        }

        private readonly ReadCSVFilesRequest request = new ReadCSVFilesRequest
        {
            FolderPath = "AnyPath",
            Percentage = 0
        };

        [Fact]        
        public void DisplayRecords_Throws_Exception_When_Reading_File()
        {
            // Arrange
            cSVFileService.Setup(s => s.ReadCSVFiles(It.IsAny<string>(), It.IsAny<int>()))
                          .Throws(new Exception("Error reading the file"));
           
            // Act
            // Assert
            Assert.Throws<Exception>(() => sut.DisplayRecords(request));
        }

        [Fact]
        public void DisplayRecords_Returns_Correct_Results()
        {
            // Arrange
            var displayRecordsList = GetDisplayRecords();
            
            cSVFileService.Setup(s => s.ReadCSVFiles(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(displayRecordsList);

            // Act
            var result = sut.DisplayRecords(request);

            // Assert
            Assert.IsType<ReadCSVFilesReponse>(result);
            Assert.True(result != null);
            Assert.True(displayRecordsList.ToList().Count() == result.Records.ToList().Count());
            Assert.Equal(displayRecordsList.Select(s => s.ToString()).FirstOrDefault(), 
                         result.Records.Select(s => s.ToString()).FirstOrDefault());
        }

        private IEnumerable<DisplayRecord> GetDisplayRecords()
        {
            var record = new Contracts.Models.Record
            {
                Value = "10.50",
                DateTime = "2019-04-21"
            };
            var recordList = new List<Contracts.Models.Record>();
            recordList.Add(record);
            var displayRecords = new DisplayRecord
            {
                Records = recordList,
                FileName = "Test File Name.csv",
                Median = 9.5
            };
            var displayRecordsList = new List<DisplayRecord>();
            displayRecordsList.Add(displayRecords);

            return displayRecordsList;
        }

        
    }
}
