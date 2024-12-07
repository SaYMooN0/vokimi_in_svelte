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


    constructor(
        realName: PrivacyValues,
        registrationDate: PrivacyValues,
        birthDate: PrivacyValues,
        publishedTest: PrivacyValues,
        friends: PrivacyValues,
        followers: PrivacyValues,
        followings: PrivacyValues,
        links: PrivacyValues
    ) {
        this.realName = realName;
        this.registrationDate = registrationDate;
        this.birthDate = birthDate;
        this.publishedTest = publishedTest;
        this.friends = friends;
        this.followers = followers;
        this.followings = followings;
        this.links = links;
    }
    public static fromResponseData(data: any): EditPagePrivacySettingsSectionData {
        return new EditPagePrivacySettingsSectionData(
            PrivacyValuesUtils.fromId(data.realName),
            PrivacyValuesUtils.fromId(data.registrationDate),
            PrivacyValuesUtils.fromId(data.registrationDate),
            PrivacyValuesUtils.fromId(data.publishedTest),
            PrivacyValuesUtils.fromId(data.friends),
            PrivacyValuesUtils.fromId(data.followers),
            PrivacyValuesUtils.fromId(data.followings),
            PrivacyValuesUtils.fromId(data.links),
        );
    }
    public copy(): EditPagePrivacySettingsSectionData {
        return new EditPagePrivacySettingsSectionData(
            this.realName,
            this.registrationDate,
            this.birthDate,
            this.publishedTest,
            this.friends,
            this.followers,
            this.followings,
            this.links
        );
    }
}