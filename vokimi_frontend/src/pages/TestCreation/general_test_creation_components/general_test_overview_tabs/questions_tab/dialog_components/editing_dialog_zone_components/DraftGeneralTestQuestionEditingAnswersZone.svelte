<script lang="ts">
    import { GeneralTestAnswerType } from "../../../../../../../ts/enums/GeneralTestAnswerType";
    import { DraftGeneralTestImageOnlyAnswerFormData } from "../../../../../../../ts/test_creation_tabs_classes/general_test_creation/draft_general_test_questions/answers/DraftGeneralTestImageOnlyAnswerFormData";
    import { DraftGeneralTestTextAndImageAnswerFormData } from "../../../../../../../ts/test_creation_tabs_classes/general_test_creation/draft_general_test_questions/answers/DraftGeneralTestTextAndImageAnswerFormData";
    import { DraftGeneralTestTextOnlyAnswerFormData } from "../../../../../../../ts/test_creation_tabs_classes/general_test_creation/draft_general_test_questions/answers/DraftGeneralTestTextOnlyAnswerFormData";
    import type { IDraftGeneralTestAnswerFormData } from "../../../../../../../ts/test_creation_tabs_classes/general_test_creation/draft_general_test_questions/answers/IDraftGeneralTestAnswerFormData";
    import AnswerResultsEditingComponent from "./answer_zone_components/AnswerResultsEditingComponent.svelte";
    import GeneralTestImageOnlyAnswerEditing from "./answer_zone_components/GeneralTestImageOnlyAnswerEditing.svelte";
    import GeneralTestTextAndImageAnswerEditing from "./answer_zone_components/GeneralTestTextAndImageAnswerEditing.svelte";
    import GeneralTestTextOnlyAnswerEditing from "./answer_zone_components/GeneralTestTextOnlyAnswerEditing.svelte";
    import ResultAssigningDialog from "./answer_zone_components/ResultAssigningDialog.svelte";

    export let answers: IDraftGeneralTestAnswerFormData[];
    export let answersType: GeneralTestAnswerType;
    export let questionId: string;

    function addAnswer() {
        const newAnswer: IDraftGeneralTestAnswerFormData = (() => {
            switch (answersType) {
                case GeneralTestAnswerType.TextOnly:
                    return new DraftGeneralTestTextOnlyAnswerFormData("", {});
                case GeneralTestAnswerType.TextAndImage:
                    return new DraftGeneralTestTextAndImageAnswerFormData(
                        "",
                        "",
                        {},
                    );
                case GeneralTestAnswerType.ImageOnly:
                    return new DraftGeneralTestImageOnlyAnswerFormData("", {});
                default:
                    throw new Error("Unknown answer type");
            }
        })();
        answers = [...answers, newAnswer];
        showAnswers = true;
    }

    function removeAnswer(answer: IDraftGeneralTestAnswerFormData) {
        answers = answers.filter((a) => a !== answer);
    }
    let showAnswers: boolean = false;
    let resultAssigningDialog: ResultAssigningDialog;
</script>

