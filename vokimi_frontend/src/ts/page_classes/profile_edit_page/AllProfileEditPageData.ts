import type { EditPageAdditionalInfoSectionData } from "./EditPageAdditionalInfoSectionData";
import type { EditPageLinksSectionData } from "./EditPageLinksSectionData";
import type { EditPageMainInfoSectionData } from "./EditPageMainInfoSectionData";
import type { EditPageUserPagePrivacySectionData } from "./EditPageUserPagePrivacySectionData";

export class AllProfileEditPageData {
    additionalInfoSection: EditPageAdditionalInfoSectionData;
    readonly email: string;
    mainInfoSection: EditPageMainInfoSectionData;
    userLinks: EditPageLinksSectionData;
    privacySettings: EditPageUserPagePrivacySectionData;
    constructor(
        additionalInfoSection: EditPageAdditionalInfoSectionData,
        email: string,
        mainInfoSection: EditPageMainInfoSectionData,
        userLinks: EditPageLinksSectionData,
        privacySettings: EditPageUserPagePrivacySectionData
    ) {
        this.additionalInfoSection = additionalInfoSection;
        this.email = email;
        this.mainInfoSection = mainInfoSection;
        this.userLinks = userLinks;
        this.privacySettings = privacySettings;
    }
}