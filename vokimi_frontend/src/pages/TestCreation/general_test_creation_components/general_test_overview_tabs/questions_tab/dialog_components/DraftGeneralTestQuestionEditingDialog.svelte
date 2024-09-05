<script lang="ts">
    import BaseDraftTestEditingDialog from "../../../../creation_shared_components/editing_dialog_components/BaseDraftTestEditingDialog.svelte";
    import TextWithOptionalImageInput from "../../../../creation_shared_components/TextWithOptionalImageInput.svelte";

    export let updateParentElementData: () => void;
    let fetchingDataErr: string = "";
    export async function open(questionIdVal: string) {
        fetchingDataErr = "";
        questionId = questionIdVal;
        const url =
            "/api/testCreation/general/getDraftGeneralTestQuestionDataToEdit/" +
            questionId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            console.log(data);
        } else if (response.status === 400) {
            const data = await response.json();
            fetchingDataErr = data.error;
        } else {
            fetchingDataErr = "Unknown error";
        }
        dialogElement.open();
    }

    let questionId: string;
    let questionText: string = "";
    //multiple answers info

    let dialogElement: BaseDraftTestEditingDialog;

    async function saveData() {
        const dataErr: string | null = checkFormDataForError();
        if (dataErr !== null) {
            dialogElement.setErrorMessage(dataErr);
            return;
        }
        const response = await fetch(
            "/api/testCreation/general/updateDraftGeneralTestQuestionData",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    questionId: questionId,
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
        <TextWithOptionalImageInput />
        <p>Editing of the {questionId} question</p>
    {:else}
        <p>{fetchingDataErr}</p>
    {/if}
</BaseDraftTestEditingDialog>

<style>
</style>
