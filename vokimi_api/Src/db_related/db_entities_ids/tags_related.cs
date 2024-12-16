namespace vokimi_api.Src.db_related.db_entities_ids
{
    public readonly record struct TestTagId(Guid Value)
    {
        public TestTagId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
    public readonly record struct TagSuggestionForTestId(Guid Value)
    {
        public TagSuggestionForTestId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
    public readonly record struct BannedTagSuggestionForTestId(Guid Value)
    {
        public BannedTagSuggestionForTestId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
}
