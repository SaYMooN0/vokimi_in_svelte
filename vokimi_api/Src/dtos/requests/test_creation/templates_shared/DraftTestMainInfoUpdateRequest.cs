﻿using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.requests.test_creation.templates_shared
{
    public record class DraftTestMainInfoUpdateRequest(
        string TestId,
        string Name,
        string Description,
        string Language
    )
    {
        public Err CheckForErr() {
            if (string.IsNullOrWhiteSpace(Name)) {
                return new Err("Test name cannot be empty");
            }
            if (!Guid.TryParse(TestId, out var _)) {
                return new Err("Incorrect data. Please refresh the page and try again");
            }
            if (LanguageExtensions.FromId(Language) is null) {
                return new Err("Please choose a language");
            }
            if (Name.Length > BaseTestCreationConsts.MaxTestNameLength ||
                Name.Length < BaseTestCreationConsts.MinTestNameLength) {
                return new Err($"Test name must be from {BaseTestCreationConsts.MinTestNameLength} to " +
                               $"{BaseTestCreationConsts.MaxTestNameLength} characters");
            }
            int descriptionLength = string.IsNullOrWhiteSpace(Description) ? 0 : Description.Length;
            if (descriptionLength > BaseTestCreationConsts.MaxTestDescriptionLength) {
                return new Err("Description length cannot be more than " +
                              $"{BaseTestCreationConsts.MaxTestDescriptionLength} characters");
            }
            return Err.None;
        }
        public ParsedDraftTestMainInfoUpdateRequest? ParseToObjWithTypes() {
            if (CheckForErr().NotNone()) {
                return null;
            }
            return new ParsedDraftTestMainInfoUpdateRequest(
                new(new(TestId)),
                Name,
                Description,
                LanguageExtensions.FromId(Language).Value
            );

        }
    }
    public record class ParsedDraftTestMainInfoUpdateRequest(
        DraftTestId TestId,
        string Name,
        string Description,
        Language Language
    );
}