namespace vokimi_api.Src.db_related.db_entities_ids
{
    public readonly record struct TestId(Guid Value)
    {
        public TestId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
    public readonly record struct GeneralTestAnswerId(Guid Value)
    {
        public GeneralTestAnswerId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
    public readonly record struct GeneralTestQuestionId(Guid Value)
    {
        public GeneralTestQuestionId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
    public readonly record struct GeneralTestResultId(Guid Value)
    {
        public GeneralTestResultId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
   
  
}
