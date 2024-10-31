namespace vokimi_api.Src.dtos.requests
{
    public record class GeneralTestTakenRequest(
        string TestId,
        Dictionary<int, string> ChosenAnswers,
        string? TestFeedback
    )
    {
        public static Err Validate() {
            return Err.None;
        }
    }
}
