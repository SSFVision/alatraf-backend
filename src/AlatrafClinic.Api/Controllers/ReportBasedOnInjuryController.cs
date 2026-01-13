using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AlatrafClinic.Application.Common.Models;
using AlatrafClinic.Application.Reports.CountableReports.Dtos;
using AlatrafClinic.Application.Reports.CountableReports.ReportBasedOnInjury;

namespace AlatrafClinic.Api.Controllers;

[Route("api/v{version:apiVersion}/reports/injury")]
[ApiVersion("1.0")]
public sealed class ReportBasedOnInjuryController(ISender sender) : ApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ReportBasedOnInjuryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieve injury-based report (paged).")]
    [EndpointDescription("Returns grouped injury report aggregated by injury detail and department/service.")]
    [EndpointName("GetReportBasedOnInjury")]
    [MapToApiVersion("1.0")]
    public async Task<IActionResult> Get(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] DateOnly? from = null,
        [FromQuery] DateOnly? to = null,
        [FromQuery] ReportBasedOnInjury? basedOn = null,
        CancellationToken ct = default)
    {
        var query = new ReportBasedOnInjuryQuery(pageNumber, pageSize, from, to, basedOn);
        var result = await sender.Send(query, ct);
        return Ok(result);
    }
}
