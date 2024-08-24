namespace EmailOTPModule.Utilities
{
    public class IOStream
    {
        private string _otpToRead;

        public IOStream(string otpToRead)
        {
            this._otpToRead = otpToRead;
        }

        public string ReadOTP()
        {
            return _otpToRead;
        }
    }
}
