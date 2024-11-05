<script lang="ts">
    import { navigate } from "svelte-routing/src/history";
    import TestTakenPostCreationDialog from "../../../../components/shared/dialogs/posts_creation_dialogs/TestTakenPostCreationDialog.svelte";
    import type { TestTemplate } from "../../../../ts/enums/TestTemplate";
    import ShareResultInDiscussionsDialog from "./next_action_offer_components/ShareResultInDiscussionsDialog.svelte";

    export let receivedResultId: string;
    export let testTemplate: TestTemplate;
    export let testId: string;
    function navigateToTestPage() {
        navigate(`/view-test/${testId}`);
    }
    function openShareInDiscussions() {}
    function navigateToTestRating() {
        navigate(`/view-test/${testId}/ratings`);
    }
    function openPostResultToMyPage() {
        testTakenPostCreationDialog.open();
    }
    let testTakenPostCreationDialog: TestTakenPostCreationDialog;
    let shareResultInDiscussionsDialog: ShareResultInDiscussionsDialog;
</script>

<p class="offer-p">What would you like to do next?</p>
<div class="actions-container unselectable">
    <div class="action" on:click={navigateToTestPage}>Go to test page</div>
    <div class="action" on:click={openShareInDiscussions}>
        Share my result in discussions
    </div>
    <div class="action" on:click={navigateToTestRating}>Rate this test</div>
    <div class="action" on:click={openPostResultToMyPage}>
        Post my result to my page
    </div>
</div>

<TestTakenPostCreationDialog
    {receivedResultId}
    {testId}
    bind:this={testTakenPostCreationDialog}
/>
<ShareResultInDiscussionsDialog
    {receivedResultId}
    bind:this={shareResultInDiscussionsDialog}
/>

<style>
    .offer-p {
        font-size: 18px;
        color: var(--text-faded);
    }

    .actions-container {
        display: flex;
        flex-direction: row;
        flex-wrap: wrap;
        justify-content: center;
        align-items: center;
        gap: 16px;
        width: 960px;
    }
    .action {
        font-size: 18px;
        padding: 6px 20px;
        background-color: var(--primary);
        color: var(--back-main);
        border-radius: 80px;
        transition: all 0.08s ease-in;
        cursor: pointer;
    }
    .action:hover {
        transform: scale(1.02);
        background-color: var(--primary-hov);
    }
    .action:active {
        transform: scale(0.98);
    }
</style>
