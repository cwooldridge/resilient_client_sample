using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resilient_client_sample
{
    public class CaseMessage
    {
        public string CaseId { get; }
        public DateTime CastDateTime { get; }
        public Guid CorrelationId { get; }

        public CaseMessage(string caseId, DateTime castDateTime,Guid correlationId)
        {
            CaseId = caseId;
            CastDateTime = castDateTime;
            CorrelationId = correlationId;
        }

        public static CaseMessage MakeTestCase()
        {
    
            return new CaseMessage(DateTime.Now.Ticks.ToString(), DateTime.Now, Guid.NewGuid());

        }
    }


}
