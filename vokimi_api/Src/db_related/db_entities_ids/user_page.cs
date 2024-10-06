namespace vokimi_api.Src.db_related.db_entities_ids
{
    public readonly record struct UserPageSettingsId(Guid Value)
    {
        public UserPageSettingsId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
    public readonly record struct UserPagePostId(Guid Value)
    {
        public UserPagePostId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
}
