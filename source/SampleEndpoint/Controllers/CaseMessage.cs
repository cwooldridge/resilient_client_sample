using System;

namespace SampleEndpoint.Controllers
{
    public class CaseMessage
    {
        public Guid CorrelationId { get; }
        public DateTime CreatedAt { get; }

        public int LineNumber { get; set; }
        public string ProductID { get; set; }
        public string CustomerId { get; set; }
        public string ContainerID { get; set; }
        public string LotNumber { get; set; }
        public double TareWeight { get; set; }
        public double NetWeight { get; set; }
        public double ModeWeight { get; set; }
        public int PrintMode { get; set; }
        public DateTime ProductionDate { get; set; }
        public long SerialNumber { get; set; }
        public long MasterCaseSerialNumber { get; set; }
        public long DailyCount { get; set; }
        public long StatusCode { get; set; }
        public long WeightStatus { get; set; }
        public string StationID { get; set; }
        public string UserID { get; set; }
        public int ProductionShift { get; set; }
        public string Barcode1 { get; set; }
        public bool OnSecondary { get; set; }
        public override string ToString()
        {
            return $"{nameof(SerialNumber)}: {SerialNumber}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(CorrelationId)}: {CorrelationId}";
        }
    }


}