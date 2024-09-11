namespace Tgs.GameFund.Core.Models;
public record GameFundTransferAdd(
	Guid Id,
	Guid GameProviderId,
	Guid WalletId,
	string PlayerCode,
	string WalletFrom,
	string WalletTo,
	Common.Enums.GameFundTransferSource Source,
	string CashTransactionId,
	string GameTransactionId,
	decimal Amount);
