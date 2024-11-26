<script lang="ts">
    import AuthorizeView from "../../components/AuthorizeView.svelte";
    import { getErrorFromResponse } from "../../ts/ErrorResponse";
    import type { AllProfileEditPageData } from "../../ts/page_classes/profile_edit_page/AllProfileEditPageData";
    import { StringUtils } from "../../ts/utils/StringUtils";
    import AdditionalInfoSection from "./page_sections/AdditionalInfoSection.svelte";
    import LoginDataSection from "./page_sections/LoginDataSection.svelte";
    import MainInfoSection from "./page_sections/MainInfoSection.svelte";
    import UserLinksSection from "./page_sections/UserLinksSection.svelte";
    import UserPagePrivacySection from "./page_sections/UserPagePrivacySection.svelte";
    let fetchingErr: string = "";
    let pageData: AllProfileEditPageData;
    async function fetchPageData(): Promise<void> {
        fetchingErr = "";
        const response = await fetch("/api/editProfile/getEditProfileData");
        if (response.ok) {
            pageData = await response.json();
        } else if (response.status === 400) {
            fetchingErr = await getErrorFromResponse(response);
        } else {
            fetchingErr = "Unknown error";
        }
    }
</script>

<AuthorizeView>
    <svelte:fragment slot="loading">
        <span>Checking Authentication...</span>
    </svelte:fragment>
    <svelte:fragment slot="authenticated">
        {#await fetchPageData() then _}
            {#if !StringUtils.isNullOrWhiteSpace(fetchingErr) || pageData === null}
                <div class="error-message-div">
                    {StringUtils.isNullOrWhiteSpace(fetchingErr)
                        ? "An error has occurred. Please try refreshing the page"
                        : fetchingErr}
                </div>
            {:else}
                <MainInfoSection sectionData={pageData.mainInfoSection} />
                <AdditionalInfoSection
                    sectionData={pageData.additionalInfoSection}
                />
                <UserLinksSection sectionData={pageData.userLinks} />
                <UserPagePrivacySection
                    sectionData={pageData.privacySettings}
                />
                <LoginDataSection email={pageData.email} />
            {/if}
        {/await}
    </svelte:fragment>
    <div slot="unauthenticated" class="error-message-div">
        You need to log in into your account to edit profile
    </div>
</AuthorizeView>

<style>
    .error-message-div {
        position: absolute;
        top: 40%;
        left: 50%;
        transform: translateX(-50%);
        display: flex;
        justify-content: center;
        align-items: center;
    }
</style>
