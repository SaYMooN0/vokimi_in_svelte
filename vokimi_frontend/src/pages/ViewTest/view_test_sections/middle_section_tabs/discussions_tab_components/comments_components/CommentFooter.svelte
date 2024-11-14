<script lang="ts">
    import CommentAnswerInput from "./CommentAnswerInput.svelte";

    export let viewersVoteIsUp: boolean | null;
    export let votesRating: number;
    export let totalVotesCount: number;
    export let commentId: string;

    function voteUpPressed() {}
    function voteDownPressed() {}

    let answerInput: CommentAnswerInput;
</script>

<div class="comment-footer">
    <span
        class="votes-rating"
        class:positive-rating={votesRating > 0}
        class:negative-ratings={votesRating < 0}
    >
        {votesRating}
    </span>
    <svg
        class="vote-btn-svg"
        class:voted={viewersVoteIsUp === true}
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 24 24"
    >
        <path
            fill-rule="evenodd"
            clip-rule="evenodd"
            d="M11.4697 8.46967C11.7626 8.17678 12.2374 8.17678 12.5303 8.46967L18.5303 14.4697C18.7448 14.6842 18.809 15.0068 18.6929 15.287C18.5768 15.5673 18.3033 15.75 18 15.75H6C5.69665 15.75 5.42318 15.5673 5.30709 15.287C5.191 15.0068 5.25517 14.6842 5.46967 14.4697L11.4697 8.46967Z"
            fill="currentColor"
        />
    </svg>
    <svg
        class="vote-btn-svg"
        class:voted={viewersVoteIsUp === false}
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 24 24"
    >
        <path
            fill-rule="evenodd"
            clip-rule="evenodd"
            d="M5.99977 8.25005L17.9998 8.25C18.3031 8.25 18.5766 8.43273 18.6927 8.71298C18.8088 8.99324 18.7446 9.31583 18.5301 9.53033L12.5301 15.5303C12.2373 15.8232 11.7624 15.8232 11.4695 15.5303L5.46944 9.53038C5.25494 9.31588 5.19077 8.9933 5.30686 8.71304C5.42294 8.43278 5.69642 8.25005 5.99977 8.25005Z"
            fill="currentColor"
        />
    </svg>
    <span class="total-votes">Total Votes: {totalVotesCount}</span>

    <div class="answer-btn" on:click={answerInput.show}>
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
            <path
                d="M20 3V5.07692C20 7.07786 20 8.07833 19.8547 8.91545C19.0547 13.5235 15.0934 17.1376 10.0426 17.8674C9.12509 18 7.19318 18 5 18"
                stroke="currentColor"
                stroke-width="2"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M7 21C6.39316 20.4102 4 18.8403 4 18C4 17.1597 6.39316 15.5898 7 15"
                stroke="currentColor"
                stroke-width="2"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
        Answer
    </div>
</div>
<CommentAnswerInput parentCommentId={commentId} bind:this={answerInput} />

<style>
    .comment-footer {
        box-sizing: border-box;
        width: 100%;
        height: 40px;
        gap: 4px;
        padding: 0 12px;
        display: flex;
        flex-direction: row;
        align-items: center;
    }
    .votes-rating {
        color: var(--text-faded);
        font-size: 18px;
        font-weight: 500;
        margin-right: 4px;
    }
    .positive-rating {
        color: var(--primary);
    }
    .negative-ratings {
        color: var(--red-del);
    }

    .vote-btn-svg {
        height: 100%;
        aspect-ratio: 1/1;
        cursor: pointer;
        color: var(--text-faded);
        padding: 0;
        border-radius: 8px;
    }
    .vote-btn-svg:hover {
        background-color: var(--back-secondary);
    }
    .vote-btn-svg:active {
        color: var(--primary-hov);
        transform: scale(0.96);
    }
    .voted {
        color: var(--primary);
    }
    .total-votes {
        margin-left: 4px;
        color: var(--text);
        font-size: 16px;
    }
    .answer-btn {
        margin-left: auto;
        display: flex;
        flex-direction: row;
        align-items: center;
        color: var(--text);
        font-size: 18px;
        border: 2px solid var(--primary);
        box-sizing: border-box;
        padding: 2px 8px;
        gap: 2px;
        border-radius: 8px;
        cursor: pointer;
    }
    .answer-btn:hover {
        text-decoration: underline;
    }
    .answer-btn svg {
        height: 20px;
    }
</style>
