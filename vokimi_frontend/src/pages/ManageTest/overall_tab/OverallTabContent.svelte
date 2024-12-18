<script lang="ts">
    import { Err } from "../../../ts/Err";
    import TabContentWrapper from "../page_layout/TabContentWrapper.svelte";

    export let testId: string;
    export let isActive: boolean;

    interface OverallTabData {}

    async function fetchTabData(): Promise<OverallTabData | Err> {
        const response = await fetch(`/api/manageTest/overall/tabData/${testId}`);
        return new Err("Something went wrong.");
    }
</script>

<TabContentWrapper {fetchTabData} let:tabDataSetRes {isActive}>
    {#if !(tabDataSetRes instanceof Err)}
        <div>Tab content</div>
    {/if}
</TabContentWrapper>
