namespace vokimi_api.Src.enums
{
    public enum ArrowIconType
    {
        Default,
        DefaultCircle,
        Double,
        DoubleSquare,
        Long
    }
    public static class ArrowIconTypeExtensions
    {
        public static string GetId(this ArrowIconType arrow) => arrow switch {
            ArrowIconType.Default => "default",
            ArrowIconType.DefaultCircle => "default_circle",
            ArrowIconType.Double => "double",
            ArrowIconType.DoubleSquare => "double_square",
            ArrowIconType.Long => "long",
            _ => throw new NotImplementedException()
        };
        public static ArrowIconType? FromId(string? id) => id switch {
            "default" => ArrowIconType.Default,
            "default_circle" => ArrowIconType.DefaultCircle,
            "double" => ArrowIconType.Double,
            "double_square" => ArrowIconType.DoubleSquare,
            "long" => ArrowIconType.Long,
            _ => null
        };
    }
}
