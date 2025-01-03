import { LanguageUtils, type Language } from "../../../enums/Language";
import { PrivacyValuesUtils, type PrivacyValues } from "../../../enums/PrivacyValues";

export class ManageTestOverallTabData {
    readonly testName: string;
    readonly testCover: string;
    testDescription: string | null;
    testPrivacy: PrivacyValues;
    readonly testLanguage: Language;
    readonly testAccentColor: string;

    constructor(testName: string, testCover: string, testDescription: string | null, testPrivacy: PrivacyValues, testLanguage: Language, testAccentColor: string) {
        this.testName = testName;
        this.testCover = testCover;
        this.testDescription = testDescription;
        this.testPrivacy = testPrivacy;
        this.testLanguage = testLanguage;
        this.testAccentColor = testAccentColor;
    }
    static fromResponseData(data: any): ManageTestOverallTabData {
        return new ManageTestOverallTabData(
            data.testName,
            data.testCover,
            data.testDescription,
            PrivacyValuesUtils.fromId(data.testPrivacy),
            LanguageUtils.fromId(data.testLanguage),
            data.testAccentColor
        );
    }
}