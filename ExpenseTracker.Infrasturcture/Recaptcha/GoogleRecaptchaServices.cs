using AspNetCore.ReCaptcha;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrasturcture.Recaptcha
{
    public class GoogleRecaptchaServices
    {
        private readonly IConfiguration _configuration;
        private readonly IReCaptchaService _recaptchaService;

        public GoogleRecaptchaServices(IConfiguration configuration, IReCaptchaService recaptchaService)
        {
            _configuration = configuration;
            _recaptchaService = recaptchaService;
        }
        public string GetSiteKey()
        {
            return _recaptchaService.;
        }

        public async Task<bool> VerifyRecaptcha(string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                return false;
            }

            return await _recaptchaService.VerifyAsync(response);
        }
    }
}
