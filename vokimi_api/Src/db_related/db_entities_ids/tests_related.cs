namespace vokimi_api.Src.db_related.db_entities_ids
{
    public readonly record struct TestRatingId(Guid Value)
    {
        public TestRatingId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
    public readonly record struct TestDiscussionsCommentId(Guid Value)
    {
        public TestDiscussionsCommentId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }   
    public readonly record struct DiscussionsCommentAttachmentId(Guid Value)
    {
        public DiscussionsCommentAttachmentId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
    public readonly record struct DiscussionsCommentVoteId(Guid Value)
    {
        public DiscussionsCommentVoteId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
    public readonly record struct TestTakenRecordId(Guid Value)
    {
        public TestTakenRecordId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
    public readonly record struct TestFeedbackRecordId(Guid Value)
    {
        public TestFeedbackRecordId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
}
