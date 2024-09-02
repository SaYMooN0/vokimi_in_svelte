<script lang="ts">
    import { GeneralTestCreationQuestionsTabData } from "../../../../../ts/test_creation_tabs_classes/general_test_creation/GeneralTestCreationQuestionsTabData";
    import LoadingMessage from "../../../../../components/shared/LoadingMessage.svelte";
    import ErrorMessageInCenter from "../../../creation_shared_components/ErrorMessageInCenter.svelte";
    import TabHeaderWithButton from "../../../creation_shared_components/TabHeaderWithButton.svelte";
    import DraftGeneralTestQuestionEditingDialog from "./DraftGeneralTestQuestionEditingDialog.svelte";

    export let questionsData: GeneralTestCreationQuestionsTabData;
    export let testId: string;

    async function loadTabData() {
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
    async function onTabEnter() {
        if (questionsData.isEmpty()) {
            await loadTabData();
        }
    }
    function openQuestionEditingDialog(id: string) {
        questionEditingDialog.open(id);
    }
    let questionEditingDialog: DraftGeneralTestQuestionEditingDialog;
</script>

{#await onTabEnter()}
    <LoadingMessage />
{:then}
    <DraftGeneralTestQuestionEditingDialog
        bind:this={questionEditingDialog}
        updateParentElementData={loadTabData}
    />
    <TabHeaderWithButton tabName="Test Questions" />
    <div class="tab-content">
        {#each questionsData.questions as question}{/each}
    </div>
{/await}

<style>
    .tab-content {
    }
</style>
