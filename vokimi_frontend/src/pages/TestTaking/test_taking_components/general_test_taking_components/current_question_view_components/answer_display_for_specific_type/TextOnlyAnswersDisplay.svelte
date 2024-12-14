<script lang="ts">
    import type { GeneralTestTakingTextOnlyAnswerData } from "../../../../../../ts/page_classes/test_taking_page/general_test/GeneralTestTakingAnswersData";
    import AnswerChosenIndicator from "../../../templates_shared/AnswerChosenIndicator.svelte";

    export let onAnswerClick: (answerId: string) => void;
    export let answers: GeneralTestTakingTextOnlyAnswerData[];
    export let isSingleChoice: boolean;
    export let chosenAnswersIds: string[];
</script>

<div class="answers-container">
    {#each answers.sort((a, b) => a.orderInQuestion - b.orderInQuestion) as answer}
        <p class="answer-btn" on:click={() => onAnswerClick(answer.answerId)}>
            <AnswerChosenIndicator
                isChosen={chosenAnswersIds.includes(answer.answerId)}
                {isSingleChoice}
            />
            <span class="answer-text">
                {answer.text}
            </span>
        </p>
    {/each}
</div>

<style>
    .answers-container {
        display: flex;
        flex-direction: column;
        gap: 8px;
    }
    .answer-btn {
        display: grid;
        grid-template-columns: auto 1fr;
        gap: 12px;
        border-right: 4px solid transparent;
    }
    .answer-btn:hover {
        border-right: 4px solid var(--test-accent);
    }
    .answer-btn span {
        font-size: 20px;
    }
</style>
