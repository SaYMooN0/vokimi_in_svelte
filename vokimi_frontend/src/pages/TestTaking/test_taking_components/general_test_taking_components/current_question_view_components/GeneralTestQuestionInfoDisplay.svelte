<script lang="ts">
    import { ImgUtils } from "../../../../../ts/utils/ImgUtils";
    import { StringUtils } from "../../../../../ts/utils/StringUtils";

    export let questionNumber: number;
    export let totalQuestions: number;
    export let questionText: string;
    export let questionImage: string | null;
    export let minAnswersCount: number;
    export let maxAnswersCount: number;
</script>

<div class="question-display-container">
    <p class="question-num-text">
        Question #{questionNumber} out of {totalQuestions}
    </p>
    <h2 class="question-text">{questionText}</h2>
    {#if !StringUtils.isNullOrWhiteSpace(questionImage)}
        <img
            class="question-image"
            src={ImgUtils.imgUrl(questionImage ?? "")}
            alt="question-img"
        />
    {/if}
    <p class="answers-count-label">
        {#if minAnswersCount === maxAnswersCount && minAnswersCount === 1}
            Choose 1 answer
        {:else}
            Choose from {minAnswersCount} to {maxAnswersCount} answers
        {/if}
    </p>
</div>

<style>
    .question-display-container {
        display: flex;
        flex-direction: column;
        align-items: center;
    }
    .question-num-text {
        font-size: 18px;
        color: var(--text-faded);
        margin: 0;
    }
    .question-text {
        margin: 4px 0;
        font-size: 24px;
        font-weight: 500;
        color: var(--text);
    }
    .answers-count-label {
        font-size: 18px;
        color: var(--text);
        margin: 0;
    }
</style>
