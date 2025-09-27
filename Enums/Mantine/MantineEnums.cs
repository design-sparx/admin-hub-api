namespace AdminHubApi.Enums.Mantine
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }

    public enum PaymentMethod
    {
        CreditCard,
        DebitCard,
        Paypal,
        ApplePay,
        GooglePay,
        Bitcoin,
        Venmo,
        GiftCard
    }

    public enum ProjectState
    {
        Pending,
        InProgress,
        Completed,
        Cancelled
    }

    public enum InvoiceStatus
    {
        Draft,
        Sent,
        Paid,
        Overdue,
        Cancelled
    }

    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Review,
        Done,
        Unassigned
    }

    public enum ChatType
    {
        Direct,
        Group,
        Channel
    }

    public enum MessageType
    {
        Text,
        Image,
        File,
        Voice
    }

    public enum FileAction
    {
        Created,
        Updated,
        Deleted,
        Moved,
        Copied,
        Shared
    }
}