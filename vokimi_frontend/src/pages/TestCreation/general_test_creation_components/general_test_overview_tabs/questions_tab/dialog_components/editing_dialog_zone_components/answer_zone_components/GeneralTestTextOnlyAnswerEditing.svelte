<script lang="ts">
    import type { DraftGeneralTestTextOnlyAnswerFormData } from "../../../../../../../../ts/test_creation_tabs_classes/general_test_creation/draft_general_test_questions/answers/DraftGeneralTestTextOnlyAnswerFormData";

    export let answerData: DraftGeneralTestTextOnlyAnswerFormData;
</script>

<div class="answer-main-content">
    <label>Text of the answer</label>
    <textarea bind:value={answerData.text} />
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
    .answer-main-content textarea {
        min-height: 100%;
        max-height: 200px;
        width: 100%;
        resize: vertical;
        outline: none;
        border-radius: 8px;
        background-color: var(--back-secondary);
        border: 2px solid transparent;
        padding: 3px 6px;
        box-sizing: border-box;
        font-size: 18px;
    }

    .answer-main-content textarea:focus {
        border-color: var(--primary);
    }
</style>
