using EmailOTPModule.Services;
using EmailOTPModule.Utilities.Constants;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace EmailOTPModuleTest
{
    public class EmailOTPModuleServiceTests
    {
        [Test]
        public void GenerateOtpEmail_InvalidEmail_ReturnsEmailInvalid()
        {
            // Arrange
            var service = new EmailOTPModuleService("customdomain.com", 60000, 3);
            var invalidEmail = "example@gmail.com";

            // Act
            var result = service.GenerateOtpEmail(invalidEmail);

            // Assert
            Assert.That(result, Is.EqualTo(EmailStatus.STATUS_EMAIL_INVALID));
        }

        [Test]
        public void GenerateOtpEmail_ValidEmail_ReturnsEmailOk()
        {
            // Arrange
            var service = new EmailOTPModuleService("customdomain.com", 60000, 3);
            var validEmail = "example@customdomain.com";

            // Act
            var result = service.GenerateOtpEmail(validEmail);

            // Assert
            Assert.That(result, Is.EqualTo(EmailStatus.STATUS_EMAIL_OK));
        }
    }
}