<script lang="ts">
    import BaseDialog from "../../../../../../../../components/BaseDialog.svelte";

    export function open() {
        dialogElement.open();
    }
    let dialogElement: BaseDialog;
</script>

<BaseDialog dialogId="result-assigning-dialog" bind:this={dialogElement}>
    <p>Result assigning</p>
</BaseDialog>
