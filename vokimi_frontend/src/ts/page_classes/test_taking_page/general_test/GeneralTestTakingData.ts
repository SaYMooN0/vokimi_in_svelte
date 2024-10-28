import type { TestStylesArrowType } from "../../../enums/TestStylesArrowType";
import { TestTemplate } from "../../../enums/TestTemplate";
import type { ITestTakingData, TestTakingConclusionData } from "../ITestTakingData";
import type { GeneralTestTakingQuestionData } from "./GeneralTestTakingQuestionData";

export class GeneralTestTakingData implements ITestTakingData {
    readonly testTemplate: TestTemplate = TestTemplate.General;
    readonly accentColor: string;
    readonly arrowIcons: TestStylesArrowType;
    readonly conclusion: TestTakingConclusionData;
    readonly questions: GeneralTestTakingQuestionData[];

    constructor(
        accentColor: string,
        arrowIcons: TestStylesArrowType,
        conclusion: TestTakingConclusionData,
        questions: GeneralTestTakingQuestionData[]
    ) {
        this.accentColor = accentColor;
        this.arrowIcons = arrowIcons;
        this.conclusion = conclusion;
        this.questions = questions;
    }
}
