namespace Banking.Core.Models;

public static class Permissions
{
    public static class Accounts
    {
        public const string Read = "Account.Read";
        public const string Update = "Account.Update";
        public const string Delete = "Account.Delete";
    }
    
    public static class CorporateAccounts
    {
        public const string Read = "CorporateAccount.Read";
        public const string Update = "CorporateAccount.Update";
    }
    
    public static class IndividualAccounts
    {
        public const string Read = "IndividualAccount.Read";
        public const string Update = "IndividualAccount.Update";
    }
    
    public static class BankAccounts
    {
        public const string Create = "BankAccount.Create";
        public const string Read = "BankAccount.Read";
        public const string Update = "BankAccount.Update";
        public const string Delete = "BankAccount.Delete";
    }
    
    public static class Cards
    {
        public const string Create = "Card.Create";
        public const string Read = "Card.Read";
        public const string Update = "Card.Update";
        public const string Delete = "Card.Delete";
    }
    
    public static class ClientAccounts
    {
        public const string Create = "ClientAccount.Create";
        public const string Read = "ClientAccount.Read";
        public const string Delete = "ClientAccount.Delete";
    }
}