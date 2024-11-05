using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared
{
    public class DraftTestMainInfo
    {
        public DraftTestMainInfoId Id { get; init; }
        public string Name { get; private set; }
        public string CoverImagePath { get; private set; }
        public string? Description { get; private set; }
        public Language Language { get; private set; }
        public static DraftTestMainInfo CreateNewFromName(string name) =>
            new()
            {
                Id = new(),
                Name = name,
                CoverImagePath = ImgOperationsConsts.DefaultTestCoverImg,
                Description = null,
                Language = Language.Other
            };

        public void UpdateCoverImage(string path) => CoverImagePath = path;
        public void Update(string name, string? description, Language language)
        {
            Name = name;
            Description = description;
            Language = language;
        }
    }

}
