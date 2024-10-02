<script lang="ts">
    import { TestStylesArrowTypeUtils } from "../../../../ts/enums/TestStylesArrowType";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { TestCreationStylesTabData } from "../../../../ts/test_creation_tabs_classes/test_creation_shared/TestCreationStylesTabData";
    import TabHeaderWithButton from "../../creation_shared_components/TabHeaderWithButton.svelte";
    import TabViewDataLoader from "../../creation_shared_components/TabViewDataLoader.svelte";
    import DraftTestStylesEditingDialog from "./DraftTestStylesEditingDialog.svelte";

    export let stylesData: TestCreationStylesTabData;
    export let testId: string;

    let dataFetchingErr: string = "Server does not respond";
    let dialogElement: DraftTestStylesEditingDialog;
    async function loadData() {
        const url = "/api/testStyles/getDraftTestStylesData/" + testId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            console.log(data);
            const arrow = TestStylesArrowTypeUtils.fromId(data.arrowType);
            const accentColor = data.accentColor;
            stylesData = new TestCreationStylesTabData(accentColor, arrow);
            dataFetchingErr = "";
        } else {
            dataFetchingErr = await getErrorFromResponse(response);
            stylesData = TestCreationStylesTabData.empty();
        }
    }
</script>

<TabViewDataLoader {loadData} isEmpty={() => dataFetchingErr !== ""}>
    <div class="unable-to-fetch" slot="empty">
        <label>Unable to fetch data</label>
        <p class="error-message">Error: {dataFetchingErr}</p>
    </div>
    <div slot="content" class="conclusion-data">
        <DraftTestStylesEditingDialog
            bind:this={dialogElement}
            updateParentElementData={loadData}
        />
        <TabHeaderWithButton tabName="Test Styles:" />
        <div class="prop-name-val-p">
            <span class="property-name">Chosen accent color:</span>
            <span class="property-value">{stylesData.accentColor}</span>
            <span style="color:{stylesData.accentColor}"> </span>
        </div>
        <div class="prop-name-val-p">
            <span class="property-name">Chosen arrow type:</span>
            <span class="property-value">{stylesData.arrowType}</span>
            <div class="arrows-container">
                <svelte:component
                    this={TestStylesArrowTypeUtils.getIcon(
                        stylesData.arrowType,
                    )}
                />
                <svelte:component
                    this={TestStylesArrowTypeUtils.getIcon(
                        stylesData.arrowType,
                    )}
                />
            </div>
        </div>
    </div>
</TabViewDataLoader>

<style>
    .unable-to-fetch {
        display: flex;
        flex-direction: column;
        align-items: center;
    }
    .unable-to-fetch {
        position: absolute;
        top: 40%;
        left: 50%;
        transform: translateX(-50%);
        display: flex;
        flex-direction: column;
        align-items: center;
        background-color: var(--back-secondary);
        padding: 10px 20px;
        border-radius: 8px;
    }
    .arrows-container {
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 4px;
    }
    .arrows-container {
    }
</style>
