<script lang="ts">
    import BaseDraftTestEditingDialog from "../../../../creation_shared_components/editing_dialog_components/BaseDraftTestEditingDialog.svelte";
    //confirmation dialog

    export let updateParentElementData: () => void;
    let questionToDeleteId: string;
    export function open(questionId: string) {
        questionToDeleteId = questionId;
        dialogElement.open();
    }
    async function deletingConfirmed() {}
    let dialogElement: BaseDraftTestEditingDialog;
</script>

<BaseDraftTestEditingDialog
    onSaveButtonClicked={deletingConfirmed}
    bind:this={dialogElement}
>
    <p>Deleting of the {questionToDeleteId} question</p>
</BaseDraftTestEditingDialog>

<style>
</style>
