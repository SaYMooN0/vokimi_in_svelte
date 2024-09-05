<script lang="ts">
    import {
        GeneralTestAnswerType,
        GeneralTestAnswerTypeUtils,
    } from "../../../../../../ts/enums/GeneralTestAnswerType";
    import BaseDraftTestEditingDialog from "../../../../creation_shared_components/editing_dialog_components/BaseDraftTestEditingDialog.svelte";

    export let updateParentElementData: () => void;
    export let testId: string;

    export function open() {
        console.log("in");
        dialogElement.open();
    }

    async function createNewQuestion() {
        if (answersType === null || answersType === undefined) {
            dialogElement.setErrorMessage(
                "Please choose the type of the answers",
            );
            return;
        }
        const url = "/api/testCreation/general/createGeneralTestQuestion";
        const data = { testId, answersType };
        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });
        if (response.ok) {
            await updateParentElementData();
            dialogElement.close();
        } else {
            dialogElement.setErrorMessage(
                "Failed to create a new question. Please try again later",
            );
        }
    }
    let dialogElement: BaseDraftTestEditingDialog;
    let answersType: GeneralTestAnswerType;
</script>

<BaseDraftTestEditingDialog
    saveButtonText="Create"
    onSaveButtonClicked={createNewQuestion}
    bind:this={dialogElement}
>
    <label for="answersType" class="property-label">
        Choose type of the answers for the question (you won't be able to change
        it):
    </label>
    <select id="answersType" bind:value={answersType}>
        {#each Object.values(GeneralTestAnswerType) as type}
            <option value={type}>
                {GeneralTestAnswerTypeUtils.getFullName(type)}
            </option>
        {/each}
    </select>
</BaseDraftTestEditingDialog>

<style>
</style>
