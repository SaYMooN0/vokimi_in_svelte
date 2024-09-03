using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.test_creation_responses.general
{
    public record DraftGeneralTestQuestionBriefInfo(
        string Id,
        string Text,
        string AnswersType,
        ushort AnswersCount,
        bool IsMultiple,
        ushort OrderInTest
    )
    {
        public static DraftGeneralTestQuestionBriefInfo FromDraftTestQuestion(DraftGeneralTestQuestion question) =>
            new(
                question.Id.Value.ToString(),
                question.Text,
                question.AnswersType.GetId(),
                (ushort)question.Answers.Count(),
                !question.IsSingleChoice,
                question.OrderInTest
            );

    }
}
