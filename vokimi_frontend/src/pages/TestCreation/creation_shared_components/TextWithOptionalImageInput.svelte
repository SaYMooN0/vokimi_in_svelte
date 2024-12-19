<script lang="ts">
    import BasicTextAreaInput from "../../../components/shared/BasicTextAreaInput.svelte";
    import { Err } from "../../../ts/Err";
    import { ImgUtils } from "../../../ts/utils/ImgUtils";
    import { StringUtils } from "../../../ts/utils/StringUtils";

    export let text: string = "";
    export let textInputLabel: string = "Text:";
    export let imagePath: string | null = null;
    export let saveImageFunction: (file: File) => Promise<string | Err>;

    let imageUploadingErr: string = "";

    async function saveImage(event: Event) {
        const input = event.target as HTMLInputElement;
        let file: File | null = null;
        if (input.files && input.files.length > 0) {
            file = input.files[0];
        }

        if (file === null) {
            imageUploadingErr = "Please choose an image first";
            return;
        }
        const saveImageData: string | Err = await saveImageFunction(file);

        if (typeof saveImageData === "string") {
            imagePath = saveImageData;
        } else {
            imageUploadingErr = saveImageData.toString();
        }
    }
    function removeImage() {
        imagePath = null;
    }
</script>

<div
    class:horizontal={!StringUtils.isNullOrWhiteSpace(imagePath)}
    class:vertical={StringUtils.isNullOrWhiteSpace(imagePath)}
>
    <div class="text-input-part">
        <label class="text-input-label" for="text-input-textarea">
            {textInputLabel}
        </label>
        <BasicTextAreaInput bind:text id="text-input-textarea" />
    </div>
    <input
        type="file"
        id="img-input"
        accept=".jpg,.png,.webp"
        hidden
        on:change={saveImage}
    />
    {#if !StringUtils.isNullOrWhiteSpace(imagePath)}
        <div class="image-editing-part">
            <img
                src={ImgUtils.imgUrlWithVersion(
                    imagePath === null ? "" : imagePath,
                )}
                alt="Image"
            />
            <div class="img-editing-btns">
                <label for="img-input" class="img-change-btn">
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        viewBox="0 0 24 24"
                        fill="none"
                    >
                        <path
                            d="M16.9767 19.5C19.4017 17.8876 21 15.1305 21 12C21 7.02944 16.9706 3 12 3C11.3126 3 10.6432 3.07706 10 3.22302M16.9767 19.5V16M16.9767 19.5H20.5M7 4.51555C4.58803 6.13007 3 8.87958 3 12C3 16.9706 7.02944 21 12 21C12.6874 21 13.3568 20.9229 14 20.777M7 4.51555V8M7 4.51555H3.5"
                            stroke="currentColor"
                            stroke-width="1.5"
                            stroke-linecap="round"
                            stroke-linejoin="round"
                        />
                    </svg>
                    Change
                </label>
                <label class="img-remove-btn" on:click={removeImage}>
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        viewBox="0 0 24 24"
                        fill="none"
                    >
                        <path
                            d="M19.0005 4.99988L5.00049 18.9999M5.00049 4.99988L19.0005 18.9999"
                            stroke="currentColor"
                            stroke-width="1.5"
                            stroke-linecap="round"
                            stroke-linejoin="round"
                        />
                    </svg>
                    Remove
                </label>
            </div>
        </div>
    {:else}
        <label for="img-input" class="add-img-btn unselectable">
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
    <p class="img-uploading-error">{imageUploadingErr}</p>
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
        display: block;
        margin: 0px 0 8px 20px;
        font-size: 24px;
        color: var(--text);
    }

    .text-input-part > :global(textarea) {
        font-family: "Inter", sans-serif;

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
    .image-editing-part {
        max-width: min(560px, 60vw);
        padding-top: 12px;
    }
    .image-editing-part img {
        border-radius: 8px;
        object-fit: contain;
        width: 100%;
        max-height: min(420px, 70vh);
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
    .img-editing-btns {
        display: flex;
        gap: 4px;
        flex-direction: column;
        align-items: center;
    }
    .img-editing-btns label {
        width: 96%;
        height: auto;
        padding: 4px 0;
        border-radius: 8px;
        display: flex;
        flex-direction: row;
        justify-content: center;
        align-items: center;
        gap: 4px;
        color: var(--back-main);
        font-size: 24px;
        transition: all 0.12s ease-in;
        cursor: pointer;
    }
    .img-editing-btns label:hover {
        gap: 12px;
        width: 98%;
        transform: scale(1.02);
    }
    .img-change-btn {
        background-color: var(--primary);
    }
    .img-change-btn:hover {
        background-color: var(--primary-hov);
    }
    .img-change-btn:hover svg {
        transform: rotate(190deg);
    }
    .img-remove-btn {
        background-color: var(--text-faded);
    }
    .img-remove-btn:hover {
        background-color: var(--red-del);
    }
    .img-editing-btns label svg {
        color: inherit;
        height: 28px;
        aspect-ratio: 1/1;
        transition: transform 0.24s ease-in;
    }
    .img-uploading-error {
        color: var(--red-del);
        font-size: 18px;
        font-weight: 500;
        margin-top: 4px;
        width: 100%;
        text-align: center;
    }
</style>
