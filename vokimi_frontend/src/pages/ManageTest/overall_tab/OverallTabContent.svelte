<script lang="ts">
    import { Err } from "../../../ts/Err";
    import TabContentWrapper from "../page_layout/TabContentWrapper.svelte";

    export let testId: string;
    export let isActive: boolean;

    interface OverallTabData {}

    async function fetchTabData(): Promise<OverallTabData | Err> {
        const response = await fetch(`/api/manageTest/overall/tabData/${testId}`);
        if (response.ok) {
            const data = await response.json();
            return data;
        } else if (response.status === 400) {
            return new Err("Something went wrong.");
        } else {
            return new Err("Something went wrong.");
        }
    }
</script>

<TabContentWrapper {fetchTabData} {isActive}>
    Tab data overall
</TabContentWrapper>
