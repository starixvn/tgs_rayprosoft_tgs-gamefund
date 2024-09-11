using System.Text.Json.Nodes;
using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;
public record GameFundTransferUpdateBySuccessRequest(
	Guid Id,
	Guid CashRespTransactionId,
	string GameRespTransactionId,
	decimal MainWalletBalance,
	string Description,
	JsonNode AdditionalData) : IRequest;
