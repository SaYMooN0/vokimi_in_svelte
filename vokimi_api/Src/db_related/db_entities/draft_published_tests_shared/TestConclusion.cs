using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.draft_published_tests_shared
{
    public class TestConclusion
    {
        public TestConclusionId Id { get; init; }

        public string Text { get; private set; }
        public string? AdditionalImage { get; private set; }
        public bool AnyFeedback { get; private set; }
        public string FeedbackText { get; private set; }
        public uint MaxCharactersForFeedback { get; private set; }
        //Create new 
        //Update

    }
}
