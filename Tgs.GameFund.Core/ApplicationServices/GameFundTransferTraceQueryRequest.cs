using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

public record class GameFundTransferTraceQueryRequest(
	string GameProviderCode,
	string? BrandCode = null,
	DateTimeOffset? StartTime = null,
	DateTimeOffset? EndTime = null)
	: IStreamRequest<string>;
