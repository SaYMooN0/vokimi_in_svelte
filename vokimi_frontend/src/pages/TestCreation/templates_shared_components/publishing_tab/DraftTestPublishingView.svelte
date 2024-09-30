<script lang="ts">
    import DraftTestPublishingDialog from "./DraftTestPublishingDialog.svelte";

    export let testId: string;

    async function publishBtnPressed() {
        await publishingDialog.open();
    }

    let publishingDialog: DraftTestPublishingDialog;
</script>

<DraftTestPublishingDialog {testId} bind:this={publishingDialog} />
<div class="test-publishing-container">
    <label class="warning-label">Warning</label>
    <p class="warning">
        Publishing the test will allow other users to see this test and take it
        (unless, of course, the privacy settings prohibit it). After publishing,
        you will not be able to edit most part of the test, but you will still
        be able to change tags and privacy settings at any time.
    </p>
    <button on:click={publishBtnPressed} class="publish-btn"> Publish </button>
</div>

<style>
    .test-publishing-container {
        margin: 0 auto;
        margin-top: 120px;
        display: flex;
        align-items: center;
        justify-content: center;
        flex-direction: column;
        background-color: var(--back-secondary);
        max-width: min(64vw, 1200px);
        padding: 20px;
        border-radius: 8px;
    }
    .warning-label {
        background-color: var(--text-faded);
        color: var(--back-main);
        padding: 8px 20px;
        border-radius: 6px;
        font-size: 18px;
        font-weight: 600;
        letter-spacing: 2px;
    }

    .warning {
        color: var(--text);
        font-size: 20px;
    }

    .publish-btn {
        margin-top: 20px;
        display: flex;
        align-items: center;
        flex-direction: row;
        gap: 8px;
        background-color: var(--primary);
        color: var(--back-main);
        border: none;
        border-radius: 6px;
        padding: 8px 24px;
        font-size: 24px;
        cursor: pointer;
        transition: all 0.12s ease-in;
    }
    .publish-btn:hover {
        background-color: var(--primary-hov);
    }
</style>
