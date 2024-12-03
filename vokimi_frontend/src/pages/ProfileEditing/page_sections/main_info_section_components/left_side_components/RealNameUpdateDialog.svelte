<script lang="ts">
    import BaseDialog from "../../../../../components/BaseDialog.svelte";
    import CloseButton from "../../../../../components/shared/CloseButton.svelte";
    import { getErrorFromResponse } from "../../../../../ts/ErrorResponse";
    import { getAuthDataForced } from "../../../../../ts/stores/authStore";
    import { StringUtils } from "../../../../../ts/utils/StringUtils";
    import UpdateDialogSaveBtn from "./UpdateDialogSaveBtn.svelte";

    let dialogElement: BaseDialog;
    let newRealName: string = "";
    let errorMessage: string = "";

    export let updateParentElement: (newVal: string) => void;
    export let currentName: string;
    export function open() {
        newRealName = "";
        errorMessage = "";
        dialogElement.open();
    }

    async function saveNewRealName() {
        if (StringUtils.isNullOrWhiteSpace(newRealName)) {
            return;
        }
        errorMessage = "";
        const response = await fetch("/api/profileEditing/updateRealName", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(newRealName),
        });

        if (response.ok) {
            await getAuthDataForced();
            updateParentElement(newRealName);
            dialogElement.close();
        } else if (response.status === 400) {
            errorMessage = await getErrorFromResponse(response);
        } else {
            errorMessage = "An unknown error occurred.";
        }
    }
</script>

<BaseDialog dialogId="realNameEditingDialog" bind:this={dialogElement}>
    <div class="dialog-content">
        <CloseButton
            onClose={() => {
                dialogElement.close();
            }}
        />
        <p class="current-name">Current real name: {currentName}</p>
        <h1 class="dialog-title">New value:</h1>
        <input
            class="new-name-input"
            type="text"
            bind:value={newRealName}
            placeholder="New value..."
        />
        {#if !StringUtils.isNullOrWhiteSpace(errorMessage)}
            <p class="error-message">{errorMessage}</p>
        {/if}
        <UpdateDialogSaveBtn onSaveClicked={saveNewRealName} />
    </div>
</BaseDialog>

<style>
    .dialog-content {
        display: flex;
        flex-direction: column;
        position: relative;
        align-items: center;
        padding: 12px 32px;
    }
    .current-name {
        max-width: 520px;
        word-break: break-all;
        text-align: center;
        margin-top: 4px;
        margin-bottom: 0;
        font-size: 18px;
        color: var(--text-faded);
    }
    .dialog-title {
        margin: 4px 0;
        font-size: 28px;
        color: var(--text);
    }
    .new-name-input {
        margin-top: 20px;
        width: 480px;
        background-color: var(--back-secondary);
        outline: none;
        border: none;
        border-radius: 4px 4px 2px 2px;
        padding: 4px 8px;
        font-size: 20px;
        border-bottom: 2px solid var(--text-faded);
        color: var(--text);
        transition: all 0.12s ease-in;
    }
    .new-name-input:focus {
        border-bottom: 2px solid var(--primary);
        border-radius: 4px 4px 0 0;
    }
    .error-message {
        margin: 8px 0;
        color: var(--red-del);
    }
</style>
