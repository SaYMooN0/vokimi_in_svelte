export class TagSuggestionForTest {
    id: string;
    value: string;
    suggestionsCount: number;
    firstSuggestionDate: Date;
    constructor(
        id: string,
        value: string,
        suggestionsCount: number,
        firstSuggestionDate: Date | string
    ) {
        this.id = id;
        this.value = value;
        this.suggestionsCount = suggestionsCount;
        this.firstSuggestionDate =
            firstSuggestionDate instanceof Date
                ? firstSuggestionDate
                : new Date(firstSuggestionDate);
    }
}