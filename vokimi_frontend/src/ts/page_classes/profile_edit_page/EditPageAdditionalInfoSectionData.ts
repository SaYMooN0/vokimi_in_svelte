import type { PrivacyValues } from "../../enums/PrivacyValues";

export class EditPageAdditionalInfoSectionData {
    realName: string;
    readonly registrationDate: Date;
    birthDate: Date | null;
    realNamePrivacy: PrivacyValues;
    registrationDatePrivacy: PrivacyValues;
    birthDatePrivacy: PrivacyValues;
    constructor(
        realName: string,
        registrationDate: Date,
        birthDate: Date | null,
        realNamePrivacy: PrivacyValues,
        registrationDatePrivacy: PrivacyValues,
        birthDatePrivacy: PrivacyValues
    ) {
        this.realName = realName;
        this.registrationDate = registrationDate;
        this.birthDate = birthDate;
        this.realNamePrivacy = realNamePrivacy;
        this.registrationDatePrivacy = registrationDatePrivacy;
        this.birthDatePrivacy = birthDatePrivacy;

    }
}