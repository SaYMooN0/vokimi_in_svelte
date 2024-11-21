<script lang="ts">
    import { getErrorFromResponse } from "../../../../../../ts/ErrorResponse";
    import { TestDiscussionCommentVm } from "../../../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/discussions_tab_classes/TestDiscussionCommentVm";
    import { StringUtils } from "../../../../../../ts/utils/StringUtils";

    export let parentCommentId: string;
    export let showSavedAnswer: (commentVm: TestDiscussionCommentVm) => void;
    let isHidden = true;
    let answerText: string = "";
    let answerSavingErr: string = "";
    export function show() {
        answerText = "";
        isHidden = false;
    }

    async function saveAnswer() {
        console.log("1");
        answerSavingErr = "";
        if (StringUtils.isNullOrWhiteSpace(answerText.trim())) {
            return;
        }
        const data = { answerText, parentCommentId }; //+ attachment if any
        console.log(data);
        const response = await fetch(
            "/api/viewTest/discussions/saveAnswerToComment",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(data),
            },
        );
        if (response.ok) {
            const responseData = await response.json();
            showSavedAnswer(
                new TestDiscussionCommentVm(
                    responseData.commentId,
                    responseData.authorId,
                    responseData.authorUsername,
                    responseData.authorProfilePicture,
                    responseData.text,
                    responseData.votesRating,
                    responseData.totalVotesCount,
                    responseData.createdAtDateTime,
                    responseData.isViewersVoteUp,
                    responseData.childVms,
                ),
            );
            isHidden = true;
            answerText = "";
        } else if (response.status === 400) {
            answerSavingErr = await getErrorFromResponse(response);
        } else {
            answerSavingErr = "An unknown error occurred while saving answer";
        }
    }
</script>

<div class="answer-input-container" class:hidden={isHidden}>
    <div class="answer-input">
        <input placeholder="Input answer..." bind:value={answerText} />
        <svg
            on:click={saveAnswer}
            class="save-comment-icon"
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            fill="none"
        >
            <path
                d="M21.7109 9.3871C21.8404 9.895 21.9249 10.4215 21.9598 10.9621C22.0134 11.7929 22.0134 12.6533 21.9598 13.4842C21.6856 17.7299 18.3536 21.1118 14.1706 21.3901C12.7435 21.485 11.2536 21.4848 9.8294 21.3901C9.33896 21.3574 8.8044 21.2403 8.34401 21.0505C7.83177 20.8394 7.5756 20.7338 7.44544 20.7498C7.31527 20.7659 7.1264 20.9052 6.74868 21.184C6.08268 21.6755 5.24367 22.0285 3.99943 21.9982C3.37026 21.9829 3.05568 21.9752 2.91484 21.7349C2.77401 21.4946 2.94941 21.1619 3.30021 20.4966C3.78674 19.5739 4.09501 18.5176 3.62791 17.6712C2.82343 16.4623 2.1401 15.0305 2.04024 13.4842C1.98659 12.6533 1.98659 11.7929 2.04024 10.9621C2.31441 6.71638 5.64639 3.33448 9.8294 3.05621C10.2156 3.03051 10.6067 3.01177 11 3"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M11.9953 12.5H12.0043M15.9908 12.5H15.9998M7.99982 12.5H8.00879"
                stroke="currentColor"
                stroke-width="2"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M22 4.49997L14 4.49997M22 4.49997C22 3.79974 20.0057 2.4915 19.5 1.99997M22 4.49997C22 5.2002 20.0057 6.50844 19.5 6.99997"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
    </div>
    <svg
        class="close-btn-svg"
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 24 24"
        fill="none"
        on:click={() => {
            isHidden = true;
        }}
    >
        <path
            d="M19.0005 4.99988L5.00049 18.9999M5.00049 4.99988L19.0005 18.9999"
            stroke="currentColor"
            stroke-width="3"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
    </svg>
    {#if !StringUtils.isNullOrWhiteSpace(answerSavingErr)}
        <p class="discussion-saving-err">{answerSavingErr}</p>
    {/if}
</div>

<style>
    .answer-input-container {
        position: relative;
        width: 100%;
        display: grid;
        align-items: center;
        gap: 12px;
        padding: 4px 12px 0 24px;
        box-sizing: border-box;
        margin-bottom: 20px;
        margin-top: 4px;
    }
    .hidden {
        display: none;
    }
    .answer-input {
        display: grid;
        grid-template-columns: 1fr 32px;
        height: 32px;
        background-color: var(--back-secondary);
        border-radius: 6px 6px 0 0px;
        border-bottom: 2px solid var(--text-faded);
    }
    .answer-input:focus-within {
        border-color: var(--primary);
    }
    .answer-input input {
        font-size: 16px;
        padding: 0 12px;
        background-color: transparent;
        border: none;
        outline: none;
    }
    .save-comment-icon {
        margin: auto 0;
        height: 24px;
        aspect-ratio: 1/1;
        color: var(--text-faded);
        cursor: pointer;
    }
    .save-comment-icon:hover {
        color: var(--primary);
    }
    .save-comment-icon:active {
        color: var(--primary-hov);
        transform: scale(0.9);
    }
    .close-btn-svg {
        position: absolute;
        left: 0;
        top: 0;
        cursor: pointer;
        height: 20px;
        aspect-ratio: 1/1;
        border-radius: 20px;
        padding: 4px;
        box-sizing: border-box;
        background-color: var(--text-faded);
        color: var(--back-primary);
        color: var(--back-main);
    }
    .close-btn-svg:hover {
        background-color: var(--red-del);
    }
</style>
