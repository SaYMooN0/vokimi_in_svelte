namespace vokimi_api.Src.db_related.db_entities_ids
{
    public readonly record struct DraftTestId(Guid Value)
    {
        public DraftTestId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
    public readonly record struct DraftTestMainInfoId(Guid Value)
    {
        public DraftTestMainInfoId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
    public readonly record struct DraftGeneralTestAnswerId(Guid Value)
    {
        public DraftGeneralTestAnswerId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
    public readonly record struct DraftGeneralTestQuestionId(Guid Value)
    {
        public DraftGeneralTestQuestionId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
    public readonly record struct DraftGeneralTestResultId(Guid Value)
    {
        public DraftGeneralTestResultId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
}
