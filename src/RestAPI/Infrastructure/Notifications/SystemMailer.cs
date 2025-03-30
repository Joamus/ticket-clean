using Application.Interfaces;
using Domain.Entities;
using FluentResults;
using Microsoft.AspNetCore.Identity;

// This class does not actually send emails, but could be implemented later

namespace Infrastructure.Notifications;

public class SystemMailer : ISystemMailer, IEmailSender<User>
{
	private readonly ILogger<SystemMailer> _logger;
	private readonly string _fromAddress;

	private readonly string _fromName;

	public SystemMailer(ILogger<SystemMailer> logger)
	{
		_logger = logger;
		_fromAddress = "system@ticketsharp.com";
		_fromName = "System";
	}

	public Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
	{
		// No impl

		return Task.FromResult(0);
	}

	public Task<Result> SendMailAsync(string toAddress, string toName, string subject, string body)
	{
		_logger.LogInformation("Mock sending email - to: {ToAddress} ({ToName}) - subject: {Subject} - body: {Body}", toAddress, toName, subject, body);

		return Task.FromResult(Result.Ok());
	}

	public Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
	{
		// No impl

		return Task.FromResult(0);
	}

	public Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
	{
		// No impl

		return Task.FromResult(0);
	}
}