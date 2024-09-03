namespace vokimi_api.Src.enums
{
    public enum TestTemplate
    {
        General,
        Scoring,
        CorrectAnswers
    }
    public static class TestTemplateExtensions
    {
        public static string GetId(this TestTemplate template) => template switch {
            TestTemplate.General => "general",
            TestTemplate.Scoring => "scoring",
            TestTemplate.CorrectAnswers => "correct_answers",
            _ => throw new NotImplementedException()
        };
        public static TestTemplate? FromId(string? id) => id switch {
            "general" => TestTemplate.General,
            "scoring" => TestTemplate.Scoring,
            "correct_answers" => TestTemplate.CorrectAnswers,
            _ => null
        };
    }
}
