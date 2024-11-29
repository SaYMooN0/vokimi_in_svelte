<script lang="ts">
    import BaseDialog from "../../../components/BaseDialog.svelte";
    import CloseButton from "../../../components/shared/CloseButton.svelte";
    import type { Err } from "../../../ts/Err";
    import { StringUtils } from "../../../ts/utils/StringUtils";

    let stringValue: string = "";
    let errorMessage: string = "";
    let inputPlaceHolderString: string = "";
    let saveChanges: (newVal: string) => Promise<Err>;
    let dialogTitle: string;
    let isHidden: boolean = true;
    export function open(
        dialogTitleMessage: string,
        initialValue: string,
        inputPlaceholder: string,
        saveChangesFunc: (newVal: string) => Promise<Err>,
    ) {
        stringValue = initialValue;
        inputPlaceHolderString = inputPlaceholder;
        dialogTitle = dialogTitleMessage;
        errorMessage = "";
        errorMessage = "";
        saveChanges = saveChangesFunc;
        isHidden = false;
        dialogElement.open();
    }

    async function saveButtonClicked() {
        errorMessage = "";
        let savingErr: Err = await saveChanges(stringValue);
        if (savingErr.notNone()) {
            errorMessage = savingErr.toString();
            return;
        }
        dialogElement.close();
        isHidden = true;
    }
    let dialogElement: BaseDialog;
</script>

<BaseDialog dialogId="textFieldEditingDialog" bind:this={dialogElement}>
    {#if !isHidden}
        <div class="dialog-content">
            <CloseButton
                onClose={() => {
                    dialogElement.close();
                    isHidden = true;
                }}
            />
            <h1 class="dialog-title">{dialogTitle}</h1>
            <input
                type="text"
                bind:value={stringValue}
                placeholder={inputPlaceHolderString}
            />
            {#if !StringUtils.isNullOrWhiteSpace(errorMessage)}
                <p class="error-message">{errorMessage}</p>
            {/if}
            <button on:click={saveButtonClicked}>Save</button>
        </div>
    {/if}
</BaseDialog>

<style>
    .dialog-content {
        display: flex;
        flex-direction: column;
        position: relative;
        align-items: center;
        gap: 12px;
        padding: 20px 24px;
        width: 500px;
    }
</style>
