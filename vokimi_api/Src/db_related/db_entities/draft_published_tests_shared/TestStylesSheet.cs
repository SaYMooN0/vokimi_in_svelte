using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.db_related.db_entities.draft_published_tests_shared
{
    public class TestStylesSheet
    {
        public TestStylesSheetId Id { get; set; }
        public string AccentColor { get; private set; }
        public ArrowIconType ArrowsType { get; private set; }
        public void Update(string newAccentColor, ArrowIconType newArrowsType) {
            AccentColor = newAccentColor;
            ArrowsType = newArrowsType;
        }
        public static TestStylesSheet CreateNew() => new() {
            Id = new(),
            AccentColor = BaseTestCreationConsts.DefaultAccentColor,
            ArrowsType = ArrowIconType.Default
        };
    }
}
