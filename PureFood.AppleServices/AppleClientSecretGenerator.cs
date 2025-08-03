using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AppleServices
{
    public class AppleClientSecretGenerator
    {
        private readonly CryptoProviderFactory _cryptoProviderFactory;

        public DateTimeOffset UtcNow
        {
            get
            {
                // the clock measures whole seconds only, to have integral expires_in results, and
                // because milliseconds do not round-trip serialization formats
                var utcNowPrecisionSeconds =
                    new DateTime((DateTime.UtcNow.Ticks / TimeSpan.TicksPerSecond) * TimeSpan.TicksPerSecond,
                        DateTimeKind.Utc);
                return new DateTimeOffset(utcNowPrecisionSeconds);
            }
        }

        public AppleClientSecretGenerator()
        {
            var cryptoProviderFactory = new CryptoProviderFactory() { CacheSignatureProviders = false };
            _cryptoProviderFactory = cryptoProviderFactory;
        }

        public async Task<string> GenerateAsync(string keyId, string clientId, string teamId, string privateKey)
        {
            (string clientSecret, DateTimeOffset expiresAt) =
                GenerateNewSecretAsync(keyId, clientId, teamId, privateKey);
            return clientSecret;
        }


        private const string Audience = "https://appleid.apple.com";

        private (string clientSecret, DateTimeOffset expiresAt) GenerateNewSecretAsync(string keyId,
            string clientId, string teamId, string privateKey)
        {
            var time = TimeSpan.FromSeconds(15777000);
            var expiresAt = UtcNow.Add(time).UtcDateTime;

            var subject = new Claim("sub", clientId);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = Audience,
                Expires = expiresAt,
                Issuer = teamId,
                Subject = new ClaimsIdentity(new[] { subject }),
            };

            byte[] keyBlob = Convert.FromBase64String(privateKey);
            string clientSecret;
            var t = keyId!;
            using (var algorithm = CreateAlgorithm(keyBlob))
            {
                tokenDescriptor.SigningCredentials = CreateSigningCredentials(t, algorithm);
                JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                clientSecret = jwtSecurityTokenHandler.CreateEncodedJwt(tokenDescriptor);
            }

            return (clientSecret, expiresAt);
        }

        private static ECDsa CreateAlgorithm(byte[] keyBlob)
        {
            var algorithm = ECDsa.Create();

            try
            {
                algorithm.ImportPkcs8PrivateKey(keyBlob, out int _);
                return algorithm;
            }
            catch (Exception)
            {
                algorithm?.Dispose();
                throw;
            }
        }

        private SigningCredentials CreateSigningCredentials(string keyId, ECDsa algorithm)
        {
            var key = new ECDsaSecurityKey(algorithm) { KeyId = keyId };

            // Use a custom CryptoProviderFactory so that keys are not cached and then disposed of, see below:
            // https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/issues/1302
            return new SigningCredentials(key, SecurityAlgorithms.EcdsaSha256Signature)
            {
                CryptoProviderFactory = _cryptoProviderFactory,
            };
        }
    }
}
