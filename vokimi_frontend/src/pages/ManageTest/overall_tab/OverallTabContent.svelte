<script lang="ts">
    import { Err } from "../../../ts/Err";
    import type { ManageTestOverallTabData } from "../../../ts/page_classes/manage_test_page/overall/ManageTestOverallTabData";
    import TabContentWrapper from "../page_layout/TabContentWrapper.svelte";

    export let testId: string;
    export let isActive: boolean;

    let tabData: ManageTestOverallTabData;
    async function fetchTabData(): Promise<Err> {
        const response = await fetch(
            `/api/manageTest/overall/tabData/${testId}`,
        );
        if (response.ok) {
            tabData = await response.json();
            return Err.none();
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
