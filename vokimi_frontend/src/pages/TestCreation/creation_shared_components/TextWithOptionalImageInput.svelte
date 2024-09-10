<script lang="ts">
    import BasicTextAreaInput from "../../../components/shared/BasicTextAreaInput.svelte";
    import { ImgUtils } from "../../../ts/utils/ImgUtils";

    export let text: string = "";
    export let textInputLabel: string = "Text:";
    export let image: string | null = null;
    export let saveImageFunction: (image: string) => Promise<string | null>;

    let imageUploadingErr: string = "";
    function anyImageAdded(): boolean {
        return image !== null;
    }

    function saveImage() {
        if (image === null) {
            imageUploadingErr = "Please choose an image first";
            return;
        }
        saveImageFunction(image).then((imgSavingErr: string | null) => {
            if (imgSavingErr !== null) {
                imageUploadingErr = imgSavingErr;
                return;
            }
        });
    }
    function removeImage() {}
</script>

<div class:horizontal={anyImageAdded()} class:vertical={!anyImageAdded()}>
    <div class="text-input-part">
        <label class="text-input-label" for="text-input-textarea">
            {textInputLabel}
        </label>
        <BasicTextAreaInput bind:text id="text-input-textarea" />
    </div>
    <input type="file" id="img-input" accept=".jpg,.png,.webp" hidden />
    {#if anyImageAdded()}
        <div class="image-editing-part">
            <div class="img-display">
                <img
                    src={ImgUtils.imgUrlWithVersion(
                        image === null ? "" : image,
                    )}
                    alt="Image"
                />
            </div>
            <label class="img-uploading-error">{imageUploadingErr}</label>
            <div class="img-editing-btns">
                <label for="img-input" class="change-btn">Change</label>
                <label class="remove-btn" on:click={removeImage}>
                    Remove
                </label>
            </div>
        </div>
    {:else}
        <label for="img-input" class="add-img-btn">
            Add Image
            <svg viewBox="0 0 24 24" fill="none">
                <path
                    d="M22 6.75C22.4142 6.75 22.75 6.41421 22.75 6C22.75 5.58579 22.4142 5.25 22 5.25V6.75ZM14 5.25C13.5858 5.25 13.25 5.58579 13.25 6C13.25 6.41421 13.5858 6.75 14 6.75V5.25ZM18.75 2C18.75 1.58579 18.4142 1.25 18 1.25C17.5858 1.25 17.25 1.58579 17.25 2H18.75ZM17.25 10C17.25 10.4142 17.5858 10.75 18 10.75C18.4142 10.75 18.75 10.4142 18.75 10H17.25ZM22 5.25H18V6.75H22V5.25ZM18 5.25H14V6.75H18V5.25ZM17.25 2V6H18.75V2H17.25ZM17.25 6V10H18.75V6H17.25Z"
                    fill="currentColor"
                />
                <path
                    d="M11.5 3C7.02166 3 4.78249 3 3.39124 4.39124C2 5.78249 2 8.02166 2 12.5C2 16.9783 2 19.2175 3.39124 20.6088C4.78249 22 7.02166 22 11.5 22C15.9783 22 18.2175 22 19.6088 20.6088C21 19.2175 21 16.9783 21 12.5V12"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                />
                <path
                    d="M2 14.1354C2.61902 14.0455 3.24484 14.0011 3.87171 14.0027C6.52365 13.9466 9.11064 14.7729 11.1711 16.3342C13.082 17.7821 14.4247 19.7749 15 22"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linejoin="round"
                />
                <path
                    d="M21 16.8962C19.8246 16.3009 18.6088 15.9988 17.3862 16.0001C15.5345 15.9928 13.7015 16.6733 12 18"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linejoin="round"
                />
            </svg>
        </label>
    {/if}
</div>

<style>
    .horizontal {
        display: grid;
        grid-template-columns: 1fr auto;
        gap: 10px;
    }

    .vertical {
        display: flex;
        flex-direction: column;
    }

    .text-input-part {
        width: 100%;
    }

    .text-input-label {
        margin-top: 10px;
        margin-bottom: 2px;
        font-size: 24px;
        color: var(--text);
    }

    .text-input-part > :global(textarea) {
        box-sizing: border-box;
        resize: vertical;
        width: 100%;
        min-height: 70px;
        max-height: 240px;
    }
    .horizontal .text-input-part > :global(textarea) {
        max-height: 480px;
        height: 480px;
    }
    .add-img-btn {
        margin-top: 12px;
        align-self: end;
        width: fit-content;
        padding: 8px 12px;
        border-radius: 7px;
        background-color: var(--primary);
        display: flex;
        justify-content: center;
        align-items: center;
        gap: 6px;
        color: var(--back-main);
        font-size: 20px;
        cursor: pointer;
    }

    .add-img-btn svg {
        color: inherit;
        height: 32px;
    }

    .add-img-btn:hover {
        background-color: var(--primary-hov);
    }
</style>
