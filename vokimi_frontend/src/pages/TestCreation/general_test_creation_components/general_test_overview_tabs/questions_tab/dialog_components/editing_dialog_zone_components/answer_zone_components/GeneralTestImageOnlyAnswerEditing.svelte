<script lang="ts">
    import { getErrorFromResponse } from "../../../../../../../../ts/ErrorResponse";
    import type { DraftGeneralTestImageOnlyAnswerFormData } from "../../../../../../../../ts/my_tests_page/test_creation_tabs_classes/general_test_creation/questions/answers/DraftGeneralTestImageOnlyAnswerFormData";
    import { ImgUtils } from "../../../../../../../../ts/utils/ImgUtils";
    import { StringUtils } from "../../../../../../../../ts/utils/StringUtils";

    export let answerData: DraftGeneralTestImageOnlyAnswerFormData;
    export let questionId: string;
    let errorString: string = "";
    let id: string = StringUtils.randomString(8);

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
    {#if !StringUtils.isNullOrWhiteSpace(answerData.image)}
        <label for={id} class="change-btn unselectable">Change Image</label>
    {:else}
        <label for={id} class="add-img-btn">
            Add Image
            <svg viewBox="0 0 512 512">
                <path
                    d="M448 80c8.8 0 16 7.2 16 16V415.8l-5-6.5-136-176c-4.5-5.9-11.6-9.3-19-9.3s-14.4 3.4-19 9.3L202 340.7l-30.5-42.7C167 291.7 159.8 288 152 288s-15 3.7-19.5 10.1l-80 112L48 416.3l0-.3V96c0-8.8 7.2-16 16-16H448zM64 32C28.7 32 0 60.7 0 96V416c0 35.3 28.7 64 64 64H448c35.3 0 64-28.7 64-64V96c0-35.3-28.7-64-64-64H64zm80 192a48 48 0 1 0 0-96 48 48 0 1 0 0 96z"
                />
            </svg>
        </label>
    {/if}
    <label class="error-string">{errorString}</label>
</div>

<style>
    .answer-main-content {
        display: grid;
        grid-template-columns: 1fr auto;
        justify-content: center;
        align-items: center;
        width: 100%;
        gap: 30px;
        padding: 4px 80px;
        box-sizing: border-box;
    }

    .answer-main-content img {
        justify-self: center;
        object-fit: contain;
        max-height: 240px;
        border-radius: 8px;
    }
    .add-img-btn {
        justify-self: center;
        width: fit-content;
        height: 110px;
        padding: 2px 20px;
        box-sizing: border-box;
        display: grid;
        grid-template-rows: auto 70px;
        align-items: center;
        border: 4px solid var(--primary);
        border-radius: 8px;
        font-size: 20px;
        font-weight: 500;
        color: var(--primary);
        cursor: pointer;
        transition: all 0.16s;
    }

    .add-img-btn svg {
        justify-self: center;
        fill: var(--primary);
        height: 80%;
        transition: all 0.13s;
    }

    .add-img-btn:hover {
        padding: 1px 30px;
        color: var(--primary-hov);
        border-color: var(--primary-hov);
        font-size: 23px;
    }

    .add-img-btn:hover svg {
        height: 100%;
        fill: var(--primary-hov);
    }

    .add-img-btn:active {
        transform: scale(0.96);
    }

    .change-btn {
        padding: 8px 24px;
        background-color: var(--primary);
        border-radius: 8px;
        font-size: 20px;
        color: var(--back-main);
        cursor: pointer;
        transition: all 0.16s;
    }

    .change-btn:hover {
        background-color: var(--primary-hov);
    }
</style>
