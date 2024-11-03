using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.requests
{
    public record class GeneralTestTakenRequest(
        string TestId,
        //question id and chosen answers ids
        Dictionary<string, string[]> ChosenAnswers,
        string? TestFeedback
    )
    {
        public Err CheckRequestForErr() {
            if (GetParsedId() is null) {
                return new Err("Unable to record test completion because of incorrect test data transfer");
            }
            if (GetParsedAnswers().Count == 0) {
                return new Err("Unable to record test completion because of incorrectly saved answers ");
            }
            return Err.None;

        }
        public Err CheckFeedbackForErr(uint feedbackMaxLength) {
            if (string.IsNullOrEmpty(TestFeedback)) {
                return Err.None;
            }
            if (TestFeedback.Length > feedbackMaxLength) {
                return new Err(
                    $"Length of the feedback is {TestFeedback.Length} characters. " +
                    $"Maximal possible length of feedback is {feedbackMaxLength} characters "
                );
            }
            return Err.None;
        }

        public TestId? GetParsedId() {
            if (Guid.TryParse(TestId, out var id)) {
                return new TestId(id);
            } else {
                return null;
            }
        }
        public Dictionary<GeneralTestQuestionId, GeneralTestAnswerId[]> GetParsedAnswers() {
            if (ChosenAnswers.Values
                .Any(
                    chosenAnswers => chosenAnswers.Any(
                       answerId => !Guid.TryParse(answerId, out var _)
                    )
                )
            ) {
                return [];
            }
            return ChosenAnswers.ToDictionary(
                kvp => new GeneralTestQuestionId(Guid.Parse(kvp.Key)),
                kvp => kvp.Value
                    .Select(a => new GeneralTestAnswerId(Guid.Parse(a)))
                    .ToArray()
            );

        }
    }
}
