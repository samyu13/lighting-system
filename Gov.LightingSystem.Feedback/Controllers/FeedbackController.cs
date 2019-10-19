using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gov.LightingSystem.Feedback.Helper;
using Gov.LightingSystem.Feedback.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gov.LightingSystem.Feedback.Controllers
{
    public class FeedbackController : Controller
    {
        private static readonly string SESS_MODEL_KEY = "feedback";

        [Route("~/")]
        [HttpGet]
        [Route("/feedback/")]
        public IActionResult Index()
        {            
            ViewBag.QuestionId = 1;

            return View(getFeedbackFromSession());
        }
      
       
        [HttpPost("/feedback")]
        [ValidateAntiForgeryToken]
        public ViewResult Index(int id, UserFeedbackModel model)
        {
            // questionId = questionId ?? 1;  
            var objComplex = getFeedbackFromSession();
            switch (id)
            {
                case 1:
                    objComplex.UserName = model.UserName;
                    break;
                case 2:
                    objComplex.EmailAddress = model.EmailAddress;
                    break;
                case 3:
                    objComplex.HomeAddress = model.HomeAddress;
                    break;
                case 4:
                    objComplex.IsHappyWithService = model.IsHappyWithService;
                    break;
                case 5:
                    objComplex.LevelOfBrightness = model.LevelOfBrightness;
                    break;
            }
            HttpContext.Session.SetObject(SESS_MODEL_KEY, objComplex);
            ViewBag.QuestionId = id+1;
            return View(objComplex);
        }       

        private UserFeedbackModel getFeedbackFromSession()
        {

            var objComplex = HttpContext.Session.GetObject<UserFeedbackModel>(SESS_MODEL_KEY);
            if (objComplex == null)
            {
                objComplex = new UserFeedbackModel();
                HttpContext.Session.SetObject(SESS_MODEL_KEY, objComplex);
            }

            return objComplex;

        }
    }


}