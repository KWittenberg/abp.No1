namespace No1;

public static class No1DomainErrorCodes
{
    /* You can add your business exception error codes here, as constants */

    public static string InvalidAppsettingsData = "App:00000";
    public static string EntityAlreadyExists = "App:00001";
    public static string RelatedEntity = "App:00002";

    public static string InvalidGeoLocation = "App:00010";
    public static string InvalidOibFormat = "App:00011";
    public static string InvalidUrl = "App:00012";
    public static string InvalidLocaleFormatException = "App:00013";

    public static string InvalidPrice = "App:00020";
    public static string InvalidDuration = "App:00021";
    public static string InvalidMaxParticipantsNumber = "App:00022";
    public static string InvalidAverageRatingsNumber = "App:00023";
    public static string InvalidStartTime = "App:00024";

    public static string InvalidRating = "App:00025";

    public static string InvalidResponse = "App:00030";
}