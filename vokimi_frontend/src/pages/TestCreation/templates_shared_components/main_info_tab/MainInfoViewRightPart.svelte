<script lang="ts">
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";

    export let imgPath: string;
    export let draftTestId: string;
    let errorString: string = "";
    async function handleImageInputChange(event: Event) {
        const input = event.target as HTMLInputElement;

        if (input.files && input.files.length > 0) {
            await updateCover(input.files[0]);
        }
    }
    async function updateCover(file: File) {
        const formData = new FormData();
        formData.append("file", file);

        const response = await fetch(
            `/api/testCreation/updateDraftTestQuestionCover/${draftTestId}`,
            {
                method: "POST",
                body: formData,
            },
        );

        const { imgPath: newImgPath, errorString: newErrorString } =
            await handleResponse(response);
        imgPath = newImgPath || imgPath;
        errorString = newErrorString || "";
    }

    async function changeImageToDefault() {
        const response = await fetch(
            `/api/testCreation/setDraftTestCoverToDefault/${draftTestId}`,
            {
                method: "POST",
            },
        );

        const { imgPath: newImgPath, errorString: newErrorString } =
            await handleResponse(response);
        imgPath = newImgPath || imgPath;
        errorString = newErrorString || "";
    }
    async function handleResponse(
        response: Response,
    ): Promise<{ imgPath?: string; errorString?: string }> {
        try {
            if (response.ok) {
                const data = await response.json();
                return { imgPath: data.imgPath, errorString: "" };
            } else if (response.status === 400) {
                const errorString = await getErrorFromResponse(response);
                return { errorString };
            } else {
                return { errorString: "Unexpected server error" };
            }
        } catch (error) {
            return { errorString: "Failed to process the request" };
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
    <button
        class="img-operations-btn remove-img-btn"
        on:click={changeImageToDefault}
    >
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
