<script lang="ts">
    import type { ManageTestConclusionTabData } from "../../../../ts/page_classes/manage_test_page/conclusion/ManageTestConclusionTabData";
    import ConclusionDataEditingDialog from "./ConclusionDataEditingDialog.svelte";
    import ConclusionDataSection from "./ConclusionDataSection.svelte";
    import FeedbackRecordsSection from "./FeedbackRecordsSection.svelte";
    import FeedbackSettingsEditingDialog from "./FeedbackSettingsEditingDialog.svelte";
    import FeedbackSettingsSection from "./FeedbackSettingsSection.svelte";

    export let testId: string;
    export let conclusionData: ManageTestConclusionTabData;
</script>

<div class="conclusion-container">
    <ConclusionDataSection
        conclusionText={conclusionData.conclusionText}
        conclusionImage={conclusionData.conclusionImage}
        anyFeedback={conclusionData.anyFeedback}
    />
    <ConclusionDataEditingDialog
        {testId}
        conclusionText={conclusionData.conclusionText}
        conclusionImage={conclusionData.conclusionImage}
        anyFeedback={conclusionData.anyFeedback}
        updateParentElement={(text, image, allowFeedback) => {
            conclusionData.conclusionText = text;
            conclusionData.conclusionImage = image;
            conclusionData.anyFeedback = allowFeedback;
        }}
    />
    {#if conclusionData.anyFeedback}
        {#if conclusionData.feedbackData === null}
            <p class="no-feedback-p">Unable to fetch feedback data</p>
        {:else}
            <FeedbackSettingsSection
                feedbackAccompanyingText={conclusionData.feedbackData
                    .accompanyingText}
                maxLength={conclusionData.feedbackData.maxLength}
            />
            <FeedbackRecordsSection {testId} />
            <FeedbackSettingsEditingDialog
                feedbackAccompanyingText={conclusionData.feedbackData
                    .accompanyingText}
                maxLength={conclusionData.feedbackData.maxLength}
                {testId}
                updateParentElement={(accompanyingText, maxLength) => {
                    if (conclusionData.feedbackData === null) {
                        return;
                    }
                    conclusionData.feedbackData.accompanyingText =
                        accompanyingText;
                    conclusionData.feedbackData.maxLength = maxLength;
                }}
            />
        {/if}
    {/if}
</div>

<style>
    .conclusion-container {
        width: 100%;
        display: flex;
        flex-direction: column;
    }
    .conclusion-container :global(.section-subheader) {
        font-size: 24px;
        font-weight: 600;
        margin-top: 24px;
        margin-bottom: 12px;
    }
    .conclusion-container :global(.prop-name-val-p) {
        margin: 12px 0;
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 16px;
        font-size: 20px;
    }
    .conclusion-container :global(.prop-name) {
    }
    .conclusion-container :global(.prop-val) {
    }
</style>
