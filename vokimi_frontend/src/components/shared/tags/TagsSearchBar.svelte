<script lang="ts">
    import { onDestroy } from "svelte";
    import { getErrorFromResponse } from "../../../ts/ErrorResponse";
    import { StringUtils } from "../../../ts/utils/StringUtils";

    export let setErrorMessage: (message: string) => void;
    export let setSearchedTags: (tags: string[]) => void;
    export let maxTagNameLength: number;

    let searchTimeout: ReturnType<typeof setTimeout>;
    let tagSearchInput: string = "";
    export function setSearchInputEmpty() {
        tagSearchInput = "";
    }

    async function searchTags(tag: string) {
        setErrorMessage("");
        const response = await fetch(
            `/api/tags/searchTags/${encodeURIComponent(tag)}`,
        );
        if (response.ok) {
            const data = await response.json();
            setSearchedTags(data);
        } else if (response.status === 400) {
            const errorMessage = await getErrorFromResponse(response);
            setErrorMessage(errorMessage);
        } else {
            setErrorMessage("Unknown error. Please try again later");
        }
    }
    $: if (!StringUtils.isNullOrWhiteSpace(tagSearchInput)) {
        if (searchTimeout) {
            clearTimeout(searchTimeout);
        }

        searchTimeout = setTimeout(() => {
            searchTags(tagSearchInput);
        }, 210);
    }

    onDestroy(() => {
        if (searchTimeout) {
            clearTimeout(searchTimeout);
        }
    });
</script>

<div class="search-input-container">
    <svg class="search-icon" viewBox="0 0 24 24" fill="none">
        <path
            d="M17.5 17.5L22 22"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
        <path
            d="M20 11C20 6.02944 15.9706 2 11 2C6.02944 2 2 6.02944 2 11C2 15.9706 6.02944 20 11 20C15.9706 20 20 15.9706 20 11Z"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linejoin="round"
        />
    </svg>
    <input
        class="search-input"
        placeholder="Search for tag..."
        bind:value={tagSearchInput}
        name={"tag-search-" + StringUtils.randomString(12)}
        maxlength={maxTagNameLength}
    />
    <svg
        class="reset-button"
        fill="none"
        viewBox="0 0 24 24"
        stroke="currentColor"
        stroke-width="2"
        on:click={() => (tagSearchInput = "")}
    >
        <path
            stroke-linecap="round"
            stroke-linejoin="round"
            d="M6 18L18 6M6 6l12 12"
        ></path>
    </svg>
</div>

<style>
    .search-input-container {
        position: relative;
        width: 90%;
        height: 42px;
        margin-bottom: 8px;
        display: flex;
        align-items: center;
        padding: 4px 12px;
        box-sizing: border-box;
        border-radius: 40px;
        transition: border-radius 0.45s ease;
        background: var(--back-secondary);
    }

    .search-icon {
        width: 20px;
        aspect-ratio: 1/1;
    }

    .search-input {
        font-size: 16px;
        background-color: transparent;
        width: 100%;
        height: 100%;
        padding-inline: 0.5em;
        padding-block: 0.7em;
        border: none;
    }

    .search-input-container:before {
        content: "";
        position: absolute;
        background: var(--primary);
        transform: scaleX(0);
        transform-origin: center;
        width: 100%;
        height: 2px;
        left: 0;
        bottom: 0;
        border-radius: 1px;
        transition: transform 0.25s ease;
    }

    .search-input-container:focus-within {
        border-radius: 4px;
    }

    .search-input:focus {
        outline: none;
    }

    .search-input-container:focus-within:before {
        transform: scale(1);
    }

    .reset-button {
        width: 20px;
        margin-top: 3px;
        cursor: pointer;
        opacity: 0;
        visibility: hidden;
    }

    .search-input:not(:placeholder-shown) ~ .reset-button {
        opacity: 1;
        visibility: visible;
    }
</style>
