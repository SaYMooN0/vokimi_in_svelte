<script lang="ts">
    import { StringUtils } from "../../../ts/utils/StringUtils";
    import BaseDialog from "../../BaseDialog.svelte";
    import Bookmark from "../../icons/collection_icons/ci_Bookmark.svelte";
    import Brain from "../../icons/collection_icons/ci_Brain.svelte";
    import Clock from "../../icons/collection_icons/ci_Clock.svelte";
    import Folder from "../../icons/collection_icons/ci_Folder.svelte";
    import Heart from "../../icons/collection_icons/ci_Heart.svelte";
    import Star from "../../icons/collection_icons/ci_Star.svelte";
    import Study from "../../icons/collection_icons/ci_Study.svelte";
    import CloseButton from "../CloseButton.svelte";

    let dialogElement: BaseDialog;
    let creationErr: string = "";
    let dialogId = "collection-creation-dialog" + StringUtils.randomString(4);
    export function open() {
        dialogElement.open();
    }
    let collectionName: string;
    let collectionColor: string = "#796cfa";
    let collectionIcon: string = "default";
    function createNewCollection() {
        if (StringUtils.isNullOrWhiteSpace(collectionName)) {
            creationErr = "Collection name cannot be empty";
            return;
        }
        if (StringUtils.isNullOrWhiteSpace(collectionColor)) {
            creationErr = "Please select a collection color";
            return;
        }
        if (StringUtils.isNullOrWhiteSpace(collectionIcon)) {
            creationErr = "Please select a collection icon";
            return;
        }
        const data = {
            name: collectionName,
            color: collectionColor,
            icon: collectionIcon,
        };
        console.log(data);
    }
    let possibleIcons = Object.entries({
        default: Folder,
        bookmark: Bookmark,
        heart: Heart,
        clock: Clock,
        star: Star,
        brain: Brain,
        study: Study,
    });
</script>

<BaseDialog {dialogId} bind:this={dialogElement}>
    <CloseButton onClose={() => dialogElement.close()} />
    <div class="dialog-content" style="--collection-color: {collectionColor}">
        <p class="dialog-header">Create new collection</p>
        <input
            type="text"
            class="collection-name-input"
            placeholder="New collection name"
            bind:value={collectionName}
        />
        <div class="inputs-div">
            <label for="collection-color-input">Collection color</label>

            <label for="collection-icon-input">Collection icon</label>
            <input
                class="collection-color-input"
                type="color"
                id="collection-color-input"
                bind:value={collectionColor}
            />
            <div class="icons-input-container">
                {#each possibleIcons as iconData}
                    <label for="icon-{iconData[0]}">
                        <input
                            checked={iconData[0] === collectionIcon}
                            id="icon-{iconData[0]}"
                            class="radio-input"
                            type="radio"
                            name="engine"
                        />
                        <span class="icon-input-tile">
                            <span class="tile-icon">
                                <svelte:component this={iconData[1]} />
                            </span>
                        </span>
                    </label>
                {/each}
            </div>
        </div>
        {#if !StringUtils.isNullOrWhiteSpace(creationErr)}
            <p class="error-string">{creationErr}</p>
        {/if}
        <button
            on:click={createNewCollection}
            class="save-collection-btn uncselectable"
        >
            Save new collection
        </button>
    </div>
</BaseDialog>

<style>
    .dialog-content {
        padding: 12px 40px;
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .dialog-header {
        font-size: 24px;
        color: var(--text-faded);
        margin: 4px 20px;
        text-align: center;
    }

    .collection-name-input {
        margin: 20px 0;
        width: 100%;
        background-color: var(--back-secondary);
        border-radius: 32px;
        border: 2px solid var(--back-secondary);
        outline: none;
        padding: 6px 16px;
        font-size: 20px;
    }

    .collection-name-input::placeholder {
        color: var(--text-faded);
    }

    .collection-name-input:focus {
        border-color: var(--primary);
    }

    .inputs-div {
        display: grid;
        grid-template-columns: 1fr 1fr;
        grid-template-rows: auto 1fr;
        justify-content: center;
        row-gap: 12px;
    }

    .inputs-div label {
        text-align: center;
        color: var(--text);
        font-size: 18px;
        font-weight: 500;
    }

    .error-string {
        color: var(--red-del);
        font-size: 18px;
        margin-top: 20px;
        margin-bottom: 0;
    }
    .collection-color-input {
        justify-self: center;
        display: block;
        appearance: none;
        -moz-appearance: none;
        -webkit-appearance: none;
        background: none;
        border: none;
        padding: none;
        height: 120px;
        width: 140px;
        cursor: pointer;
        transition: transform 0.12s ease;
    }
    .collection-color-input:hover {
        transform: scale(1.04);
    }
    .save-collection-btn {
        margin-top: 40px;
        margin-bottom: 12px;
        border: none;
        font-size: 18px;
        color: var(--back-main);
        background-color: var(--primary);
        border-radius: 6px;
        padding: 6px 20px;
        cursor: pointer;
    }

    .save-collection-btn:hover {
        background-color: var(--primary-hov);
    }
    .save-collection-btn:active {
        transform: scale(0.98);
    }
    .icons-input-container {
        display: flex;
        flex-wrap: wrap;
        justify-content: center;
        align-items: center;
        max-width: 320px;
        gap: 8px;
    }

    .radio-input {
        position: absolute;
        display: none;
    }

    .icon-input-tile {
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        width: 60px;
        height: 60px;
        border-radius: 8px;
        border: 2px solid var(--text-faded);
        background-color: var(--back-main);
        transition: transform 0.12s ease;
        cursor: pointer;
        position: relative;
    }

    .icon-input-tile:before {
        content: "";
        position: absolute;
        display: block;
        width: 10px;
        height: 10px;
        border: 2px solid var(--text-faded);
        box-sizing: border-box;
        background-color: var(--back-main);
        border-radius: 50%;
        opacity: 0;
        transform: scale(0);
        transition:
            transform 0.25s ease,
            opacity 0.25s ease;
        bottom: 4px;
        left: 50%;
        transform: translateX(-50%);
    }

    .icon-input-tile:hover {
        box-shadow: rgb(67, 58, 178, 0.14) 0px 0px 0px 3px;
    }

    .icon-input-tile:hover:before {
        transform: scale(1), translateX(-50%);
        opacity: 1;
    }

    .tile-icon :global(svg) {
        width: 28px;
        aspect-ratio: 1/1;
        color: var(--text-faded);
    }

    .radio-input:checked + .icon-input-tile {
        border-color: var(--collection-color);
        box-shadow: 0 5px 10px rgba(0, 0, 0, 0.1);
        color: var(--collection-color);
    }

    .radio-input:checked + .icon-input-tile:before {
        transform: scale(1), translateX(-50%);
        opacity: 1;
        background-color: var(--collection-color);
        border-color: var(--collection-color);
    }

    .radio-input:checked + .icon-input-tile .tile-icon :global(svg) {
        color: var(--collection-color);
    }

    .radio-input:focus + .icon-input-tile:before {
        transform: scale(1), translateX(-50%);
        opacity: 1;
    }
</style>
