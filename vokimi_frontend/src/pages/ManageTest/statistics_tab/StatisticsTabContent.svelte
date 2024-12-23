<script lang="ts">
    import { Err } from "../../../ts/Err";
    import TabContentWrapper from "../page_layout/TabContentWrapper.svelte";

    export let testId: string;
    export let isActive: boolean;

    interface StatisticsTabData {}

    async function fetchTabData(): Promise<StatisticsTabData | Err> {
        const response = await fetch(
            `/api/manageTest/statistics/tabData/${testId}`,
        );
        return new Err("Something went wrong.");
    }
</script>

<TabContentWrapper {fetchTabData} {isActive}>
    Tab data statistics
</TabContentWrapper>
