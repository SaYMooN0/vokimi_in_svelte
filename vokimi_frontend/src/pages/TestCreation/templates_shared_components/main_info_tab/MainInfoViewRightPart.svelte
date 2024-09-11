<script lang="ts">
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";

    export let imgPath: string;
    export let draftTestId: string;
    let errorString: string = "";
    async function uploadFile(file: File) {
        const formData = new FormData();
        formData.append("file", file);
        try {
            const response = await fetch(
                `/api/testCreation/updateDraftTestQuestionCover/${draftTestId}`,
                {
                    method: "POST",
                    body: formData,
                },
            );

            if (response.ok) {
                const data = await response.json();
                imgPath = data.imgPath;
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

<div class="img-container">
    <img src={ImgUtils.imgUrlWithVersion(imgPath)} alt="test cover" />
    <label for="test-cover-input" class="img-operations-btn change-img-btn">
        Change Test Cover
    </label>
    <input
        type="file"
        id="test-cover-input"
        accept=".jpg,.png,.webp"
        hidden
        on:change={handleImageInputChange}
    />
    <button class="img-operations-btn remove-img-btn">
        Set Cover To Default
    </button>

    <label class="error-string">{errorString}</label>
</div>

<style>
    .img-container {
        width: 600px;
        display: flex;
        flex-direction: column;
        align-items: center;
        gap: 4px;
    }
    .img-container img {
        border-radius: 14px;
        width: 100%;
        height: 100%;
        object-fit: contain;
    }
    .change-img-btn,
    .remove-img-btn {
        height: 36px;
        width: 82%;
        display: flex;
        justify-content: center;
        align-items: center;
        border-radius: 16px;
        border: none;
        color: white;
        font-size: 20px;
        cursor: pointer;
        transition: all 0.12s ease-in;
    }
    .change-img-btn {
        margin-top: 8px;
        background-color: var(--primary);
    }
    .remove-img-btn {
        background-color: var(--red-del);
    }
    .change-img-btn:hover,
    .remove-img-btn:hover {
        width: 86%;
    }
</style>
