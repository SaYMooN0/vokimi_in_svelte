export class EditPageLinksSectionData {
    telegram: string | null;
    youtube: string | null;
    facebook: string | null;
    x: string | null;
    instagram: string | null;
    gitHub: string | null;
    other1: string | null;
    other2: string | null;
    constructor(data: any) {
        this.telegram = data.telegram;
        this.youtube = data.youTube;
        this.facebook = data.facebook;
        this.x = data.x;
        this.instagram = data.instagram;
        this.gitHub = data.gitHub;
        this.other1 = data.other1;
        this.other2 = data.other2;
    }
    public toDictionary(): Map<string, string | null> {
        const dictionary = new Map<string, string | null>();
        for (const key of Object.keys(this)) {
            const value = (this as any)[key];
            const capitalizedKey = key.charAt(0).toUpperCase() + key.slice(1);
            dictionary.set(capitalizedKey, value);
        }

        return dictionary;
    }
}