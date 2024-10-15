using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared.general_test_answers;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;
using vokimi_api.Src.dtos.shared.general_test_creation.draft_general_test_answers;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.test_creation_responses.general
{
    public record class DraftGeneralTestQuestionDataResponse(
        string Id,
        string Text,
        string? ImagePath,
        bool ShuffleAnswers,
        string AnswersType,
        bool IsMultiple,
        ushort MinAnswersCount,
        ushort MaxAnswersCount,
        BaseDraftGeneralTestAnswerFormData[] answers
    )
    {
        public static DraftGeneralTestQuestionDataResponse FromDraftTestQuestion(DraftGeneralTestQuestion question) =>
            new(
                question.Id.Value.ToString(),
                question.Text,
                question.ImagePath,
                question.ShuffleAnswers,
                question.AnswersType.GetId(),
                !question.IsSingleChoice,
                question.MinAnswersCount,
                question.MaxAnswersCount,
                ExtractAnswers(question).ToArray()
            );
        private static IEnumerable<BaseDraftGeneralTestAnswerFormData> ExtractAnswers(
            DraftGeneralTestQuestion question)
        {
            if (question.Answers.Count == 0)
                return [];

            switch (question.AnswersType)
            {
                case GeneralTestAnswerType.TextOnly:
                    return question.Answers.Select(a => new DraftGeneralTestTextOnlyAnswerFormData(
                            (a.AdditionalInfo as TextOnlyAnswerAdditionalInfo).Text,
                            a.RelatedResults.ToDictionary(r => r.Id, r => r.Name)
                        )
                    );
                case GeneralTestAnswerType.ImageOnly:
                    return question.Answers.Select(a => new DraftGeneralTestImageOnlyAnswerFormData(
                           (a.AdditionalInfo as ImageOnlyAnswerAdditionalInfo).ImagePath,
                           a.RelatedResults.ToDictionary(r => r.Id, r => r.Name)
                       )
                    );
                case GeneralTestAnswerType.TextAndImage:
                    return question.Answers.Select(a => new DraftGeneralTestTextAndImageAnswerFormData(
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