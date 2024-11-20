<script lang="ts">
    import { StringUtils } from "../../../ts/utils/StringUtils";
    import BaseDialog from "../../BaseDialog.svelte";
    import CloseButton from "../CloseButton.svelte";

    let dialogElement: BaseDialog;
    let creationErr: string = "";
    let dialogId = "collection-creation-dialog" + StringUtils.randomString(4);
    export function open() {
        dialogElement.open();
    }
    function createNewCollection() {}
</script>

<BaseDialog {dialogId} bind:this={dialogElement}>
    <CloseButton onClose={() => dialogElement.close()} />
    <div class="dialog-content">
        <p class="dialog-header">Create new collection</p>
        <input
            type="text"
            class="collection-name-input"
            placeholder="New collection name"
        />
        color icon
        {#if !StringUtils.isNullOrWhiteSpace(creationErr)}
            <p class="error-string">{creationErr}</p>
        {/if}
        <button on:click={createNewCollection}>Save</button>
    </div>
</BaseDialog>

<style>
    .dialog-content {
        padding: 8px 40px;
    }
    .dialog-header {
        font-size: 24px;
        color: var(--text-faded);
        margin: 4px 20px;
        text-align: center;
    }
    .collection-name-input {
        margin-top: 20px;
        width: 100%;
        background-color: var(--back-secondary);
        border-radius: 4px;
        border: 2px solid var(--back-secondary);
        outline: none;
        padding: 6px 16px;
        border-radius: 32px;
        font-size: 20px;
    }
    .collection-name-input::placeholder {
        color: var(--text-faded);
    }
    .collection-name-input:focus {
        border-color: var(--primary);
    }
    .error-string {
        color: var(--red-del);
        font-size: 18px;
        margin-top: 4px;
    }
</style>
