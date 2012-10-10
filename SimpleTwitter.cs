using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twitterizer;
using System.Windows.Forms;

namespace uTorrentNotifier2.Net
{
    class SimpleTwitter
    {
        private SimpleTwitterConfig m_config;
        OAuthTokenResponse tokenResponse2 = new OAuthTokenResponse();

        public SimpleTwitter(SimpleTwitterConfig i_config)
        {
            this.m_config = i_config;
        }

        internal void UpdateStatus(string i_msg)
        {
            bool result = false;

            if (m_config.Autorized)
            {
                result = _updateStatus(i_msg);
            }
            else {
                _autorize();
            }

            if (!result)
            {
                result = _updateStatus(i_msg);
            }

            return;
            
        }

        private void _autorize()
        {
            OAuthTokenResponse tokenResponse = new OAuthTokenResponse();
            //tokenResponse = Twitterizer.OAuthUtility.GetRequestToken(consumerKey, consumerSecret, callbackAddy);
            tokenResponse = Twitterizer.OAuthUtility.GetRequestToken(m_config.AppConsumerKey, m_config.AppConsumerSecret, "oob");
            string pin = "";

            if (true)
            {
                // Need to check if the user is a valid user.

                //OAuthTokenResponse tokenResponse = new OAuthTokenResponse();
                //tokenResponse = Twitterizer.OAuthUtility.GetRequestToken(m_config.AppConsumerKey, m_config.AppConsumerSecret, "oob");
                //txt_login.Text = "Token is:  " + tokenResponse.Token.ToString();

                string target = "http://twitter.com/oauth/authorize?oauth_token=" + tokenResponse.Token;
                try
                {
                    System.Diagnostics.Process.Start(target);
                }
                catch (System.ComponentModel.Win32Exception noBrowser)
                {
                    if (noBrowser.ErrorCode == -2147467259)
                        MessageBox.Show(noBrowser.Message);
                }
                catch (System.Exception other)
                {
                    MessageBox.Show(other.Message);
                }

                pin = "3470994"; //This WILL NOT WORK. User needs to enter the PIN
                EnterPin enterpin = new EnterPin();
                enterpin.ShowDialog(); //show dialog causes it to wait for user input. Show() would not work
                //pin = Properties.Settings.Default.pin;
            }
            else
            {
                //pin = Properties.Settings.Default.pinSaved;
            }


            tokenResponse2 = OAuthUtility.GetAccessToken(m_config.AppConsumerKey, m_config.AppConsumerSecret, tokenResponse.Token, pin);
            m_config.Autorized = true;
            //txt_login.Text = "App " + tokenResponse2.ScreenName.ToString() + " access allowed.";
            
        }

        private bool _updateStatus(string i_msg)
        {
            bool result = false;
            OAuthTokens tokens = new OAuthTokens();

            tokens.ConsumerKey = m_config.AppConsumerKey;
            tokens.ConsumerSecret = m_config.AppConsumerSecret;

            tokens.AccessToken = m_config.AccessToken;
            tokens.AccessTokenSecret = m_config.AccessTokenSecret;

            TwitterResponse<TwitterStatus> tweetResponse = TwitterStatus.Update(tokens, i_msg);

            if (tweetResponse.Result == RequestResult.Success)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }
    }

    public class SimpleTwitterConfig
    {
        public bool Autorized;
        public string AppConsumerKey;
        public string AppConsumerSecret;
        public string AccessToken;
        public string AccessTokenSecret;
        public string AccessPin;
        public string BodyPattern;
        public List<string> SkipStringList;

        public SimpleTwitterConfig()
        {
            SkipStringList = new List<string>();
        }
    }

    
}
