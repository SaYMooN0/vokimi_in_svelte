<script lang="ts">
    import { GeneralTestCreationResultsTabData } from "../../../../../ts/test_creation_tabs_classes/general_test_creation/GeneralTestCreationResultsTabData";
    import TabHeaderWithButton from "../../../creation_shared_components/TabHeaderWithButton.svelte";
    import TabViewDataLoader from "../../../creation_shared_components/TabViewDataLoader.svelte";

    export let resultsData: GeneralTestCreationResultsTabData;
    export let testId: string;

    async function loadData() {
        const url =
            "/api/testCreation/general/getTestResultsDataToEdit/" + testId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            resultsData = new GeneralTestCreationResultsTabData(data);
        } else {
            resultsData = GeneralTestCreationResultsTabData.empty();
        }
    }
    async function createNewResult() {
        const name = "New Result #" + resultsData.results.length+1;
        //server
        await loadData(); 
    }
    function openResultEditingDialog(id: string) {
        resultEditingDialog.open(id);
    }
    function openResultDeletingDialog(id: string) {
        resultDeletingDialog.open(id);
    }

    let resultEditingDialog: DraftGeneralTestResultEditingDialog;
    let resultDeletingDialog: DraftGeneralTestQuestionDeletingConfirmation;
    let errorMessage: string = "";
</script>

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
            onButtonClick={() => createNewResult}
        />
        <div class="tab-content">
            {#each resultsData.results as question}
                <!-- result view Element -->
            {/each}
        </div>
    </div>
</TabViewDataLoader>

<style>
    .tab-content {
    }
</style>
