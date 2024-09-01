<script lang="ts">
    import BaseDialog from "../../../../components/BaseDialog.svelte";
    import EditingDialogCloseButton from "./EditingDialogCloseButton.svelte";
    import EditingDialogSaveButton from "./EditingDialogSaveButton.svelte";

    export function setErrorMessage(message: string) {
        errorMessage = message;
    }
    export function open() {
        dialogElement.open();
    }
    export function close() {
        dialogElement.close();
    }
    export let onSaveButtonClicked: () => void;
    let dialogElement: BaseDialog;
    let errorMessage: string = "";
</script>

<BaseDialog dialogId="editingDialog" bind:this={dialogElement}>
    <div class="dialog-body">
        <EditingDialogCloseButton onClose={() => dialogElement.close()} />
        <slot></slot>
        <p class="error-message">{errorMessage}</p>
        <EditingDialogSaveButton onClick={() => onSaveButtonClicked()} />
    </div>
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
