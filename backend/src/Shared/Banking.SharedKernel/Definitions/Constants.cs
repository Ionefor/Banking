namespace Banking.SharedKernel.Definitions;

public static class Constants
{
    public static class Shared
    {
        public const int MaxLowTextLength = 20;
        public const int MaxMediumTextLength = 100;
        public const int MaxLargeTextLength = 2000;
      
        public const int MaxDegreeOfParallelism = 10;
        public const string BucketNamePhotos = "Photos";
        public const string Database = "Database";
      
        public const string ConfigurationsWrite = "Configurations.Write";
        public const string ConfigurationsRead = "Configurations.Read";
        
        public const string PatternEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        public const string PatternPhoneNumber = @"^\+?(\d[\d\- ]{7,}\d)$";
        
        public const int MinYearBirthday = 1950;
    }

    public static class Accounts
    {
        public const string AccountsPath = "etc/accounts.json";
    }
}