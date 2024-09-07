<script lang="ts">
    import { GeneralTestCreationQuestionsTabData } from "../../../../../ts/test_creation_tabs_classes/general_test_creation/GeneralTestCreationQuestionsTabData";
    import TabViewDataLoader from "../../../creation_shared_components/TabViewDataLoader.svelte";
    import TabHeaderWithButton from "../../../creation_shared_components/TabHeaderWithButton.svelte";
    import DraftGeneralTestQuestionEditingDialog from "./dialog_components/DraftGeneralTestQuestionEditingDialog.svelte";
    import DraftGeneralTestQuestionCreationDialog from "./dialog_components/DraftGeneralTestQuestionCreationDialog.svelte";
    import DraftGeneralTestQuestionViewElement from "./DraftGeneralTestQuestionViewElement.svelte";
    import DraftGeneralTestQuestionDeletingConfirmation from "./dialog_components/DraftGeneralTestQuestionDeletingConfirmation.svelte";

    export let questionsData: GeneralTestCreationQuestionsTabData;
    export let testId: string;

    async function loadData() {
        const url =
            "/api/testCreation/general/getGeneralDraftTestQuestionsData/" +
            testId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            questionsData.update(data);
            questionsData = questionsData;
        } else {
            questionsData = GeneralTestCreationQuestionsTabData.empty();
        }
    }

    function openQuestionEditingDialog(id: string) {
        questionEditingDialog.open(id);
    }
    function openQuestionDeletingDialog(id: string) {
        questionDeletingDialog.open(id);
    }

    let questionEditingDialog: DraftGeneralTestQuestionEditingDialog;
    let questionCreationDialog: DraftGeneralTestQuestionCreationDialog;
    let questionDeletingDialog: DraftGeneralTestQuestionDeletingConfirmation;
    let errorMessage: string = "";
</script>

<DraftGeneralTestQuestionCreationDialog
    bind:this={questionCreationDialog}
    updateParentElementData={loadData}
    {testId}
/>
<TabViewDataLoader {loadData} isEmpty={() => questionsData.isEmpty()}>
    <div slot="empty">
        <h2>It seems like there is no questions added yet</h2>
        <a on:click={loadData}>Refresh</a>
        <p class="error-message">{errorMessage}</p>
        <button on:click={() => questionCreationDialog.open()}>
            Create First Question
        </button>
    </div>
    <div slot="content">
        <DraftGeneralTestQuestionEditingDialog
            bind:this={questionEditingDialog}
            updateParentElementData={loadData}
        />
        <DraftGeneralTestQuestionDeletingConfirmation
            bind:this={questionDeletingDialog}
            updateParentElementData={loadData}
        />
        <TabHeaderWithButton
            tabName="Test Questions({questionsData.questions.length})"
            buttonText="Add Question"
            onButtonClick={() => questionCreationDialog.open()}
        />
        <div class="tab-content">
            {#each questionsData.questions.sort((a, b) => a.orderInTest - b.orderInTest) as question}
                <DraftGeneralTestQuestionViewElement
                    {question}
                    {openQuestionEditingDialog}
                    {openQuestionDeletingDialog}
                />
            {/each}
        </div>
    </div>
</TabViewDataLoader>

<style>
    .tab-content {
    }
</style>
