<script lang="ts">
    import type { Err } from "../../../ts/Err";
    import BaseDialog from "../../BaseDialog.svelte";

    export let saveTestSettings: () => Promise<Err>;
    export function open() {
        dialogElement.open();
    }
    let dialogElement: BaseDialog;
    async function saveBtnClicked() {
        let savingErr: Err = await saveTestSettings();
        if (savingErr.notNone()) {
        }
    }
</script>

<BaseDialog dialogId="test-settings-editing-dialog" bind:this={dialogElement}>
    <div class="dialog-content"></div>
</BaseDialog>

<style>
    .dialog-content {
        width: min(1400px, 72vw);
        display: flex;
        flex-direction: column;
        padding: 8px 12px;
    }
    .property-label {
        color: var(--text-faded);
    }
    .test-description {
        background-color: var(--back-secondary);
        color: var(--text);
        max-height: max(12vh, 400px);
        resize: vertical;
        outline: none;
        border: 2px solid var(--back-secondary);
        border-radius: 8px;
        box-sizing: border-box;
        padding: 4px 8px;
    }
    .test-description:focus {
        border-color: var(--primary);
    }
</style>
