<script lang="ts">
    import BaseDialog from "../../../../components/BaseDialog.svelte";
    import CloseButton from "../../../../components/shared/CloseButton.svelte";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import type { EditPagePrivacySettingsSectionData } from "../../../../ts/page_classes/profile_edit_page/EditPagePrivacySettingsSectionData";
    import { StringUtils } from "../../../../ts/utils/StringUtils";
    import UpdateDialogSaveBtn from "../main_info_section_components/left_side_components/UpdateDialogSaveBtn.svelte";
    import PrivacySettingValueRadioInput from "./PrivacySettingValueRadioInput.svelte";

    export let sectionData: EditPagePrivacySettingsSectionData;
    export let updateParentElement: (
        newVal: EditPagePrivacySettingsSectionData,
    ) => void;
    let dialogElement: BaseDialog;
    let errorMessage: string = "";
    export function open() {
        errorMessage = "";
        dialogElement.open();
    }
    async function saveNewPrivacySettings() {
        errorMessage = "";
        console.log( JSON.stringify(sectionData));
        const response = await fetch(
            "/api/profileEditing/updatePrivacySettings",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(sectionData),
            },
        );

        if (response.ok) {
            updateParentElement(sectionData);
            dialogElement.close();
        } else if (response.status === 400) {
            errorMessage = await getErrorFromResponse(response);
        } else {
            errorMessage = "An unknown error occurred.";
        }
    }
</script>

<BaseDialog dialogId="linksEditingDialog" bind:this={dialogElement}>
    <div class="dialog-content">
        <CloseButton
            onClose={() => {
                dialogElement.close();
            }}
        />
        <p class="dialog-title-p">Privacy settings</p>
        <div class="settings-container">
            <span class="prop-name">Real Name Visibility</span>
            <PrivacySettingValueRadioInput bind:value={sectionData.realName} />
            <span class="prop-name">Registration Date Visibility</span>
            <PrivacySettingValueRadioInput
                bind:value={sectionData.registrationDate}
            />
            <span class="prop-name">Birthdate Visibility</span>
            <PrivacySettingValueRadioInput bind:value={sectionData.birthDate} />
            <span class="prop-name">Published Tests Visibility</span>
            <PrivacySettingValueRadioInput
                bind:value={sectionData.publishedTest}
            />
            <span class="prop-name">Friends Visibility</span>
            <PrivacySettingValueRadioInput bind:value={sectionData.friends} />
            <span class="prop-name">Followers Visibility</span>
            <PrivacySettingValueRadioInput bind:value={sectionData.followers} />
            <span class="prop-name">Followings Visibility</span>
            <PrivacySettingValueRadioInput
                bind:value={sectionData.followings}
            />
            <span class="prop-name">Links Visibility</span>
            <PrivacySettingValueRadioInput bind:value={sectionData.links} />
        </div>
        {#if !StringUtils.isNullOrWhiteSpace(errorMessage)}
            <p class="error-message">{errorMessage}</p>
        {/if}
        <UpdateDialogSaveBtn onSaveClicked={saveNewPrivacySettings} />
    </div>
</BaseDialog>

<style>
    .dialog-content {
        display: flex;
        flex-direction: column;
        position: relative;
        align-items: center;
        padding: 12px 48px;
    }
    .dialog-title-p {
        margin: 8px 20px;
        font-size: 20px;
        color: var(--text);
    }

    .error-message {
        margin: 8px 0;
        color: var(--red-del);
    }
    .settings-container {
        display: grid;
        grid-template-columns: auto auto;
        row-gap: 12px;
        column-gap: 24px;
        font-size: 20px;
    }
</style>
