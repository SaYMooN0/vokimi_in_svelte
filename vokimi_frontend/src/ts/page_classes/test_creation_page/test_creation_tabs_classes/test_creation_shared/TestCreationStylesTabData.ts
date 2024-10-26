import { TestStylesArrowType } from "../../../../enums/TestStylesArrowType";

export class TestCreationStylesTabData {
    accentColor: string;
    arrowType: TestStylesArrowType;
    static readonly defaultAccentColor: string = "#796cfa";

    constructor(
        accentColor: string,
        arrowType: TestStylesArrowType
    ) {
        this.accentColor = accentColor;
        this.arrowType = arrowType;
    }
    isEmpty(): boolean {
        return false;
    }
    copy(): TestCreationStylesTabData {
        return new TestCreationStylesTabData(this.accentColor, this.arrowType);
    }
    static empty(): TestCreationStylesTabData {
        return new TestCreationStylesTabData(TestCreationStylesTabData.defaultAccentColor, TestStylesArrowType.Default);
    }

}
