using vokimi_api.Src.db_related.db_entities.published_tests.published_tests_shared;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.view_test_page
{
    public record class ViewTestBasicTestInfoResponse(
        string TestName,
        string TestCreatorUsername,
        string CreatorId,
        string TestCoverPath,
        string TestDescription,
        string Template,
        string Language,
        string[] Tags
    )
    {
        public static ViewTestBasicTestInfoResponse FromTest(BaseTest test) => new(
            test.Name,
            test.Creator.Username,
            test.CreatorId.Value.ToString(),
            test.Cover,
            test.Description ?? "",
            test.Template.GetId(),
            test.Language.GetId(),
            test.Tags.Select(tag => tag.Value).ToArray()
        );
    }
}
