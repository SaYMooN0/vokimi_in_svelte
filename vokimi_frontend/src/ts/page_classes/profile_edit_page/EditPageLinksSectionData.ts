export class EditPageLinksSectionData {
    telegram: string | null;
    youTube: string | null;
    facebook: string | null;
    x: string | null;
    instagram: string | null;
    gitHub: string | null;
    other1: string | null;
    other2: string | null;
    constructor(data: any) {
        this.telegram = data.telegram;
        this.youTube = data.youTube;
        this.facebook = data.facebook;
        this.x = data.x;
        this.instagram = data.instagram;
        this.gitHub = data.gitHub;
        this.other1 = data.other1;
        this.other2 = data.other2;
    }
    public toNameValList(): { linkName: string; linkVal: string | null }[] {
        const dictionary: { linkName: string; linkVal: string | null }[] = [];
        for (const key of Object.keys(this)) {
            const value = (this as any)[key];
            const capitalizedKey = key.charAt(0).toUpperCase() + key.slice(1);
            dictionary.push({ linkName: capitalizedKey, linkVal: value });
        }
        return dictionary;
    }
    public static fromNameValList(
        list: { linkName: string; linkVal: string | null }[]
    ): EditPageLinksSectionData {
        const data: any = {};
        list.forEach(({ linkName, linkVal }) => {
            const lowerCaseKey = linkName.charAt(0).toLowerCase() + linkName.slice(1);
            data[lowerCaseKey] = linkVal;
        });
        return new EditPageLinksSectionData(data);
    }
}