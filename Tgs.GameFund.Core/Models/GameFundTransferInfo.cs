namespace Tgs.GameFund.Core.Models;
public record GameFundTransferInfo(
	Guid Id,
	Guid GameProviderId,
	Guid WalletId,
	string PlayerCode,
	string WalletFrom,
	string WalletTo,
	Common.Enums.GameFundTransferSource Source,
	Common.Enums.GameFundTransferStatus Status,
	string CashTransactionId,
	string GameTransactionId,
	decimal Amount,
	DateTimeOffset CreatedOn,
	DateTimeOffset UpdatedOn);
