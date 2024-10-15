namespace vokimi_api.Src
{
    public record struct Err(string Message)
    {
        public Err(Exception ex) : this(ex.Message) { }
        public Err(string prefix, Err err) : this(prefix + err.ToString()) { }
        public override string ToString() => Message;
        public static Err None => new(string.Empty);
        public bool NotNone() => this != None;
    }
}
