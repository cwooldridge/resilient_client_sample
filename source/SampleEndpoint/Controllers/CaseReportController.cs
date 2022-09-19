using Microsoft.AspNetCore.Mvc;

namespace SampleEndpoint.Controllers;

[ApiController]
[Route("case_report")]
public class CaseReportController : ControllerBase
{


    private readonly ILogger<CaseReportController> _logger;

    public CaseReportController(ILogger<CaseReportController> logger)
    {
        _logger = logger;
    }

      
    [HttpPost]
    public  ActionResult<CaseMessage> PostCaseMessage(CaseMessage caseMessage)
    {
        _logger.LogInformation("Received Case " + caseMessage);
        return CreatedAtAction(nameof(PostCaseMessage), new { id = caseMessage.CaseId }, caseMessage);
    }
}