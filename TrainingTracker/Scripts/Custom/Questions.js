$(document).ready(function () {
    my.questionVm = function () {
        var questionsVm = {},
            currentUser = { UserId: 1 },
            nowShowingQuestions = ko.observableArray(),
            newQuestion = {
                QuestionText: ko.observable(""),
                Description: ko.observable(),
                SkillId: ko.observable(),
                AddedBy: 0,
                LevelMapping:[]
            },
            setQuestionLevel = function (level, value) {
                if (level.questionLevel() == value) {
                    level.questionLevel(0);
                } else {
                    level.questionLevel(value);
                }
            },
            questionLevels = {
                expLevel0: {
                    questionLevel: ko.observable(0)
                },
                expLevel1: {
                    questionLevel: ko.observable(0)
                },
                expLevel2: {
                    questionLevel: ko.observable(0)
                },
                expLevel3: {
                    questionLevel: ko.observable(0)
                },
                expLevel4: {
                    questionLevel: ko.observable(0)
                }
            },
            userSelections = {
                skillId: ko.observable(0),
                startExperience: ko.observable(0),
                endExperience: ko.observable(1),
                level: ko.observable(1),
                addNew: ko.observable(false)
            },
            alerts = {
                questionValidation: ko.observable(""),
                questionAddedSuccess: ko.observable("")
            },
            addQuestionCallback = function (response) {
                my.questionVm.alerts.questionAddedSuccess("Question added.");
                my.questionVm.newQuestion.QuestionText("");
                my.questionVm.newQuestion.Description("");
                my.questionVm.newQuestion.AddedBy = my.meta.currentUser.UserId;
                my.questionVm.getQuestionsBySkillAndExperience();
            },
            addQuestion = function () {
                my.questionVm.alerts.questionAddedSuccess("");
                my.questionVm.newQuestion.SkillId = my.questionVm.userSelections.skillId();
                my.questionsService.addQuestion(my.questionVm.newQuestion, my.questionVm.addQuestionCallback);
            },
            validateAndAddQuestion = function () {
                var message = '';
                my.questionVm.generateLevelMapping();
                my.questionVm.alerts.questionValidation('');
                if (my.questionVm.newQuestion.QuestionText() == '') {
                    message += "Enter question. ";
                }
                if (my.questionVm.newQuestion.LevelMapping.length == 0) {
                    message += "Select at least one experience level. ";
                }

                if (message == '') {
                    my.questionVm.addQuestion();
                } else {
                    my.questionVm.alerts.questionValidation(message);
                }
            },
            clearLevelSelection = function() {
                my.questionVm.questionLevels.expLevel0.questionLevel(0);
                my.questionVm.questionLevels.expLevel1.questionLevel(0);
                my.questionVm.questionLevels.expLevel2.questionLevel(0);
                my.questionVm.questionLevels.expLevel3.questionLevel(0);
                my.questionVm.questionLevels.expLevel4.questionLevel(0);
            },
            generateLevelMapping = function () {
                my.questionVm.newQuestion.LevelMapping = [];
                if (my.questionVm.questionLevels.expLevel0.questionLevel() != 0) {
                    my.questionVm.newQuestion.LevelMapping.push({
                        Level: my.questionVm.questionLevels.expLevel0.questionLevel(),
                        ExperienceStartRange: 0,
                        ExperienceEndRange: 1
                    });
                }
                if (my.questionVm.questionLevels.expLevel1.questionLevel() != 0) {
                    my.questionVm.newQuestion.LevelMapping.push({
                        Level: my.questionVm.questionLevels.expLevel1.questionLevel(),
                        ExperienceStartRange: 1,
                        ExperienceEndRange: 2
                    });
                }
                if (my.questionVm.questionLevels.expLevel2.questionLevel() != 0) {
                    my.questionVm.newQuestion.LevelMapping.push({
                        Level: my.questionVm.questionLevels.expLevel2.questionLevel(),
                        ExperienceStartRange: 2,
                        ExperienceEndRange: 3
                    });
                }
                if (my.questionVm.questionLevels.expLevel3.questionLevel() != 0) {
                    my.questionVm.newQuestion.LevelMapping.push({
                        Level: my.questionVm.questionLevels.expLevel3.questionLevel(),
                        ExperienceStartRange: 3,
                        ExperienceEndRange: 4
                    });
                }
                if (my.questionVm.questionLevels.expLevel4.questionLevel() != 0) {
                    my.questionVm.newQuestion.LevelMapping.push({
                        Level: my.questionVm.questionLevels.expLevel4.questionLevel(),
                        ExperienceStartRange: 4,
                        ExperienceEndRange: 5
                    });
                }
            },
            getQuesionsVmCallback = function (response) {
                my.questionVm.questionsVm = response;
                ko.applyBindings(my.questionVm);
            },
            getQuestionsVm = function () {
                my.questionsService.getQuestionsVm(getQuesionsVmCallback);
            },
            displayFilteredQuestions = function () {
                my.questionVm.nowShowingQuestions([]);
                $.each(my.questionVm.questionsVm.Questions, function (arrayId, item) {
                    if (item.Level == my.questionVm.userSelections.level()) {
                        my.questionVm.nowShowingQuestions.push(item);
                    }
                });
            },
            getQuestionsBySkillAndExperienceCallback = function (response) {
                my.questionVm.questionsVm.Questions = response;
                my.questionVm.displayFilteredQuestions();
            },
            getQuestionsBySkillAndExperience = function () {
                my.questionsService.getQuestionsBySkillAndExperience(my.questionVm.userSelections.skillId(),
                my.questionVm.userSelections.startExperience(), my.questionVm.userSelections.endExperience(),
                my.questionVm.getQuestionsBySkillAndExperienceCallback);
            };
        return {
            questionsVm: questionsVm,
            currentUser: currentUser,
            userSelections: userSelections,
            newQuestion: newQuestion,
            getQuesionsVmCallback: getQuesionsVmCallback,
            getQuestionsVm: getQuestionsVm,
            addQuestionCallback: addQuestionCallback,
            addQuestion: addQuestion,
            validateAndAddQuestion: validateAndAddQuestion,
            getQuestionsBySkillAndExperienceCallback: getQuestionsBySkillAndExperienceCallback,
            getQuestionsBySkillAndExperience: getQuestionsBySkillAndExperience,
            alerts: alerts,
            nowShowingQuestions: nowShowingQuestions,
            displayFilteredQuestions: displayFilteredQuestions,
            questionLevels: questionLevels,
            setQuestionLevel: setQuestionLevel,
            generateLevelMapping: generateLevelMapping,
            clearLevelSelection: clearLevelSelection
        };
    }();
    $('[data-toggle="tooltip"]').tooltip();
    my.questionVm.getQuestionsVm();
});