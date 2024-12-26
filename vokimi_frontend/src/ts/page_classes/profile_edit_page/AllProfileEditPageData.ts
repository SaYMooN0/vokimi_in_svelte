import type { EditPageLinksSectionData } from "./EditPageLinksSectionData";
import type { EditPageMainInfoSectionData } from "./EditPageMainInfoSectionData";
import type { EditPagePrivacySettingsSectionData } from "./EditPagePrivacySettingsSectionData";

export class AllProfileEditPageData {
    readonly email: string;
    mainInfoSection: EditPageMainInfoSectionData;
    userLinks: EditPageLinksSectionData;
    privacySettings: EditPagePrivacySettingsSectionData;
    constructor(
        email: string,
        mainInfoSection: EditPageMainInfoSectionData,
        userLinks: EditPageLinksSectionData,
        privacySettings: EditPagePrivacySettingsSectionData
    ) {
        this.email = email;
        this.mainInfoSection = mainInfoSection;
        this.userLinks = userLinks;
        this.privacySettings = privacySettings;
    }
}