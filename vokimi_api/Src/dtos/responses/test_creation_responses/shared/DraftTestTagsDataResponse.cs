using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;

namespace vokimi_api.Src.dtos.responses.test_creation_responses.shared
{
    public record class DraftTestTagsDataResponse(
        string[] tags,
        int maxTagsForTestCount,
        int maxTagNameLength
    )
    {
        public static DraftTestTagsDataResponse FromDraftTest(BaseDraftTest test) {
            if (test is null) {
                throw new ArgumentNullException(nameof(test));
            }
            return new DraftTestTagsDataResponse(
                test.Tags.ToArray(),
                TestTagsConsts.MaxTagsForTestCount,
                TestTagsConsts.MaxTagLength
            );
        }
    }
}
