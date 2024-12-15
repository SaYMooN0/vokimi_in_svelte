export class TagsSuggestionForTest {
    value: string;
    suggestionsCount: number;
    firstSuggestionDate: Date;
    constructor(value: string, suggestionsCount: number, firstSuggestionDate: Date) {
        this.value = value;
        this.suggestionsCount = suggestionsCount;
        this.firstSuggestionDate = firstSuggestionDate;
    }
}