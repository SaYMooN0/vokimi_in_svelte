namespace vokimi_api.Src.dtos.responses.test_creation_responses.shared
{
    public record TestPublishingProblem(string Category, string Message)
    {
        public static TestPublishingProblem ForMainInfoCategory(string message) =>
            new("Main info", message);
        public static TestPublishingProblem ForQuestionsCategory(string message) =>
            new("Questions", message);
        public static TestPublishingProblem ForResultsCategory(string message) =>
            new("Results", message);
        public static TestPublishingProblem ForConclusionCategory(string message) =>
            new("Conclusion", message);
        public static TestPublishingProblem ForTagsCategory(string message) =>
            new("Tags", message);
        public static TestPublishingProblem TestNotFound() =>
            new("Test error", "Test not found");
    }
}
