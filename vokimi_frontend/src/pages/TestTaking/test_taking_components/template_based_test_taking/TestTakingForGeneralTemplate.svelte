<script lang="ts">
    import { TestTemplate } from "../../../../ts/enums/TestTemplate";
    import { Err } from "../../../../ts/Err";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import type { GeneralTestTakingData } from "../../../../ts/page_classes/test_taking_page/general_test/GeneralTestTakingData";
    import { StringUtils } from "../../../../ts/utils/StringUtils";
    import GeneralTestControlButtonsZone from "../general_test_taking_components/GeneralTestControlButtonsZone.svelte";
    import GeneralTestCurrentQuestionZone from "../general_test_taking_components/GeneralTestCurrentQuestionZone.svelte";
    import TestConclusionDisplay from "../templates_shared/TestConclusionDisplay.svelte";
    import { navigate } from "svelte-routing";

    export let testId: string;
    export let testTakingData: GeneralTestTakingData;
    let chosenAnswers: string[][] = Array.from(
        { length: testTakingData.questions.length },
        () => [],
    );

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
        let testFeedbackData: { feedback: string; anonymous: boolean } | null =
            null;

        if (testTakingData.conclusion !== null) {
            testFeedbackData = conclusionDisplayComponent.getFeedback();
            if (
                testTakingData.conclusion.anyFeedback &&
                testFeedbackData !== null &&
                testFeedbackData.feedback.length >
                    testTakingData.conclusion.maxFeedbackLength
            ) {
                testCompletionErr = `Feedback can't be longer than ${testTakingData.conclusion.maxFeedbackLength}characters`;
                return;
            }
        }
        const answerValidatingErr = validateChosenAnswers();
        if (answerValidatingErr.notNone()) {
            testCompletionErr = answerValidatingErr.toString();
            return;
        }

        const feedbackText: string | null =
            testFeedbackData !== null ? testFeedbackData.feedback : null;
        const isFeedbackAnonymous: boolean =
            testFeedbackData?.anonymous ?? true;

        const requestResult = await sendTestCompleteRequest(
            feedbackText,
            isFeedbackAnonymous,
        );
        if (requestResult instanceof Err) {
            testCompletionErr = requestResult.toString();
            return;
        } else {
            navigate(
                `/test-taking/view-result/${testId}/${TestTemplate.General}/${requestResult}`,
            );
        }
    }
    async function sendTestCompleteRequest(
        feedbackText: string | null,
        isFeedbackAnonymous: boolean,
    ): Promise<Err | string> {
        let chosenAnswersToSend: Record<string, string[]> = {};

        for (let i = 0; i < chosenAnswers.length; i++) {
            const qId = testTakingData.questions[i].id;
            const answersForQ = chosenAnswers[i];
            chosenAnswersToSend[qId] = answersForQ;
        }

        const data = {
            testId,
            chosenAnswers: chosenAnswersToSend,
            feedbackText: feedbackText,
            isFeedBackAnonymous: isFeedbackAnonymous,
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
            return data.receivedResultId;
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

<div style="--test-accent: {testTakingData.accentColor};">
    {#key currentQuestion}
        {#if currentQuestion < testTakingData.questions.length}
            <GeneralTestCurrentQuestionZone
                bind:this={currentQuestionView}
                currentQuestionData={testTakingData.questions[currentQuestion]}
                currentQuestionIndex={currentQuestion}
                totalQuestionsCount={testTakingData.questions.length}
                questionChosenAnswers={chosenAnswers[currentQuestion]}
            />
        {:else if currentQuestion === testTakingData.questions.length}
            {#if testTakingData.conclusion !== null}
                <TestConclusionDisplay
                    bind:this={conclusionDisplayComponent}
                    conclusionData={testTakingData.conclusion}
                />
            {:else}
                <div>You have reached the end of the test</div>
            {/if}
        {/if}

        <GeneralTestControlButtonsZone
            prevBtnHidden={currentQuestion === 0}
            nextBtnHidden={currentQuestion >= testTakingData.questions.length}
            {prevBtnClicked}
            {nextBtnClicked}
            {backgroundColorAccent}
            btnArrowIcons={testTakingData.arrowIcons}
        />
        {#if currentQuestion === testTakingData.questions.length}
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
    {/key}
</div>

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
