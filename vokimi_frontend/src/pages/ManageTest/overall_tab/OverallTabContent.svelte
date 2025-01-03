<script lang="ts">
    import { Err } from "../../../ts/Err";
    import { getErrorFromResponse } from "../../../ts/ErrorResponse";
    import { ManageTestOverallTabData } from "../../../ts/page_classes/manage_test_page/overall/ManageTestOverallTabData";
    import TabContentWrapper from "../page_layout/TabContentWrapper.svelte";
    import ChangeTestPrivacyDialog from "./ChangeTestPrivacyDialog.svelte";
    import TestActionsContainer from "./TestActionsContainer.svelte";

    export let testId: string;
    export let isActive: boolean;

    let tabData: ManageTestOverallTabData;
    async function fetchTabData(): Promise<Err> {
        const response = await fetch(
            `/api/manageTest/overall/tabData/${testId}`,
        );
        if (response.ok) {
            const data = await response.json();
            tabData = ManageTestOverallTabData.fromResponseData(data);
            return Err.none();
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        } else {
            return new Err("Something went wrong.");
        }
    }
    let privacyChangingDialog: ChangeTestPrivacyDialog;
</script>

<TabContentWrapper {fetchTabData} {isActive}>
    Tab data overall
    <ChangeTestPrivacyDialog
        bind:this={privacyChangingDialog}
        {testId}
        currentTestPrivacy={tabData.testPrivacy}
        updateParentElementPrivacy={(newPrivacyVal) =>
            (tabData.testPrivacy = newPrivacyVal)}
    />
    <TestActionsContainer
        {testId}
        openTestPrivacyChangingDialog={() => privacyChangingDialog.open()}
    />
</TabContentWrapper>
