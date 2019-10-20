using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.LightingSystem.Feedback.Models
{
    public class SQLRepository : IUserFeedbackRepository
    {
        private readonly AppDbContext _context;

        public SQLRepository(AppDbContext context)
        {
            _context = context;
        }

        public UserFeedback AddFeedback(UserFeedback feedback)
        {
            try
            {
                _context.UserFeedbacks.Add(feedback);
                _context.SaveChanges();
                return feedback;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