<ResultAssigningDialog bind:this={resultAssigningDialog} />
<div class="answers-zone">
    <p class="answers-title">
        Answers ({answers.length})
        <label
            class="answers-visibility-toggle"
            on:click={() => (showAnswers = !showAnswers)}
        >
            {showAnswers ? "Hide" : "Show"}
        </label>
    </p>
    <div class="answers-container" class:hide-answers={!showAnswers}>
        {#each answers as answer, index}
            <div class="answer-element">
                <label class="answer-number">#{index + 1}</label>
                <AnswerResultsEditingComponent
                    openResultAssigningDialog={() =>
                        resultAssigningDialog.open()}
                    bind:relatedResults={answer.relatedResults}
                />
                {#if answer instanceof DraftGeneralTestTextOnlyAnswerFormData}
                    <GeneralTestTextOnlyAnswerEditing
                        bind:answerData={answer}
                    />
                {:else if answer instanceof DraftGeneralTestTextAndImageAnswerFormData}
                    <GeneralTestTextAndImageAnswerEditing
                        {questionId}
                        bind:answerData={answer}
                    />
                {:else if answer instanceof DraftGeneralTestImageOnlyAnswerFormData}
                    <GeneralTestImageOnlyAnswerEditing
                        bind:answerData={answer}
                    />
                {:else}
                    <p>Unknown answer form type</p>
                {/if}
                <svg
                    class="delete-answer-btn"
                    on:click={() => removeAnswer(answer)}
                    fill="none"
                    viewBox="0 0 50 59"
                    xmlns="http://www.w3.org/2000/svg"
                    xmlns:xlink="http://www.w3.org/1999/xlink"
                >
                    <path
                        d="M0 7.5C0 5.01472 2.01472 3 4.5 3H45.5C47.9853 3 50 5.01472 50 7.5V7.5C50 8.32843 49.3284 9 48.5 9H1.5C0.671571 9 0 8.32843 0 7.5V7.5Z"
                    ></path>
                    <path
                        d="M17 3C17 1.34315 18.3431 0 20 0H29.3125C30.9694 0 32.3125 1.34315 32.3125 3V3H17V3Z"
                    ></path>
                    <path
                        d="M2.18565 18.0974C2.08466 15.821 3.903 13.9202 6.18172 13.9202H43.8189C46.0976 13.9202 47.916 15.821 47.815 18.0975L46.1699 55.1775C46.0751 57.3155 44.314 59.0002 42.1739 59.0002H7.8268C5.68661 59.0002 3.92559 57.3155 3.83073 55.1775L2.18565 18.0974ZM18.0003 49.5402C16.6196 49.5402 15.5003 48.4209 15.5003 47.0402V24.9602C15.5003 23.5795 16.6196 22.4602 18.0003 22.4602C19.381 22.4602 20.5003 23.5795 20.5003 24.9602V47.0402C20.5003 48.4209 19.381 49.5402 18.0003 49.5402ZM29.5003 47.0402C29.5003 48.4209 30.6196 49.5402 32.0003 49.5402C33.381 49.5402 34.5003 48.4209 34.5003 47.0402V24.9602C34.5003 23.5795 33.381 22.4602 32.0003 22.4602C30.6196 22.4602 29.5003 23.5795 29.5003 24.9602V47.0402Z"
                        clip-rule="evenodd"
                        fill-rule="evenodd"
                    ></path>
                    <path d="M2 13H48L47.6742 21.28H2.32031L2 13Z"></path>
                </svg>
            </div>
        {/each}
    </div>
    <button class="add-answer-button" on:click={addAnswer}>Add Answer</button>
</div>

<style>
    .answers-title {
        position: relative;
        display: flex;
        justify-content: center;
        font-size: 24px;
        color: var(--text);
        font-weight: 500;
    }
    .answers-visibility-toggle {
        position: absolute;
        top: 50%;
        transform: translateY(-50%);
        right: 40px;
        cursor: pointer;
        color: var(--text-faded);
        font-size: 18px;
        font-weight: 500;
        text-decoration: underline;
    }

    .answers-visibility-toggle:hover {
        color: var(--primary);
    }
    .answers-container {
        width: 100%;
        box-sizing: border-box;
        display: flex;
        flex-direction: column;
        align-items: center;
        gap: 8px;
    }
    .hide-answers {
        display: none;
    }
    .answer-element {
        border-radius: 10px;
        min-height: 120px;
        width: 98%;
        display: grid;
        grid-template-columns: auto auto 1fr auto;
    }
    .answer-number {
        margin: 10px 4px 0 2px;
        color: var(--text-faded);
        font-size: 18px;
    }

    .delete-answer-btn {
        align-self: center;
        margin-right: 10px;
        width: 36px;
        box-sizing: border-box;
        padding: 8px;
        aspect-ratio: 1/1;
        display: flex;
        align-items: center;
        justify-content: center;
        background-color: transparent;
        border: none;
        border-radius: 8px;
        cursor: pointer;
        transition: all 0.2s;
        position: relative;
        background-color: var(--text-faded);
        fill: var(--back-main);
    }

    .delete-answer-btn:hover {
        background-color: var(--red-del);
    }

    .delete-answer-btn:active {
        transform: scale(0.98);
    }
</style>
