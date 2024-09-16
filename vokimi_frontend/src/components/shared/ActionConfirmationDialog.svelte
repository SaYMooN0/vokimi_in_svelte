<script lang="ts">
    import { StringUtils } from "../../ts/utils/StringUtils";
    import BaseDialog from "../BaseDialog.svelte";

    export let dialogId: string = "resultConfirmationDialog";
    export let cancelBtnText: string = "Cancel";
    export let confirmBtnText: string = "Confirm";
    export function close() {
        dialogElement.close();
    }
    export function open(
        confirmAction: () => Promise<string | null>,
        text: string,
    ) {
        confirmationText = text;
        onConfirmAction = confirmAction;
        dialogElement.open();
    }
    async function onConfirmButtonClicked() {
        const result = await onConfirmAction();
        if (result === null) {
            return;
        }
        errorString = result;
    }
    let onConfirmAction: () => Promise<string | null>;
    let dialogElement: BaseDialog;
    let confirmationText = "";
    let errorString: string;
</script>

<BaseDialog {dialogId} bind:this={dialogElement}>
    <div class="dialog-content">
        <label class="confirm-action-label">Confirm your action</label>
        <p class="dialog-message">{confirmationText}</p>
        {#if !StringUtils.isNullOrWhiteSpace(errorString)}
            <p class="error-string">{errorString}</p>
        {/if}
        <div class="dialog-buttons">
            <button class="cancel-btn" on:click={() => dialogElement.close()}>
                {cancelBtnText}
            </button>
            <button
                class="confirm-btn"
                on:click={() => onConfirmButtonClicked()}
            >
                {confirmBtnText}
            </button>
        </div>
    </div>
</BaseDialog>

<style>
    .dialog-content {
        padding: 12px 8px;
        width: 800px;
        min-height: 140px;
        display: flex;
        flex-direction: column;
        align-items: center;
        box-shadow: 0px 0px 10px 5px rgba(0, 0, 0, 0.705);
    }

    .confirm-action-label {
        margin-top: 6px;
        color: var(--text-faded);
        font-size: 22px;
    }

    .dialog-message {
        font-size: 24px;
        text-align: center;
    }

    .error-string {
        font-weight: 500;
        font-size: 18px;
        color: var(--del-red);
    }

    .dialog-buttons {
        align-self: end;
        margin: auto 20px 20px auto;
        display: grid;
        grid-template-columns: 1fr 1fr;
        gap: 8px;
    }

    .dialog-buttons button {
        padding: 8px 20px;
        border-radius: 6px;
        outline: none;
        border: none;
        color: var(--back-main);
        font-size: 20px;
        text-align: center;
        cursor: pointer;
        transition: all 0.08s ease-in;
    }
    .dialog-buttons button:hover {
        transform: scale(1.04);
    }

    .cancel-btn {
        background-color: var(--text-faded);
    }

    .confirm-btn {
        background-color: var(--primary);
    }
</style>
