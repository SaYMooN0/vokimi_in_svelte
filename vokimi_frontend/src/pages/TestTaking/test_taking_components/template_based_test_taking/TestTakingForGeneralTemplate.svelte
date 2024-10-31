<script lang="ts">
    import { TestStylesArrowTypeUtils } from "../../../../ts/enums/TestStylesArrowType";
    import { Err } from "../../../../ts/Err";
    import type { GeneralTestTakingData } from "../../../../ts/page_classes/test_taking_page/general_test/GeneralTestTakingData";
    import GeneralTestAnswersDisplay from "../general_test_taking_components/GeneralTestAnswersDisplay.svelte";
    import GeneralTestQuestionDisplay from "../general_test_taking_components/GeneralTestQuestionDisplay.svelte";
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
        answersChoosingErr = "";
        const minAnswersCount =
            testTakingData.questions[currentQuestion].minAnswersCount;
        const maxAnswersCount =
            testTakingData.questions[currentQuestion].maxAnswersCount;
        const answers = answersChoosingComponents.getChosenAnswers(
            minAnswersCount,
            maxAnswersCount,
        );
        if (answers instanceof Err) {
            answersChoosingErr = answers.toString();
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
    let answersChoosingErr = "";
    let testCompletionErr = "";
    let answersChoosingComponents: GeneralTestAnswersDisplay;
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
                <GeneralTestQuestionDisplay
                    questionNumber={currentQuestion + 1}
                    totalQuestions={testTakingData.questions.length}
                    questionText={testTakingData.questions[currentQuestion]
                        .text}
                    questionImage={testTakingData.questions[currentQuestion]
                        .image}
                    minAnswersCount={testTakingData.questions[currentQuestion]
                        .minAnswersCount}
                    maxAnswersCount={testTakingData.questions[currentQuestion]
                        .maxAnswersCount}
                />

                <GeneralTestAnswersDisplay
                    bind:this={answersChoosingComponents}
                    answers={testTakingData.questions[currentQuestion].answers}
                    isSingleChoice={testTakingData.questions[currentQuestion]
                        .isSingleChoice}
                    answersType={testTakingData.questions[currentQuestion]
                        .answersType}
                    chosenAnswersIds={chosenAnswers[currentQuestion]}
                />
                <p class="answer-chosoing-error">{answersChoosingErr}</p>
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
            <div class="control-btns">
                <button
                    class:hide-btn={currentQuestion == 0}
                    class="prev-btn"
                    style={backgroundColorAccent}
                    on:click={prevBtnClicked}
                >
                    <svelte:component
                        this={TestStylesArrowTypeUtils.getIcon(
                            testTakingData.arrowIcons,
                        )}
                    />
                    <span>Previous</span>
                </button>
                <button
                    class:hide-btn={currentQuestion ===
                        testTakingData.questions.length}
                    class="next-btn"
                    style={backgroundColorAccent}
                    on:click={nextBtnClicked}
                >
                    <span>Next</span>
                    <svelte:component
                        this={TestStylesArrowTypeUtils.getIcon(
                            testTakingData.arrowIcons,
                        )}
                    />
                </button>
            </div>
        {/key}
    </div>
{/if}

<style>
    .control-btns {
        display: flex;
        justify-content: space-between;
        align-items: center;
        flex-direction: row;
        padding: 0 min(4vw, 40px);
    }
    .control-btns button {
        width: 160px;
        height: 40px;
        display: grid;
        align-items: center;
        gap: 4px;
        color: var(--back-main);
        border: none;
        border-radius: 8px;
        cursor: pointer;
        transition: transform 0.2s ease-in-out;
    }
    .control-btns button:hover {
        transform: scale(1.04);
    }
    .control-btns button:active {
        transform: scale(1.02);
    }
    .control-btns button span {
        font-size: 18px;
    }
    .hide-btn {
        transform: scale(0);
        pointer-events: none;
        opacity: 0;
    }
    .prev-btn {
        grid-template-columns: auto 1fr;
    }
    .next-btn {
        grid-template-columns: 1fr auto;
    }
    .control-btns button > :global(svg) {
        height: 100%;
        aspect-ratio: 1/1;
    }
    .next-btn > :global(svg) {
        transform: rotate(180deg);
    }
</style>
