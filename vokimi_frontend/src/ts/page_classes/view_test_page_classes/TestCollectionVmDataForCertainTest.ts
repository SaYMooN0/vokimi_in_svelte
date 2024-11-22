export class TestCollectionVmDataForCertainTest {
    collectionId: string;
    name: string;
    iconName: string;
    color: string;
    isTestIsInCollection: boolean
    constructor(collectionId: string, name: string, iconName: string, color: string, isTestIsInCollection: boolean) {
        this.collectionId = collectionId;
        this.name = name;
        this.iconName = iconName;
        this.color = color;
        this.isTestIsInCollection = isTestIsInCollection;
    }
}