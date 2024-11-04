<script lang="ts">
    import type { GeneralTestTakingTextAndImageAnswerData } from "../../../../../../ts/page_classes/test_taking_page/general_test/GeneralTestTakingAnswersData";
    import { ImgUtils } from "../../../../../../ts/utils/ImgUtils";
    import AnswerChosenIndicator from "../../../templates_shared/AnswerChosenIndicator.svelte";


    export let onAnswerClick: (answerId: string) => void;
    export let answers: GeneralTestTakingTextAndImageAnswerData[];
    export let isSingleChoice: boolean;
    export let chosenAnswersIds: string[];
</script>

{#each answers.sort((a, b) => a.orderInQuestion - b.orderInQuestion) as answer}
    <div class="answer-btn" on:click={() => onAnswerClick(answer.answerId)}>
        <p>{answer.text}</p>
        <img src={ImgUtils.imgUrl(answer.image)} />
        <AnswerChosenIndicator
            isChosen={chosenAnswersIds.includes(answer.answerId)}
            {isSingleChoice}
        />
    </div>
{/each}

<style>
    .answer-btn {
        cursor: pointer;
    }
    .answer-btn:hover :global(.is-chosen-indicator) {
        border-color: var(--test-accent);
    }
</style>
