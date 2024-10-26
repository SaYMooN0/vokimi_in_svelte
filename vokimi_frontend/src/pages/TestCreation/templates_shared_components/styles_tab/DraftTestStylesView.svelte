<script lang="ts">
    import { TestStylesArrowTypeUtils } from "../../../../ts/enums/TestStylesArrowType";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { TestCreationStylesTabData } from "../../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationStylesTabData";
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
            {testId}
            bind:this={dialogElement}
            updateParentElementData={loadData}
        />
        <TabHeaderWithButton
            tabName="Test Styles:"
            onButtonClick={() => dialogElement.open(stylesData)}
            buttonText="Edit Styles"
        />
        <div class="prop-name-val-p">
            <span class="property-name">Chosen accent color:</span>
            <span
                style="background-color:{stylesData.accentColor}"
                class="accent-color"
            >
            </span>
            <span class="property-value">{stylesData.accentColor}</span>
        </div>
        <div class="prop-name-val-p">
            <span class="property-name">Chosen arrow type:</span>
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
            <span class="property-value">
                ({TestStylesArrowTypeUtils.getFullName(stylesData.arrowType)})
            </span>
        </div>
    </div>
</TabViewDataLoader>

<style>
    .unable-to-fetch {
        position: absolute;
        display: flex;
        flex-direction: column;
        align-items: center;
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
    .prop-name-val-p {
        display: flex;
        flex-direction: row;
        align-items: center;
        height: 44px;
        margin: 4px 8px;
        gap: 12px;
    }
    .prop-name-val-p .property-name {
        font-weight: 500;
        font-size: 18px;
    }
    .prop-name-val-p .property-value {
        font-size: 20px;
    }

    .accent-color {
        display: flex;
        width: max(12vw, 120px);
        height: 80%;
        border-radius: 8px;
    }
    .arrows-container {
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 8px;
        height: 100%;
    }
    .arrows-container :global(svg) {
        height: 90%;
        aspect-ratio: 1/1;
        border-radius: 24%;
        color: var(--text);
    }
    .arrows-container :global(svg:nth-child(2)) {
        transform: rotate(180deg);
    }
</style>
