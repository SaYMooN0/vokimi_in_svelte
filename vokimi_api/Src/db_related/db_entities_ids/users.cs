namespace vokimi_api.Src.db_related.db_entities_ids
{
    public readonly record struct AppUserId(Guid Value)
    {
        public AppUserId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
    public readonly record struct UnconfirmedAppUserId(Guid Value)
    {
        public UnconfirmedAppUserId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
    public readonly record struct LoginInfoId(Guid Value)
    {
        public LoginInfoId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }
    public readonly record struct UserAdditionalInfoId(Guid Value)
    {
        public UserAdditionalInfoId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }    
    public readonly record struct TestCollectionId(Guid Value)
    {
        public TestCollectionId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    } 
}
