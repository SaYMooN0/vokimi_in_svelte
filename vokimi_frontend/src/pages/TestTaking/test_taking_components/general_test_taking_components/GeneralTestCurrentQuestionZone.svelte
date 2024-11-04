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
    answers={currentQuestionData.answers}
    isSingleChoice={currentQuestionData.isSingleChoice}
    answersType={currentQuestionData.answersType}
    chosenAnswersIds={questionChosenAnswers}
/>
<p class="answer-chosoing-error">{answersChoosingErr}</p>
