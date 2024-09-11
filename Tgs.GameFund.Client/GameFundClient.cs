using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Web;
using Tgs.GameFund.Common.Enums;
using Tgs.GameFund.Common.ViewModels;

namespace Tgs.GameFund.Client;

internal class GameFundClient(HttpClient httpClient) : IGameFundClient
{
	private static readonly JsonSerializerOptions _SerializerOptions = new()
	{
		WriteIndented = false,
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		Converters = { new JsonStringEnumConverter() }
	};

	public async ValueTask<GameFundTransferInfoViewModel> GameFundTransferAddFundInAsync(Guid id, Guid gameProviderId, Guid walletId, string playerCode, string walletFrom, string walletTo, string cashTransactionId, string gameTransactionId, decimal amount, CancellationToken cancellationToken = default)
	{
		var response = await httpClient.PostAsJsonAsync(
			requestUri: "transfer/add/fund-in/",
			value: new GameFundTransferAddViewModel
			{
				Id = id,
				GameProviderId = gameProviderId,
				WalletId = walletId,
				PlayerCode = playerCode,
				WalletFrom = walletFrom,
				WalletTo = walletTo,
				CashTransactionId = cashTransactionId,
				GameTransactionId = gameTransactionId,
				Amount = amount
			},
			options: _SerializerOptions,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		_ = response.EnsureSuccessStatusCode();

		var contentJson = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

		return JsonSerializer.Deserialize<GameFundTransferInfoViewModel>(contentJson, _SerializerOptions)!;
	}

	public async ValueTask<GameFundTransferInfoViewModel> GameFundTransferAddFundOutAsync(Guid id, Guid gameProviderId, Guid walletId, string playerCode, string walletFrom, string walletTo, string cashTransactionId, string gameTransactionId, decimal amount, CancellationToken cancellationToken = default)
	{
		var response = await httpClient.PostAsJsonAsync(
			requestUri: "transfer/add/fund-out/",
			value: new GameFundTransferAddViewModel
			{
				Id = id,
				GameProviderId = gameProviderId,
				WalletId = walletId,
				PlayerCode = playerCode,
				WalletFrom = walletFrom,
				WalletTo = walletTo,
				CashTransactionId = cashTransactionId,
				GameTransactionId = gameTransactionId,
				Amount = amount
			},
			options: _SerializerOptions,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		_ = response.EnsureSuccessStatusCode();

		var contentJson = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

		return JsonSerializer.Deserialize<GameFundTransferInfoViewModel>(contentJson, _SerializerOptions)!;
	}

	public async ValueTask<GameFundTransferInfoViewModel?> GameFundTransferGetAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var response = await httpClient.GetAsync(
			requestUri: $"transfer/{id}/",
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		_ = response.EnsureSuccessStatusCode();

		var contentJson = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

		return JsonSerializer.Deserialize<GameFundTransferInfoViewModel>(contentJson, _SerializerOptions);
	}

	public async ValueTask<GameFundTransferQueryOperatorResultViewModel> GameFundTransferQueryOperatorAsync(string playerCode, DateTimeOffset startTime, DateTimeOffset endTime, GameFundTransferSort sort = GameFundTransferSort.CreatedOn, bool isAscending = true, int page = 1, int rows = 20, CancellationToken cancellationToken = default)
	{
		var path = new Uri(httpClient.BaseAddress!, "transfer/query/operator");
		var builder = new UriBuilder(path);
		var query = HttpUtility.ParseQueryString(builder.Query);
		query["playerCode"] = playerCode.ToString();
		query["startTime"] = startTime.ToString("u");
		query["endTime"] = endTime.ToString("u");
		query["sort"] = sort.ToString();
		query["isAscending"] = isAscending.ToString();
		query["page"] = page.ToString();
		query["rows"] = rows.ToString();
		builder.Query = query.ToString();

		var response = await httpClient.GetAsync(
			requestUri: builder.ToString(),
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		_ = response.EnsureSuccessStatusCode();

		var contentJson = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

		return JsonSerializer.Deserialize<GameFundTransferQueryOperatorResultViewModel>(contentJson, _SerializerOptions)!;
	}

	public async ValueTask GameFundTransferUpdateByGameWalletFailedAsync(Guid id, string description, JsonNode additionalData, CancellationToken cancellationToken = default)
	{
		var response = await httpClient.PutAsJsonAsync(
			requestUri: $"transfer/{id}/game-wallet/failed",
			value: new GameFundTransferUpdateByGameWalletFailedViewModel
			{
				Description = description,
				AdditionalData = additionalData
			},
			options: _SerializerOptions,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		_ = response.EnsureSuccessStatusCode();
	}

	public async ValueTask GameFundTransferUpdateByMainWalletFailedAsync(Guid id, string description, JsonNode additionalData, CancellationToken cancellationToken = default)
	{
		var response = await httpClient.PutAsJsonAsync(
			requestUri: $"transfer/{id}/main-wallet/failed",
			value: new GameFundTransferUpdateByMainWalletFailedViewModel
			{
				Description = description,
				AdditionalData = additionalData
			},
			options: _SerializerOptions,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		_ = response.EnsureSuccessStatusCode();
	}

	public async ValueTask GameFundTransferUpdateByMainWalletRefundFailedAsync(Guid id, string description, JsonNode additionalData, CancellationToken cancellationToken = default)
	{
		var response = await httpClient.PutAsJsonAsync(
			requestUri: $"transfer/{id}/main-wallet/refund/failed",
			value: new GameFundTransferUpdateByMainWalletRefundFailedViewModel
			{
				Description = description,
				AdditionalData = additionalData
			},
			options: _SerializerOptions,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		_ = response.EnsureSuccessStatusCode();
	}

	public async ValueTask GameFundTransferUpdateByMainWalletRefundSuccessAsync(Guid id, Guid cashRespTransactionId, string gameRespTransactionId, decimal mainWalletBalance, string description, JsonNode additionalData, CancellationToken cancellationToken = default)
	{
		var response = await httpClient.PutAsJsonAsync(
			requestUri: $"transfer/{id}/main-wallet/refund/success",
			value: new GameFundTransferUpdateByMainWalletRefundSuccessViewModel
			{
				CashRespTransactionId = cashRespTransactionId,
				GameRespTransactionId = gameRespTransactionId,
				MainWalletBalance = mainWalletBalance,
				Description = description,
				AdditionalData = additionalData
			},
			options: _SerializerOptions,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		_ = response.EnsureSuccessStatusCode();
	}

	public async ValueTask GameFundTransferUpdateBySuccessAsync(Guid id, Guid cashRespTransactionId, string gameRespTransactionId, decimal mainWalletBalance, string description, JsonNode additionalData, CancellationToken cancellationToken = default)
	{
		var response = await httpClient.PutAsJsonAsync(
			requestUri: $"transfer/{id}/success",
			value: new GameFundTransferUpdateBySuccessViewModel
			{
				CashRespTransactionId = cashRespTransactionId,
				GameRespTransactionId = gameRespTransactionId,
				MainWalletBalance = mainWalletBalance,
				Description = description,
				AdditionalData = additionalData
			},
			options: _SerializerOptions,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		_ = response.EnsureSuccessStatusCode();
	}
}
