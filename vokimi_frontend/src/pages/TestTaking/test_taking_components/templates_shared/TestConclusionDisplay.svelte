<script lang="ts">
    import type { TestTakingConclusionData } from "../../../../ts/page_classes/test_taking_page/ITestTakingData";
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";
    import { StringUtils } from "../../../../ts/utils/StringUtils";

    export let conclusionData: TestTakingConclusionData;
    export function getFeedback(): string | null {
        if (!conclusionData.anyFeedback) {
            return null;
        }
        return feedback;
    }
    let feedback: string = "";
</script>

<div class="conclusion-display-container">
    <p class="conclusion-text">{conclusionData.conclusionText}</p>
    {#if !StringUtils.isNullOrWhiteSpace(conclusionData.additionalImage)}
        <img
            class="conclusion-image"
            src={ImgUtils.imgUrl(conclusionData.additionalImage ?? "")}
            alt="conclusion-img"
        />
    {/if}
    {#if conclusionData.anyFeedback}
        {#if !StringUtils.isNullOrWhiteSpace(conclusionData.feedbackAccompanyingText)}
            <p class="feedback-accompanying-p">
                {conclusionData.feedbackAccompanyingText}
            </p>
        {/if}
        <textarea
            bind:value={feedback}
            class="feedback-textarea"
            maxlength={conclusionData.maxFeedbackLength}
        />
    {/if}
</div>

<style>
    .conclusion-display-container {
        display: flex;
        flex-direction: column;
        align-items: center;
    }
    .conclusion-text {
        font-size: 24px;
        font-weight: 500;
        color: var(--text);
        margin: 0;
    }
    .conclusion-image {
        margin: 8px 0;
        max-height: 400px;
        max-width: 640px;
        object-fit: contain;
        border-radius: 8px;
    }
    .feedback-accompanying-p {
        font-size: 20px;
        font-weight: 500;
        color: var(--text);
        margin: 8px 0;
    }
    .feedback-textarea {
        width: 100%;
        height: 120px;
        border: 2px solid var(--back-secondary);
        background-color: var(--back-secondary);
        box-sizing: border-box;
        padding: 8px;
        border-radius: 8px;
        outline: none;
        resize: none;
        font-size: 18px;
        color: var(--text);
    }
    .feedback-textarea:focus {
        border-color: var(--test-accent);
    }
</style>
