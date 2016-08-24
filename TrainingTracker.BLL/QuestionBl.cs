using System;
using System.Collections.Generic;
using TrainingTracker.BLL.Base;
using TrainingTracker.Common.Entity;
using TrainingTracker.Common.ViewModel;

namespace TrainingTracker.BLL
{
    public class QuestionBl : BussinessBase
    {
        public List<Question> GetQuestionsBySkillAndExperience(int skillId, int startExperience, int endExperience)
        {
            return QuestionDataAccesor.GetQuestionsBySkillAndExperience(skillId, startExperience, endExperience);
        }

        public List<Question> GetQuestionsBySkillAndUserId(int skillId, int userId)
        {
            return QuestionDataAccesor.GetQuestionsBySkillAndUserId(skillId, userId);
        }

        public List<Question> GetQuestionsByUserId(int userId)
        {
            return QuestionDataAccesor.GetQuestionsByUserId(userId);
        }

        public bool AddQuestion(Question question)
        {
            question.AddedDate = DateTime.Now;
            question.Description = question.Description ?? string.Empty;
            return QuestionDataAccesor.AddQuestion(question);
        }

        public bool AddSkill(Skill skill)
        {
            return SkillDataAccesor.AddSkill(skill);
        }

        public QuestionVm GetQuestionVm()
        {
            return new QuestionVm
            {
                Categories = SkillDataAccesor.GetSkillsWithQuestionCount(),
                Questions = new List<Question>()
            };
        }
    }
}
