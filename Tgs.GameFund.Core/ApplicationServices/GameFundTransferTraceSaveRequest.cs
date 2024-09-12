using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

public record class GameFundTransferTraceSaveRequest(
	Guid GameProviderId,
	Guid WalletId,
	string PlayerCode) : IRequest;
