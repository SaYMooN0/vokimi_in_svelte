<script lang="ts">
    import { TestStylesArrowTypeUtils } from "../../../../ts/enums/TestStylesArrowType";
    import { Err } from "../../../../ts/Err";
    import type { GeneralTestTakingData } from "../../../../ts/page_classes/test_taking_page/general_test/GeneralTestTakingData";
    import AccentColorPicker from "../../../TestCreation/templates_shared_components/styles_tab/editing_dialog_components/AccentColorPicker.svelte";
    import GeneralTestControlButtonsZone from "../general_test_taking_components/GeneralTestControlButtonsZone.svelte";
    import GeneralTestCurrentQuestionZone from "../general_test_taking_components/GeneralTestCurrentQuestionZone.svelte";
    import TestConclusionDisplay from "../templates_shared/TestConclusionDisplay.svelte";

    export let testTakingData: GeneralTestTakingData;
    let chosenAnswers: string[][] = Array.from(
        { length: testTakingData.questions.length },
        () => [],
    );
    let alreadyTaken = false;

    let currentQuestion = 0; //if this is greater than total questions - 1, than its conclusion or test is over
    function prevBtnClicked() {
        if (currentQuestion == 0) {
            return;
        }
        currentQuestion--;
    }
    function nextBtnClicked() {
        if (currentQuestion >= testTakingData.questions.length) {
            return;
        }
        currentQuestionView.setErrMessage("");
        const answers = currentQuestionView.getChosenAnswers();
        if (answers instanceof Err) {
            currentQuestionView.setErrMessage(answers.toString());
            return;
        } else {
            chosenAnswers[currentQuestion] = [...answers];
            currentQuestion++;
        }
    }
    function completeTest() {
        testCompletionErr = "";
        const testFeedback: string | null =
            conclusionDisplayComponent.getFeedback();
        if (
            testTakingData.conclusion.anyFeedback &&
            testFeedback !== null &&
            testFeedback.length > testTakingData.conclusion.maxFeedbackLength
        ) {
            testCompletionErr = `Feedback can't be longer than ${testTakingData.conclusion.maxFeedbackLength}characters`;
            return;
        }
        const answerValidatingErr = validateChosenAnswers();
        if (answerValidatingErr.notNone()) {
            testCompletionErr = answerValidatingErr.toString();
            return;
        }
        //request
        //if no err:
        alreadyTaken = true;
        console.log(alreadyTaken);
    }
    function validateChosenAnswers(): Err {
        for (let i = 0; i < testTakingData.questions.length; i++) {
            const question = testTakingData.questions[i];
            const minAnswersCount = question.minAnswersCount;
            const maxAnswersCount = question.maxAnswersCount;

            if (chosenAnswers[i].length < minAnswersCount) {
                return new Err(
                    `Question ${i + 1} must have at least ${minAnswersCount} answers`,
                );
            }
            if (chosenAnswers[i].length > maxAnswersCount) {
                return new Err(
                    `Question ${i + 1} must have at most ${maxAnswersCount} answers`,
                );
            }
        }
        return Err.none();
    }
    let testCompletionErr = "";
    let currentQuestionView: GeneralTestCurrentQuestionZone;

    let conclusionDisplayComponent: TestConclusionDisplay;
    let backgroundColorAccent =
        "background-color:" + testTakingData.accentColor;
</script>

{#if alreadyTaken}
    <div>Show result</div>
{:else}
    <div class="test-taking-frame">
        {#key currentQuestion}
            {#if currentQuestion < testTakingData.questions.length}
                <GeneralTestCurrentQuestionZone
                    bind:this={currentQuestionView}
                    currentQuestionData={testTakingData.questions[
                        currentQuestion
                    ]}
                    currentQuestionIndex={currentQuestion}
                    totalQuestionsCount={testTakingData.questions.length}
                    questionChosenAnswers={chosenAnswers[currentQuestion]}
                />
            {:else}
                <TestConclusionDisplay
                    bind:this={conclusionDisplayComponent}
                    conclusionData={testTakingData.conclusion}
                />
                <button
                    class="complete-btn"
                    style={backgroundColorAccent}
                    on:click={completeTest}
                >
                    Complete
                </button>
            {/if}
            <GeneralTestControlButtonsZone
                prevBtnHidden={currentQuestion === 0}
                nextBtnHidden={currentQuestion ===
                    testTakingData.questions.length}
                {prevBtnClicked}
                {nextBtnClicked}
                {backgroundColorAccent}
                btnArrowIcons={testTakingData.arrowIcons}
            />
        {/key}
    </div>
{/if}
