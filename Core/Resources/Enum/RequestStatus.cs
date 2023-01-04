namespace Core.Resources.Enum
{
    public enum RequestStatus : int
    {
        ManagerDeclined = 0,
        New = 1,
        SentToManager = 2,
        SentToHr = 3,
        Approved = 4,
        Declined = 5
    }
}
