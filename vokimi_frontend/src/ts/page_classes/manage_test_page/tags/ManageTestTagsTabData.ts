import { TagSuggestionForTest } from "./TagSuggestionForTest";

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
    static fromResponseData(data: any) {
        return new ManageTestTagsTabData(
            data.testTags,
            data.tagsSuggestions.map(
                (tag: any) =>
                    new TagSuggestionForTest(
                        tag.id,
                        tag.value,
                        tag.suggestionsCount,
                        tag.firstSuggestionDate,
                    ),
            ),
            data.tagsSuggestionsAllowed,
            data.maxTagsForTestCount,
        );
    }
}