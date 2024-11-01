<script lang="ts">
    import type { GeneralTestTakingTextOnlyAnswerData } from "../../../../../../ts/page_classes/test_taking_page/general_test/GeneralTestTakingAnswersData";
    import AnswerChosenIndicator from "../../../templates_shared/AnswerChosenIndicator.svelte";


    export let onAnswerClick: (answerId: string) => void;
    export let answers: GeneralTestTakingTextOnlyAnswerData[];
    export let isSingleChoice: boolean;
    export let chosenAnswersIds: string[];
</script>

{#each answers.sort((a, b) => a.orderInQuestion - b.orderInQuestion) as answer}
    <p class="answer-btn" on:click={() => onAnswerClick(answer.answerId)}>
        <AnswerChosenIndicator
            isChosen={chosenAnswersIds.includes(answer.answerId)}
            {isSingleChoice}
        />
        {answer.text}
    </p>
{/each}

<style>
    .answer-btn {
        cursor: pointer;
    }
    .answer-btn:hover :global(.is-chosen-indicator) {
        border-color: var(--primary);
    }
</style>
