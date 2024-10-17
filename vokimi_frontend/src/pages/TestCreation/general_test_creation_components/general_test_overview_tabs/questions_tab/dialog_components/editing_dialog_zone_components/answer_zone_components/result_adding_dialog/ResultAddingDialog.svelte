<script lang="ts">
    import LoadingMessage from "../../../../../../../../../components/shared/LoadingMessage.svelte";
    import BaseDraftTestEditingDialog from "../../../../../../../creation_shared_components/editing_dialog_components/BaseDraftTestEditingDialog.svelte";
    import ResultAssigningContent from "./ResultAssigningContent.svelte";
    import ResultCreationContent from "./ResultCreationContent.svelte";

    export async function open(
        alreadyChosenResults: { [key: string]: string },
        chooseResults: (chosenResults: { [key: string]: string }) => void,
    ) {
        dialogElement.open();
        dialogElement.setErrorMessage("");

        chosenResults = { ...alreadyChosenResults };
        updateChosenResults = chooseResults;

        isResultCreationState = false;
        await fetchResults();
    }
    export let testId: string;
    export let maxRelatedResultsForAnswerCount: number;

    let dialogElement: BaseDraftTestEditingDialog;
    let resultCreationElement: ResultCreationContent;

    let isResultCreationState: boolean = false;
    let chosenResults: { [key: string]: string } = {};
    let updateChosenResults: (chosenResults: { [key: string]: string }) => void;
    let allResults: { [key: string]: string } = {};
    let resultsFetched: boolean = false;

    async function fetchResults() {
        resultsFetched = false;
        const url =
            "/api/testCreation/general/getResultsIdNameDictionary/" + testId;
        const response = await fetch(url);
        if (response.ok) {
            allResults = await response.json();
        } else if (response.status === 400) {
            const errorResponse = await response.json();
            dialogElement.setErrorMessage(
                errorResponse.error || "An unknown error occurred.",
            );
        } else {
            dialogElement.setErrorMessage("An unknown error occurred.");
        }
        resultsFetched = true;
    }

    async function onSaveButtonClicked() {
        if (isResultCreationState) {
            await resultCreationElement.onCreateButtonClick();
        } else {
            if (
                Object.entries(chosenResults).length >
                maxRelatedResultsForAnswerCount
            ) {
                dialogElement.setErrorMessage(
                    `Cannot assign more than ${maxRelatedResultsForAnswerCount} results for answer.`,
                );
                return;
            }
            updateChosenResults(chosenResults);
            dialogElement.close();
        }
    }
</script>

<BaseDraftTestEditingDialog
    dialogId="result-assigning-dialog"
    {onSaveButtonClicked}
    bind:this={dialogElement}
    saveButtonText={isResultCreationState ? "Create" : "Save Results"}
>
    <div class="dialog-content">
        {#if resultsFetched}
            {#if isResultCreationState}
                <ResultCreationContent
                    bind:this={resultCreationElement}
                    {testId}
                    setErrorMessage={dialogElement.setErrorMessage}
                    changeStateToResultAssigning={async () => {
                        resultsFetched = false;
                        await fetchResults();
                        isResultCreationState = false;
                    }}
                />
            {:else}
                <ResultAssigningContent
                    {allResults}
                    bind:chosenResults
                    changeStateToResultCreation={() =>
                        (isResultCreationState = true)}
                />
            {/if}
        {:else}
            <LoadingMessage />
        {/if}
    </div>
</BaseDraftTestEditingDialog>

<style>
    .dialog-content {
        padding: 30px;
        min-width: 600px;
        display: flex;
        flex-direction: column;
        align-items: center;
    }
</style>
