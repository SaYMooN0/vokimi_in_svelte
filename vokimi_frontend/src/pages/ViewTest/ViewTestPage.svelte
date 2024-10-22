<script lang="ts">
    import FollowingNeededToViewTest from "./test_view_errors/FollowingNeededToViewTest.svelte";
    import FriendshipNeededToViewTest from "./test_view_errors/FriendshipNeededToViewTest.svelte";
    import TestNotFound from "./test_view_errors/TestNotFound.svelte";
    import ViewTestPageContent from "./view_test_page_components/ViewTestPageContent.svelte";

    export let testId: string;
    enum ViewTestPageState {
        AccessDenied = 0,
        TestNotFound = 1,
        FriendshipNeeded = 2,
        FollowingNeeded = 3,
        Success = 4,
    }
    let pageState: ViewTestPageState = ViewTestPageState.AccessDenied;
    async function loadTestViewInfo() {
        let response = await fetch("" + testId);
    }
</script>

{#await loadTestViewInfo()}
    <div class="loading-message-div">Loading test data...</div>
{:then}
    {#if pageState === ViewTestPageState.Success}
        <ViewTestPageContent />
    {:else if pageState === ViewTestPageState.TestNotFound}
        <TestNotFound />
    {:else if pageState === ViewTestPageState.FriendshipNeeded}
        <FriendshipNeededToViewTest />
    {:else if pageState === ViewTestPageState.FollowingNeeded}
        <FollowingNeededToViewTest />
    {:else if pageState === ViewTestPageState.AccessDenied}
        <p>Access denied</p>
    {:else}
        <p>Unknown error</p>
    {/if}
{/await}
<p>{testId}</p>
