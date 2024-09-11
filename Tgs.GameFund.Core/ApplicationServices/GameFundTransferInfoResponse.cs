namespace Tgs.GameFund.Core.ApplicationServices;
public record GameFundTransferInfoResponse(
	Guid Id,
	Guid GameProviderId,
	Guid WalletId,
	string PlayerCode,
	string WalletFrom,
	string WalletTo,
	Common.Enums.GameFundTransferSource Source,
	Common.Enums.GameFundTransferStatus Status,
	string CashTransactionId,
	Guid? CashRespTransactionId,
	string GameTransactionId,
	decimal Amount,
	DateTimeOffset CreatedOn,
	DateTimeOffset UpdatedOn);
