<script lang="ts">
    import BaseDialog from "../../../../components/BaseDialog.svelte";
    import { StringUtils } from "../../../../ts/utils/StringUtils";
    import EditingDialogCloseButton from "./EditingDialogCloseButton.svelte";
    import EditingDialogSaveButton from "./EditingDialogSaveButton.svelte";

    export function setErrorMessage(message: string) {
        errorMessage = message;
    }
    export function open() {
        isOpen = true;
        dialogElement.open();
    }
    export function close() {
        isOpen = false;
        dialogElement.close();
    }
    export let onSaveButtonClicked: () => void;
    export let saveButtonText: string = "Save";
    export let dialogId: string = "editingDialog";
    let dialogElement: BaseDialog;
    let errorMessage: string = "";
    let isOpen: boolean = false;
</script>

<BaseDialog {dialogId} bind:this={dialogElement}>
    {#if isOpen}
        <div class="dialog-body">
            <EditingDialogCloseButton onClose={() => dialogElement.close()} />
            <slot></slot>
            {#if !StringUtils.isNullOrWhiteSpace(errorMessage)}
                <p class="error-message">{errorMessage}</p>
            {/if}
            <EditingDialogSaveButton
                buttonText={saveButtonText}
                onClick={() => onSaveButtonClicked()}
            />
        </div>
    {/if}
</BaseDialog>

<style>
    .dialog-body {
        position: relative;
        padding: 10px 20px;
        display: flex;
        flex-direction: column;
        gap: 20px;
    }

    .error-message {
        align-self: center;
        margin: 4px 0;
        font-size: 18px;
        font-weight: 500;
    }
</style>
