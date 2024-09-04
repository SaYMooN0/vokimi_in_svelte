<script lang="ts">
    import type { DraftGeneralTestQuestionBriefInfo } from "../../../../../ts/test_creation_tabs_classes/general_test_creation/GeneralTestCreationQuestionsTabData";

    export let question: DraftGeneralTestQuestionBriefInfo;
    export let openQuestionEditingDialog: (questionId: string) => void;
    export let openQuestionDeletingDialog: (questionId: string) => void;

    async function MoveQuestionUpInOrder() {}
    async function MoveQuestionDownInOrder() {}
</script>

<div class="question-container">
    <div class="question-order-buttons">
        <svg
            viewBox="0 0 24 24"
            fill="none"
            on:click={() => MoveQuestionUpInOrder()}
        >
            <path
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
            Answers count: {question.answersCount}
            {question.isMultiple ? "Multiple-choice" : "Single-choice"}
        </label>
    </div>
    <div class="question-actions">
        <div
            class="edit-question-btn"
            on:click={() => openQuestionEditingDialog(question.id)}
        >
            Edit
            <svg viewBox="0 0 24 24" fill="none">
                <path
                    d="M16.2141 4.98239L17.6158 3.58063C18.39 2.80646 19.6452 2.80646 20.4194 3.58063C21.1935 4.3548 21.1935 5.60998 20.4194 6.38415L19.0176 7.78591M16.2141 4.98239L10.9802 10.2163C9.93493 11.2616 9.41226 11.7842 9.05637 12.4211C8.70047 13.058 8.3424 14.5619 8 16C9.43809 15.6576 10.942 15.2995 11.5789 14.9436C12.2158 14.5877 12.7384 14.0651 13.7837 13.0198L19.0176 7.78591M16.2141 4.98239L19.0176 7.78591"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
                <path
                    d="M21 12C21 16.2426 21 18.364 19.682 19.682C18.364 21 16.2426 21 12 21C7.75736 21 5.63604 21 4.31802 19.682C3 18.364 3 16.2426 3 12C3 7.75736 3 5.63604 4.31802 4.31802C5.63604 3 7.75736 3 12 3"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                />
            </svg>
        </div>
        <svg
            class="delete-question-btn"
            on:click={() => openQuestionDeletingDialog(question.id)}
            viewBox="0 0 24 24"
            fill="none"
        >
            <path
                d="M16 12L8 12"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M2.5 12C2.5 7.52166 2.5 5.28249 3.89124 3.89124C5.28249 2.5 7.52166 2.5 12 2.5C16.4783 2.5 18.7175 2.5 20.1088 3.89124C21.5 5.28249 21.5 7.52166 21.5 12C21.5 16.4783 21.5 18.7175 20.1088 20.1088C18.7175 21.5 16.4783 21.5 12 21.5C7.52166 21.5 5.28249 21.5 3.89124 20.1088C2.5 18.7175 2.5 16.4783 2.5 12Z"
                stroke="currentColor"
                stroke-width="1.5"
            />
        </svg>
    </div>
</div>

<style>
    .question-container {
        margin-top: 10px;
        min-height: 120px;
        padding: 10px;
        box-sizing: border-box;
        border-radius: 10px;
        display: grid;
        grid-template-columns: 40px 1fr auto;
    }
    .question-order-buttons {
        height: 100%;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        gap: 10px;
    }

    .question-order-buttons svg {
        cursor: pointer;
    }

    .question-order-buttons path {
        fill: var(--text-faded);
    }

    .question-order-buttons svg:hover path {
        fill: var(--primary-hov);
    }
</style>
