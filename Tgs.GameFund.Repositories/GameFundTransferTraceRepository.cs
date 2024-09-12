using Microsoft.EntityFrameworkCore;
using Tgs.GameFund.Core;
using Tgs.GameFund.Entities;

namespace Tgs.GameFund.Repositories;

public class GameFundTransferTraceRepository(
	TgsContext context,
	TimeProvider timeProvider) : IGameFundTransferTraceRepository
{
	public async ValueTask SaveTraceAsync(Guid gameProviderId, Guid walletId, string playerCode, CancellationToken cancellationToken = default)
	{
		var systemTime = timeProvider.GetUtcNow();

		var entity = await context.GameFundTransferTraces
			.FirstOrDefaultAsync(
				e =>
					e.GameProviderId == gameProviderId &&
					e.WalletId == walletId &&
					e.PlayerCode == playerCode,
				cancellationToken)
			.ConfigureAwait(false);

		if (entity is null)
		{
			entity = new()
			{
				GameProviderId = gameProviderId,
				WalletId = walletId,
				PlayerCode = playerCode,
				CreatedOn = systemTime,
				UpdatedOn = systemTime
			};
			await context.GameFundTransferTraces
				.AddAsync(entity, cancellationToken)
				.ConfigureAwait(false);
		}
		else
		{
			entity.UpdatedOn = systemTime;
		}

		await context
			.SaveChangesAsync(cancellationToken)
			.ConfigureAwait(false);
	}

}
