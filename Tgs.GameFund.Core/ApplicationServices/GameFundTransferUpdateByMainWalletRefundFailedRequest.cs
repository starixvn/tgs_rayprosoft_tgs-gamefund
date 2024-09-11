using System.Text.Json.Nodes;
using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;
public record GameFundTransferUpdateByMainWalletRefundFailedRequest(
	Guid Id,
	string Description,
	JsonNode AdditionalData) : IRequest;
