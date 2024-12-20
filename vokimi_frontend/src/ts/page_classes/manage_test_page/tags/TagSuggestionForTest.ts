export class TagSuggestionForTest {
    id: string;
    value: string;
    suggestionsCount: number;
    firstSuggestionDate: Date;
    constructor(id: string, value: string, suggestionsCount: number, firstSuggestionDate: Date) {
        this.id = id;
        this.value = value;
        this.suggestionsCount = suggestionsCount;
        this.firstSuggestionDate = firstSuggestionDate;
    }
}