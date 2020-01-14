using CarProject.Protected;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CarProject.Helpers {
    public class SendMail {
        public static async Task SendGmail(string subject, string body, string password) {
            MailAddress fromAddress = new MailAddress(Emails.fromEmailAddress, "No Reply");
            MailAddress toAddress = new MailAddress(Emails.toEmailAddress);

            SmtpClient smtp = new SmtpClient {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, password)
            };

            using MailMessage msg = new MailMessage(fromAddress, toAddress) {
                Subject = subject,
                Body = body
            };

            await smtp.SendMailAsync(msg);
            msg.Dispose(); // Probably isn't necessary
            smtp.Dispose();
        }
    }
}
