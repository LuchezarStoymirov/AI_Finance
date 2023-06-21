using System;

namespace AIF.Models.GoogleModels
{
	public class GoogleTokenValidationResult
	{
        public string Email { get; set; }

        public bool IsValid { get; set; }

        public string ErrorMessage { get; set; }
    }
}