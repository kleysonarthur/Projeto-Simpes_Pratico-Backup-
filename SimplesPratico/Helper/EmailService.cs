using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using MimeKit;
using SimplesPratico.Models;
using SimplesPratico.Repositorio;

namespace SimplesPratico.Helper {
    public class EmailService {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IFuncionarioRepositorio funcionarioRepositorio, IOptions<SmtpSettings> smtpSettings) {
            _funcionarioRepositorio = funcionarioRepositorio;
            _smtpSettings = smtpSettings.Value;
        }

        public bool EnviarEmail(string email, string assunto, string mensagem) {
            FuncionarioModel funcionario = _funcionarioRepositorio.ObterEmail(email);
            try {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(MailboxAddress.Parse(_smtpSettings.EmailRemetente));
                mimeMessage.To.Add(MailboxAddress.Parse(email));
                mimeMessage.Priority = MessagePriority.Urgent;
                mimeMessage.Subject = assunto;
                mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) {
                    Text = mensagem
                };
                using var smtp = new SmtpClient();
                smtp.Connect(_smtpSettings.Servidor, _smtpSettings.Porta, SecureSocketOptions.StartTls);
                smtp.Authenticate(_smtpSettings.EmailRemetente, _smtpSettings.SenhaEmail);
                smtp.Send(mimeMessage);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        } 
    }
}
