<script lang="ts">
    import type { Err } from "../../../../ts/Err";
    import type { GeneralTestTakingQuestionData } from "../../../../ts/page_classes/test_taking_page/general_test/GeneralTestTakingQuestionData";
    import GeneralTestAnswersDisplay from "./current_question_view_components/GeneralTestAnswersDisplay.svelte";
    import GeneralTestQuestionInfoDisplay from "./current_question_view_components/GeneralTestQuestionInfoDisplay.svelte";

    export let currentQuestionData: GeneralTestTakingQuestionData;
    export let currentQuestionIndex: number;
    export let totalQuestionsCount: number;
    export let questionChosenAnswers: string[];
    export function setErrMessage(message: string) {
        answersChoosingErr = message;
    }
    export function getChosenAnswers(): string[] | Err {
        const minAnswersCount = currentQuestionData.minAnswersCount;
        const maxAnswersCount = currentQuestionData.maxAnswersCount;
        return answersChoosingComponent.getChosenAnswers(
            minAnswersCount,
            maxAnswersCount,
        );
    }
    function getSortedAnswers() {
        const allOrdersAreZero = currentQuestionData.answers.every(
            (answer) => answer.orderInQuestion === 0,
        );

        if (allOrdersAreZero) {
            return currentQuestionData.answers.sort(() => Math.random() - 0.5);
        }

        return currentQuestionData.answers.sort(
            (a, b) => a.orderInQuestion - b.orderInQuestion,
        );
    }
    let answersChoosingErr: string = "";
    let answersChoosingComponent: GeneralTestAnswersDisplay;
</script>

<GeneralTestQuestionInfoDisplay
    questionNumber={currentQuestionIndex + 1}
    totalQuestions={totalQuestionsCount}
    questionText={currentQuestionData.text}
    questionImage={currentQuestionData.image}
    minAnswersCount={currentQuestionData.minAnswersCount}
    maxAnswersCount={currentQuestionData.maxAnswersCount}
/>

<GeneralTestAnswersDisplay
    bind:this={answersChoosingComponent}
    answers={getSortedAnswers()}
    isSingleChoice={currentQuestionData.isSingleChoice}
    answersType={currentQuestionData.answersType}
    chosenAnswersIds={questionChosenAnswers}
/>
<p class="answer-choosing-error">{answersChoosingErr}</p>

<style>
    .answer-choosing-error {
        color: var(--red-del);
        font-size: 18px;
        font-weight: 500;
        text-align: center;
    }
</style>
