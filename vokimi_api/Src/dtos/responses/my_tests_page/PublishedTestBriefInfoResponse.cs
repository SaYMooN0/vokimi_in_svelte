using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.enums;
using VokimiShared.src.models.db_classes.test.test_types;

namespace vokimi_api.Src.dtos.responses.my_tests_page
{
    public record class PublishedTestBriefInfoResponse(
       string Id,
       string Name,
       string Cover,
       string Template,
       string PublishedDate,
       int TakersCount,
       float AverageRating,
       int CommentsCount
    )
    {
        public static PublishedTestBriefInfoResponse FromTest(BaseTest test) => new(
            test.Id.Value.ToString(),
            test.Name,
            test.Cover,
            test.Template.GetId(),
            test.PublicationDate.ToString("dd.MM.yyyy"),
            0,
            0,
            0
        );
    }
}
