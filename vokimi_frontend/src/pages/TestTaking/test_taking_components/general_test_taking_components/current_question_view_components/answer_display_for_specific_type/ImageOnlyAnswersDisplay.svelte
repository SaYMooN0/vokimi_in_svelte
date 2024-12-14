<script lang="ts">
    import type { GeneralTestTakingImageOnlyAnswerData } from "../../../../../../ts/page_classes/test_taking_page/general_test/GeneralTestTakingAnswersData";
    import { ImgUtils } from "../../../../../../ts/utils/ImgUtils";
    import AnswerChosenIndicator from "../../../templates_shared/AnswerChosenIndicator.svelte";

    export let onAnswerClick: (answerId: string) => void;
    export let answers: GeneralTestTakingImageOnlyAnswerData[];
    export let isSingleChoice: boolean;
    export let chosenAnswersIds: string[];
</script>

<div class="answers-container">
    {#each answers.sort((a, b) => a.orderInQuestion - b.orderInQuestion) as answer}
        <div class="answer-btn" on:click={() => onAnswerClick(answer.answerId)}>
            <img src={ImgUtils.imgUrl(answer.image)} />
            <AnswerChosenIndicator
                isChosen={chosenAnswersIds.includes(answer.answerId)}
                {isSingleChoice}
            />
        </div>
    {/each}
</div>

<style>
    .answers-container {
        display: flex;
        flex-direction: row;
        flex-wrap: wrap;
        justify-content: center;
        gap: 12px;
        margin-top: 8px;
    }
    .answer-btn {
        flex-flow: 1;
        display: grid;
        grid-template-rows: 280px auto;
        justify-content: center;
        justify-items: center;
        gap: 8px;
        border-top: 4px solid transparent;
        padding: 8px;
    }
    .answer-btn:hover {
        border-top: 4px solid var(--test-accent);
    }
    .answer-btn img {
        height: 100%;
        max-width: 320px;
        border-radius: 8px;
    }
</style>
