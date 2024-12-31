<script lang="ts">
    import { Err } from "../../../ts/Err";
    import { getErrorFromResponse } from "../../../ts/ErrorResponse";
    import { ManageTestStatisticsTabData } from "../../../ts/page_classes/manage_test_page/statistics/ManageTestStatisticsTabData";
    import TabContentWrapper from "../page_layout/TabContentWrapper.svelte";

    export let testId: string;
    export let isActive: boolean;

    let tabData: ManageTestStatisticsTabData;
    async function fetchTabData(): Promise<Err> {
        const response = await fetch(
            `/api/manageTest/statistics/tabData/${testId}`,
        );
        if (response.ok) {
            const data = await response.json();
            tabData = ManageTestStatisticsTabData.fromResponseData(data);
            return Err.none();
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        } else {
            return new Err("Something went wrong.");
        }
    }
</script>

<TabContentWrapper {fetchTabData} {isActive}>
    <p>{JSON.stringify(tabData, null, 2)}</p>
</TabContentWrapper>
