namespace Application.Common.Shared_Models
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double AccessTokenDurationInMinutes { get; set; }
        public double RefreshTokenDurationInHours { get; set; }
    }
}
