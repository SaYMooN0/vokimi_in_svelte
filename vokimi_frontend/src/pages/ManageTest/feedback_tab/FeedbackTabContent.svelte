<script lang="ts">
    import { Err } from "../../../ts/Err";
    import TabContentWrapper from "../page_layout/TabContentWrapper.svelte";
    export let testId: string;
    export let isActive: boolean;

    interface FeedbackTabData {}

    async function fetchTabData(): Promise<FeedbackTabData | Err> {
        const response = await fetch(
            `/api/manageTest/feedback/tabData/${testId}`,
        );
        return new Err("Something went wrong.");
    }
</script>

<TabContentWrapper {fetchTabData} let:tabDataSetRes {isActive}>
    {#if !(tabDataSetRes instanceof Err)}
        <div>
            <h1>Feedback</h1>
        </div>
    {/if}
</TabContentWrapper>
