<script lang="ts">
    import BaseDialog from "../../../../components/BaseDialog.svelte";
    import CloseButton from "../../../../components/shared/CloseButton.svelte";
    import { StringUtils } from "../../../../ts/utils/StringUtils";

    export let testId: string;
    export let feedbackAccompanyingText: string;
    export let maxLength: number;

    export let updateParentElement: (
        feedbackAccompanyingText: string,
        maxLength: number,
    ) => void;
    export function open() {
        dialogElement.open();
        errorMessage = "";
    }

    let errorMessage: string = "";
    let dialogElement: BaseDialog;

    async function saveData() {
        const bodyData = {
            testId,
            feedbackAccompanyingText,
            maxLength,
        };
        const url = "/api/manageTest/conclusion/saveFeedbackChanges";
        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(bodyData),
        });
        if (response.ok) {
            const data = await response.json();
            updateParentElement(data.feedbackAccompanyingText, data.maxLength);
            errorMessage = "";
            dialogElement.close();
        } else if (response.status === 400) {
            const data = await response.json();
            dialogElement.setErrorMessage(data.error);
        } else {
            dialogElement.setErrorMessage("Unknown error");
        }
    }
</script>

<BaseDialog bind:this={dialogElement} dialogId="conclusionDataEditingDialog">
    <div class="dialog-content">
        <CloseButton onClose={() => dialogElement.close()} />
        <label for="feedbackAccompanyingText">
            Feedback Accompanying Text:
        </label>
        <textarea
            id="feedbackAccompanyingText"
            bind:value={feedbackAccompanyingText}
        />
        <label for="maxLength">
            <span>Feedback Max Length:</span>
            <input type="number" id="maxLength" bind:value={maxLength} />
        </label>
        {#if !StringUtils.isNullOrWhiteSpace(errorMessage)}
            <p class="err-msg">{errorMessage}</p>
        {/if}
        <button class="save-btn" on:click={saveData}>Save</button>
    </div>
</BaseDialog>

<style>
</style>
