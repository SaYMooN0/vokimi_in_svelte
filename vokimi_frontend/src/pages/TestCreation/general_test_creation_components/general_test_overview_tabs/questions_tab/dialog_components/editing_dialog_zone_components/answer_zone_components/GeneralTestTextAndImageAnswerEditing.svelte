<script lang="ts">
    import BasicTextAreaInput from "../../../../../../../../components/shared/BasicTextAreaInput.svelte";
    import { ImgUtils } from "../../../../../../../../ts/utils/ImgUtils";
    import type { DraftGeneralTestTextAndImageAnswerFormData } from "../../../../../../../../ts/test_creation_tabs_classes/general_test_creation/draft_general_test_questions/answers/DraftGeneralTestTextAndImageAnswerFormData";
    import { StringUtils } from "../../../../../../../../ts/utils/StringUtils";
    export let answerData: DraftGeneralTestTextAndImageAnswerFormData;
    export let questionId: string;
    let id: string = StringUtils.randomString(8);
    let errorString: string = "";
    async function uploadFile(file: File) {
        const formData = new FormData();
        formData.append("file", file);
        const fileKey: string = `test_answers/${questionId}/${StringUtils.randomString(16)}`;
        try {
            const response = await fetch(ImgUtils.saveImgApiUrl(fileKey), {
                method: "POST",
                body: formData,
            });

            if (!response.ok) {
                errorString = "Server error. Please try again later";
            }
        } catch (error) {
            errorString = "Failed to upload the file";
        }
    }

    function handleImageInputChange(event: Event) {
        const input = event.target as HTMLInputElement;

        if (input.files && input.files.length > 0) {
            let selectedFile: File = input.files[0];
            uploadFile(selectedFile);
        }
    }
</script>

<div class="answer-main-content">
    <BasicTextAreaInput
        bind:text={answerData.text}
        placeholder="Type here text of the answer..."
    />
    <div class="image-input-container">
        {#if !StringUtils.isNullOrWhiteSpace(answerData.imagePath)}
            <img src={ImgUtils.imgUrlWithVersion(answerData.imagePath)} />
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
            {StringUtils.isNullOrWhiteSpace(answerData.imagePath)
                ? "Add image"
                : "Change"}
        </label>
    </div>
</div>

<style>
    .image-input-container {
        display: flex;
        gap: 4px;
        flex-direction: column;
        align-items: center;
        justify-content: center;
    }
    .image-input-container img {
        max-height: 420px;
        height: auto;
        width: 100%;
    }
    .change-img-btn {
        margin-bottom: auto;
        width: 100%;
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
        background-color: var(--primary-hov);
        letter-spacing: 2px;
    }

    .change-img-btn:active {
        width: 96%;
    }
</style>
