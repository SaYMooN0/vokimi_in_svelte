<script lang="ts">
    import { GeneralTestAnswerType } from "../../../../../ts/enums/GeneralTestAnswerType";
    import { Err } from "../../../../../ts/Err";
    import type {
        BaseGeneralTestTakingAnswerData,
        GeneralTestTakingImageOnlyAnswerData,
        GeneralTestTakingTextAndImageAnswerData,
    } from "../../../../../ts/page_classes/test_taking_page/general_test/GeneralTestTakingAnswersData";
    import type { GeneralTestTakingTextOnlyAnswerData } from "../../../../../ts/page_classes/test_taking_page/general_test/GeneralTestTakingAnswersData";
    import TextOnlyAnswersDisplay from "./answer_display_for_specific_type/TextOnlyAnswersDisplay.svelte";
    import ImageOnlyAnswersDisplay from "./answer_display_for_specific_type/ImageOnlyAnswersDisplay.svelte";
    import TextAndImageAnswersDisplay from "./answer_display_for_specific_type/TextAndImageAnswersDisplay.svelte";

    export let answersType: GeneralTestAnswerType;
    export let answers: BaseGeneralTestTakingAnswerData[];
    export let isSingleChoice: boolean;
    export let chosenAnswersIds: string[];

    function handleAnswerClick(answerId: string) {
        const wasChosen: boolean = chosenAnswersIds.includes(answerId);
        if (isSingleChoice && wasChosen) {
            return;
        } else if (isSingleChoice && !wasChosen) {
            chosenAnswersIds = [answerId];
        } else if (wasChosen) {
            chosenAnswersIds = chosenAnswersIds
                .filter((a) => a !== answerId)
                .slice();
        } else {
            chosenAnswersIds = [...chosenAnswersIds, answerId];
        }
    }
    export function getChosenAnswers(
        minAnswersCount: number,
        maxAnswersCount: number,
    ): string[] | Err {
        if (chosenAnswersIds.length > maxAnswersCount) {
            return new Err(
                `You cannot choose more than ${maxAnswersCount} answers`,
            );
        } else if (chosenAnswersIds.length < minAnswersCount) {
            return new Err(
                `You have to choose at least ${minAnswersCount} answers`,
            );
        }
        return chosenAnswersIds;
    }
    function extractTextOnlyAnswers() {
        return answers as GeneralTestTakingTextOnlyAnswerData[];
    }
    function extractImageOnlyAnswers() {
        return answers as GeneralTestTakingImageOnlyAnswerData[];
    }
    function extractTextAndImageAnswers() {
        return answers as GeneralTestTakingTextAndImageAnswerData[];
    }
</script>

{#if answersType === GeneralTestAnswerType.TextOnly}
    <TextOnlyAnswersDisplay
        onAnswerClick={handleAnswerClick}
        answers={extractTextOnlyAnswers()}
        {isSingleChoice}
        {chosenAnswersIds}
    />
{:else if answersType === GeneralTestAnswerType.ImageOnly}
    <ImageOnlyAnswersDisplay
        onAnswerClick={handleAnswerClick}
        answers={extractImageOnlyAnswers()}
        {isSingleChoice}
        {chosenAnswersIds}
    />
{:else if answersType === GeneralTestAnswerType.TextAndImage}
    <TextAndImageAnswersDisplay
        onAnswerClick={handleAnswerClick}
        answers={extractTextAndImageAnswers()}
        {isSingleChoice}
        {chosenAnswersIds}
    />
{:else}
    <p>Unable to display question answers</p>
{/if}

<style>
    :global(.answer-btn) {
        cursor: pointer;
        transition: all 0.12s ease-in;
        box-sizing: border-box;

    }
    :global(.answer-btn):hover {
        transform: scale(1.01);
    }
    :global(.answer-btn):hover :global(.is-chosen-indicator) {
        border-color: var(--test-accent);
    }
</style>
