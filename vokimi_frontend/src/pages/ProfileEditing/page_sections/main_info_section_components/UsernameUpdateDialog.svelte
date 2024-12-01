<script lang="ts">
    import BaseDialog from "../../../../components/BaseDialog.svelte";
    import CloseButton from "../../../../components/shared/CloseButton.svelte";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { getAuthDataForced } from "../../../../ts/stores/authStore";
    import { StringUtils } from "../../../../ts/utils/StringUtils";

    let dialogElement: BaseDialog;
    let newUsername: string = "";
    let errorMessage: string = "";

    export let updateParentElement: (newVal: string) => void;
    export let currentUsername: string;
    export function open() {
        newUsername = "";
        errorMessage = "";
        dialogElement.open();
    }

    async function saveNewUsername() {
        if (StringUtils.isNullOrWhiteSpace(newUsername)) {
            return;
        }
        errorMessage = "";
        const response = await fetch("/api/profileEditing/updateUsername", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(newUsername),
        });

        if (response.ok) {
            await getAuthDataForced();
            updateParentElement(newUsername);
            dialogElement.close();
        } else if (response.status === 400) {
            errorMessage = await getErrorFromResponse(response);
        } else {
            errorMessage = "An unknown error occurred.";
        }
    }
</script>

<BaseDialog dialogId="textFieldEditingDialog" bind:this={dialogElement}>
    <div class="dialog-content">
        <CloseButton
            onClose={() => {
                dialogElement.close();
            }}
        />
        <p class="current-username">Current username: {currentUsername}</p>
        <h1 class="dialog-title">New username:</h1>
        <input
            class="new-username-input"
            type="text"
            bind:value={newUsername}
            placeholder="New username"
        />
        {#if !StringUtils.isNullOrWhiteSpace(errorMessage)}
            <p class="error-message">{errorMessage}</p>
        {/if}
        <button on:click={saveNewUsername} class="save-btn unselectable">
            Save
        </button>
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
    .current-username {
        max-width: 520px;
        word-break: normal;
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
    .new-username-input {
        margin-top: 20px;
        width: 480px;
        background-color: var(--back-secondary);
        outline: none;
        border: none;
        border-radius: 4px 4px 2px 2px;
        padding: 4px 8px;
        font-size: 20px;
        border-bottom: 2px solid var(--text-faded);
        columns: var(--text);
        transition: all 0.12s ease-in;
    }
    .new-username-input:focus {
        border-bottom: 2px solid var(--primary);
        border-radius: 4px 4px 0 0;
    }
    .error-message {
        margin: 8px 0;
        color: var(--red-del);
    }
    .save-btn {
        margin-top: 32px;
        padding: 4px 24px;
        font-size: 22px;
        color: var(--back-main);
        background-color: var(--primary);
        border-radius: 4px;
        outline: none;
        border: none;
        cursor: pointer;
        transition: all 0.08s ease-in;
    }
    .save-btn:hover {
        background-color: var(--primary-hov);
    }
    .save-btn:active {
        transform: scale(0.94);
    }
</style>
