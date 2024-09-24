export class TestCreationTagsTabData {
    public readonly tags: string[];
    constructor(
        tags: string[] = []
    ) {
        this.tags = tags;
    }
    static empty(): TestCreationTagsTabData {
        return new TestCreationTagsTabData([]);
    }
}
