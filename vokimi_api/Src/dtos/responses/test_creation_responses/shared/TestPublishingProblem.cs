namespace vokimi_api.Src.dtos.responses.test_creation_responses.shared
{
    public record TestPublishingProblem(string Category, string Err)
    {
        public static TestPublishingProblem ForMainInfoCategory(string err) =>
            new("Main info", err);
        public static TestPublishingProblem ForQuestionsCategory(string err) =>
            new("Questions", err);
        public static TestPublishingProblem ForResultsCategory(string err) =>
            new("Results", err);
        public static TestPublishingProblem ForConclusionCategory(string err) =>
            new("Conclusion", err);
        public static TestPublishingProblem ForTagsCategory(string err) =>
            new("Tags", err);
        public static TestPublishingProblem TestNotFound() =>
            new("Test error", "Test not found");
    }
}
