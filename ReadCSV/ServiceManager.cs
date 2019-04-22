using ReadCSV.Contracts;
using ReadCSV.Responses;
using ReadCSV.Services;

namespace ReadCSV
{
    /// <summary>
    /// Microservice - Consumer    
    /// </summary>
    public class ServiceManager
    {
        private readonly ICSVFileService cSVFileService;
        
        public ServiceManager(ICSVFileService cSVFileService)
        {
            this.cSVFileService = cSVFileService;            
        }

        /// <summary>
        /// Returns IReadCSVFilesResponse result after receiving IReadCSVFilesRequest 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IReadCSVFilesResponse DisplayRecords(IReadCSVFilesRequest request)
        {
            var displayRecords = cSVFileService.ReadCSVFiles(request.FolderPath,request.Percentage);

            return new ReadCSVFilesReponse
            {
                Records = displayRecords
            };
        }
    }
}
