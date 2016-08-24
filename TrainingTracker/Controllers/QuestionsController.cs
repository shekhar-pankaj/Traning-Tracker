using System.Web.Mvc;
using TrainingTracker.BLL;
using TrainingTracker.Common.Entity;

namespace TrainingTracker.Controllers
{
    [Authorize]
    public class QuestionsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetQuestionsVm()
        {
            return Json(new QuestionBl().GetQuestionVm(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetQuestionsBySkillAndExperience(int skillId, int startExperience, int endExperience)
        {
            return Json(new QuestionBl().GetQuestionsBySkillAndExperience(skillId, startExperience, endExperience), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetQuestionsBySkillAndUserId(int skillId, int userId)
        {
            return Json(new QuestionBl().GetQuestionsBySkillAndUserId(skillId, userId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetQuestionsByUserId(int userId)
        {
            return Json(new QuestionBl().GetQuestionsByUserId(userId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddQuestion(Question question)
        {
            return Json(new QuestionBl().AddQuestion(question).ToString());
        }

        [HttpPost]
        public ActionResult AddCategory(Skill category)
        {
            return Json(new QuestionBl().AddSkill(category).ToString());
        }
    }
}