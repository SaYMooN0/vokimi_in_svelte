using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.my_tests_page
{
    public record class PublishedTestBriefInfoResponse(
       string Id,
       string Name,
       string Cover,
       string Template,
       DateOnly PublishedDate,
       int TakersCount
    )
    {
        public static PublishedTestBriefInfoResponse FromTest(BaseTest test) => new(
            test.Id.Value.ToString(),
            test.Name,
            test.Cover,
            test.Template.GetId(),
            test.PublicationDate,
            test.BaseTestTakings.Count()
        );
    }
}
