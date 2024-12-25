<script lang="ts">
    import AuthorizeView from "../../../../components/AuthorizeView.svelte";
    import CustomCheckbox from "../../../../components/shared/CustomCheckbox.svelte";
    import type { TestTakingConclusionData } from "../../../../ts/page_classes/test_taking_page/ITestTakingData";
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";
    import { StringUtils } from "../../../../ts/utils/StringUtils";

    export let conclusionData: TestTakingConclusionData;
    export function getFeedback(): {
        feedback: string;
        anonymous: boolean;
    } | null {
        if (!conclusionData.anyFeedback) {
            return null;
        }
        return { feedback: feedback, anonymous: anonymousFeedback };
    }
    let feedback: string = "";
    let anonymousFeedback: boolean = false;
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
        <AuthorizeView>
            <svelte:fragment slot="authenticated">
                <p class="anonymous-feedback-p">
                    Leave feedback anonymously
                    <CustomCheckbox bind:isChecked={anonymousFeedback} />
                </p>
            </svelte:fragment>
        </AuthorizeView>
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
        height: 140px;
        border: 2px solid var(--back-secondary);
        background-color: var(--back-secondary);
        box-sizing: border-box;
        padding: 8px;
        border-radius: 8px;
        outline: none;
        resize: none;
        font-size: 18px;
        color: var(--text);
        margin-bottom: 20px;
    }
    .feedback-textarea:focus {
        border-color: var(--test-accent);
    }
    .anonymous-feedback-p {
        width: 100%;
        display: flex;
        justify-content: center;
        align-items: center;
    }
</style>
