using EmailOTPModule.Services;
using EmailOTPModule.Utilities.Constants;

class Program
{
    private const string EmailDomain = "@customdomain.com";
    private const int TimeoutMilliseconds = 60000;
    private const int MaxAttempts = 10;

    static async Task Main(string[] args)
    {
        EmailOTPModuleService service = new EmailOTPModuleService(EmailDomain, TimeoutMilliseconds, MaxAttempts);

        Console.WriteLine("Enter email address:");
        string email = Console.ReadLine();

        int emailStatus = service.GenerateOtpEmail(email);
        if (emailStatus == EmailStatus.STATUS_EMAIL_OK)
        {
            await service.UserInputSession();
        }
        else if(emailStatus == EmailStatus.STATUS_EMAIL_FAIL) {
            Console.WriteLine("Email address does not exist or sending to the email has failed.");
        }
        else {
            Console.WriteLine("Email address is invalid");
        }
    }


}