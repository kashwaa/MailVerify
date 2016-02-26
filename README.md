# MailVerify

MailVerify is a small commandline tool written in C# which tries to verify whether a specific email address exists or not.

## How it works

MailVerify gets the MX "Mail Exchange" record of the given email address server, then tries to connect to the smtp service to verify whether the given email is accepted by the server or not.

## Usage

mailverify.exe emailaddress
