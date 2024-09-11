using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Tgs.GameFund.Core;
using Tgs.GameFund.Entities;

namespace Tgs.GameFund.Repositories;

public class GameFundTransferRepository(
	TgsContext context,
	TimeProvider timeProvider) : IGameFundTransferRepository
{
	public async ValueTask AddAsync(Core.Models.GameFundTransferAdd source, CancellationToken cancellationToken = default)
	{
		var currentTime = timeProvider.GetUtcNow();
		var entity = new GameProviderFundTransferTransaction()
		{
			GameProviderFundTransferTransactionId = source.Id,
			GameProviderId = source.GameProviderId,
			WalletId = source.WalletId,
			GameProviderTransactionId = source.GameTransactionId,
			PlayerCode = source.PlayerCode,
			FundSource = (short)source.Source,
			FromWallet = source.WalletFrom,
			ToWallet = source.WalletTo,
			Amount = source.Amount,
			Status = (short)Common.Enums.GameFundTransferStatus.Processing,
			CreatedOn = currentTime.UtcDateTime,
			UpdatedOn = currentTime.UtcDateTime,
		};
		_ = await context.GameProviderFundTransferTransactions.AddAsync(
			entity: entity,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		_ = await context.SaveChangesAsync(
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);
	}

	public async ValueTask<Core.Models.GameFundTransferInfo?> GetAsync(Guid id, CancellationToken cancellationToken = default)
		=> await context.GameProviderFundTransferTransactions
		.Where(x => x.GameProviderFundTransferTransactionId == id)
		.Select(x => new Core.Models.GameFundTransferInfo(
			x.GameProviderFundTransferTransactionId,
			x.GameProviderId,
			x.WalletId,
			x.PlayerCode,
			x.FromWallet,
			x.ToWallet,
			(Common.Enums.GameFundTransferSource)x.FundSource,
			(Common.Enums.GameFundTransferStatus)x.Status,
			x.GameProviderFundTransferTransactionId.ToString(),
			x.GameProviderTransactionId ?? string.Empty,
			x.Amount,
			x.CreatedOn,
			x.UpdatedOn
			))
		.FirstOrDefaultAsync(cancellationToken: cancellationToken)
		.ConfigureAwait(false);

	public async IAsyncEnumerable<Core.Models.GameFundTransferInfo> QueryOperatorAsync(string playerCode, DateTimeOffset startTime, DateTimeOffset endTime, Common.Enums.GameFundTransferSort sort, bool isAscending = true, [EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		var query = context.GameProviderFundTransferTransactions
		.Where(x => x.PlayerCode == playerCode
		&& x.CreatedOn >= startTime.UtcDateTime
		&& x.CreatedOn < endTime.UtcDateTime
		&& !(x.FundSource == (int)Common.Enums.GameFundTransferSource.FundIn && x.Status == (int)Common.Enums.GameFundTransferStatus.GameProviderFundFailed))
		.Select(x => new Core.Models.GameFundTransferInfo(
			x.GameProviderFundTransferTransactionId,
			x.GameProviderId,
			x.WalletId,
			x.PlayerCode,
			x.FromWallet,
			x.ToWallet,
			(Common.Enums.GameFundTransferSource)x.FundSource,
			(Common.Enums.GameFundTransferStatus)x.Status,
			x.GameProviderFundTransferTransactionId.ToString(),
			x.GameProviderTransactionId ?? string.Empty,
			x.Amount,
			x.CreatedOn,
			x.UpdatedOn
			))
		.ToAsyncEnumerable();

		query = isAscending
			? sort switch
			{
				Common.Enums.GameFundTransferSort.UpdatedOn => query.OrderBy(keySelector: conform => conform.UpdatedOn),
				_ => query.OrderBy(keySelector: conform => conform.CreatedOn)
			}
			: sort switch
			{
				Common.Enums.GameFundTransferSort.UpdatedOn => query.OrderByDescending(keySelector: conform => conform.UpdatedOn),
				_ => query.OrderByDescending(keySelector: conform => conform.CreatedOn),
			};

		foreach (var info in await query
			.Skip(0)
			.Take(1000)
			.ToArrayAsync(cancellationToken: cancellationToken)
			.ConfigureAwait(false))
		{
			yield return info;
		}
	}

	public async ValueTask UpdateByGameWalletFailedAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var entity = await context.GameProviderFundTransferTransactions
			.Where(x => x.GameProviderFundTransferTransactionId == id)
			.FirstAsync(cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		entity.TransactionId = null;
		entity.Status = (short)Common.Enums.GameFundTransferStatus.GameProviderFundFailed;
		entity.UpdatedOn = timeProvider.GetUtcNow().DateTime;

		_ = context.GameProviderFundTransferTransactions.Update(entity);

		_ = await context.SaveChangesAsync(
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);
	}

	public async ValueTask UpdateByMainWalletFailedAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var entity = await context.GameProviderFundTransferTransactions
			.Where(x => x.GameProviderFundTransferTransactionId == id)
			.FirstAsync(cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		entity.Status = (short)Common.Enums.GameFundTransferStatus.MainWalletFundFailed;
		entity.UpdatedOn = timeProvider.GetUtcNow().DateTime;

		_ = context.GameProviderFundTransferTransactions.Update(entity);

		_ = await context.SaveChangesAsync(
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);
	}

	public async ValueTask UpdateByMainWalletRefundFailedAsync(Guid id, string description, CancellationToken cancellationToken = default)
	{
		var entity = await context.GameProviderFundTransferTransactions
			.Where(x => x.GameProviderFundTransferTransactionId == id)
			.FirstAsync(cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		entity.Status = (short)Common.Enums.GameFundTransferStatus.MainWalletFundRefundFailed;
		entity.Description = description;
		entity.UpdatedOn = timeProvider.GetUtcNow().DateTime;

		_ = context.GameProviderFundTransferTransactions.Update(entity);

		_ = await context.SaveChangesAsync(
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);
	}

	public async ValueTask UpdateByMainWalletRefundSuccessAsync(Guid id, Guid cashRespTransactionId, decimal balance, string description, CancellationToken cancellationToken = default)
	{
		var entity = await context.GameProviderFundTransferTransactions
			.Where(x => x.GameProviderFundTransferTransactionId == id)
			.FirstAsync(cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		entity.TransactionId = cashRespTransactionId;
		entity.Status = (short)Common.Enums.GameFundTransferStatus.MainWalletFundRefundSuccess;
		entity.Description = description;
		entity.UpdatedOn = timeProvider.GetUtcNow().DateTime;

		_ = context.GameProviderFundTransferTransactions.Update(entity);

		_ = await context.SaveChangesAsync(
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);
	}

	public async ValueTask UpdateBySuccessAsync(Guid id, Guid cashRespTransactionId, decimal balance, CancellationToken cancellationToken = default)
	{
		var entity = await context.GameProviderFundTransferTransactions
			.Where(x => x.GameProviderFundTransferTransactionId == id)
			.FirstAsync(cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		entity.TransactionId = cashRespTransactionId;
		entity.Status = (short)Common.Enums.GameFundTransferStatus.Success;
		entity.Balance = balance;
		entity.UpdatedOn = timeProvider.GetUtcNow().DateTime;

		_ = context.GameProviderFundTransferTransactions.Update(entity);

		_ = await context.SaveChangesAsync(
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);
	}
}
