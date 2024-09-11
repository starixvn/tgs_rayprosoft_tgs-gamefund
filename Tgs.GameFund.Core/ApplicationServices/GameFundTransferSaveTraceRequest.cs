using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

public record class GameFundTransferSaveTraceRequest(
    Guid GameProviderId,
    Guid WalletId,
    string PlayerCode) : IRequest;
