using FluentResults;

namespace Application.Interfaces;
public interface ISystemMailer
{

	public Task<Result> SendMailAsync(string toAddress, string toName, string subject, string body);
}