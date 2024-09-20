<script lang="ts">
    import { GeneralTestCreationQuestionsTabData } from "../../../../../ts/test_creation_tabs_classes/general_test_creation/questions/GeneralTestCreationQuestionsTabData";
    import TabViewDataLoader from "../../../creation_shared_components/TabViewDataLoader.svelte";
    import TabHeaderWithButton from "../../../creation_shared_components/TabHeaderWithButton.svelte";
    import DraftGeneralTestQuestionEditingDialog from "./dialog_components/DraftGeneralTestQuestionEditingDialog.svelte";
    import DraftGeneralTestQuestionCreationDialog from "./dialog_components/DraftGeneralTestQuestionCreationDialog.svelte";
    import DraftGeneralTestQuestionViewElement from "./DraftGeneralTestQuestionViewElement.svelte";
    import ActionConfirmationDialog from "../../../../../components/shared/ActionConfirmationDialog.svelte";
    import { getErrorFromResponse } from "../../../../../ts/ErrorResponse";

    export let questionsData: GeneralTestCreationQuestionsTabData;
    export let testId: string;

    async function loadData() {
        const url =
            "/api/testCreation/general/getGeneralDraftTestQuestionsData/" +
            testId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            questionsData = new GeneralTestCreationQuestionsTabData(data);
        } else {
            questionsData = GeneralTestCreationQuestionsTabData.empty();
        }
    }

    function openQuestionEditingDialog(id: string) {
        questionEditingDialog.open(id);
    }
    function openQuestionDeletingDialog(
        questionId: string,
        questionText: string,
    ) {
        const deletingAction: () => Promise<string | null> = async () => {
            const url =
                "/api/testCreation/general/deleteGeneralDraftTestQuestion/" +
                questionId;
            const response = await fetch(url, {
                method: "DELETE",
            });
            if (response.ok) {
                await loadData();
                questionDeletingDialog.close();
                return null;
            } else {
                const errorMessage = await getErrorFromResponse(response);
                return errorMessage;
            }
        };
        questionDeletingDialog.open(
            deletingAction,
            `Do you really want to delete "${questionText}" question?`,
        );
    }

    let questionEditingDialog: DraftGeneralTestQuestionEditingDialog;
    let questionCreationDialog: DraftGeneralTestQuestionCreationDialog;
    let questionDeletingDialog: ActionConfirmationDialog;
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
            {testId}
            bind:this={questionEditingDialog}
            updateParentElementData={loadData}
        />
        <ActionConfirmationDialog
            bind:this={questionDeletingDialog}
            confirmBtnText="Delete"
        />
        <TabHeaderWithButton
            tabName="Test Questions({questionsData.questions.length})"
            buttonText="Add Question"
            onButtonClick={() => questionCreationDialog.open()}
        />
        <div class="tab-content">
            {#each questionsData.questions.sort((a, b) => a.orderInTest - b.orderInTest) as question}
                <DraftGeneralTestQuestionViewElement
                    refreshParentComponentAction={loadData}
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
