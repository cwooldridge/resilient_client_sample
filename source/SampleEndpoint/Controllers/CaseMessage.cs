namespace SampleEndpoint.Controllers
{
    public class CaseMessage
    {
        public string CaseId { get; set; }
        public DateTime CastDateTime { get; set; }
        public Guid CorrelationId { get; set; }


        public override string ToString()
        {
            return $"{nameof(CaseId)}: {CaseId}, {nameof(CastDateTime)}: {CastDateTime}, {nameof(CorrelationId)}: {CorrelationId}";
        }
    }


}