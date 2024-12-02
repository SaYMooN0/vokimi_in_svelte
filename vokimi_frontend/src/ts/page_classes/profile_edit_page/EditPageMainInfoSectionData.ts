export class EditPageMainInfoSectionData {
    username: string;
    profilePicture: string;
    bannerColor: string;
    realName: string;
    birthDate: Date | null;
    aboutMe: string;
    readonly registrationDate: Date;

    constructor(
        username: string,
        profilePicture: string,
        bannerColor: string,
        realName: string,
        birthDate: Date | null,
        aboutMe: string,
        registrationDate: Date
    ) {
        this.username = username;
        this.profilePicture = profilePicture;
        this.bannerColor = bannerColor;
        this.realName = realName;
        this.birthDate = birthDate;
        this.aboutMe = aboutMe;
        this.registrationDate = registrationDate;
    }
}