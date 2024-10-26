<script lang="ts">
    import type { DraftGeneralTestResultDataToView } from "../../../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/general_test_creation/results/GeneralTestCreationResultsTabData";
    import { ImgUtils } from "../../../../../ts/utils/ImgUtils";
    import { StringUtils } from "../../../../../ts/utils/StringUtils";
    import ElementEditDeleteActions from "../../../creation_shared_components/ElementEditDeleteActions.svelte";

    export let result: DraftGeneralTestResultDataToView;
    export let openResultEditingDialog: (resultId: string) => void;
    export let openResultDeletingDialog: (
        resultId: string,
        resultName: string,
    ) => void;

    let isHidden: boolean = true;
</script>

<div class="result-view">
    <div class="always-shown">
        <label class="result-name">
            Result Name: {result.name}
        </label>

        <svg
            xmlns="http://www.w3.org/2000/svg"
            xmlns:xlink="http://www.w3.org/1999/xlink"
            viewBox="0 0 24 24"
            fill="none"
            class="hide-content-btn"
            class:rotated={isHidden}
            on:click={() => (isHidden = !isHidden)}
        >
            <path
                d="M18 15C18 15 13.5811 9.00001 12 9C10.4188 8.99999 6 15 6 15"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
    </div>
    <div class:hiddenContent={isHidden} class:resultInnerContent={!isHidden}>
        <label class="result-text">
            Result Text:
            <span class="res-text-span">
                {StringUtils.isNullOrWhiteSpace(result.text)
                    ? "(No result text added)"
                    : result.text}
            </span>
        </label>
        {#if StringUtils.isNullOrWhiteSpace(result.imagePath)}
            <p class="no-result-image">(No result image added)</p>
        {:else}
            <img
                class="result-image"
                src={ImgUtils.imgUrl(result.imagePath ?? "")}
                alt="result image"
            />
        {/if}
        <div class="result-actions-container unselectable">
            <ElementEditDeleteActions
                editButtonAction={() => openResultEditingDialog(result.id)}
                deleteButtonAction={() =>
                    openResultDeletingDialog(result.id, result.name)}
            />
        </div>
    </div>
</div>

<style>
    .result-view {
        background-color: var(--back-secondary);
        border-radius: 8px;
        margin-top: 8px;
        padding: 0;
    }

    .always-shown {
        width: 100%;
        padding: 0px 4px;
        box-sizing: border-box;
        display: grid;
        grid-template-columns: 1fr auto;
    }
    .result-name {
        margin: 8px 16px;
        font-size: 20px;
    }
    .hide-content-btn {
        margin-right: 6px;
        width: 36px;
        padding: 0;
        border-radius: 30%;
        aspect-ratio: 1/1;
        color: var(--text-faded);
        transition:
            all 0.12s ease,
            transform 0.28s ease;
        cursor: pointer;
    }
    .hide-content-btn path {
        stroke-width: 2;
    }

    .hide-content-btn:hover {
        color: var(--primary);
    }

    .hide-content-btn:active {
        border-radius: 34%;
    }
    .rotated {
        transform: rotate(180deg);
    }
    .resultInnerContent {
        display: grid;
        grid-template-columns: 1fr min(420px, 40vw) auto;
        padding: 12px;
        gap: 10px;
        box-sizing: border-box;
        animation: slideIn 0.4s forwards;
    }
    .hiddenContent {
        display: none;
    }
    .result-text {
        color: var(--text-faded);
        font-weight: 500;
        font-size: 18px;
    }
    .res-text-span {
        color: var(--text);
        font-weight: 400;
        font-size: 20px;
        word-break: break-all;
    }
    .result-image {
        width: 100%;
        height: 100%;
        object-fit: contain;
        border-radius: 8px;
    }
    .no-result-image {
        text-align: center;
        margin-bottom: auto;
        margin-top: 0;
    }

    @keyframes slideIn {
        from {
            opacity: 0.1;
        }

        to {
            opacity: 1;
        }
    }
</style>
