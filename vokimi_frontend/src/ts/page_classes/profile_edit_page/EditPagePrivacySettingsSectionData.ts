import { PrivacyValuesUtils, type PrivacyValues } from "../../enums/PrivacyValues";

export class EditPagePrivacySettingsSectionData {
    realName: PrivacyValues;
    registrationDate: PrivacyValues;
    birthDate: PrivacyValues;
    publishedTest: PrivacyValues;
    friends: PrivacyValues;
    followers: PrivacyValues;
    followings: PrivacyValues;
    links: PrivacyValues

    constructor(data: any) {
        this.realName = PrivacyValuesUtils.fromId(data.realNamePrivacy);
        this.registrationDate = PrivacyValuesUtils.fromId(data.registrationDatePrivacy);
        this.birthDate = PrivacyValuesUtils.fromId(data.birthDatePrivacy);
        this.publishedTest = PrivacyValuesUtils.fromId(data.publishedTest);
        this.friends = PrivacyValuesUtils.fromId(data.friends);
        this.followers = PrivacyValuesUtils.fromId(data.followers);
        this.followings = PrivacyValuesUtils.fromId(data.followings);
        this.links = PrivacyValuesUtils.fromId(data.linksPrivacy);
    }
}