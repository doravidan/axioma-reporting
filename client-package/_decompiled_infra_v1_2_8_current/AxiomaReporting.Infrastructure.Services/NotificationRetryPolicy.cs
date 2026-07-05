using System;

namespace AxiomaReporting.Infrastructure.Services;

public static class NotificationRetryPolicy
{
	public const int MaxAttempts = 5;

	public static TimeSpan Backoff(int attempt)
	{
		int num = Math.Max(attempt, 1);
		return TimeSpan.FromMinutes(Math.Min(60.0, 5.0 * Math.Pow(2.0, num - 1)));
	}
}
