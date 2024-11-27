import type { PrivacyValues } from "../../enums/PrivacyValues";

export class EditPageLinksSectionData {
    telegram: string | null;
    youtube: string | null;
    facebook: string | null;
    x: string | null;
    instagram: string | null;
    gitHub: string | null;
    other1: string | null;
    other2: string | null;
    linksPrivacy: PrivacyValues;
    constructor(
        telegram: string | null,
        youtube: string | null,
        facebook: string | null,
        x: string | null,
        instagram: string | null,
        gitHub: string | null,
        other1: string | null,
        other2: string | null,
        linksPrivacy: PrivacyValues
    ) {
        this.telegram = telegram;
        this.youtube = youtube;
        this.facebook = facebook;
        this.x = x;
        this.instagram = instagram;
        this.gitHub = gitHub;
        this.other1 = other1;
        this.other2 = other2;
        this.linksPrivacy = linksPrivacy;
    }
}