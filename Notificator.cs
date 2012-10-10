using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uTorrentNotifier2.Net
{
    class Notificator
    {
        internal static void SendNotification_viaEmail(string i_moduleName, string i_message, SimpleMailConfig i_mailConfig)
        {
            SimpleMail sMsg = new SimpleMail(i_mailConfig);

            sMsg.Subject = String.Format(i_mailConfig.SubjectPattern, i_moduleName, i_message);

            string body = BuildBody(i_moduleName, i_message, i_mailConfig.BodyPattern);
            sMsg.Body = body;
            sMsg.Send();            
        }
        private static string BuildBody(string i_moduleName, string i_message, string i_bodyPattern)
        {
            return BuildBody(i_moduleName, i_message, i_bodyPattern, 999);
        }

        private static string BuildBody(string i_moduleName, string i_message, string i_bodyPattern, int i_lenghtLimitation)
        {
                    string body = "";
                    switch (i_moduleName)
                    {
                        case "uTorrent":
                            body = String.Format(i_bodyPattern, i_moduleName, i_message);
                            break;

                        default:
                            body = String.Format(i_bodyPattern, i_moduleName, i_message);
                            break;
                    }
                    return body + DateTime.Now.Second.ToString();
        }

        internal static void SendNotification_viaTwitter(string moduleName, string message, SimpleTwitterConfig i_config)
        {
            string msg = BuildBody(moduleName, message, i_config.BodyPattern, 140);
            SimpleTwitter sTwit = new SimpleTwitter(i_config);

            sTwit.UpdateStatus(msg);
            
        }
    }
}
