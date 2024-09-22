<script lang="ts">
    import { TestCreationConclusionTabData } from "../../../../ts/test_creation_tabs_classes/test_creation_shared/TestCreationConclusionTabData";
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";
    import { StringUtils } from "../../../../ts/utils/StringUtils";
    import TabHeaderWithButton from "../../creation_shared_components/TabHeaderWithButton.svelte";
    import TabViewDataLoader from "../../creation_shared_components/TabViewDataLoader.svelte";
    import TextWithOptionalImageInput from "../../creation_shared_components/TextWithOptionalImageInput.svelte";

    export let conclusionData: TestCreationConclusionTabData;
    export let testId: string;

    async function loadData() {
        const url = "/api/testCreation/getDraftTestConclusionData/" + testId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            conclusionData = new TestCreationConclusionTabData(
                data.id,
                data.text,
                data.additionalImage,
                data.anyFeedback,
                data.feedbackText,
                data.maxFeedbackLength,
            );
        } else {
            conclusionData = TestCreationConclusionTabData.empty();
        }
    }
    async function createConclusion() {
        const url = "/api/testCreation/createDraftTestConclusion/" + testId;
        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
        });
        if (response.ok) {
            await loadData();
        } else {
            dataFetchingErr =
                "Failed to create conclusion. Please refresh the page and try again";
        }
    }
    let dataFetchingErr: string = "";
</script>

<TabViewDataLoader {loadData} isEmpty={() => conclusionData.isEmpty()}>
    <div slot="empty" class="no-conclusion-div">
        <h2>It seems like you haven't added conclusion yet</h2>
        <button class="add-conclusion-btn" on:click={createConclusion}>
            Add conclusion
        </button>
        <p class="error-message">{dataFetchingErr}</p>
    </div>
    <div slot="content" class="conlusion-data">
        <TabHeaderWithButton tabName="Test Conclusion:" />
        <p>
            <span class="property-name">Conclusion text:</span>
            <span class="property-value">{conclusionData.text}</span>
        </p>
        {#if !StringUtils.isNullOrWhiteSpace(conclusionData.additionalImage)}
            <img
                src={ImgUtils.imgUrlWithVersion(
                    conclusionData.additionalImage ?? "",
                )}
                class="conclusion-img"
            />
        {/if}
        {#if conclusionData.anyFeedback}
            <p class="feedback-p">Feedback:</p>
            <p>
                <span class="property-name">Feedback accompanying text:</span>
                <span class="property-value"
                    >{conclusionData.feedbackText}
                </span>
            </p>
            <p>
                <span class="property-name">Max feedback length:</span>
                <span class="property-value">
                    {conclusionData.maxFeedbackLength}
                </span>
            </p>
        {:else}
            <p class="no-feedback">Conclusion does not imply any feedback</p>
        {/if}
        <p>{JSON.stringify(conclusionData)}</p>
    </div>
</TabViewDataLoader>

<style>
    .no-conclusion-div {
        margin: 0 auto;
        display: flex;
        flex-direction: column;
        align-items: center;
    }
    .add-conclusion-btn {
        background-color: var(--primary);
        color: var(--back-main);
        border: none;
        border-radius: 4px;
        padding: 8px 20px;
        font-size: 20px;
        cursor: pointer;
        transition: all 0.12s ease-in;
    }
    .add-conclusion-btn:hover {
        background-color: var(--primary-hov);
        padding: 8px 28px;
    }
    .add-conclusion-btn:active {
        transform: scale(0.98);
    }
</style>
