export class EditPageMainInfoSectionData {
    username: string;
    profilePicture: string;
    aboutMe: string;
    bannerColor: string;
    constructor(username: string, profilePicture: string, aboutMe: string, bannerColor: string) {
        this.username = username;
        this.profilePicture = profilePicture;
        this.aboutMe = aboutMe;
        this.bannerColor = bannerColor;
    }
}