<script lang="ts">
    import LoadingMessage from "../../../../../../../../../components/shared/LoadingMessage.svelte";
    import BaseDraftTestEditingDialog from "../../../../../../../creation_shared_components/editing_dialog_components/BaseDraftTestEditingDialog.svelte";
    import ResultAssigningContent from "./ResultAssigningContent.svelte";
    import ResultCreationContent from "./ResultCreationContent.svelte";

    export async function open(chosenResults: { [key: string]: string }) {
        resultsFetched = false;
        dialogElement.open();
        dialogElement.setErrorMessage("");
        chosenResultsRef = chosenResults;
        isResultCreationState = false;
        await fetchResults();
    }
    export let testId: string;

    let dialogElement: BaseDraftTestEditingDialog;
    let resultCreationElement: ResultCreationContent;
    let resultAssigningElement: ResultAssigningContent;

    let isResultCreationState: boolean = false;
    let chosenResultsRef: { [key: string]: string } = {};
    let allResults: { [key: string]: string } = {};
    let resultsFetched: boolean = false;

    async function fetchResults() {
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
    function getAllResultWithoutChosen(): { [key: string]: string } {
        return Object.keys(allResults)
            .filter((key) => !chosenResultsRef[key])
            .reduce((res: { [key: string]: string }, key: string) => {
                res[key] = allResults[key];
                return res;
            }, {});
    }
    async function onSaveButtonClicked() {
        if (isResultCreationState) {
            await resultCreationElement.onCreateButtonClick();
        } else {
            const chosenRes = resultAssigningElement.chosenResultName;
            if (chosenRes === undefined) {
                dialogElement.setErrorMessage("Please choose a result");
            } else {
                const [key, value] = chosenRes;
                chosenResultsRef[key] = value;
                dialogElement.close();
            }
        }
    }
</script>

<BaseDraftTestEditingDialog
    dialogId="result-assigning-dialog"
    {onSaveButtonClicked}
    bind:this={dialogElement}
    saveButtonText={isResultCreationState ? "Create" : "Assign"}
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
                    bind:this={resultAssigningElement}
                    resultsToChooseFrom={Object.entries(
                        getAllResultWithoutChosen(),
                    )}
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
