<script lang="ts">
    import { getErrorFromResponse } from "../../ts/ErrorResponse";
    import FollowingNeededToViewTest from "./test_view_errors/FollowingNeededToViewTest.svelte";
    import FriendshipNeededToViewTest from "./test_view_errors/FriendshipNeededToViewTest.svelte";
    import TestNotFound from "./test_view_errors/TestNotFound.svelte";
    import ViewTestPageContent from "./view_test_page_components/ViewTestPageContent.svelte";

    export let testId: string;
    let creatorData: { creatorId: string; creatorProfilePath: string } = {
        creatorId: "",
        creatorProfilePath: "",
    };
    enum ViewTestPageState {
        AccessDenied = 0,
        TestNotFound = 1,
        FriendshipNeeded = 2,
        FollowingNeeded = 3,
        Success = 4,
        ErrorOccurred = 5,
    }
    let pageState: ViewTestPageState = ViewTestPageState.AccessDenied;
    let fetchingErr: string = "";
    async function loadTestViewInfo() {
        const response = await fetch(
            "/api/viewTest/checkUserAccessToViewTest/" + testId,
        );
        if (response.status === 200) {
            const data = await response.json();
            switch (data.userAccess) {
                case "denied":
                    pageState = ViewTestPageState.AccessDenied;
                    break;
                case "following_needed":
                    pageState = ViewTestPageState.FollowingNeeded;
                    creatorData = {
                        creatorId: data.creatorId,
                        creatorProfilePath: data.creatorProfilePath,
                    };
                    break;
                case "friendship_needed":
                    pageState = ViewTestPageState.FriendshipNeeded;
                    creatorData = {
                        creatorId: data.creatorId,
                        creatorProfilePath: data.creatorProfilePath,
                    };
                    break;
                case "success":
                    pageState = ViewTestPageState.Success;
                    break;
                case "test_not_found":
                    pageState = ViewTestPageState.TestNotFound;
                    break;
                default:
                    pageState = ViewTestPageState.ErrorOccurred;
                    fetchingErr = "Unknown error. Please try again later";
                    break;
            }
        } else if (response.status === 400) {
            pageState = ViewTestPageState.ErrorOccurred;
            fetchingErr = await getErrorFromResponse(response);
        } else {
            pageState = ViewTestPageState.ErrorOccurred;
            fetchingErr = "Unknown error. Please try again later";
        }
    }
</script>

{#await loadTestViewInfo()}
    <div class="loading-message-div">Loading test data...</div>
{:then}
    {#if pageState === ViewTestPageState.Success}
        <ViewTestPageContent {testId} />
    {:else if pageState === ViewTestPageState.TestNotFound}
        <TestNotFound />
    {:else if pageState === ViewTestPageState.FriendshipNeeded && creatorData.creatorId !== ""}
        <FriendshipNeededToViewTest
            creatorId={creatorData.creatorId}
            creatorProfilePath={creatorData.creatorProfilePath}
        />
    {:else if pageState === ViewTestPageState.FollowingNeeded && creatorData.creatorId !== ""}
        <FollowingNeededToViewTest
            creatorId={creatorData.creatorId}
            creatorProfilePath={creatorData.creatorProfilePath}
        />
    {:else if pageState === ViewTestPageState.AccessDenied}
        <p>Access denied</p>
    {:else}
        <p>Unknown error</p>
    {/if}
{/await}
