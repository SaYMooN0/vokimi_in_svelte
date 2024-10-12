class UserAdditionalInfo {
    realName: string;
    registrationDate: Date | null;
    birthDate: Date | null;
    links: { [key: string]: string | null };

    constructor(
        realName: string,
        registrationDate: Date | null,
        birthDate: Date | null,
        links: { [key: string]: string | null }
    ) {
        this.realName = realName;
        this.registrationDate = registrationDate;
        this.birthDate = birthDate;
        this.links = links;
    }
    static empty(): UserAdditionalInfo {
        return new UserAdditionalInfo("", null, null, {});
    }
}