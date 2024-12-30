<script lang="ts">
    import BaseDialog from "../../../../components/BaseDialog.svelte";
    import CloseButton from "../../../../components/shared/CloseButton.svelte";
    import CustomSwitch from "../../../../components/shared/CustomSwitch.svelte";
    import TextWithOptionalImageInput from "../../../../components/shared/TextWithOptionalImageInput.svelte";
    import type { Err } from "../../../../ts/Err";
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";
    import { StringUtils } from "../../../../ts/utils/StringUtils";

    export let testId: string;

    export let conclusionText: string;
    export let conclusionImage: string | null;
    export let anyFeedback: boolean;

    export let updateParentElement: (
        conclusionText: string,
        conclusionImage: string | null,
        anyFeedback: boolean,
    ) => void;

    export function open() {
        dialogElement.open();
        errorMessage = "";
    }
    let errorMessage: string = "";
    let dialogElement: BaseDialog;
    async function saveConclusionImage(file: File): Promise<string | Err> {
        return await ImgUtils.saveImage(
            file,
            `addTestConclusionImage/${testId}`,
        );
    }
    async function saveData() {
        const bodyData = {
            testId,
            conclusionText,
            conclusionImage,
            anyFeedback,
        };
        const url = "/api/manageTest/conclusion/saveConclusionChanges";
        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(bodyData),
        });
        if (response.ok) {
            const data = await response.json();
            updateParentElement(
                data.conclusionText,
                data.conclusionImage,
                data.anyFeedback,
            );
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
        <TextWithOptionalImageInput
            bind:text={conclusionText}
            bind:imagePath={conclusionImage}
            textInputLabel="Conclusion Text"
            saveImageFunction={saveConclusionImage}
        />
        <p class="allow-feedback-p">
            Allow feedback for this test:
            <CustomSwitch bind:isChecked={anyFeedback} />
        </p>
        {#if !StringUtils.isNullOrWhiteSpace(errorMessage)}
            <p class="err-msg">{errorMessage}</p>
        {/if}
        <button class="save-btn" on:click={saveData}>Save</button>
    </div>
</BaseDialog>

<style>
</style>
