import type { PrivacyValues } from "../../enums/PrivacyValues";

export class EditPagePrivacySettingsSectionData {
    realNamePrivacy: PrivacyValues;
    registrationDatePrivacy: PrivacyValues;
    birthDatePrivacy: PrivacyValues;
    publishedTest: PrivacyValues;
    friends: PrivacyValues;
    followers: PrivacyValues;
    followings: PrivacyValues;
    linksPrivacy: PrivacyValues

    constructor(
        realNamePrivacy: PrivacyValues,
        registrationDatePrivacy: PrivacyValues,
        birthDatePrivacy: PrivacyValues,
        publishedTest: PrivacyValues,
        friends: PrivacyValues,
        followers: PrivacyValues,
        followings: PrivacyValues,
        linksPrivacy: PrivacyValues
    ) {
        this.realNamePrivacy = realNamePrivacy;
        this.registrationDatePrivacy = registrationDatePrivacy;
        this.birthDatePrivacy = birthDatePrivacy;
        this.publishedTest = publishedTest;
        this.friends = friends;
        this.followers = followers;
        this.followings = followings;
        this.linksPrivacy = linksPrivacy;
    }
}