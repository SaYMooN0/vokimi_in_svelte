import type { PrivacyValues } from "../../enums/PrivacyValues";

export class EditPageUserPagePrivacySectionData {
    publishedTest: PrivacyValues;
    friends: PrivacyValues;
    followers: PrivacyValues;
    followings: PrivacyValues;
    constructor(publishedTest: PrivacyValues, friends: PrivacyValues, followers: PrivacyValues, followings: PrivacyValues) {
        this.publishedTest = publishedTest;
        this.friends = friends;
        this.followers = followers;
        this.followings = followings;
    }
}