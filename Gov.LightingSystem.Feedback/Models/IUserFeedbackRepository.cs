
namespace Gov.LightingSystem.Feedback.Models
{
    public  interface IUserFeedbackRepository
    {
        UserFeedback AddFeedback(UserFeedback feedback);
    }
}
