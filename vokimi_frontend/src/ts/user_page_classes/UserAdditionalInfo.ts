export class UserAdditionalInfo {
    realName: string;
    registrationDate: string;
    birthDate: string;
    links: { [key: string]: string };
    linksMassage: string;

    constructor(
        realName: string,
        registrationDate: string,
        birthDate: string,
        links: { [key: string]: string },
        linksMassage: string
    ) {
        this.realName = realName;
        this.registrationDate = registrationDate;
        this.birthDate = birthDate;
        this.links = links;
        this.linksMassage = linksMassage;
    }
    static empty(): UserAdditionalInfo {
        return new UserAdditionalInfo("", "", "", {}, "");
    }
}