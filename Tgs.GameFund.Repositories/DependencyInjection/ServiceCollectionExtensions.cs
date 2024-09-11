using System.Data;
using Microsoft.EntityFrameworkCore;
using Tgs.GameFund.Core;
using Tgs.GameFund.Entities;
using Tgs.GameFund.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddGameFundRepositories(
		this IServiceCollection services,
		Action<IServiceProvider, DbContextOptionsBuilder> tgsConfigure)
		=> services
		.AddDbContext<TgsContext>(tgsConfigure)
		.AddScoped<IDbConnection>((sp) => sp.GetRequiredService<TgsContext>().Database.GetDbConnection())
		.AddTransient<IGameFundTransferRepository, GameFundTransferRepository>();
}
