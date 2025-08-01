<script lang="ts">
    import { getErrorFromResponse } from "../../../../../ts/ErrorResponse";
    import { TestDiscussionCommentVm } from "../../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/discussions_tab_classes/TestDiscussionCommentVm";
    import { StringUtils } from "../../../../../ts/utils/StringUtils";

    export let testId: string;
    export let showNewAddedDiscussion: (
        commentVm: TestDiscussionCommentVm,
    ) => void;
    let discussionText: string = "";
    let discussionSavingErr: string = "";
    async function saveNewDiscussion() {
        discussionSavingErr = "";
        if (StringUtils.isNullOrWhiteSpace(discussionText.trim())) {
            return;
        }
        const data = { commentText: discussionText, testId }; //+ attachment if any
        const response = await fetch(
            "/api/viewTest/discussions/startNewDiscussion",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(data),
            },
        );
        if (response.ok) {
            discussionText = "";
            const responseData = await response.json();
            showNewAddedDiscussion(
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
        } else if (response.status === 400) {
            discussionSavingErr = await getErrorFromResponse(response);
        } else {
            discussionSavingErr =
                "An unknown error occurred while starting new discussion";
        }
    }
</script>

<div class="new-discussion-container">
    <input
        placeholder="Start a new discussion..."
        type="text"
        class="new-discussion-input"
        bind:value={discussionText}
    />
    <button class="save-new-discussion-btn" on:click={saveNewDiscussion}>
        <svg
            class="save-new-discussion-icon"
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
    </button>
</div>
{#if !StringUtils.isNullOrWhiteSpace(discussionSavingErr)}
    <p class="new-discussion-saving-err">{discussionSavingErr}</p>
{/if}

<style>
    .new-discussion-container {
        width: 100%;
        height: 48px;
        display: grid;
        align-items: center;
        grid-template-columns: 1fr auto;
        background-color: var(--back-secondary);
        padding: 0;
        border-radius: 50px;
        border: 2px solid transparent;
        box-sizing: border-box;
    }
    .new-discussion-container:focus-within {
        border-color: var(--primary);
    }

    .new-discussion-input {
        height: 100%;
        background-color: transparent;
        outline: none;
        border: none;
        padding-left: 16px;
        font-size: 16px;
        color: var(--text);
    }

    .save-new-discussion-btn {
        width: fit-content;
        height: 100%;
        background-color: transparent;
        outline: none;
        border: none;
        display: flex;
        align-items: center;
        justify-content: center;
        cursor: pointer;
        transition: all 0.3s;
    }
    .save-new-discussion-icon {
        margin-right: 12px;
        color: var(--text-faded);
        height: 100%;
        aspect-ratio: 1/1;
        padding: 8px;
        box-sizing: border-box;
    }
    .save-new-discussion-icon:hover {
        color: var(--primary);
    }
    .save-new-discussion-icon:active {
        color: var(--primary-hov);
    }
    .new-discussion-saving-err {
        margin: 4px 0 12px 16px;
        color: var(--red-del);
        font-size: 16px;
        font-weight: 500;
    }
</style>
