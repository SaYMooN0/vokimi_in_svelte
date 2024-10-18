namespace vokimi_api.Src.constants_store_classes
{
    public static class GeneralTestCreationConsts
    {
        public const int
            ResultMinTextLength = 8,
            ResultMaxTextLength = 500,
            ResultNameMinLength = 8,
            ResultNameMaxLength = 60;

        public const ushort MaxRelatedResultsForAnswerCount = 5;
        public const ushort MaxResultsForTestCount = 30;

        public const ushort MaxQuestionsForTestCount = 30;
        public const int
            QuestionTextMinLength = 10,
            QuestionTextMaxLength = 255;

        public const ushort MaxAnswersForQuestionCount = 30;

        public const int
            AnswerTextMinLength = 6,
            AnswerTextMaxLength = 255;
    }
}
