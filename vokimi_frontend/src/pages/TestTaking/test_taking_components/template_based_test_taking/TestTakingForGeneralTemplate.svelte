<script lang="ts">
    import { TestStylesArrowTypeUtils } from "../../../../ts/enums/TestStylesArrowType";
    import type { GeneralTestTakingData } from "../../../../ts/page_classes/test_taking_page/general_test/GeneralTestTakingData";
    import GeneralTestAnswersDisplay from "../general_test_taking_components/GeneralTestAnswersDisplay.svelte";
    import GeneralTestQuestionDisplay from "../general_test_taking_components/GeneralTestQuestionDisplay.svelte";

    export let testTakingData: GeneralTestTakingData;
    console.log(testTakingData);
    let currentQuestion = 0; //if this is greater than total questions - 1, than its conclusion or test is over
    function prevBtnClicked() {
        if (currentQuestion == 0) {
            return;
        }
        currentQuestion--;
    }
    function nextBtnClicked() {
        if (currentQuestion == testTakingData.questions.length) {
            //validate chosen answers
            return;
        }
        currentQuestion++;
    }
    let backgroundColorAccent =
        "background-color:" + testTakingData.accentColor;
</script>

<div class="test-taking-frame">
    {#if currentQuestion < testTakingData.questions.length}
        <GeneralTestQuestionDisplay
            questionNumber={currentQuestion + 1}
            totalQuestions={testTakingData.questions.length}
            questionText={testTakingData.questions[currentQuestion].text}
            questionImage={testTakingData.questions[currentQuestion].image}
            minAnswersCount={testTakingData.questions[currentQuestion]
                .minAnswersCount}
            maxAnswersCount={testTakingData.questions[currentQuestion]
                .maxAnswersCount}
        />

        <GeneralTestAnswersDisplay
            answers={testTakingData.questions[currentQuestion].answers}
            isSingleChoice={testTakingData.questions[currentQuestion]
                .isSingleChoice}
            answersType={testTakingData.questions[currentQuestion].answersType}
        />
    {:else}
        Conclusion:
        <button class="complete-btn" style={backgroundColorAccent}>
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
            class:hide-btn={currentQuestion === testTakingData.questions.length}
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
</div>

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
