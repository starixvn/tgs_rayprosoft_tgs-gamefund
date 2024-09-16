using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

internal class GameFundTransferTraceBrandQueryService(
	IGameFundTransferTraceRepository repository)
	: IStreamRequestHandler<GameFundTransferTraceBrandQueryRequest, string>
{
	public IAsyncEnumerable<string> Handle(GameFundTransferTraceBrandQueryRequest request, CancellationToken cancellationToken)
		=> repository
			.QueryPlayerCode(
				gameProviderCode: request.GameProviderCode,
				brandCode: request.BrandCode,
				cancellationToken: cancellationToken);
}
