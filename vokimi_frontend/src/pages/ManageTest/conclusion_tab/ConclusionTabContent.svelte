<script lang="ts">
    import { Err } from "../../../ts/Err";
    import { getErrorFromResponse } from "../../../ts/ErrorResponse";
    import { ManageTestConclusionTabData } from "../../../ts/page_classes/manage_test_page/conclusion/ManageTestConclusionTabData";
    import TabContentWrapper from "../page_layout/TabContentWrapper.svelte";
    import ConclusionEnabledMessage from "./ConclusionEnabledMessage.svelte";
    import NoConclusionMessage from "./NoConclusionMessage.svelte";
    import ViewConclusionTabContent from "./view_conclusion_components/ViewConclusionContent.svelte";
    export let testId: string;
    export let isActive: boolean;

    let tabConclusionData: ManageTestConclusionTabData;
    let testHasConclusion = false;
    async function fetchTabData(): Promise<Err> {
        const response = await fetch(
            `/api/manageTest/conclusion/tabData/${testId}`,
        );
        if (response.ok) {
            const data = await response.json();
            if (data.testHasConclusion) {
                testHasConclusion = true;
                tabConclusionData =
                    ManageTestConclusionTabData.fromResponseData(
                        data.conclusionData,
                    );
            } else {
                testHasConclusion = false;
            }
            return Err.none();
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        } else {
            return new Err("Something went wrong.");
        }
    }
</script>

<TabContentWrapper {fetchTabData} {isActive} let:updateTabData>
    {#if !testHasConclusion}
        <NoConclusionMessage {testId} updateTabData={() => updateTabData()} />
    {:else}
        <ConclusionEnabledMessage
            {testId}
            updateTabData={() => updateTabData()}
        />
        <ViewConclusionTabContent conclusionData={tabConclusionData} {testId} />
    {/if}
</TabContentWrapper>
