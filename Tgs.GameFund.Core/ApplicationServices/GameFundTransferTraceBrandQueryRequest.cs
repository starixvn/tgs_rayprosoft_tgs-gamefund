using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

public record class GameFundTransferTraceBrandQueryRequest(
	string GameProviderCode,
	string BrandCode)
	: IStreamRequest<string>;
