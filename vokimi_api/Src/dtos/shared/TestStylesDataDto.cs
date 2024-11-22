using System.Drawing;
using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.shared
{
    public record class TestStylesDataDto(
        string AccentColor,
        string ArrowType
    )
    {
        public static TestStylesDataDto FromTestStylesSheet(TestStylesSheet styles) => new(
            styles.AccentColor,
            styles.ArrowsType.GetId()
        );
        public Err CheckForErr() {
            string accentcolor = this.AccentColor;
            if (string.IsNullOrEmpty(accentcolor) || SharedConsts.HexColorRegex.IsMatch(accentcolor)) {

                return new Err("Invalid accent color");
            }

            ArrowIconType? arrow = ArrowIconTypeExtensions.FromId(this.ArrowType);
            if (arrow is null) {
                return new Err("Incorrect arrow icon type");
            }

            return Err.None;
        }

        public (string accentColor, ArrowIconType arrowType)? GetDataWithTypes() {
            if (CheckForErr().NotNone()) {
                return null;
            }
            ArrowIconType? arrowType = ArrowIconTypeExtensions.FromId(this.ArrowType);
            if (arrowType is null) {
                return null;
            }
            return new(this.AccentColor, arrowType.Value);
        }
    }
}
