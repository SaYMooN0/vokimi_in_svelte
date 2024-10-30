<script lang="ts">
    import { GeneralTestTakingTextAndImageAnswerData } from "../../../../../ts/page_classes/test_taking_page/general_test/GeneralTestTakingAnswersData";
    import { ImgUtils } from "../../../../../ts/utils/ImgUtils";

    export let onAnswerClick: (answerId: string) => void;
    export let answers: GeneralTestTakingTextAndImageAnswerData[];
</script>

{#each answers.sort((a, b) => a.orderInQuestion - b.orderInQuestion) as answer}
    <div class="answer-btn" on:click={() => onAnswerClick(answer.id)}>
        <p>{answer.text}</p>
        <img src={ImgUtils.imgUrl(answer.image)} />
    </div>
{/each}
