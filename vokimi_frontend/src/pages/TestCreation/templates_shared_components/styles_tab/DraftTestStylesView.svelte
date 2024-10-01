<script lang="ts">
    import { TestStylesArrowTypeUtils } from "../../../../ts/enums/TestStylesArrowType";
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
        const url = "/api/testCreation/getDraftTestStylesData/" + testId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            const arrow = TestStylesArrowTypeUtils.fromId(data.arrowType);
            const accentColor = data.accentColor;
            stylesData = new TestCreationStylesTabData(accentColor, arrow);
        } else {
            dataFetchingErr = await getErrorFromResponse(response);
            stylesData = TestCreationStylesTabData.empty();
        }
    }
</script>

{#if dataFetchingErr === ""}
    <TabViewDataLoader {loadData} isEmpty={() => false}>
        <div slot="content" class="conclusion-data">
            <DraftTestStylesEditingDialog
                bind:this={dialogElement}
                updateParentElementData={loadData}
            />
            <TabHeaderWithButton tabName="Test Styles:" />
            styles
        </div>
    </TabViewDataLoader>
{:else}
    <div class="unable-to-fetch">
        <label>Unable to fetch data</label>
        <p class="error-message">Error: {dataFetchingErr}</p>
    </div>
{/if}

<style>
    .unable-to-fetch {
        display: flex;
        flex-direction: column;
        align-items: center;
    }
</style>
