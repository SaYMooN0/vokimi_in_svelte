<script lang="ts">
    import { Err } from "../../../ts/Err";
    import { getErrorFromResponse } from "../../../ts/ErrorResponse";
    import { ManageTestStatisticsTabData } from "../../../ts/page_classes/manage_test_page/statistics/ManageTestStatisticsTabData";
    import { GeneralTestStatisticsData } from "../../../ts/page_classes/manage_test_page/statistics/templates/GeneralTestStatisticsData";
    import { ScoringTestStatisticsData } from "../../../ts/page_classes/manage_test_page/statistics/templates/ScoringTestStatisticsData";
    import TabContentWrapper from "../page_layout/TabContentWrapper.svelte";
    import SectionStatisticsCardsContainer from "./sections_shared_components/SectionStatisticsCardsContainer.svelte";
    import StatisticsTabSectionHeader from "./sections_shared_components/StatisticsTabSectionHeader.svelte";
    import GeneralTestStatisticsTab from "./templates_based_content/GeneralTestStatisticsTab.svelte";
    import ScoringTestStatisticsTab from "./templates_based_content/ScoringTestStatisticsTab.svelte";

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
    {#if tabData instanceof GeneralTestStatisticsData}
        <GeneralTestStatisticsTab {tabData} />
    {:else if tabData instanceof ScoringTestStatisticsData}
        <ScoringTestStatisticsTab {tabData} />
    {/if}

</TabContentWrapper>

<style>
</style>
