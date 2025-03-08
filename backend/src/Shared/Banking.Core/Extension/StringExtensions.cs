using System.Text.RegularExpressions;
using Banking.SharedKernel.Definitions;

namespace Banking.Core.Extension;

public static class StringExtensions
{
    public static bool IsOnlyLetters(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return false; 

        return input.All(char.IsLetter);
    }
    
    public static bool IsEmail(this string email)
    {
        return Regex.IsMatch(email, Constants.Shared.PatternEmail);
    }
    
    public static bool IsValidPhoneNumber(this string phoneNumber)
    {
       
        return Regex.IsMatch(phoneNumber, Constants.Shared.PatternPhoneNumber);
    }
}