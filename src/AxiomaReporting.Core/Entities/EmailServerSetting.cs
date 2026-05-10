using AxiomaReporting.Core.Entities.Base;

namespace AxiomaReporting.Core.Entities;

public class EmailServerSetting : BaseEntity
{
  public string SmtpServer { get; set; } = string.Empty;
  public int Port { get; set; }
  public string Username { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public string FromAddress { get; set; } = string.Empty;
  public string? FromName { get; set; }
  public bool UseSsl { get; set; } = true;
}
