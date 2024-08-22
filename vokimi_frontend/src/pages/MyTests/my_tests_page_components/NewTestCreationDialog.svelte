<script lang="ts">
    import BaseDialog from "../../../components/BaseDialog.svelte";
    let dialogElement: BaseDialog;

    export function open() {
        dialogElement.open();
    }
</script>

<BaseDialog dialogId="new-test-creation-dialog" bind:this={dialogElement}>
    Choose
</BaseDialog>
