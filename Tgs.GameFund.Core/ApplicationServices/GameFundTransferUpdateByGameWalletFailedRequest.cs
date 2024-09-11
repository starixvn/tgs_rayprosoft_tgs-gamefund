using System.Text.Json.Nodes;
using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

public record GameFundTransferUpdateByGameWalletFailedRequest(
	Guid Id,
	string Description,
	JsonNode AdditionalData) : IRequest;
