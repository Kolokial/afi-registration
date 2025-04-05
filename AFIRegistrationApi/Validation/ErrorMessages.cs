namespace AFIRegistration.Validation;

public static class ErrorMessages
{
    public static string DATE_FORMAT_INVALID = "Date format must be YYYY-MM-DD. Check month and day are in correct order.";
    public static string MUST_BE_OVER_18 = "You must be over the age of 18 to register.";
    public static string INVALID_TLD = "Email address must end with .co.uk or .com";
    public static string INVALID_LOCAL_PART = "The local part of your email address must be 4 or more characters long.";
}