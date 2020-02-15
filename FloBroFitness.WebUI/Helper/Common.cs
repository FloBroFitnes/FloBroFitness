using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace FloBroFitness.WebUI.Helper
{
    public class Common
    {
        public static string AdminCVFile(string folderpath,HttpPostedFileBase file)
        {
            string fileName = (Path.GetExtension(file.FileName)).ToLower();

            string newFileName = Common.GetDateTime().ToString("MMddyyHHmmssfff") + fileName;
            string fileWithPath = System.Web.HttpContext.Current.Server.MapPath(folderpath + newFileName);
            file.SaveAs(fileWithPath);
            return newFileName;
        }
        public static DateTime GetDateTime()
        {
            return DateTime.UtcNow;
        }
        public static bool SendEmail(String toEmail, String subject, String body, String ccEmail)
        {
            MailMessage objMail = new MailMessage();
            objMail.To.Add(toEmail);

            if (ccEmail != string.Empty)
            {
                objMail.Bcc.Add(ccEmail);
            }

            try
            {
                objMail.IsBodyHtml = true;
                objMail.Subject = subject;
                objMail.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(System.Text.RegularExpressions.Regex.Replace(body, @"<(.|\n)*?>", string.Empty), null, "text/plain");
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                //objMail.Body = body;
                objMail.AlternateViews.Add(plainView);
                objMail.AlternateViews.Add(htmlView);
                SmtpClient SmtpClnt = new SmtpClient();
                SmtpClnt.Send(objMail);
                return true;
            }
            catch (Exception)
            {
                //throw ex;
                return false;
            }
        }

        public static String GetContextFromHTML(Hashtable paramLst, string Path)
        {
            string context = "";

            using (StreamReader sr = new StreamReader(Path))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    context += line;
                }
            }
            if (context.Length > 0)
            {
                foreach (DictionaryEntry key in paramLst)
                {
                    context = context.Replace(key.Key.ToString(), Convert.ToString(key.Value));
                }
            }
            return context;
        }
        public static String GetContextFromHTMLForEmailBody(Hashtable paramLst, string Path, string emailbody)
        {
            string context = "";
            if (!string.IsNullOrEmpty(Path))
            {
                using (StreamReader sr = new StreamReader(Path))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        context += line;
                    }
                }
            }
            else
            {
                context = emailbody;
            }
            if (context.Length > 0)
            {
                foreach (DictionaryEntry key in paramLst)
                {
                    context = context.Replace(key.Key.ToString(), Convert.ToString(key.Value));
                }
            }
            return context;
        }
    }
}