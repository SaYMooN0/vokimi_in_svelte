<script lang="ts">
    import { Err } from "../../../ts/Err";
    import { getErrorFromResponse } from "../../../ts/ErrorResponse";
    import { ManageTestFeedbackTabData } from "../../../ts/page_classes/manage_test_page/feedback/ManageTestFeedbackTabData";
    import TabContentWrapper from "../page_layout/TabContentWrapper.svelte";
    export let testId: string;
    export let isActive: boolean;


  
    let tabData: ManageTestFeedbackTabData;
    let feedbackAllowedChangeError: string = "";
    async function fetchTabData(): Promise<ManageTestFeedbackTabData | Err> {
        const response = await fetch(`/api/manageTest/feedback/tabData/${testId}`);
        if (response.ok) {
            const data = await response.json();
            tabData = ManageTestFeedbackTabData.fromResponseData(data);
            return tabData;
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        } else {
            return new Err("Something went wrong.");
        }
    }
</script>

<TabContentWrapper {fetchTabData} {isActive}>
    {JSON.stringify(tabData)}
</TabContentWrapper>
