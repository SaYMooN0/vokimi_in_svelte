<script lang="ts">
    import type { GeneralTestTakingTextOnlyAnswerData } from "../../../../../ts/page_classes/test_taking_page/general_test/GeneralTestTakingAnswersData";

    export let onAnswerClick: (answerId: string) => void;
    export let answers: GeneralTestTakingTextOnlyAnswerData[];
</script>

{#each answers as answer}
    <p class="answer-btn" on:click={() => onAnswerClick(answer.id)}>
        {answer.text}
    </p>
{/each}
