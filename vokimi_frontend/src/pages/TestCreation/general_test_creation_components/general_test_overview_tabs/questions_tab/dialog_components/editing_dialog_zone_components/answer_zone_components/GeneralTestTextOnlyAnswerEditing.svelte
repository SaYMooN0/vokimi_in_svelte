<script lang="ts">
    import BasicTextAreaInput from "../../../../../../../../components/shared/BasicTextAreaInput.svelte";
    import type { DraftGeneralTestTextOnlyAnswerFormData } from "../../../../../../../../ts/test_creation_tabs_classes/general_test_creation/questions/answers/DraftGeneralTestTextOnlyAnswerFormData";

    export let answerData: DraftGeneralTestTextOnlyAnswerFormData;
</script>

<div class="answer-main-content">
    <label>Text of the answer</label>
    <BasicTextAreaInput bind:text={answerData.text} />
</div>

<style>
    .answer-main-content {
        display: grid;
        grid-template-rows: auto 1fr;
        box-sizing: border-box;
        padding: 4px 10px 10px 10px;
        gap: 2px;
    }

    .answer-main-content label {
        color: var(--text-faded);
        font-size: 20px;
    }
    .answer-main-content > :global(textarea) {
        min-height: 100%;
        max-height: 200px;
        width: 100%;
        resize: vertical;
    }
</style>
