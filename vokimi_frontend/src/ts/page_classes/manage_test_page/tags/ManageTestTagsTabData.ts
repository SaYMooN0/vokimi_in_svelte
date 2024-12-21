import type { TagSuggestionForTest } from "./TagSuggestionForTest";

export class ManageTestTagsTabData {
    testTags: string[];
    tagsSuggestions: TagSuggestionForTest[];
    tagsSuggestionsAllowed: boolean;
    maxTagsForTestCount: number;
    constructor(
        testTags: string[],
        tagsSuggestions: TagSuggestionForTest[],
        tagsSuggestionsAllowed: boolean,
        maxTagsForTestCount: number,
    ) {
        this.testTags = testTags;
        this.tagsSuggestions = tagsSuggestions;
        this.tagsSuggestionsAllowed = tagsSuggestionsAllowed;
        this.maxTagsForTestCount = maxTagsForTestCount;
    }
}