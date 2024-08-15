namespace vokimi_api.Src.db_related.db_entities_ids
{
    public readonly record struct TestConclusionId(Guid Value)
    {
        public TestConclusionId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }

    public readonly record struct TestStylesSheetId(Guid Value)
    {
        public TestStylesSheetId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
    public readonly record struct GeneralTestAnswerTypeSpecificInfoId(Guid Value)
    {
        public GeneralTestAnswerTypeSpecificInfoId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();

    }
   
}
