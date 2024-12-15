<script lang="ts">
    import CustomCheckbox from "../../../../components/shared/CustomCheckbox.svelte";
    import CustomSwitch from "../../../../components/shared/CustomSwitch.svelte";
    import type { Err } from "../../../../ts/Err";
    import type { TestCreationConclusionTabData } from "../../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationConclusionTabData";
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";
    import BaseDraftTestEditingDialog from "../../creation_shared_components/editing_dialog_components/BaseDraftTestEditingDialog.svelte";
    import TextWithOptionalImageInput from "../../creation_shared_components/TextWithOptionalImageInput.svelte";

    export let updateParentElementData: () => void;
    export let testId: string;

    let conclusionData: TestCreationConclusionTabData;
    let dialogElement: BaseDraftTestEditingDialog;

    export function open(conclusion: TestCreationConclusionTabData) {
        conclusionData = conclusion.copy();
        dialogElement.setErrorMessage("");
        dialogElement.open();
    }

    async function saveData() {
        const response = await fetch(
            "/api/testCreation/updateDraftTestConclusion/" + testId,
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(conclusionData),
            },
        );
        if (response.ok) {
            await updateParentElementData();
            dialogElement.close();
        } else if (response.status === 400) {
            const data = await response.json();
            dialogElement.setErrorMessage(data.error);
        } else {
            dialogElement.setErrorMessage("Unknown error");
        }
    }
    async function saveConclusionImage(file: File): Promise<string | Err> {
        return await ImgUtils.saveImage(
            file,
            `saveDraftTestConclusionImage/${testId}`,
        );
    }
</script>

<BaseDraftTestEditingDialog
    onSaveButtonClicked={saveData}
    bind:this={dialogElement}
>
    <div class="dialog-content">
        <TextWithOptionalImageInput
            bind:text={conclusionData.text}
            bind:imagePath={conclusionData.additionalImage}
            textInputLabel="Conclusion Text"
            saveImageFunction={saveConclusionImage}
        />

        <div class="manage-feedback-zone">
            <div class="allow-feedback-div">
                <span class="allow-feedback-span">Allow feedback</span>
                <CustomSwitch bind:isChecked={conclusionData.anyFeedback} />
            </div>

            <div
                class="feedback-added-only"
                class:showFeedback={conclusionData.anyFeedback}
                class:hideFeedback={!conclusionData.anyFeedback}
            >
                <label class="feedback-text-input-label">
                    Accompanying text:
                    <input
                        type="text"
                        bind:value={conclusionData.feedbackText}
                    />
                </label>
                <label class="feedback-chars-count-label">
                    Maximum number of characters in feedback:
                    <input
                        type="number"
                        bind:value={conclusionData.maxFeedbackLength}
                    />
                </label>
            </div>
        </div>
    </div>
</BaseDraftTestEditingDialog>

<style>
    .dialog-content {
        width: 1200px;
        max-height: 76vh;
        box-sizing: border-box;
        overflow-y: auto;
        overflow-x: hidden;
    }
    .dialog-content :global(.input-label) {
        gap: 8px;
        font-size: 22px;
        display: flex;
        flex-direction: row;
        justify-content: left;
        align-items: center;
    }

    .manage-feedback-zone {
        display: flex;
        flex-direction: column;
        font-size: 20px;
    }

    .allow-feedback-div {
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 8px;
        background-color: var(--back-main);
        padding: 4px 12px;
        font-size: 14px;
    }
    .allow-feedback-span {
        font-size: 22px;
    }

    .feedback-added-only {
        margin-left: 10px;
        padding-left: 10px;
        display: flex;
        flex-direction: column;
    }

    .feedback-added-only label {
        margin-top: 10px;
    }

    .feedback-added-only input {
        padding: 2px 6px;
        box-sizing: border-box;
        outline: none;
        border: 2px solid transparent;
        border-radius: 4px;
        background-color: var(--back-secondary);
        font-size: 20px;
    }

    .feedback-added-only input:focus {
        border-color: var(--primary);
    }

    .feedback-text-input-label input {
        width: 100%;
    }

    .feedback-chars-count-label input {
        width: 80px;
        text-align: center;
        margin-left: 10px;
    }

    .showFeedback {
        animation: slideIn 0.8s forwards;
    }

    .hideFeedback {
        display: none !important;
    }
    @keyframes slideIn {
        from {
            transform: translateY(-16%);
            opacity: 0.2;
        }

        to {
            transform: translateY(0);
            opacity: 1;
        }
    }
</style>
