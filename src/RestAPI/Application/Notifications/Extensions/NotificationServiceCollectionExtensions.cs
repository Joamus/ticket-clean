using Domain.Entities;
using Infrastructure.Notifications;
using Microsoft.AspNetCore.Identity;

namespace Applicatoin.Notifications.Extensions;

public static class NotificationServiceCollectionExtensions
{
	public static IServiceCollection AddNotificationServices(this IServiceCollection services)
	{
		services.AddSingleton<IEmailSender<User>, SystemMailer>();

		return services;
	}
}