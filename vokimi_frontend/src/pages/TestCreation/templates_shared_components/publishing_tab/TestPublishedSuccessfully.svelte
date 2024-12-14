<script lang="ts">
    import { navigate } from "svelte-routing";
    import TestPublishedPostCreationDialog from "../../../../components/shared/dialogs/posts_creation_dialogs/TestPublishedPostCreationDialog.svelte";

    export let publishedTestId: string;
    export let publishedTestName: string;
    let newPostMakingErr: string = "";
    function makeNewPost() {
        newPostMakingErr = "Not implemented T_T";
    }
    function goToTestPage() {
        navigate(`/view-test/${publishedTestId}`);
    }
    let postsCreationDialog: TestPublishedPostCreationDialog;
</script>

<TestPublishedPostCreationDialog bind:this={postsCreationDialog} />
<h2 class="test-published-message">
    Your test "<span>{publishedTestName}</span>" has been published
    successfully
</h2>
<p class="new-post-p">Would you like to make a post about it?</p>

<button class="new-post-btn unselectable" on:click={makeNewPost}>
    Make new post
</button>
<p class="new-post-err-p">{newPostMakingErr}</p>
<label class="test-view-link unselectable" on:click={goToTestPage}>
    Go to the test page
</label>
<style>
    .test-published-message {
        margin-top: 24px;
        margin-bottom: 8px;
        max-width: min(68vw, 800px);
        min-width: auto;
        font-size: 32px;
        color: var(--text);
        text-align: center;
    }
    .test-published-message span {
        text-decoration: underline;
        text-decoration-color: var(--primary);
        text-underline-offset: 6px;
        text-overflow: ellipsis;
        overflow: hidden;
        white-space: nowrap;
    }

    .new-post-p {
        margin: 0;
        font-size: 24px;
    }

    .new-post-btn {
        margin-top: 48px;
        color: var(--back-main);
        background-color: var(--primary);
        padding: 8px 20px;
        border-radius: 4px;
        border: none;
        font-size: 22px;
        cursor: pointer;
        transition: all 0.12s ease-in;
    }

    .new-post-btn:hover {
        background-color: var(--primary-hov);
    }
    .test-view-link {
        margin: 8px 0;
        font-size: 18px;
        color: var(--text-faded);
        text-decoration: underline;
        text-decoration-color: var(--text-faded);
        text-underline-offset: 2px;
        padding: 4px 8px;
        border-radius: 4px;
        transition: all 0.08s;
        cursor: pointer;
    }

    .test-view-link:hover {
        color: var(--primary);
        text-decoration-color: var(--primary);
    }
    .test-view-link:active {
        color: var(--primary-hov);
        text-decoration-color: var(--primary-hov);
        background-color: var(--back-secondary);
    }
</style>
