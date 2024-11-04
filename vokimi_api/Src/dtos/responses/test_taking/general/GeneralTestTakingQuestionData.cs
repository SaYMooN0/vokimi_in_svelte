using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared.general_test_answers;
using vokimi_api.Src.db_related.db_entities.published_tests.general_test_related;
using vokimi_api.Src.dtos.responses.test_taking.general.answers_data;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.test_taking.general
{
    public record class GeneralTestTakingQuestionData(
        string Id,
        string Text,
        string? Image,
        ushort OrderInTest,
        ushort MinAnswersCount,
        ushort MaxAnswersCount,
        bool IsSingleChoice,
        string AnswersType,
        IGeneralTestTakingAnswerData[] Answers
    )
    {
        public static GeneralTestTakingQuestionData FromQuestion(GeneralTestQuestion question) => new(
            question.Id.Value.ToString(),
            question.Text,
            question.ImagePath,
            question.OrderInTest,
            question.MinAnswersCount,
            question.MaxAnswersCount,
            question.IsSingleChoice,
            question.AnswersType.GetId(),
            ExtractAnswersFromQuestion(question)
        );
        private static IGeneralTestTakingAnswerData[] ExtractAnswersFromQuestion(GeneralTestQuestion question) =>
            question.AnswersType switch {
                GeneralTestAnswerType.TextOnly => question.Answers.Select(
                    a => GeneralTestTakingTextOnlyAnswerData.FromAnswer(
                        (a.TypeSpecificInfo as TextOnlyAnswerTypeSpecificInfo).Text,
                        a.OrderInQuestion,
                        a.Id
                    )
                ).ToArray(),
                GeneralTestAnswerType.ImageOnly => question.Answers.Select(
                    a => GeneralTestTakingImageOnlyAnswerData.FromAnswer(
                        (a.TypeSpecificInfo as ImageOnlyAnswerTypeSpecificInfo).ImagePath,
                        a.OrderInQuestion,
                        a.Id
                 )
             ).ToArray(),
                GeneralTestAnswerType.TextAndImage => question.Answers.Select(
                    a => GeneralTestTakingTextAndImageAnswerData.FromAnswer(
                        (a.TypeSpecificInfo as TextAndImageAnswerTypeSpecificInfo).Text,
                        (a.TypeSpecificInfo as TextAndImageAnswerTypeSpecificInfo).ImagePath,
                        a.OrderInQuestion,
                        a.Id
                 )
             ).ToArray(),
                _ => throw new ArgumentException($"Unknown answers type: {question.AnswersType}")
            };
    }
}
