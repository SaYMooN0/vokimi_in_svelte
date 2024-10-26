<script lang="ts">
    import { TestAccessPageState } from "../../ts/enums/TestAccessPageState";
    import { Err } from "../../ts/Err";
    import { getErrorFromResponse } from "../../ts/ErrorResponse";
    import { TestCreatorBasicData } from "../../ts/page_classes/view_test_page_classes/TestCreatorBasicData";
    import RelationshipConditionsNotMet from "./test_access_err_components/RelationshipConditionsNotMet.svelte";
    import TestAccessDenied from "./test_access_err_components/TestAccessDenied.svelte";
    import TestNotFound from "./test_access_err_components/TestNotFound.svelte";

    export let testId: string;
    let creatorData: TestCreatorBasicData;

    async function checkTetAccessState(): Promise<TestAccessPageState | Err> {
        const response = await fetch(
            "/api/viewTest/checkTestViewPermission/" + testId,
        );
        if (response.status === 200) {
            const data = await response.json();

            switch (data.accessStringValue) {
                case "denied":
                    return TestAccessPageState.AccessDenied;
                case "following_needed":
                    creatorData = new TestCreatorBasicData(
                        data.testCreatorId,
                        data.testCreatorUsername,
                        data.testCreatorProfilePath,
                    );
                    return TestAccessPageState.FollowingNeeded;
                case "friendship_needed":
                    creatorData = new TestCreatorBasicData(
                        data.testCreatorId,
                        data.testCreatorUsername,
                        data.testCreatorProfilePath,
                    );
                    return TestAccessPageState.FriendshipNeeded;
                case "success":
                    return TestAccessPageState.Success;
                case "test_not_found":
                    return TestAccessPageState.TestNotFound;
                default:
                    return new Err("Unknown error. Please try again later");
            }
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        } else {
            return new Err("Unknown error. Please try again later");
        }
    }
</script>

{#await checkTetAccessState()}
    <div class="loading-message-div">Loading test data...</div>
{:then pageState}
    {#if pageState instanceof Err}
        <div class="error-occured">{pageState.toString()}</div>
    {:else if pageState === TestAccessPageState.Success}
        <slot />
    {:else if pageState === TestAccessPageState.TestNotFound}
        <TestNotFound />
    {:else if pageState === TestAccessPageState.FriendshipNeeded && creatorData.testCreatorId !== ""}
        <RelationshipConditionsNotMet
            {creatorData}
            conditionMessage={"Friendship required"}
        />
    {:else if pageState === TestAccessPageState.FollowingNeeded && creatorData.testCreatorId !== ""}
        <RelationshipConditionsNotMet
            {creatorData}
            conditionMessage={"Following required"}
        />
    {:else if pageState === TestAccessPageState.AccessDenied}
        <TestAccessDenied />
    {:else}
        <p>Unknown error</p>
    {/if}
{/await}
