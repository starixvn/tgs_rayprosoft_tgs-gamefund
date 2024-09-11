using System.Text.Json.Nodes;
using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;
public record GameFundTransferUpdateByMainWalletRefundSuccessRequest(
	Guid Id,
	Guid CashRespTransactionId,
	string GameRespTransactionId,
	decimal MainWalletBalance,
	string Description,
	JsonNode AdditionalData) : IRequest;
