using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.LightingSystem.Feedback.Models
{
    public class UserFeedbackRepository : IUserFeedbackRepository
    {
        private readonly IUserFeedbackRepository _userFeedbackRepository;

        public UserFeedbackRepository(IUserFeedbackRepository userFeedbackRepository)
        {
            _userFeedbackRepository = userFeedbackRepository;
        }

        public UserFeedback AddFeedback(UserFeedback feedback)
        {
          return   _userFeedbackRepository.AddFeedback(feedback);
        }
    }
}
