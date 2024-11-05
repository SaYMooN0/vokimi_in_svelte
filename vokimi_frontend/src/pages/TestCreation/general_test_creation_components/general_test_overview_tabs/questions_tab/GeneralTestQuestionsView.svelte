<script lang="ts">
    import TabViewDataLoader from "../../../creation_shared_components/TabViewDataLoader.svelte";
    import TabHeaderWithButton from "../../../creation_shared_components/TabHeaderWithButton.svelte";
    import DraftGeneralTestQuestionEditingDialog from "./dialog_components/DraftGeneralTestQuestionEditingDialog.svelte";
    import DraftGeneralTestQuestionCreationDialog from "./dialog_components/DraftGeneralTestQuestionCreationDialog.svelte";
    import DraftGeneralTestQuestionViewElement from "./DraftGeneralTestQuestionViewElement.svelte";
    import ActionConfirmationDialog from "../../../../../components/shared/dialogs/ActionConfirmationDialog.svelte";
    import { getErrorFromResponse } from "../../../../../ts/ErrorResponse";
    import { Err } from "../../../../../ts/Err";
    import { StringUtils } from "../../../../../ts/utils/StringUtils";
    import { GeneralTestCreationQuestionsTabData } from "../../../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/general_test_creation/questions/GeneralTestCreationQuestionsTabData";

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
        const deletingAction: () => Promise<Err> = async () => {
            const url =
                "/api/testCreation/general/deleteGeneralDraftTestQuestion/" +
                questionId;
            const response = await fetch(url, {
                method: "DELETE",
            });
            if (response.ok) {
                await loadData();
                questionDeletingDialog.close();
                return Err.none();
            } else if (response.status === 400) {
                const errorMessage = await getErrorFromResponse(response);
                return new Err(errorMessage);
            }
            return new Err("Something went wrong...");
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
    <div slot="empty" class="no-questions-div">
        <label>It seems like you have not added any questions yet</label>
        <a on:click={loadData}>Refresh</a>
        {#if !StringUtils.isNullOrWhiteSpace(errorMessage)}
            <p class="error-message">{errorMessage}</p>
        {/if}
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
    .no-questions-div {
        position: absolute;
        top: 40%;
        left: 50%;
        transform: translateX(-50%);
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        max-width: min(640px, 60vw);
        padding: 10px 20px;
        border: 2px dashed var(--primary);
        background-color: var(--back-secondary);
        border-radius: 8px;
    }
    .no-questions-div label {
        font-size: 26px;
        color: var(--text);
        text-align: center;
        margin: 12px 20px 8px 20px;
        font-weight: 500;
    }
    .no-questions-div a {
        color: var(--text-faded);
        font-size: 18px;
    }
    .no-questions-div a:hover {
        color: var(--primary);
        text-decoration: underline;
        cursor: pointer;
    }
    .no-questions-div .error-message {
        color: var(--text-faded);
        font-weight: 500;
        font-size: 18px;
        margin: 4px;
    }
    .no-questions-div button {
        margin-top: 24px;
        background-color: var(--primary);
        color: var(--back-main);
        border: none;
        padding: 8px 24px;
        border-radius: 4px;
        font-size: 18px;
        cursor: pointer;
    }
    .no-questions-div button:hover {
        background-color: var(--primary-hov);
    }
</style>
