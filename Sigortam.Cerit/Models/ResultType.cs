namespace Sigortam.Cerit.Models
{
    public enum ResultType
    {
        Succeeded = 101,
        Failed = 500,
        IsLockedOut = 300,
        RequiresTwoFactor = 600,
        ConnectionFailed = 404
    }
}
