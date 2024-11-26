export class EditPageMainInfoSectionData {
    nickname: string;
    profilePicture: string;
    aboutMe: string;
    bannerColor: string;
    constructor(nickname: string, profilePicture: string, aboutMe: string, bannerColor: string) {
        this.nickname = nickname;
        this.profilePicture = profilePicture;
        this.aboutMe = aboutMe;
        this.bannerColor = bannerColor;
    }
}