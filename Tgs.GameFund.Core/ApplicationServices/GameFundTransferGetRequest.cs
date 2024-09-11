using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;
public record GameFundTransferGetRequest(
	Guid Id) : IRequest<GameFundTransferInfoResponse?>;
