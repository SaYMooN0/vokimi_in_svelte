<script lang="ts">
    import AuthorizeView from "../../../components/AuthorizeView.svelte";
    import { ImgUtils } from "../../../ts/utils/ImgUtils";
    import AddingToCollectionDropdown from "./test_cover_section_components/AddingToCollectionDropdown.svelte";

    export let testId: string;
    export let coverPath: string;

    let collectionsDropdown: AddingToCollectionDropdown;
    async function showCollectionsDropDown() {
        await collectionsDropdown.open();
    }
</script>

<div class="view-left-part unselectable">
    <div class="test-cover-section">
        <img class="test-cover-img" src={ImgUtils.imgUrl(coverPath)} />

        <a class="take-test-btn under-img-btn" href="/test-taking/{testId}">
            Take Test
        </a>
        <AuthorizeView>
            <svelte:fragment slot="authenticated">
                <div
                    class="collection-btn under-img-btn"
                    on:click={showCollectionsDropDown}
                >
                    <svg viewBox="0 0 24 24">
                        <path
                            d="M16.9017 6.12874C18 7.25748 18 9.07416 18 12.7075V14.2925C18 17.9258 18 19.7425 16.9017 20.8713C15.8033 22 14.0355 22 10.5 22C6.96447 22 5.1967 22 4.09835 20.8713C3 19.7425 3 17.9258 3 14.2925V12.7075C3 9.07416 3 7.25748 4.09835 6.12874C5.1967 5 6.96447 5 10.5 5C14.0355 5 15.8033 5 16.9017 6.12874Z"
                            stroke="currentColor"
                            stroke-width="1.5"
                            stroke-linecap="round"
                            stroke-linejoin="round"
                        />
                        <path
                            d="M7.5 5.5V10.3693C7.5 11.3046 7.5 11.7722 7.78982 11.9396C8.35105 12.2638 9.4038 11.1823 9.90375 10.8567C10.1937 10.6678 10.3387 10.5734 10.5 10.5734C10.6613 10.5734 10.8063 10.6678 11.0962 10.8567C11.5962 11.1823 12.6489 12.2638 13.2102 11.9396C13.5 11.7722 13.5 11.3046 13.5 10.3693V5.5"
                            stroke="currentColor"
                            stroke-width="1.5"
                            stroke-linecap="round"
                            stroke-linejoin="round"
                        />
                        <path
                            d="M9 2H11C15.714 2 18.0711 2 19.5355 3.46447C21 4.92893 21 7.28595 21 12V18"
                            stroke="currentColor"
                            stroke-width="1.5"
                            stroke-linecap="round"
                            stroke-linejoin="round"
                        />
                    </svg>
                    <label> Add To Collection </label>
                    <svg viewBox="0 0 24 24">
                        <path
                            d="M18 9.00005C18 9.00005 13.5811 15 12 15C10.4188 15 6 9 6 9"
                            stroke="currentColor"
                            stroke-width="1.5"
                            stroke-linecap="round"
                            stroke-linejoin="round"
                        />
                    </svg>
                </div>
                <AddingToCollectionDropdown
                    bind:this={collectionsDropdown}
                    {testId}
                />
            </svelte:fragment>
        </AuthorizeView>
    </div>
</div>

<style>
    .test-cover-section {
        display: flex;
        flex-direction: column;
        align-items: center;
        gap: 8px;
    }

    .test-cover-img {
        max-width: calc(120px + 21vw);
        min-width: 220px;
        max-height: calc(120px + 40vh);
        object-fit: contain;
        box-shadow:
            rgba(0, 0, 0, 0.05) 0px 6px 24px 0px,
            rgb(67, 58, 178, 0.1) 0px 0px 0px 1px;
        border-radius: 12px;
        margin-bottom: 6px;
    }
    .under-img-btn {
        width: 80%;
        min-width: 300px;
        height: 36px;
        background-color: var(--primary);
        border-radius: 6px;
        color: var(--back-main);
        font-size: 20px;
        cursor: pointer;
    }

    .under-img-btn:hover {
        background-color: var(--primary-hov);
    }

    .take-test-btn {
        display: flex;
        justify-content: center;
        align-items: center;
        text-decoration: none;
    }

    .collection-btn {
        display: flex;
        justify-content: space-between;
        align-items: center;
        padding: 0 12px;
        box-sizing: border-box;
    }

    .collection-btn label {
        cursor: inherit;
    }

    .collection-btn svg {
        height: 100%;
        aspect-ratio: 1/1;
        box-sizing: border-box;
        color: inherit;
        fill: none;
        padding: 4px;
    }
</style>
