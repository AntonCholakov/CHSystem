﻿using CHSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace CHSystem.Services
{
    public static class EmailService
    {
        public static void SendEmail(User user)
        {
            MailMessage message = new MailMessage();
            message.Sender = new MailAddress("no-reply@chsystem.com");
            message.To.Add(user.Email);
            message.Subject = "Subject";
            message.From = new MailAddress("no-reply@chsystem.com");
            message.Body = "Hello! It's me!";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            #region Private
            smtp.Credentials = new System.Net.NetworkCredential("antoncholakov14@gmail.com", "...");
            #endregion

            smtp.Send(message);
        }
    }
}