namespace vokimi_api.Src.enums
{
    public enum TestTemplate
    {
        General,
        Knowledge
    }
    public static class TestTemplateExtensions
    {
        public static string[] Features(this TestTemplate template) =>
            template switch {
                TestTemplate.General => [
                    "Completely customize your test the way you want it",
                    "No restrictions or necessities (almost)"],
                TestTemplate.Knowledge => [
                    "See how well test takers know the subject",
                    "Specially selected types of questions and the method of evaluating answers"],
                _ => throw new NotImplementedException()
            };
    }
}
