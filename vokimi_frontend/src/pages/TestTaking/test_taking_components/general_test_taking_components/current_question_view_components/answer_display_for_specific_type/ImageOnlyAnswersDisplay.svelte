<script lang="ts">
    import type { GeneralTestTakingImageOnlyAnswerData } from "../../../../../../ts/page_classes/test_taking_page/general_test/GeneralTestTakingAnswersData";
    import { ImgUtils } from "../../../../../../ts/utils/ImgUtils";
    import AnswerChosenIndicator from "../../../templates_shared/AnswerChosenIndicator.svelte";


    export let onAnswerClick: (answerId: string) => void;
    export let answers: GeneralTestTakingImageOnlyAnswerData[];
    export let isSingleChoice: boolean;
    export let chosenAnswersIds: string[];
</script>

{#each answers.sort((a, b) => a.orderInQuestion - b.orderInQuestion) as answer}
    <div class="answer-btn" on:click={() => onAnswerClick(answer.answerId)}>
        <img src={ImgUtils.imgUrl(answer.image)} />
    </div>
    <AnswerChosenIndicator
        isChosen={chosenAnswersIds.includes(answer.answerId)}
        {isSingleChoice}
    />
{/each}

<style>
    .answer-btn {
        cursor: pointer;
    }
    .answer-btn:hover :global(.is-chosen-indicator) {
        border-color: var(--test-accent);
    }
</style>
