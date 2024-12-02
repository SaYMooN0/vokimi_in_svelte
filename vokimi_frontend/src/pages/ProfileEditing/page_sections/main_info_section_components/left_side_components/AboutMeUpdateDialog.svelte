<script lang="ts">
    import BaseDialog from "../../../../../components/BaseDialog.svelte";
    import CloseButton from "../../../../../components/shared/CloseButton.svelte";
    import { getErrorFromResponse } from "../../../../../ts/ErrorResponse";
    import { getAuthDataForced } from "../../../../../ts/stores/authStore";
    import { StringUtils } from "../../../../../ts/utils/StringUtils";
    import UpdateDialogSaveBtn from "./UpdateDialogSaveBtn.svelte";

    let dialogElement: BaseDialog;
    let newAboutMe: string = "";
    let errorMessage: string = "";

    export let updateParentElement: (newVal: string) => void;
    export let currentAboutMe: string;
    export function open() {
        newAboutMe = currentAboutMe;
        errorMessage = "";
        dialogElement.open();
    }

    async function saveNewAboutMe() {
        if (StringUtils.isNullOrWhiteSpace(newAboutMe)) {
            return;
        }
        errorMessage = "";
        const response = await fetch("/api/profileEditing/updateAboutMe", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(newAboutMe),
        });

        if (response.ok) {
            await getAuthDataForced();
            updateParentElement(newAboutMe);
            dialogElement.close();
        } else if (response.status === 400) {
            errorMessage = await getErrorFromResponse(response);
        } else {
            errorMessage = "An unknown error occurred.";
        }
    }
</script>

<BaseDialog dialogId="aboutMeEditingDialog" bind:this={dialogElement}>
    <div class="dialog-content">
        <CloseButton
            onClose={() => {
                dialogElement.close();
            }}
        />
        <p class="dialog-title-p">About me:</p>
        <textarea class="about-me-textarea" bind:value={newAboutMe} />
        <label class="textarea-length-label">
            length:
            <span>
                {newAboutMe.length}
            </span>
        </label>
        {#if !StringUtils.isNullOrWhiteSpace(errorMessage)}
            <p class="error-message">{errorMessage}</p>
        {/if}
        <UpdateDialogSaveBtn onSaveClicked={saveNewAboutMe} />
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
        align-self: flex-start;
    }
    .about-me-textarea {
        resize: none;
        width: 520px;
        height: 240px;
        background-color: var(--back-secondary);
        outline: none;
        border: 2px solid var(--text-faded);
        border-radius: 6px;
        padding: 4px 8px;
        box-sizing: border-box;
        font-size: 20px;
        color: var(--text);
        border-bottom: 2px solid var(--text-faded);
        transition: all 0.12s ease-in;
    }
    .about-me-textarea:focus {
        border-color: var(--primary);
    }
    .textarea-length-label {
        display: block;
        margin: 2px 12px 0 auto;
        color: var(--text-faded);
        font-size: 14px;
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 4px;
    }
    .textarea-length-label span {
        min-width: 32px;
        display: flex;
    }
    .error-message {
        margin: 8px 0;
        color: var(--red-del);
    }
</style>
