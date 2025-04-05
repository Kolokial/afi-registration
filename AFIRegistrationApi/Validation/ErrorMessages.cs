namespace AFIRegistration.Validation;

public static class ErrorMessages
{
    public static string DATE_FORMAT_INVALID = "Date format must be YYYY-MM-DD. Check month and day are in correct order.";
    public static string MUST_BE_OVER_18 = "You must be over the age of 18 to register.";
    public static string INVALID_TLD = "Email address must end with .co.uk or .com";
    public static string INVALID_LOCAL_PART = "The local part of your email address must be 4 or more characters long.";
    public static string DATE_OR_EMAIL_REQUIRED = "Either Date of Birth or Email must be provided.";
    public static string FIRST_NAME_LENGTH = "First name must be between 3 and 50 characters.";
    public static string LAST_NAME_LENGTH = "Last name msut be between 3 and 50 characters";
    public static string POLICY_NUMBER_FORMAT = "Policy number must be XX-123456 format.";
}