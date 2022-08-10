import sys
import email
import smtplib

class SendingEmail:
  def __init__(self) -> None:
    pass

  def Sending(mail_user):
    msg = email.message_from_string('Wellcome to mail verify credentials of u SingUp!!')
    msg['From'] = "****@email.com"
    msg['To'] = mail_user
    msg['Subject'] = "Success Sing up"

    s = smtplib.SMTP("smtp.office365.com",587)
    s.ehlo()
    s.starttls() 
    s.ehlo()
    s.login("****@email.com", '*****')

    s.sendmail("****@email.com", mail_user, msg.as_string())

    s.quit()

  if __name__ == '__main__':
    mail = sys.argv[1]
    Sending(mail)
    print('Email sending successfull!!')