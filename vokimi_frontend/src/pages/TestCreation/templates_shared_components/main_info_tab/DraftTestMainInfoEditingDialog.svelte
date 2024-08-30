<script lang="ts">
    import BaseDialog from "../../../../components/BaseDialog.svelte";
    import EditingDialogCloseButton from "../../creation_shared_components/EditingDialogCloseButton.svelte";

    export let fetchData: () => void;
    async function saveData() {
        await fetchData();
        close();
    }
</script>


<BaseDialog dialogId="draftTestMainInfo">
    <EditingDialogCloseButton onClose={() => {}} />
</BaseDialog>
