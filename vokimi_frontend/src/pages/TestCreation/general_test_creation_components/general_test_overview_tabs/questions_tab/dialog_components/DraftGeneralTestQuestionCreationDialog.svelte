<script lang="ts">
    import {
        GeneralTestAnswerType,
        GeneralTestAnswerTypeUtils,
    } from "../../../../../../ts/enums/GeneralTestAnswerType";
    import BaseDraftTestEditingDialog from "../../../../creation_shared_components/editing_dialog_components/BaseDraftTestEditingDialog.svelte";

    export let updateParentElementData: () => void;
    export let testId: string;

    export function open() {
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
    <div class="dialog-content">
        <p class="choose-label">Choose type of the answers for the question</p>
        <p class="no-changes-label">(You won't be able to change it)</p>
        <div class="answer-type-select">
            <select bind:value={answersType}>
                {#each Object.values(GeneralTestAnswerType) as type}
                    <option value={type}>
                        {GeneralTestAnswerTypeUtils.getFullName(type)}
                    </option>
                {/each}
            </select>
        </div>
    </div>
</BaseDraftTestEditingDialog>

<style>
    .dialog-content {
        display: flex;
        align-items: center;
        flex-direction: column;
        padding: 12px 24px;
    }
    .choose-label {
        margin: 4px auto;
        font-size: 24px;
        color: var(--text);
    }
    .no-changes-label {
        margin: 0 auto;
        margin-bottom: 12px;
        font-size: 20px;
        color: var(--text-faded);
    }
    .answer-type-select {
        position: relative;
        width: fit-content;
    }
    .answer-type-select select {
        appearance: none;
        font-size: 18px;
        padding: 12px 40px 12px 16px;
        background-color: var(--back-main);
        border: 2px solid var(--text);
        border-radius: 6px;
        cursor: pointer;
        outline: none;
    }
    .answer-type-select::before,
    .answer-type-select::after {
        --size: 5px;
        content: "";
        position: absolute;
        right: 12px;
        pointer-events: none;
    }

    .answer-type-select::before {
        border-left: var(--size) solid transparent;
        border-right: var(--size) solid transparent;
        border-bottom: var(--size) solid var(--text);
        top: 35%;
    }

    .answer-type-select::after {
        border-left: var(--size) solid transparent;
        border-right: var(--size) solid transparent;
        border-top: var(--size) solid var(--text);
        top: 60%;
    }
</style>
