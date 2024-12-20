import type { TagSuggestionForTest } from "./TagSuggestionForTest";

export class ManageTestTagsTabData {
    testTags: string[];
    tagsSuggestions: TagSuggestionForTest[];
    tagsSuggestionsAllowed: boolean;
    constructor(
        testTags: string[],
        tagsSuggestions: TagSuggestionForTest[],
        tagsSuggestionsAllowed: boolean
    ) {
        this.testTags = testTags;
        this.tagsSuggestions = tagsSuggestions;
        this.tagsSuggestionsAllowed = tagsSuggestionsAllowed;
    }
}