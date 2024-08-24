using EmailOTPModule.Utilities;
using EmailOTPModule.Utilities.Constants;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace EmailOTPModule.Services
{
    public class EmailOTPModuleService
    {
        private readonly string _emailDomain;
        private readonly int _timeoutMilliseconds;
        private readonly int _maxAttempts;
        private string? _currentOTP;
        private DateTime _otpTimestamp;

        public EmailOTPModuleService(string emailDomain, int timeoutMilliseconds, int maxAttempts)
        {
            this._emailDomain = emailDomain;
            this._timeoutMilliseconds = timeoutMilliseconds;
            this._maxAttempts = maxAttempts;
        }

        public void Start()
        {
            // Optional to implement
        }

        public void Close()
        {
            // Optional to implement
        }

        public int GenerateOtpEmail(string userEmail)
        {
            if (!IsValidEmail(userEmail))
            {
                return EmailStatus.STATUS_EMAIL_INVALID;
            }

            if (!userEmail.EndsWith(_emailDomain))
            {
                return EmailStatus.STATUS_EMAIL_INVALID;
            }

            try
            {
                _currentOTP = GenerateOtp();
                string emailBody = $"Your OTP Code is {_currentOTP}. The code is valid for 1 minute.";
                SendEmail(userEmail, emailBody);
                _otpTimestamp = DateTime.Now;
                return EmailStatus.STATUS_EMAIL_OK;
            }
            catch (Exception)
            {
                return EmailStatus.STATUS_EMAIL_FAIL;
            }
        }

        public async Task UserInputSession()
        {
            int attempts = 0;
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(_timeoutMilliseconds));

            try
            {
                while (attempts < _maxAttempts)
                {
                    if (cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        throw new TaskCanceledException();
                    }

                    Console.Write("Enter OTP: ");

                    var readTask = Task.Run(() => Console.ReadLine(), cancellationTokenSource.Token);
                    var completedTask = await Task.WhenAny(readTask, Task.Delay(Timeout.Infinite, cancellationTokenSource.Token));

                    if (completedTask == readTask)
                    {
                        string userInput = await readTask;
                        int statusCode = this.CheckOtp(new IOStream(userInput));

                        if (statusCode == OTPStatus.STATUS_OTP_OK)
                        {
                            Console.WriteLine("Valid OTP!");
                            return;
                        }
                        else if (statusCode == OTPStatus.STATUS_OTP_TIMEOUT)
                        {
                            Console.WriteLine("OTP Timeout!");
                            attempts++;
                        }
                        else
                        {
                            attempts++;
                            Console.WriteLine($"Invalid OTP! ({10 - attempts} tries remaining)");
                        }
                    }
                }
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Timeout!");
            }
        }

        private int CheckOtp(IOStream input)
        {
            //Check if OTP timeout
            if (DateTime.Now > _otpTimestamp.AddMilliseconds(_timeoutMilliseconds))
            {
                return OTPStatus.STATUS_OTP_TIMEOUT;
            }

            //Check user input
            string enteredOtp = input.ReadOTP();
            if (enteredOtp == _currentOTP)
            {
                return OTPStatus.STATUS_OTP_OK;
            }

            //Else fail
            return OTPStatus.STATUS_OTP_FAIL;
        }

        private string GenerateOtp()
        {
            Random rand = new();
            return rand.Next(0, 1000000).ToString("D6");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                MailAddress mailAddress = new(email);
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            }
            catch
            {
                return false;
            }
        }

        private void SendEmail(string emailAddress, string emailBody)
        {
            Console.WriteLine($"Sending email to {emailAddress}: {emailBody}");
        }
    }
}
