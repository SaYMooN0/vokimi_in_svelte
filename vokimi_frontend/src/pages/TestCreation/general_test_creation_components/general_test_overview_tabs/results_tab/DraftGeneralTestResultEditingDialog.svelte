<script lang="ts">
    import type { DraftGeneralTestResultEditingData } from "../../../../../ts/test_creation_tabs_classes/general_test_creation/results/DraftGenralTestResultEditingData";
    import BaseDraftTestEditingDialog from "../../../creation_shared_components/editing_dialog_components/BaseDraftTestEditingDialog.svelte";

    export let updateParentElementData: () => void;
    let fetchingDataErr: string = "";
    let resultId: string = "";
    export async function open(resultIdVal: string) {
        fetchingDataErr="";
        resultId = resultIdVal;
        //fetchdata
    }

    let resultData: DraftGeneralTestResultEditingData;
    let dialogElement: BaseDraftTestEditingDialog;

    async function saveData() {
        const dataErr: string | null = checkFormDataForError();
        if (dataErr !== null) {
            dialogElement.setErrorMessage(dataErr);
            return;
        }
        const response = await fetch(
            "/api/testCreation/general/updateDraftGeneralTestResultData",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    questionId: resultData,
                }),
            },
        );
        if (response.ok) {
            await updateParentElementData();
            dialogElement.close();
        } else if (response.status === 400) {
            const data = await response.json();
            dialogElement.setErrorMessage(data.error);
        } else {
            dialogElement.setErrorMessage("Unknown error");
        }
    }
    function checkFormDataForError(): string | null {
        return null;
    }
</script>

<BaseDraftTestEditingDialog
    onSaveButtonClicked={saveData}
    bind:this={dialogElement}
>
    {#if fetchingDataErr === ""}
        <div class="dialog-content">
           
        </div>
    {:else}
        <p>{fetchingDataErr}</p>
    {/if}
</BaseDraftTestEditingDialog>

<style>
    .dialog-content {
        width: 1200px;
        max-height: 76vh;
        box-sizing: border-box;
        overflow-y: auto;
        overflow-x: hidden;
    }
    .dialog-content :global(.input-label) {
        gap: 8px;
        font-size: 22px;
        display: flex;
        flex-direction: row;
        justify-content: left;
        align-items: center;
    }
</style>
