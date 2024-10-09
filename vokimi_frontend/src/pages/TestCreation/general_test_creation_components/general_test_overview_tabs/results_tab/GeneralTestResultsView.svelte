<script lang="ts">
    import ActionConfirmationDialog from "../../../../../components/shared/ActionConfirmationDialog.svelte";
    import { Err } from "../../../../../ts/Err";
    import { getErrorFromResponse } from "../../../../../ts/ErrorResponse";
    import { GeneralTestCreationResultsTabData } from "../../../../../ts/my_tests_page/test_creation_tabs_classes/general_test_creation/results/GeneralTestCreationResultsTabData";
    import TabHeaderWithButton from "../../../creation_shared_components/TabHeaderWithButton.svelte";
    import TabViewDataLoader from "../../../creation_shared_components/TabViewDataLoader.svelte";
    import DraftGeneralTesResultViewElement from "./DraftGeneralTesResultViewElement.svelte";
    import DraftGeneralTestResultEditingDialog from "./DraftGeneralTestResultEditingDialog.svelte";

    export let resultsData: GeneralTestCreationResultsTabData;
    export let testId: string;

    async function loadData() {
        const url =
            "/api/testCreation/general/getGeneralDraftTestResultsData/" +
            testId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            resultsData = new GeneralTestCreationResultsTabData(data);
        } else {
            resultsData = GeneralTestCreationResultsTabData.empty();
        }
    }

    async function createNewResult() {
        const resultName =
            "New Draft General Test Result #" +
            (resultsData.results.length + 1);
        const data = { resultName, testId };
        const response = await fetch(
            "/api/testCreation/general/createNewResult",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(data),
            },
        );
        if (response.ok) {
            await loadData();
        }
    }
    function openResultEditingDialog(id: string) {
        resultEditingDialog.open(id);
    }
    async function openResultDeletingDialog(
        resultId: string,
        resultName: string,
    ) {
        const deletingAction: () => Promise<Err> = async () => {
            const url =
                "/api/testCreation/general/deleteDraftGeneralTestResult/" +
                resultId;
            const response = await fetch(url, {
                method: "DELETE",
            });
            if (response.ok) {
                await loadData();
                resultDeletingDialog.close();
                return Err.none();
            } else {
                const errorMessage = await getErrorFromResponse(response);
                return new Err(errorMessage);
            }
        };

        resultDeletingDialog.open(
            deletingAction,
            `Do you really want to delete "${resultName}" result?`,
        );
    }

    let resultEditingDialog: DraftGeneralTestResultEditingDialog;
    let resultDeletingDialog: ActionConfirmationDialog;
    let errorMessage: string = "";
</script>

<DraftGeneralTestResultEditingDialog
    bind:this={resultEditingDialog}
    updateParentElementData={loadData}
/>
<ActionConfirmationDialog bind:this={resultDeletingDialog} />
<TabViewDataLoader {loadData} isEmpty={() => resultsData.isEmpty()}>
    <div slot="empty">
        <h2>It seems like there is no results added yet</h2>
        <a on:click={loadData}>Refresh</a>
        <p class="error-message">{errorMessage}</p>
        <button on:click={() => createNewResult()}>
            Create First Result
        </button>
    </div>
    <div slot="content">
        <TabHeaderWithButton
            tabName="Test Results({resultsData.results.length})"
            buttonText="Add New Result"
            onButtonClick={() => createNewResult()}
        />
        <div class="tab-content">
            {#each resultsData.results as result}
                <DraftGeneralTesResultViewElement
                    {result}
                    {openResultEditingDialog}
                    {openResultDeletingDialog}
                />
            {/each}
        </div>
    </div>
</TabViewDataLoader>

<style>
    .tab-content {
        display: flex;
        flex-direction: column;
        gap: 10px;
        padding: 10px 40px;
    }
</style>
