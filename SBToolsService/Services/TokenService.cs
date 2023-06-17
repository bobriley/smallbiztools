using SBToolsService.POCOs;
using SBToolsService.ServiceInterfaces;
using System.Security.Cryptography;
using System.Text;

namespace SBToolsService.Services
{
    public class TokenService : ITokenService
    {
        public bool IsTokenValid(STToken token)
        {

            return true;
            // To validate the token
            //var receivedToken = /* get the token from the other system */;
            var receivedHash = token.Hash;
            var receivedTimestamp = token.Timestamp;

            var expectedUnencrypted = "Red Riding Hood!" + receivedTimestamp;
            byte[] expectedBytes = Encoding.UTF8.GetBytes(expectedUnencrypted);
            byte[] expectedHashBytes;

            using (var sha256 = SHA256.Create())
            {
                expectedHashBytes = sha256.ComputeHash(expectedBytes);
            }

            var expectedHash = Convert.ToHexString(expectedHashBytes);

            return expectedHash.Equals(receivedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
