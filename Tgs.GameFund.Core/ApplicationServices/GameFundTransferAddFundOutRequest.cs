using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

public record GameFundTransferAddFundOutRequest(
	Guid Id,
	Guid GameProviderId,
	Guid WalletId,
	string PlayerCode,
	string WalletFrom,
	string WalletTo,
	string CashTransactionId,
	string GameTransactionId,
	decimal Amount) : IRequest<GameFundTransferInfoResponse>;
