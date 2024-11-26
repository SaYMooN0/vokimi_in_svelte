export class EditPageLinksSectionData {
    telegram: string | null;
    youtube: string | null;
    facebook: string | null;
    x: string | null;
    instagram: string | null;
    gitHub: string | null;
    linksPrivacy: string | null;
    constructor(
        telegram: string | null,
        youtube: string | null,
        facebook: string | null,
        x: string | null,
        instagram: string | null,
        gitHub: string | null,
        linksPrivacy: string | null
    ) {
        this.telegram = telegram;
        this.youtube = youtube;
        this.facebook = facebook;
        this.x = x;
        this.instagram = instagram;
        this.gitHub = gitHub;
        this.linksPrivacy = linksPrivacy;
    }
}