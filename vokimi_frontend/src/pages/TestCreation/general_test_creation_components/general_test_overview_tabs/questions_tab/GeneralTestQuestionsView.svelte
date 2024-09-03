<script lang="ts">
    import { GeneralTestCreationQuestionsTabData } from "../../../../../ts/test_creation_tabs_classes/general_test_creation/GeneralTestCreationQuestionsTabData";
    import TabViewDataLoader from "../../../creation_shared_components/TabViewDataLoader.svelte";
    import LoadingMessage from "../../../../../components/shared/LoadingMessage.svelte";
    import TabHeaderWithButton from "../../../creation_shared_components/TabHeaderWithButton.svelte";
    import DraftGeneralTestQuestionEditingDialog from "./DraftGeneralTestQuestionEditingDialog.svelte";

    export let questionsData: GeneralTestCreationQuestionsTabData;
    export let testId: string;

    async function loadData() {
        const url =
            "/api/test-creation/general/getGeneralDraftTestQuestionsData/" +
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

    async function createNewQuestion() {
        const url = "/api/test-creation/general/createGeneralTestQuestion";
        //post to create new question
        
        // const response = await fetch(url);
        // if (response.ok) {
        //     await loadData();
        // } else {
        //     questionsData = GeneralTestCreationQuestionsTabData.empty();
        // }
    }

    let questionEditingDialog: DraftGeneralTestQuestionEditingDialog;
    let errorMessage: string = "";
</script>

<TabViewDataLoader {loadData} isEmpty={() => questionsData.isEmpty()}>
    <div slot="empty">
        <h2>It seems like there is no questions added yet</h2>
        <a on:click={loadData}>Refresh</a>
        <p class="error-message">{errorMessage}</p>
        <button on:click={() => createNewQuestion()}>
            Create First Question
        </button>
    </div>
    <div slot="content">
        <DraftGeneralTestQuestionEditingDialog
            bind:this={questionEditingDialog}
            updateParentElementData={loadData}
        />
        <TabHeaderWithButton tabName="Test Questions" />
        <div class="tab-content">
            {#each questionsData.questions as question}
                <div class="question-container">
                    <label>{question.text}</label>
                    <button
                        on:click={() => openQuestionEditingDialog(question.id)}
                    >
                        Edit
                    </button>
                </div>
            {/each}
        </div>
    </div>
</TabViewDataLoader>

<style>
    .tab-content {
    }
</style>
