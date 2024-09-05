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
            Answers count: {question.answersCount}
            {question.isMultiple ? "Multiple-choice" : "Single-choice"}
        </label>
    </div>
    <div class="question-actions unselectable">
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
                fill-rule="evenodd"
                clip-rule="evenodd"
                d="M4.63751 20.1665L3.82444 6.75092L3.73431 5.06621C3.72513 4.89447 3.8619 4.75018 4.03388 4.75018H19.9945C20.1685 4.75018 20.306 4.89769 20.2938 5.07124L20.1756 6.75092L19.3625 20.1665C19.2745 21.618 18.0717 22.7502 16.6176 22.7502H7.38247C5.9283 22.7502 4.72548 21.618 4.63751 20.1665ZM8.74963 16.5002C8.74963 16.9144 9.08542 17.2502 9.49963 17.2502C9.91385 17.2502 10.2496 16.9144 10.2496 16.5002V10.5002C10.2496 10.086 9.91385 9.75018 9.49963 9.75018C9.08542 9.75018 8.74963 10.086 8.74963 10.5002V16.5002ZM14.4996 9.75018C14.9138 9.75018 15.2496 10.086 15.2496 10.5002V16.5002C15.2496 16.9144 14.9138 17.2502 14.4996 17.2502C14.0854 17.2502 13.7496 16.9144 13.7496 16.5002V10.5002C13.7496 10.086 14.0854 9.75018 14.4996 9.75018Z"
                fill="currentColor"
            />
            <path
                fill-rule="evenodd"
                clip-rule="evenodd"
                d="M8.31879 2.46286C8.63394 1.7275 9.35702 1.2507 10.1571 1.2507H13.8383C14.6383 1.2507 15.3614 1.7275 15.6766 2.46286L16.6569 4.75034H19.2239C19.2903 4.75034 19.3523 4.75034 19.4102 4.7507H19.4637C19.4857 4.74973 19.5079 4.74972 19.5303 4.7507H20.9977C21.55 4.7507 21.9977 5.19842 21.9977 5.7507C21.9977 6.30299 21.55 6.7507 20.9977 6.7507H2.99768C2.4454 6.7507 1.99768 6.30299 1.99768 5.7507C1.99768 5.19842 2.4454 4.7507 2.99768 4.7507H4.46507C4.48746 4.74972 4.50968 4.74973 4.53167 4.7507H4.58469C4.6426 4.75034 4.70457 4.75034 4.77093 4.75034H7.33844L8.31879 2.46286ZM13.8903 3.37192L14.481 4.75034H9.5144L10.1052 3.37192C10.1367 3.29838 10.209 3.2507 10.289 3.2507L13.7064 3.2507C13.7864 3.2507 13.8587 3.29838 13.8903 3.37192Z"
                fill="currentColor"
            />
        </svg>
    </div>
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
    .question-actions {
        align-self: center;
        height: 36px;
        display: flex;
        flex-direction: row;
        gap: 10px;
        --btns-border-radius: 6px;
        --btns-transitions: background-color 0.2s, transform 0.08s ease-in-out;
    }

    .edit-question-btn {
        border-radius: var(--btns-border-radius);
        cursor: pointer;
        box-sizing: border-box;
        height: 100%;
        width: fit-content;
        padding: 4px 20px;
        background-color: var(--primary);
        font-family: Roboto;
        color: white;
        font-size: 22px;
        display: flex;
        flex-direction: row;
        justify-content: center;
        align-items: center;
        gap: 6px;
        transition: var(--btns-transitions);
    }

    .edit-question-btn svg {
        height: 96%;
        aspect-ratio: 1/1;
    }

    .edit-question-btn:hover {
        background-color: var(--primary-hov);
    }

    .edit-question-btn:active {
        transform: scale(0.96);
    }

    .delete-question-btn {
        border-radius: var(--btns-border-radius);
        height: 100%;
        aspect-ratio: 1/1;
        padding: 6px;
        box-sizing: border-box;
        color: var(--back-main);
        background-color: var(--text-faded);
        cursor: pointer;
        transition: var(--btns-transitions);
    }

    .delete-question-btn:hover {
        background-color: var(--red-del);
    }

    .delete-question-btn:active {
        transform: scale(0.96);
    }
</style>
