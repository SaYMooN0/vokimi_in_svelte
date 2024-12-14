<script lang="ts">
    import type { GeneralTestTakingTextAndImageAnswerData } from "../../../../../../ts/page_classes/test_taking_page/general_test/GeneralTestTakingAnswersData";
    import { ImgUtils } from "../../../../../../ts/utils/ImgUtils";
    import AnswerChosenIndicator from "../../../templates_shared/AnswerChosenIndicator.svelte";

    export let onAnswerClick: (answerId: string) => void;
    export let answers: GeneralTestTakingTextAndImageAnswerData[];
    export let isSingleChoice: boolean;
    export let chosenAnswersIds: string[];
</script>

<div class="answers-container">
    {#each answers.sort((a, b) => a.orderInQuestion - b.orderInQuestion) as answer}
        <div class="answer-btn" on:click={() => onAnswerClick(answer.answerId)}>
            <AnswerChosenIndicator
                isChosen={chosenAnswersIds.includes(answer.answerId)}
                {isSingleChoice}
            />
            <span>{answer.text}</span>
            <div class="img-container">
                <img src={ImgUtils.imgUrl(answer.image)} />
            </div>
        </div>
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
        grid-template-columns: auto 1fr 280px;
        gap: 12px;
        border-right: 4px solid transparent;

    }
    .answer-btn:hover {
        border-right: 4px solid var(--test-accent);
    }
    .answer-btn span {
        font-size: 20px;
    }
    .answer-btn .img-container {
        width: 100%;
        max-height: 240px;
        border-radius: 8px;
        display: flex;
        justify-content: center;
    }
    .answer-btn .img-container img {
        max-height: inherit;
        object-fit: contain;
        border-radius: 8px;
    }
</style>
