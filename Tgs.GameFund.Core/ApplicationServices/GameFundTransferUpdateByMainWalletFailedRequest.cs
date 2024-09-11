using System.Text.Json.Nodes;
using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

public record GameFundTransferUpdateByMainWalletFailedRequest(
	Guid Id,
	string Description,
	JsonNode AdditionalData) : IRequest;
