const maxTagsForTestCountForEmptyData = -1;
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

    copy(): TestCreationTagsTabData {
        return new TestCreationTagsTabData(this.tags.slice(), this.maxTagsForTestCount, this.maxTagNameLength);
    }
    isEmpty(): boolean {
        return this.maxTagsForTestCount == maxTagsForTestCountForEmptyData;
    }
    static empty(): TestCreationTagsTabData {
        return new TestCreationTagsTabData([], maxTagsForTestCountForEmptyData, 10);
    }

}
