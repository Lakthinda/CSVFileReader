using System.Collections.Generic;
using System.Text;

namespace ReadCSV.Contracts.Models
{
    public class DisplayRecord
    {
        public IEnumerable<Record> Records { get; set; }
        public double Median { get; set; }
        public string FileName { get; set; }

        public override string ToString() {
            StringBuilder stringBuilder = new StringBuilder();
            foreach(var record in Records)
            {
                stringBuilder.Append(string.Format("{0} {1} {2} {3}",
                                     FileName,
                                     record.DateTime,
                                     record.Value,
                                     Median));
                
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }
    }
}
