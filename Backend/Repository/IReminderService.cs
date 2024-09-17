namespace Backend.Repository
{
    public interface IReminderService
    {
        Task SendExpiringPolicyRemindersAsync();
    }
}
