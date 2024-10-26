<script lang="ts">
    import { GeneralTestAnswerTypeUtils } from "../../../../../ts/enums/GeneralTestAnswerType";
    import type { DraftGeneralTestQuestionBriefInfo } from "../../../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/general_test_creation/questions/GeneralTestCreationQuestionsTabData";
    import ElementEditDeleteActions from "../../../creation_shared_components/ElementEditDeleteActions.svelte";

    export let refreshParentComponentAction: () => void;
    export let question: DraftGeneralTestQuestionBriefInfo;
    export let openQuestionEditingDialog: (questionId: string) => void;
    export let openQuestionDeletingDialog: (
        questionId: string,
        questionText: string,
    ) => void;

    async function MoveQuestionUpInOrder() {
        if (question.orderInTest === 0) {
            return;
        }
        const url =
            "/api/testCreation/general/moveQuestionUpInOrder/" + question.id;
        const response = await fetch(url, {
            method: "POST",
        });
        if (response.ok) {
            await refreshParentComponentAction();
        }
    }
    async function MoveQuestionDownInOrder() {
        const url =
            "/api/testCreation/general/moveQuestionDownInOrder/" + question.id;
        const response = await fetch(url, {
            method: "POST",
        });
        if (response.ok) {
            await refreshParentComponentAction();
        }
    }
</script>

<div class="question-container">
    <div class="question-order-buttons">
        <svg
            viewBox="0 0 24 24"
            fill="none"
            on:click={() => MoveQuestionUpInOrder()}
        >
            <path
                fill="currentColor"
                fill-rule="evenodd"
                clip-rule="evenodd"
                d="M11.4697 8.46967C11.7626 8.17678 12.2374 8.17678 12.5303 8.46967L18.5303 14.4697C18.7448 14.6842 18.809 15.0068 18.6929 15.287C18.5768 15.5673 18.3033 15.75 18 15.75H6C5.69665 15.75 5.42318 15.5673 5.30709 15.287C5.191 15.0068 5.25517 14.6842 5.46967 14.4697L11.4697 8.46967Z"
            />
        </svg>
        <svg
            viewBox="0 0 24 24"
            fill="none"
            on:click={() => MoveQuestionDownInOrder()}
        >
            <path
                fill="currentColor"
                fill-rule="evenodd"
                clip-rule="evenodd"
                d="M5.99977 8.25005L17.9998 8.25C18.3031 8.25 18.5766 8.43273 18.6927 8.71298C18.8088 8.99324 18.7446 9.31583 18.5301 9.53033L12.5301 15.5303C12.2373 15.8232 11.7624 15.8232 11.4695 15.5303L5.46944 9.53038C5.25494 9.31588 5.19077 8.9933 5.30686 8.71304C5.42294 8.43278 5.69642 8.25005 5.99977 8.25005Z"
            />
        </svg>
    </div>
    <div class="question-info">
        <p class="question-text">
            {question.orderInTest + 1}. {question.text}
        </p>
        <label class="answers-count">
            {GeneralTestAnswerTypeUtils.getFullName(question.answersType)},
            Answers count: {question.answersCount},
            {question.isMultiple ? "Multiple-choice" : "Single-choice"}
        </label>
    </div>
    <ElementEditDeleteActions
        editButtonAction={() => openQuestionEditingDialog(question.id)}
        deleteButtonAction={() =>
            openQuestionDeletingDialog(question.id, question.text)}
    />
</div>

<style>
    .question-container {
        margin: 10px 10px;
        min-height: 120px;
        padding: 0 12px 0 4px;
        box-sizing: border-box;
        border-radius: 10px;
        display: grid;
        grid-template-columns: 40px 1fr auto;
        background-color: var(--back-secondary);
    }
    .question-order-buttons {
        width: 100%;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        gap: 8px;
    }

    .question-order-buttons svg {
        cursor: pointer;
        color: var(--text-faded);
    }

    .question-order-buttons path {
    }

    .question-order-buttons svg:hover path {
        fill: var(--primary-hov);
    }
</style>
