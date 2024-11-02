namespace vokimi_api.Src.db_related.db_entities_ids
{
    public readonly record struct TestTagId(Guid Value)
    {
        public TestTagId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
    public readonly record struct TestTakenRecordId(Guid Value)
    {
        public TestTakenRecordId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
}
