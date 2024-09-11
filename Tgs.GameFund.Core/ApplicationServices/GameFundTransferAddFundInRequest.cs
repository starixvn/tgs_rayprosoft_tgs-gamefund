using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

public record GameFundTransferAddFundInRequest(
	Guid Id,
	Guid GameProviderId,
	Guid WalletId,
	string PlayerCode,
	string WalletFrom,
	string WalletTo,
	string CashTransactionId,
	string GameTransactionId,
	decimal Amount) : IRequest<GameFundTransferInfoResponse>;
