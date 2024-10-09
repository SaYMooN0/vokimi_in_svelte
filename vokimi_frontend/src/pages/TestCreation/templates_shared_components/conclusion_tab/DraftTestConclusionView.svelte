<script lang="ts">
    import ActionConfirmationDialog from "../../../../components/shared/ActionConfirmationDialog.svelte";
    import { Err } from "../../../../ts/Err";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { TestCreationConclusionTabData } from "../../../../ts/my_tests_page/test_creation_tabs_classes/test_creation_shared/TestCreationConclusionTabData";
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";
    import { StringUtils } from "../../../../ts/utils/StringUtils";
    import TabHeaderWithButton from "../../creation_shared_components/TabHeaderWithButton.svelte";
    import TabViewDataLoader from "../../creation_shared_components/TabViewDataLoader.svelte";
    import ConclusionEditingDialog from "./ConclusionEditingDialog.svelte";

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
    async function openConclusionDeletingDialog() {
        const conclusionDeletingAction: () => Promise<Err> = async () => {
            const url = "/api/testCreation/deleteDraftTestConclusion/" + testId;
            const response = await fetch(url, {
                method: "DELETE",
            });
            if (response.ok) {
                await loadData();
                conclusionDeletingDialog.close();
                return Err.none();
            } else {
                const errorMessage = await getErrorFromResponse(response);
                return new Err(errorMessage);
            }
        };
        conclusionDeletingDialog.open(
            conclusionDeletingAction,
            "Do you really want to remove test conclusion?",
        );
    }
    let dataFetchingErr: string = "";
    let conclusionEditingDialog: ConclusionEditingDialog;
    let conclusionDeletingDialog: ActionConfirmationDialog;
</script>

<TabViewDataLoader {loadData} isEmpty={() => conclusionData.isEmpty()}>
    <div slot="empty" class="no-conclusion-div">
        <h2>It seems like you haven't added conclusion yet</h2>
        <button class="add-conclusion-btn" on:click={createConclusion}>
            Add conclusion
        </button>
        <p class="error-message">{dataFetchingErr}</p>
    </div>
    <div slot="content" class="conclusion-data">
        <ConclusionEditingDialog
            bind:this={conclusionEditingDialog}
            updateParentElementData={loadData}
        />
        <ActionConfirmationDialog bind:this={conclusionDeletingDialog} />
        <div class="content-header">
            <TabHeaderWithButton tabName="Test Conclusion:" />
            <div class="header-btns">
                <button
                    class="edit-btn"
                    on:click={() =>
                        conclusionEditingDialog.open(conclusionData)}
                >
                    Edit Conclusion
                </button>
                <button
                    class="remove-btn"
                    on:click={openConclusionDeletingDialog}
                >
                    Remove
                </button>
            </div>
        </div>
        <p class="prop-name-val-p">
            <span class="property-name">Conclusion text:</span>
            <span class="property-value">{conclusionData.text}</span>
        </p>
        {#if !StringUtils.isNullOrWhiteSpace(conclusionData.additionalImage)}
            <p class="prop-name-val-p">
                <span class="property-name">Conclusion Image:</span>
            </p>
            <img
                class="conclusion-img"
                src={ImgUtils.imgUrlWithVersion(
                    conclusionData.additionalImage ?? "",
                )}
            />
        {:else}
            <p class="prop-name-val-p">
                <span class="property-name">Conclusion Image:</span>
                <span class="property-value">(Not added)</span>
            </p>
        {/if}
        {#if conclusionData.anyFeedback}
            <p class="feedback-p">Feedback:</p>
            <p class="prop-name-val-p">
                <span class="property-name">Feedback accompanying text:</span>
                <span class="property-value"
                    >{conclusionData.feedbackText}
                </span>
            </p>
            <p class="prop-name-val-p">
                <span class="property-name">Max feedback length:</span>
                <span class="property-value">
                    {conclusionData.maxFeedbackLength}
                </span>
            </p>
        {:else}
            <p class="prop-name-val-p">
                <span class="property-name">Feedback:</span>
                <span class="property-value"
                    >Conclusion does not imply any feedback</span
                >
            </p>
        {/if}
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
    .conclusion-data {
        padding: 4px 12px;
    }
    .content-header {
        display: flex;
        justify-content: space-between;
        align-items: center;
    }
    .header-btns {
        margin-left: 10px;
        display: flex;
        justify-content: center;
        align-items: center;
        flex-direction: row;
        gap: 8px;
    }

    .header-btns button {
        padding: 8px 16px;
        color: var(--back-main);
        font-size: 20px;
        text-align: center;
        cursor: pointer;
        border-radius: 4px;
        border: none;
        outline: none;
        transition: all 0.12s ease-in;
    }

    .edit-btn {
        background-color: var(--primary);
    }

    .edit-btn:hover {
        background-color: var(--primary-hov);
    }

    .remove-btn {
        background-color: var(--text-faded);
    }

    .remove-btn:hover {
        background-color: var(--red-del);
    }

    .prop-name-val-p {
        gap: 20px;
    }
    .prop-name-val-p .property-name {
        font-size: 16px;
        font-weight: 500;
        color: var(--text-faded);
    }
    .prop-name-val-p .property-value {
        font-size: 18px;
        color: var(--text);
    }
    .feedback-p {
        margin-left: 10px;
        font-size: 20px;
    }
    .conclusion-img {
        max-width: min(60vw, 1200px);
        max-height: 400px;
        width: auto;
        height: auto;
        border-radius: 12px;
        object-fit: contain;
        margin-left: 20px;
    }
</style>
