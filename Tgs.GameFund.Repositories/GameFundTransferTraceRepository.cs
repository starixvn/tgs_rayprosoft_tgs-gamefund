using System.Runtime.CompilerServices;
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

	public async IAsyncEnumerable<string> QueryPlayerCode(string gameProviderCode, string brandCode, DateTimeOffset? startTime, DateTimeOffset? endTime, [EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		var baseQuery = context.GameFundTransferTraces
			.Join(
				inner: context.GameProviders,
				outerKeySelector: t => t.GameProviderId,
				innerKeySelector: gp => gp.Id,
				resultSelector: (t, gp) => new { t, gp })
			.Join(
				inner: context.Players,
				outerKeySelector: tg => tg.t.PlayerCode,
				innerKeySelector: p => p.PlayerCode,
				(tg, p) => new { tg.t, tg.gp, p })
			.Join(
				inner: context.Brands,
				outerKeySelector: tgp => tgp.p.BrandId,
				innerKeySelector: b => b.Id,
				resultSelector: (tgp, b) => new { tgp.t, tgp.gp, b })
			.Where(x => x.b.Code == brandCode && x.gp.Code == gameProviderCode);

		if (startTime is DateTimeOffset start)
			baseQuery = baseQuery.Where(x => x.t.UpdatedOn >= start);

		if (endTime is DateTimeOffset end)
			baseQuery = baseQuery.Where(x => x.t.UpdatedOn < end);

		var query = baseQuery
			.Select(x => x.t.PlayerCode)
			.AsAsyncEnumerable()
			.WithCancellation(cancellationToken);
		await foreach (var playerCode in query)
			yield return playerCode;
	}

	public async IAsyncEnumerable<string> QueryPlayerCode(string gameProviderCode, DateTimeOffset? startTime, DateTimeOffset? endTime, [EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		var baseQuery = context.GameFundTransferTraces
			.Join(
				inner: context.GameProviders,
				outerKeySelector: t => t.GameProviderId,
				innerKeySelector: gp => gp.Id,
				resultSelector: (t, gp) => new { t, gp })
			.Where(x => x.gp.Code == gameProviderCode);

		if (startTime is DateTimeOffset start)
			baseQuery = baseQuery.Where(x => x.t.UpdatedOn >= start);

		if (endTime is DateTimeOffset end)
			baseQuery = baseQuery.Where(x => x.t.UpdatedOn < end);

		var query = baseQuery
			.Select(x => x.t.PlayerCode)
			.AsAsyncEnumerable()
			.WithCancellation(cancellationToken);
		await foreach (var playerCode in query)
			yield return playerCode;
	}
}
