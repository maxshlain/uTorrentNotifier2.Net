using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace uTorrentNotifier2.Net
{
    struct SimpleMailConfig
    {
        public string EmailFrom;
        public string EmailTo;

        public string EmailFromUsername;
        public string EmailFromPassword;

        public string SmtpHost;
        public int SmtpPort;

        public string SubjectPattern;
        public string BodyPattern;

    }
    class SimpleMail
    {
        private SimpleMailConfig m_mailConfig;
        private string m_subject = "";
        private string m_body = "";
        private string m_to = "";

        public SimpleMail(SimpleMailConfig i_mailConfig)
        {
            m_mailConfig = i_mailConfig;

            return;
        }


        public string To
        {
            get { return m_to; }
            set { m_to = value; }
        }

        public string Subject
        {
            get { return m_subject; }
            set { m_subject = value; }
        }

        public string Body
        {
            get { return m_body; }
            set { m_body = value; }
        }

        internal void Send()
        {
            SmtpClient client = null;
            NetworkCredential credential = null;
            MailMessage message = null;

            try
            {

                client = new SmtpClient(m_mailConfig.SmtpHost, m_mailConfig.SmtpPort);
                credential = new NetworkCredential(m_mailConfig.EmailFromUsername, m_mailConfig.EmailFromPassword);
                client.Credentials = credential;
                client.EnableSsl = true;

                message = new MailMessage(m_mailConfig.EmailFrom, m_mailConfig.EmailTo, m_subject, m_body);
                message.SubjectEncoding = Encoding.GetEncoding(1251);
                message.BodyEncoding = Encoding.GetEncoding(1251);

                client.Send(message);

            }
            catch (Exception ex)
            {
                string nop = ex.Message;
                Console.WriteLine(ex);
            }
            finally 
            {
                client = null;
                credential = null;
                message = null;
            }

            return;

        }

    }
}
