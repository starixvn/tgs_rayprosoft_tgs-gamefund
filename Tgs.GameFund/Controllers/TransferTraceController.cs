using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tgs.GameFund.Common.ViewModels;
using Tgs.GameFund.Core.ApplicationServices;

namespace Tgs.GameFund.Controllers;

[Route("[controller]")]
[ApiController]
public class TransferTraceController : ControllerBase
{
	[HttpPut("trace/save")]
	public async ValueTask GameFundTransferTraceSaveAsync(
		[FromServices] IMediator mediator,
		[FromBody] GameFundTransferTraceSaveViewModel source)
	{
		await mediator.Send(
			request: new GameFundTransferTraceSaveRequest(
				GameProviderId: source.GameProviderId,
				WalletId: source.WalletId,
				PlayerCode: source.PlayerCode),
			cancellationToken: HttpContext.RequestAborted)
			.ConfigureAwait(false);
	}
}
