<script lang="ts">
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { TestCreationStylesTabData } from "../../../../ts/test_creation_tabs_classes/test_creation_shared/TestCreationStylesTabData";
    import TabHeaderWithButton from "../../creation_shared_components/TabHeaderWithButton.svelte";
    import TabViewDataLoader from "../../creation_shared_components/TabViewDataLoader.svelte";
    import DraftTestStylesEditingDialog from "./DraftTestStylesEditingDialog.svelte";

    export let stylesData: TestCreationStylesTabData;
    export let testId: string;

    let dataFetchingErr: string = "";
    let dialogElement: DraftTestStylesEditingDialog;
    async function loadData() {
        const url = "/api/testCreation/getDraftTestConclusionData/" + testId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            stylesData = new TestCreationStylesTabData();
        } else {
            dataFetchingErr = await getErrorFromResponse(response);
            stylesData = TestCreationStylesTabData.empty();
        }
    }
</script>

<TabViewDataLoader {loadData} isEmpty={() => stylesData.isEmpty()}>
    <div slot="empty" class="unable-to-fetch">
        <label>Unable to fetch data</label>

        <p class="error-message">Error: {dataFetchingErr}</p>
    </div>
    <div slot="content" class="conclusion-data">
        <DraftTestStylesEditingDialog
            bind:this={dialogElement}
            updateParentElementData={loadData}
        />
        <TabHeaderWithButton tabName="Test Styles:" />
        styles
    </div>
</TabViewDataLoader>
