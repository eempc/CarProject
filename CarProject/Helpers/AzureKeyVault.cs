using CarProject.Protected;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.Services.AppAuthentication;
using System.Threading.Tasks;

namespace CarProject.Helpers {
    public class AzureKeyVault {
        public static async Task<string> GetPasswordAsync() {
            string password = "";
            try {
                AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
                KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                SecretBundle secret = await keyVaultClient.GetSecretAsync(Emails.keyVaultUrl).ConfigureAwait(false);
                password = secret.Value;
            } catch (KeyVaultErrorException e) {
                //Message = e.Message;
            }
            return password;
        }
    }
}
