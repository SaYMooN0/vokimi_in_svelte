<script lang="ts">
    import BaseDialog from "../../../../components/BaseDialog.svelte";
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
    let dialogElement: BaseDialog;
    let errorMessage: string = "";
    let isOpen: boolean = false;
</script>

<BaseDialog dialogId="editingDialog" bind:this={dialogElement}>
    {#if isOpen}
        <div class="dialog-body">
            <EditingDialogCloseButton onClose={() => dialogElement.close()} />
            <slot></slot>
            <p class="error-message">{errorMessage}</p>
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
</style>
