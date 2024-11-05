<script lang="ts">
    import { TestTemplate } from "../../../ts/enums/TestTemplate";
    import { Err } from "../../../ts/Err";
    import { getErrorFromResponse } from "../../../ts/ErrorResponse";
    import { GeneralTestTakenReceivedResultData } from "../../../ts/page_classes/test_taking_page/general_test/GeneralTestTakenReceivedResultData";
    import AllPossibleResultsDisplay from "./general_test/AllPossibleResultsDisplay.svelte";
    import ReceivedResultDisplay from "./general_test/ReceivedResultDisplay.svelte";
    import NextActionOffer from "./shared/NextActionOffer.svelte";

    export let receivedResId: string;
    export let testId: string;

    async function loadReceivedResultData(): Promise<
        Err | GeneralTestTakenReceivedResultData
    > {
        const response = await fetch(
            "/api/testTaking/getGeneralTestReceivedResultData/" + receivedResId,
        );
        if (response.ok) {
            const data = await response.json();
            return new GeneralTestTakenReceivedResultData(
                data.receivedResultName,
                data.receivedResultImage,
                data.receivedResultText,
                data.allResults,
            );
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        } else {
            return new Err(
                "Something went wrong... Please refresh the page and try again",
            );
        }
    }
</script>

{#await loadReceivedResultData() then loadingRes}
    {#if loadingRes instanceof Err}
        <p class="err-message">{loadingRes.toString()}</p>
    {:else}
        <div class="view-res-container">
            <ReceivedResultDisplay
                receivedResultName={loadingRes.receivedResultName}
                receivedResultImage={loadingRes.receivedResultImage}
                receivedResultText={loadingRes.receivedResultText}
            />
            <AllPossibleResultsDisplay
                receivedResultId={receivedResId}
                allResults={loadingRes.allResult}
            />
            <NextActionOffer
                receivedResultId={receivedResId}
                {testId}
                testTemplate={TestTemplate.General}
            />
        </div>
    {/if}
{/await}

<style>
    .view-res-container {
        display: flex;
        flex-direction: column;
        align-items: center;
    }
</style>
