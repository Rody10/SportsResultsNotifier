using HtmlAgilityPack;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace SportsResultsNotifier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sports Results Notifier");
            var html = @"https://www.basketball-reference.com/boxscores/";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);

            StringBuilder sbResults = new StringBuilder(); 

            // Get collection of nodes that contain results.
            var gameNodes = htmlDoc.DocumentNode.SelectNodes("//table[@class='teams']/tbody/tr");

            for (int i = 0; i < gameNodes.Count; i = i + 2) // incrementing by 2 to process winner and loser together
            {
                var team1Node = gameNodes[i];
                var team2Node = gameNodes[i + 1];

                string team1Team = team1Node.SelectSingleNode("td/a").InnerText.Trim();
                string team1Score = team1Node.SelectSingleNode("td[@class='right']").InnerText.Trim();

                string team2Team = team2Node.SelectSingleNode("td/a").InnerText.Trim();
                string team2Score = team2Node.SelectSingleNode("td[@class='right']").InnerText.Trim();

                sbResults.Append(team1Team + " " + team1Score + " " + "-" + " " + team2Score + " " + team2Team + '\n');
 
            }
            string resultsString = sbResults.ToString();

            string senderEmailAddress = "";
            string receiverEmailAddress = "";
            string senderPassword = "";

            SendResultsEmail(senderEmailAddress, senderPassword, receiverEmailAddress, resultsString);
            Console.WriteLine("Succesfully send email message!");

            static void SendResultsEmail(string senderEmailAddress, string senderPassword, string receiverEmailAddress,string message)
            {
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;
                string subject = "Basketball Results";
                using(MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(senderEmailAddress);
                    mail.To.Add(receiverEmailAddress);
                    mail.Subject = subject;
                    mail.Body = message;
                    mail.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(senderEmailAddress, senderPassword);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }          
            }
        }
    }
}
