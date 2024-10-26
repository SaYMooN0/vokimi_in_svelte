<script lang="ts">
    import BasicTextAreaInput from "../../../../../../../../components/shared/BasicTextAreaInput.svelte";
    import { ImgUtils } from "../../../../../../../../ts/utils/ImgUtils";
    import { StringUtils } from "../../../../../../../../ts/utils/StringUtils";
    import { getErrorFromResponse } from "../../../../../../../../ts/ErrorResponse";
    import type { DraftGeneralTestTextAndImageAnswerFormData } from "../../../../../../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/general_test_creation/questions/answers/DraftGeneralTestTextAndImageAnswerFormData";
    export let answerData: DraftGeneralTestTextAndImageAnswerFormData;
    export let questionId: string;
    let id: string = StringUtils.randomString(8);
    let errorString: string = "";
    async function uploadFile(file: File) {
        const formData = new FormData();
        formData.append("file", file);
        try {
            const response = await fetch(
                `/api/saveimg/saveDraftGeneralTestAnswerImage/${questionId}`,
                {
                    method: "POST",
                    body: formData,
                },
            );

            if (response.ok) {
                const data = await response.json();
                answerData.image = data.imgPath;
                errorString = "";
            } else if (response.status === 400) {
                errorString = await getErrorFromResponse(response);
            } else {
            }
        } catch (error) {
            errorString = "Failed to upload the file";
        }
    }

    function handleImageInputChange(event: Event) {
        const input = event.target as HTMLInputElement;

        if (input.files && input.files.length > 0) {
            uploadFile(input.files[0]);
        }
    }
</script>

<div class="answer-main-content">
    <BasicTextAreaInput
        bind:text={answerData.text}
        placeholder="Type here text of the answer..."
    />
    <div class="image-input-container">
        {#if !StringUtils.isNullOrWhiteSpace(answerData.image)}
            <img src={ImgUtils.imgUrlWithVersion(answerData.image)} />
        {/if}
        <input
            type="file"
            {id}
            accept=".jpg,.png,.webp"
            hidden
            on:change={handleImageInputChange}
        />

        <label class="error-string">{errorString}</label>
        <label for={id} class="change-img-btn">
            {StringUtils.isNullOrWhiteSpace(answerData.image)
                ? "Add image"
                : "Change"}
        </label>
    </div>
</div>

<style>
    .answer-main-content {
        width: 100%;
        display: grid;
        grid-template-columns: 1fr 300px;
        gap: 8px;
        box-sizing: border-box;
        padding: 8px;
    }
    .answer-main-content > :global(textarea) {
        min-height: 100%;
        max-height: 400px;
        resize: vertical;
    }
    .image-input-container {
        display: flex;
        gap: 4px;
        flex-direction: column;
        align-items: center;
        justify-content: center;
    }
    .image-input-container img {
        max-height: 420px;
        border-radius: 4px;
        height: auto;
        width: 100%;
    }
    .change-img-btn {
        align-self: center;
        width: 92%;
        padding: 6px;
        box-sizing: border-box;
        background-color: var(--primary);
        border-radius: 8px;
        font-family: Roboto;
        font-size: 20px;
        color: white;
        text-align: center;
        cursor: pointer;
        transition: all 0.12s ease-in-out;
    }

    .change-img-btn:hover {
        width: 100%;
        background-color: var(--primary-hov);
    }

    .change-img-btn:active {
        letter-spacing: 1px;
    }
</style>
