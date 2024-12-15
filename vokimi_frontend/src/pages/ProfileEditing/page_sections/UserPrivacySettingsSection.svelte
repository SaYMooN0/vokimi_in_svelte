<script lang="ts">
    import { PrivacyValuesUtils } from "../../../ts/enums/PrivacyValues";
    import type { EditPagePrivacySettingsSectionData } from "../../../ts/page_classes/profile_edit_page/EditPagePrivacySettingsSectionData";
    import SectionHeader from "../section_shared_components/SectionHeader.svelte";
    import SectionsEditButtons from "../section_shared_components/SectionsEditButtons.svelte";
    import PrivacySettingsUpdatingDialog from "./privacy_settings_section_components/PrivacySettingsUpdatingDialog.svelte";

    export let sectionData: EditPagePrivacySettingsSectionData;
    let settingsUpdatingDialog: PrivacySettingsUpdatingDialog;
</script>

<SectionHeader headerText="Privacy settings:" />
<PrivacySettingsUpdatingDialog
    updateParentElement={(newVal) => (sectionData = newVal)}
    {sectionData}
    bind:this={settingsUpdatingDialog}
/>
<div class="privacy-divs-container">
    <p class="privacy-p">
        <span class="privacy-prop-name">Real Name Visibility</span>
        <span class="privacy-prop-value">
            {PrivacyValuesUtils.getFullName(sectionData.realName)}
        </span>
    </p>
    <p class="privacy-p">
        <span class="privacy-prop-name">Registration Date Visibility</span>
        <span class="privacy-prop-value">
            {PrivacyValuesUtils.getFullName(sectionData.registrationDate)}
        </span>
    </p>
    <p class="privacy-p">
        <span class="privacy-prop-name">Birthdate Visibility</span>
        <span class="privacy-prop-value">
            {PrivacyValuesUtils.getFullName(sectionData.birthDate)}
        </span>
    </p>
    <p class="privacy-p">
        <span class="privacy-prop-name">Published Tests Visibility</span>
        <span class="privacy-prop-value">
            {PrivacyValuesUtils.getFullName(sectionData.publishedTest)}
        </span>
    </p>
    <p class="privacy-p">
        <span class="privacy-prop-name">Friends Visibility</span>
        <span class="privacy-prop-value">
            {PrivacyValuesUtils.getFullName(sectionData.friends)}
        </span>
    </p>
    <p class="privacy-p">
        <span class="privacy-prop-name">Followers Visibility</span>
        <span class="privacy-prop-value">
            {PrivacyValuesUtils.getFullName(sectionData.followers)}
        </span>
    </p>
    <p class="privacy-p">
        <span class="privacy-prop-name">Followings Visibility</span>
        <span class="privacy-prop-value">
            {PrivacyValuesUtils.getFullName(sectionData.followings)}
        </span>
    </p>
    <p class="privacy-p">
        <span class="privacy-prop-name">Links Visibility</span>
        <span class="privacy-prop-value">
            {PrivacyValuesUtils.getFullName(sectionData.links)}
        </span>
    </p>
</div>


<SectionsEditButtons
    editButtonAction={() => settingsUpdatingDialog.open()}
    text="Edit privacy settings"
/>

<style>
    .privacy-divs-container {
        display: flex;
        flex-direction: column;
    }

    .privacy-p {
        display: flex;
        justify-content: space-between;
        align-items: center;
        border-bottom: 1px solid var(--text-faded);
        font-size: 22px;
    }
</style>
