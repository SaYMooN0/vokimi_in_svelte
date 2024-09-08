using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared.general_test_answers;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;
using vokimi_api.Src.dtos.shared.draft_general_test_answers;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.test_creation_responses.general
{
    public record class DraftGeneralTestQuestionDataToEdit(
        string Id,
        string Text,
        string? imagePath,
        bool shuffleAnswers,
        string answersType,
        ushort minAnswersCount,
        ushort maxAnswersCount,
        List<BaseDraftGeneralTestAnswerFormData> answers
    )
    {
        public static DraftGeneralTestQuestionDataToEdit FromDraftTestQuestion(DraftGeneralTestQuestion question) =>
            new(
                question.Id.Value.ToString(),
                question.Text,
                question.ImagePath,
                question.ShuffleAnswers,
                question.AnswersType.GetId(),
                question.MinAnswersCount,
                question.MaxAnswersCount,
                ExtractAnswers(question).ToList()
            );
        private static IEnumerable<BaseDraftGeneralTestAnswerFormData> ExtractAnswers(
            DraftGeneralTestQuestion question) {
            if (question.Answers.Count == 0)
                return [];

            switch (question.AnswersType) {
                case GeneralTestAnswerType.TextOnly:
                    return question.Answers.Select(a =>
                        DraftGeneralTestTextOnlyAnswerFormData.New(
                            (a.AdditionalInfo as TextOnlyAnswerAdditionalInfo).Text,
                            a.RelatedResults.ToDictionary(r => r.Id, r => r.Name)
                        )
                    );
                case GeneralTestAnswerType.ImageOnly:
                    return question.Answers.Select(a =>
                       DraftGeneralTestImageOnlyAnswerFormData.New(
                           (a.AdditionalInfo as ImageOnlyAnswerAdditionalInfo).ImagePath,
                           a.RelatedResults.ToDictionary(r => r.Id, r => r.Name)
                       )
                    );
                case GeneralTestAnswerType.TextAndImage:
                    return question.Answers.Select(a =>
                       DraftGeneralTestTextAndImageAnswerFormData.New(
                           (a.AdditionalInfo as TextAndImageAnswerAdditionalInfo).Text,
                           (a.AdditionalInfo as TextAndImageAnswerAdditionalInfo).ImagePath,
                           a.RelatedResults.ToDictionary(r => r.Id, r => r.Name)
                       )
                    );
                default: { throw new NotImplementedException(); }
            }
        }
    }
}