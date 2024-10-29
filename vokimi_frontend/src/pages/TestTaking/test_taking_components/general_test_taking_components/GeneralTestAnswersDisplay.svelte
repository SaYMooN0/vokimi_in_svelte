<script lang="ts">
    import { GeneralTestAnswerType } from "../../../../ts/enums/GeneralTestAnswerType";
    import type { BaseGeneralTestTakingAnswerData } from "../../../../ts/page_classes/test_taking_page/general_test/GeneralTestTakingAnswersData";

    export let answersType: GeneralTestAnswerType;
    export let answers: BaseGeneralTestTakingAnswerData[];
    export let isSingleChoice: boolean;
    let chosenAnswersIds: string[] = [];

    function handleAnswerClick(answerId: string) {
        const wasChosen: boolean = chosenAnswersIds.includes(answerId);
        if (isSingleChoice && wasChosen) {
            return;
        } else if (isSingleChoice && !wasChosen) {
            chosenAnswersIds = [answerId];
        } else if (wasChosen) {
            chosenAnswersIds = chosenAnswersIds.filter((a) => a !== answerId);
        } else {
            chosenAnswersIds = [...chosenAnswersIds, answerId];
        }
    }
</script>

{#if answersType === GeneralTestAnswerType.TextOnly}{:else if answersType === GeneralTestAnswerType.ImageOnly}{:else if answersType === GeneralTestAnswerType.TextAndImage}{/if}
