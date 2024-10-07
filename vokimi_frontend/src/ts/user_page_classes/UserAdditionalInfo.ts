class UserAdditionalInfo {
    id: string;
    realName: string;
    registrationDate: Date;
    birthDate?: Date;
    links: { [key: string]: string | null };

    private constructor(id: string, registrationDate: Date) {
        this.id = id;
        this.realName = '';
        this.registrationDate = registrationDate;
        this.birthDate = undefined;
        this.links = {};
    }
}