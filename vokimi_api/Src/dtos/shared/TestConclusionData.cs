﻿using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;

namespace vokimi_api.Src.dtos.shared
{
    public record class TestConclusionData(
        string Text,
        string? AdditionalImage,
        bool AnyFeedback,
        string FeedbackText,
        uint MaxFeedbackLength
    )
    {
        public static TestConclusionData FromConclusion(TestConclusion? conclusion) =>
            conclusion is null ?
            new(string.Empty, null, false, "Conclusion Feedback Text", 64) :
            new(
                conclusion.Text,
                conclusion.AdditionalImage,
                conclusion.AnyFeedback,
                conclusion.FeedbackAccompanyingText ?? "",
                conclusion.MaxFeedbackLength
            );
        public Err CheckForErr() {
            int textLength = string.IsNullOrWhiteSpace(Text) ? 0 : Text.Length;
            if (textLength > BaseTestCreationConsts.ConclusionMaxTextLength) {
                return new Err($"Conclusion text cannot be longer than {BaseTestCreationConsts.ConclusionMaxTextLength} characters");
            }
            if (textLength == 0 && string.IsNullOrWhiteSpace(AdditionalImage)) {
                return new Err($"Conclusion must have either text or image");
            }
            if (AnyFeedback) {
                int feedbackAccomplyingTextLength = string.IsNullOrWhiteSpace(FeedbackText) ? 0 : FeedbackText.Length;
                if (feedbackAccomplyingTextLength > BaseTestCreationConsts.ConclusionMaxAccompanyingFeedbackTextLength) {
                    return new Err("Maximal length of the feedback accompanying text is" +
                        $"{BaseTestCreationConsts.ConclusionMaxAccompanyingFeedbackTextLength} characters");
                }
                if (MaxFeedbackLength > BaseTestCreationConsts.ConclusionMaxFeedbackLength) {
                    return new Err("Maximal feedback length cannot be more than " +
                        $"{BaseTestCreationConsts.ConclusionMaxFeedbackLength} characters");
                }
            }
            return Err.None;
        }
    }
}
