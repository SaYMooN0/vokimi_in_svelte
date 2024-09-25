export class TestCreationTagsTabData {
    public tags: string[];
    public readonly maxTagsForTestCount: number;
    public readonly maxTagNameLength: number;
    constructor(
        tags: string[] = [],
        maxTagsForTestCount: number,
        maxTagNameLength: number
    ) {
        this.tags = tags;
        this.maxTagsForTestCount = maxTagsForTestCount;
        this.maxTagNameLength = maxTagNameLength;
    }
    static empty(): TestCreationTagsTabData {
        return new TestCreationTagsTabData([], 10, 10);
    }
}
