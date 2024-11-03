<script lang="ts">
    import { Err } from "../../../../ts/Err";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import type { GeneralTestTakingData } from "../../../../ts/page_classes/test_taking_page/general_test/GeneralTestTakingData";
    import { StringUtils } from "../../../../ts/utils/StringUtils";
    import GeneralTestControlButtonsZone from "../general_test_taking_components/GeneralTestControlButtonsZone.svelte";
    import GeneralTestCurrentQuestionZone from "../general_test_taking_components/GeneralTestCurrentQuestionZone.svelte";
    import TestConclusionDisplay from "../templates_shared/TestConclusionDisplay.svelte";

    interface TestTakenSuccessfullyData {
        receivedResultId: string;
    }

    export let testId: string;
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
    async function completeTest() {
        testCompletionErr = "";
        const testFeedback: string | null =
            conclusionDisplayComponent.getFeedback();
        console.log(testFeedback, testFeedback?.length);
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
        const requestErr = sendTestCompleteRequest(testFeedback);
        if (requestErr instanceof Err) {
            testCompletionErr = requestErr.toString();
            return;
        } else {
            alreadyTaken = true;
        }
    }
    async function sendTestCompleteRequest(
        feedback: string | null,
    ): Promise<Err | TestTakenSuccessfullyData> {
        let chosenAnswersToSend: Record<string, string[]> = {};

        for (let i = 0; i < chosenAnswers.length; i++) {
            const qId = testTakingData.questions[i].id;
            const answersForQ = chosenAnswers[i];
            chosenAnswersToSend[qId] = answersForQ;
        }

        const data = {
            testId,
            chosenAnswers: chosenAnswersToSend,
            feedback,
        };
        const response = await fetch(
            "/api/testTaking/generalTestTakenRequest",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(data),
            },
        );
        if (response.status === 200) {
            const data = await response.json();
            return {
                receivedResultId: data.receivedResultId,
            };
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        } else {
            return new Err("An unknown error occurred.");
        }
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
    <div style=" --test-accent: {testTakingData.accentColor};">
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
                <div class="complete-btn-wrapper">
                    {#if !StringUtils.isNullOrWhiteSpace(testCompletionErr)}
                        <p class="test-completion-err">{testCompletionErr}</p>
                    {/if}
                    <button
                        class="complete-btn"
                        style={backgroundColorAccent}
                        on:click={completeTest}
                    >
                        Complete
                    </button>
                </div>
            {/if}
            <GeneralTestControlButtonsZone
                prevBtnHidden={currentQuestion === 0 ||
                    currentQuestion >= testTakingData.questions.length}
                nextBtnHidden={currentQuestion >=
                    testTakingData.questions.length}
                {prevBtnClicked}
                {nextBtnClicked}
                {backgroundColorAccent}
                btnArrowIcons={testTakingData.arrowIcons}
            />
        {/key}
    </div>
{/if}

<style>
    .complete-btn-wrapper {
        display: flex;
        flex-direction: column;
        align-items: center;
    }
    .complete-btn {
        background-color: var(--test-accent);
        align-self: center;
        margin-top: 16px;
        outline: none;
        border: none;
        border-radius: 8px;
        padding: 8px 24px;
        color: var(--back-main);
        font-size: 24px;
        font-weight: 500;
        cursor: pointer;
        transition: all 0.12s ease-in;
    }
    .complete-btn:hover {
        padding: 8px 28px;
    }
    .complete-btn:active {
        transform: scale(0.98);
    }
</style>
